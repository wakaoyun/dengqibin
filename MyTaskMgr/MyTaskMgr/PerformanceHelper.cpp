#include "StdAfx.h"
#include "PerformanceHelper.h"


CPerformanceHelper::CPerformanceHelper(void)
{
	SID_IDENTIFIER_AUTHORITY NtSidAuthority = {SECURITY_NT_AUTHORITY};
    NTSTATUS    status;

    InitializeCriticalSection(&PerfDataCriticalSection);

    /*
     * Get number of processors in the system
     */
	PNtQuerySystemInformation NtQuerySystemInformation = NULL;
	if(!GetNtQuerySystemInformation(&NtQuerySystemInformation))
	{
#ifdef _DEBUG
		ShowError(_T("GetNtQuerySystemInformation"));
#endif
		return;
	}
    status = NtQuerySystemInformation(SystemBasicInformation, &SystemBasicInfo, sizeof(SystemBasicInfo), NULL);
    if (status != NO_ERROR)
	{
#ifdef _DEBUG
		ShowError(_T("GetSystemBasicInformation"));
#endif
		return;
	}

    /*
     * Create the SYSTEM Sid
     */
    AllocateAndInitializeSid(&NtSidAuthority, 1, SECURITY_LOCAL_SYSTEM_RID, 0, 0, 0, 0, 0, 0, 0, &SystemUserSid);
	PerfDataRefresh();
    return;
}

CPerformanceHelper::~CPerformanceHelper(void)
{
	if (pPerfData != NULL)
		HeapFree(GetProcessHeap(), 0, pPerfData);

	DeleteCriticalSection(&PerfDataCriticalSection);

	if (SystemUserSid != NULL)
	{
		FreeSid(SystemUserSid);
		SystemUserSid = NULL;
	}
}


void CPerformanceHelper::SidToUserName(PSID Sid, LPWSTR szBuffer, DWORD BufferSize)
{
	static WCHAR szDomainNameUnused[255];
	DWORD DomainNameLen = sizeof(szDomainNameUnused) / sizeof(szDomainNameUnused[0]);
	SID_NAME_USE Use;

	if (Sid != NULL)
		LookupAccountSidW(NULL, Sid, szBuffer, &BufferSize, szDomainNameUnused, &DomainNameLen, &Use);
}


void CPerformanceHelper::PerfDataRefresh()
{
	ULONG                                      ulSize;
    NTSTATUS                                   status;
    LPBYTE                                     pBuffer;
    ULONG                                      BufferSize;
    PSYSTEM_PROCESS_INFORMATION                pSPI;
    PPERFDATA                                  pPDOld;
    ULONG                                      Idx, Idx2;
    HANDLE                                     hProcess;
    HANDLE                                     hProcessToken;
    SYSTEM_PERFORMANCE_INFORMATION             SysPerfInfo;
    SYSTEM_TIMEOFDAY_INFORMATION               SysTimeInfo;
    SYSTEM_FILECACHE_INFORMATION               SysCacheInfo;
    LPBYTE                                     SysHandleInfoData;
    PSYSTEM_PROCESSOR_PERFORMANCE_INFORMATION  SysProcessorTimeInfo;
    double                                     CurrentKernelTime;
    PSECURITY_DESCRIPTOR                       ProcessSD;
    PSID                                       ProcessUser;
    ULONG                                      Buffer[64]; /* must be 4 bytes aligned! */

	PNtQuerySystemInformation NtQuerySystemInformation;
	if(!GetNtQuerySystemInformation(&NtQuerySystemInformation))
	{
#ifdef _DEBUG
		ShowError(_T("GetNtQuerySystemInformation"));
#endif
		return;
	}
    /* Get new system time */
    status = NtQuerySystemInformation(SystemTimeOfDayInformation, &SysTimeInfo, sizeof(SysTimeInfo), 0);
    if (status != NO_ERROR)
	{
#ifdef _DEBUG
		ShowError(_T("GetSystemTimeOfDayInformation"));
#endif
		return;
	}

    /* Get new CPU's idle time */
    /*status = NtQuerySystemInformation(SystemPerformanceInformation, &SysPerfInfo, sizeof(SysPerfInfo), NULL);
    if (status != NO_ERROR)
	{
#ifdef _DEBUG		
		ShowError(_T("GetSystemPerformanceInformation"));
#endif
		return;
	}*/

    /* Get system cache information */
    status = NtQuerySystemInformation(SystemFileCacheInformation, &SysCacheInfo, sizeof(SysCacheInfo), NULL);
    if (status != NO_ERROR)
	{
#ifdef _DEBUG
		ShowError(_T("GetSystemFileCacheInformation"));
#endif
		return;
	}

    /* Get processor time information */
    SysProcessorTimeInfo = (PSYSTEM_PROCESSOR_PERFORMANCE_INFORMATION)HeapAlloc(GetProcessHeap(), 0, sizeof(SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION) * SystemBasicInfo.NumberOfProcessors);
    status = NtQuerySystemInformation(SystemProcessorPerformanceInformation, SysProcessorTimeInfo, sizeof(SYSTEM_PROCESSOR_PERFORMANCE_INFORMATION) * SystemBasicInfo.NumberOfProcessors, &ulSize);

    if (status != NO_ERROR)
    {
        if (SysProcessorTimeInfo != NULL)
            HeapFree(GetProcessHeap(), 0, SysProcessorTimeInfo);
#ifdef _DEBUG
		ShowError(_T("GetSystemProcessorPerformanceInformation"));
#endif
        return;
    }

    /* Get handle information
     * We don't know how much data there is so just keep
     * increasing the buffer size until the call succeeds
     */
	BufferSize = 0;
	do
	{
		BufferSize += 0x10000;
		SysHandleInfoData = (LPBYTE)HeapAlloc(GetProcessHeap(), 0, BufferSize);

		status = NtQuerySystemInformation(SystemHandleInformation, SysHandleInfoData, BufferSize, &ulSize);

		if (status == STATUS_INFO_LENGTH_MISMATCH) {
			HeapFree(GetProcessHeap(), 0, SysHandleInfoData);
		}

	} while (status == STATUS_INFO_LENGTH_MISMATCH);

    /* Get process information
     * We don't know how much data there is so just keep
     * increasing the buffer size until the call succeeds
     */
    BufferSize = 0;
    do
    {
        BufferSize += 0x10000;
        pBuffer = (LPBYTE)HeapAlloc(GetProcessHeap(), 0, BufferSize);

        status = NtQuerySystemInformation(SystemProcessInformation, pBuffer, BufferSize, &ulSize);

        if (status == STATUS_INFO_LENGTH_MISMATCH) {
            HeapFree(GetProcessHeap(), 0, pBuffer);
        }

    } while (status == STATUS_INFO_LENGTH_MISMATCH);

    EnterCriticalSection(&PerfDataCriticalSection);

    /*
     * Save system performance info
     */
    memcpy(&SystemPerfInfo, &SysPerfInfo, sizeof(SYSTEM_PERFORMANCE_INFORMATION));

    /*
     * Save system cache info
     */
    memcpy(&SystemCacheInfo, &SysCacheInfo, sizeof(SYSTEM_FILECACHE_INFORMATION));

    /*
     * Save system processor time info
     */
    if (SystemProcessorTimeInfo) {
        HeapFree(GetProcessHeap(), 0, SystemProcessorTimeInfo);
    }
    SystemProcessorTimeInfo = SysProcessorTimeInfo;

    /*
     * Save system handle info
     */
    memcpy(&SystemHandleInfo, SysHandleInfoData, sizeof(SYSTEM_HANDLE_INFORMATION));
    HeapFree(GetProcessHeap(), 0, SysHandleInfoData);

    for (CurrentKernelTime=0, Idx=0; Idx<(ULONG)SystemBasicInfo.NumberOfProcessors; Idx++) {
        CurrentKernelTime += Li2Double(SystemProcessorTimeInfo[Idx].KernelTime);
        CurrentKernelTime += Li2Double(SystemProcessorTimeInfo[Idx].DpcTime);
        CurrentKernelTime += Li2Double(SystemProcessorTimeInfo[Idx].InterruptTime);
    }

    /* If it's a first call - skip idle time calcs */
    if (liOldIdleTime.QuadPart != 0) {
        /*  CurrentValue = NewValue - OldValue */
        dbIdleTime = Li2Double(SysPerfInfo.IdleProcessTime) - Li2Double(liOldIdleTime);
        dbKernelTime = CurrentKernelTime - OldKernelTime;
        dbSystemTime = Li2Double(SysTimeInfo.CurrentTime) - Li2Double(liOldSystemTime);

        /*  CurrentCpuIdle = IdleTime / SystemTime */
        dbIdleTime = dbIdleTime / dbSystemTime;
        dbKernelTime = dbKernelTime / dbSystemTime;

        /*  CurrentCpuUsage% = 100 - (CurrentCpuIdle * 100) / NumberOfProcessors */
        dbIdleTime = 100.0 - dbIdleTime * 100.0 / (double)SystemBasicInfo.NumberOfProcessors; /* + 0.5; */
        dbKernelTime = 100.0 - dbKernelTime * 100.0 / (double)SystemBasicInfo.NumberOfProcessors; /* + 0.5; */
    }

    /* Store new CPU's idle and system time */
    liOldIdleTime = SysPerfInfo.IdleProcessTime;
    liOldSystemTime = SysTimeInfo.CurrentTime;
    OldKernelTime = CurrentKernelTime;

    /* Determine the process count
     * We loop through the data we got from NtQuerySystemInformation
     * and count how many structures there are (until RelativeOffset is 0)
     */
    ProcessCountOld = ProcessCount;
    ProcessCount = 0;
    pSPI = (PSYSTEM_PROCESS_INFORMATION)pBuffer;
    while (pSPI) {
        ProcessCount++;
        if (pSPI->NextEntryOffset == 0)
            break;
        pSPI = (PSYSTEM_PROCESS_INFORMATION)((LPBYTE)pSPI + pSPI->NextEntryOffset);
    }

    /* Now alloc a new PERFDATA array and fill in the data */
    if (pPerfDataOld) {
        HeapFree(GetProcessHeap(), 0, pPerfDataOld);
    }
    pPerfDataOld = pPerfData;
    /* Clear out process perf data structures with HEAP_ZERO_MEMORY flag: */
    pPerfData = (PPERFDATA)HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, sizeof(PERFDATA) * ProcessCount);
    pSPI = (PSYSTEM_PROCESS_INFORMATION)pBuffer;
    for (Idx=0; Idx<ProcessCount; Idx++) {
        /* Get the old perf data for this process (if any) */
        /* so that we can establish delta values */
        pPDOld = NULL;
        for (Idx2=0; Idx2<ProcessCountOld; Idx2++) {
            if (pPerfDataOld[Idx2].ProcessId == pSPI->UniqueProcessId) {
                pPDOld = &pPerfDataOld[Idx2];
                break;
            }
        }

        if (pSPI->ImageName.Buffer) {
            /* Don't assume a UNICODE_STRING Buffer is zero terminated: */
            int len = pSPI->ImageName.Length / 2;
            /* Check against max size and allow for terminating zero (already zeroed): */
            if(len >= MAX_PATH)len=MAX_PATH - 1;
            wcsncpy(pPerfData[Idx].ImageName, pSPI->ImageName.Buffer, len);
        } else {
            /*LoadStringW(hInst, IDS_IDLE_PROCESS, pPerfData[Idx].ImageName,
                       sizeof(pPerfData[Idx].ImageName) / sizeof(pPerfData[Idx].ImageName[0]));*/
        }

        pPerfData[Idx].ProcessId = pSPI->UniqueProcessId;

        if (pPDOld)    {
            double    CurTime = Li2Double(pSPI->KernelTime) + Li2Double(pSPI->UserTime);
            double    OldTime = Li2Double(pPDOld->KernelTime) + Li2Double(pPDOld->UserTime);
            double    CpuTime = (CurTime - OldTime) / dbSystemTime;
            CpuTime = CpuTime * 100.0 / (double)SystemBasicInfo.NumberOfProcessors; /* + 0.5; */
            pPerfData[Idx].CPUUsage = (ULONG)CpuTime;
        }
        pPerfData[Idx].CPUTime.QuadPart = pSPI->UserTime.QuadPart + pSPI->KernelTime.QuadPart;
        pPerfData[Idx].WorkingSetSizeBytes = pSPI->WorkingSetSize;
        pPerfData[Idx].PeakWorkingSetSizeBytes = pSPI->PeakWorkingSetSize;
        if (pPDOld)
            pPerfData[Idx].WorkingSetSizeDelta = labs((LONG)pSPI->WorkingSetSize - (LONG)pPDOld->WorkingSetSizeBytes);
        else
            pPerfData[Idx].WorkingSetSizeDelta = 0;
        pPerfData[Idx].PageFaultCount = pSPI->PageFaultCount;
        if (pPDOld)
            pPerfData[Idx].PageFaultCountDelta = labs((LONG)pSPI->PageFaultCount - (LONG)pPDOld->PageFaultCount);
        else
            pPerfData[Idx].PageFaultCountDelta = 0;
        pPerfData[Idx].VirtualMemorySizeBytes = pSPI->VirtualSize;
		pPerfData[Idx].PagedPoolUsagePages = pSPI->QuotaPeakPagedPoolUsage;
		pPerfData[Idx].NonPagedPoolUsagePages = pSPI->QuotaPeakNonPagedPoolUsage;
        pPerfData[Idx].BasePriority = pSPI->BasePriority;
        pPerfData[Idx].HandleCount = pSPI->HandleCount;
        pPerfData[Idx].ThreadCount = pSPI->NumberOfThreads;
        pPerfData[Idx].SessionId = pSPI->SessionId;
        pPerfData[Idx].UserName[0] = L'\0';
        pPerfData[Idx].USERObjectCount = 0;
        pPerfData[Idx].GDIObjectCount = 0;
        ProcessUser = SystemUserSid;
        ProcessSD = NULL;

        if (pSPI->UniqueProcessId != NULL) {
            hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | READ_CONTROL, FALSE, PtrToUlong(pSPI->UniqueProcessId));
            if (hProcess) {
                /* don't query the information of the system process. It's possible but
                   returns Administrators as the owner of the process instead of SYSTEM */
                if (pSPI->UniqueProcessId != (HANDLE)0x4)
                {
                    if (OpenProcessToken(hProcess, TOKEN_QUERY, &hProcessToken))
                    {
                        DWORD RetLen = 0;
                        BOOL Ret;

                        Ret = GetTokenInformation(hProcessToken, TokenUser, (LPVOID)Buffer, sizeof(Buffer), &RetLen);
                        CloseHandle(hProcessToken);

                        if (Ret)
                            ProcessUser = ((PTOKEN_USER)Buffer)->User.Sid;
                        else
                            goto ReadProcOwner;
                    }
                    else
                    {
ReadProcOwner:
                        GetSecurityInfo(hProcess, SE_KERNEL_OBJECT, OWNER_SECURITY_INFORMATION, &ProcessUser, NULL, NULL, NULL, &ProcessSD);
                    }

                    pPerfData[Idx].USERObjectCount = GetGuiResources(hProcess, GR_USEROBJECTS);
                    pPerfData[Idx].GDIObjectCount = GetGuiResources(hProcess, GR_GDIOBJECTS);
                }

                GetProcessIoCounters(hProcess, &pPerfData[Idx].IOCounters);
                CloseHandle(hProcess);
            } else {
                goto ClearInfo;
            }
        } else {
ClearInfo:
            /* clear information we were unable to fetch */
            ZeroMemory(&pPerfData[Idx].IOCounters, sizeof(IO_COUNTERS));
        }

        SidToUserName(ProcessUser, pPerfData[Idx].UserName, sizeof(pPerfData[0].UserName) / sizeof(pPerfData[0].UserName[0]));

        if (ProcessSD != NULL)
        {
            LocalFree((HLOCAL)ProcessSD);
        }

        pPerfData[Idx].UserTime.QuadPart = pSPI->UserTime.QuadPart;
        pPerfData[Idx].KernelTime.QuadPart = pSPI->KernelTime.QuadPart;
        pSPI = (PSYSTEM_PROCESS_INFORMATION)((LPBYTE)pSPI + pSPI->NextEntryOffset);
    }
    HeapFree(GetProcessHeap(), 0, pBuffer);
    LeaveCriticalSection(&PerfDataCriticalSection);
}


ULONG CPerformanceHelper::PerfDataGetProcessCount()
{
	return ProcessCount;
}

ULONG CPerformanceHelper::PerfDataGetProcessorUsage()
{
	return (ULONG)dbIdleTime;
}

ULONG CPerformanceHelper::PerfDataGetProcessorSystemUsage()
{
	return (ULONG)dbKernelTime;
}

BOOL CPerformanceHelper::PerfDataGetImageName(ULONG Index, LPWSTR lpImageName, int nMaxCount)
{
	BOOL  bSuccessful;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount) {
		wcsncpy(lpImageName, pPerfData[Index].ImageName, nMaxCount);
		bSuccessful = TRUE;
	} else {
		bSuccessful = FALSE;
	}
	LeaveCriticalSection(&PerfDataCriticalSection);
	return bSuccessful;
}

int CPerformanceHelper::PerfGetIndexByProcessId(DWORD dwProcessId)
{
	int FoundIndex = -1;
	ULONG Index;

	EnterCriticalSection(&PerfDataCriticalSection);

	for (Index = 0; Index < ProcessCount; Index++)
	{
		if (PtrToUlong(pPerfData[Index].ProcessId) == dwProcessId)
		{
			FoundIndex = Index;
			break;
		}
	}

	LeaveCriticalSection(&PerfDataCriticalSection);

	return FoundIndex;
}

ULONG CPerformanceHelper::PerfDataGetProcessId(ULONG Index)
{
	ULONG  ProcessId;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		ProcessId = PtrToUlong(pPerfData[Index].ProcessId);
	else
		ProcessId = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return ProcessId;
}

BOOL CPerformanceHelper::PerfDataGetUserName(ULONG Index, LPWSTR lpUserName, int nMaxCount)
{
	BOOL  bSuccessful;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount) {
		wcsncpy(lpUserName, pPerfData[Index].UserName, nMaxCount);
		bSuccessful = TRUE;
	} else {
		bSuccessful = FALSE;
	}

	LeaveCriticalSection(&PerfDataCriticalSection);

	return bSuccessful;
}

ULONG CPerformanceHelper::PerfDataGetSessionId(ULONG Index)
{
	ULONG  SessionId;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		SessionId = pPerfData[Index].SessionId;
	else
		SessionId = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return SessionId;
}

ULONG CPerformanceHelper::PerfDataGetCPUUsage(ULONG Index)
{
	ULONG  CpuUsage;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		CpuUsage = pPerfData[Index].CPUUsage;
	else
		CpuUsage = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return CpuUsage;
}

LARGE_INTEGER CPerformanceHelper::PerfDataGetCPUTime(ULONG Index)
{
	LARGE_INTEGER  CpuTime = {{0,0}};

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		CpuTime = pPerfData[Index].CPUTime;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return CpuTime;
}

ULONG CPerformanceHelper::PerfDataGetWorkingSetSizeBytes(ULONG Index)
{
	ULONG  WorkingSetSizeBytes;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		WorkingSetSizeBytes = pPerfData[Index].WorkingSetSizeBytes;
	else
		WorkingSetSizeBytes = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return WorkingSetSizeBytes;
}

ULONG CPerformanceHelper::PerfDataGetPeakWorkingSetSizeBytes(ULONG Index)
{
	ULONG  PeakWorkingSetSizeBytes;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		PeakWorkingSetSizeBytes = pPerfData[Index].PeakWorkingSetSizeBytes;
	else
		PeakWorkingSetSizeBytes = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return PeakWorkingSetSizeBytes;
}

ULONG CPerformanceHelper::PerfDataGetWorkingSetSizeDelta(ULONG Index)
{
	ULONG  WorkingSetSizeDelta;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		WorkingSetSizeDelta = pPerfData[Index].WorkingSetSizeDelta;
	else
		WorkingSetSizeDelta = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return WorkingSetSizeDelta;
}

ULONG CPerformanceHelper::PerfDataGetPageFaultCount(ULONG Index)
{
	ULONG  PageFaultCount;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		PageFaultCount = pPerfData[Index].PageFaultCount;
	else
		PageFaultCount = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return PageFaultCount;
}

ULONG CPerformanceHelper::PerfDataGetPageFaultCountDelta(ULONG Index)
{
	ULONG  PageFaultCountDelta;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		PageFaultCountDelta = pPerfData[Index].PageFaultCountDelta;
	else
		PageFaultCountDelta = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return PageFaultCountDelta;
}

ULONG CPerformanceHelper::PerfDataGetVirtualMemorySizeBytes(ULONG Index)
{
	ULONG  VirtualMemorySizeBytes;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		VirtualMemorySizeBytes = pPerfData[Index].VirtualMemorySizeBytes;
	else
		VirtualMemorySizeBytes = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return VirtualMemorySizeBytes;
}

ULONG CPerformanceHelper::PerfDataGetPagedPoolUsagePages(ULONG Index)
{
	ULONG  PagedPoolUsage;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		PagedPoolUsage = pPerfData[Index].PagedPoolUsagePages;
	else
		PagedPoolUsage = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return PagedPoolUsage;
}

ULONG CPerformanceHelper::PerfDataGetNonPagedPoolUsagePages(ULONG Index)
{
	ULONG  NonPagedPoolUsage;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		NonPagedPoolUsage = pPerfData[Index].NonPagedPoolUsagePages;
	else
		NonPagedPoolUsage = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return NonPagedPoolUsage;
}

ULONG CPerformanceHelper::PerfDataGetBasePriority(ULONG Index)
{
	ULONG  BasePriority;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		BasePriority = pPerfData[Index].BasePriority;
	else
		BasePriority = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return BasePriority;
}

ULONG CPerformanceHelper::PerfDataGetHandleCount(ULONG Index)
{
	ULONG  HandleCount;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		HandleCount = pPerfData[Index].HandleCount;
	else
		HandleCount = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return HandleCount;
}

ULONG CPerformanceHelper::PerfDataGetThreadCount(ULONG Index)
{
	ULONG  ThreadCount;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		ThreadCount = pPerfData[Index].ThreadCount;
	else
		ThreadCount = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return ThreadCount;
}

ULONG CPerformanceHelper::PerfDataGetUSERObjectCount(ULONG Index)
{
	ULONG  USERObjectCount;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		USERObjectCount = pPerfData[Index].USERObjectCount;
	else
		USERObjectCount = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return USERObjectCount;
}

ULONG CPerformanceHelper::PerfDataGetGDIObjectCount(ULONG Index)
{
	ULONG  GDIObjectCount;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
		GDIObjectCount = pPerfData[Index].GDIObjectCount;
	else
		GDIObjectCount = 0;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return GDIObjectCount;
}

BOOL CPerformanceHelper::PerfDataGetIOCounters(ULONG Index, PIO_COUNTERS pIoCounters)
{
	BOOL  bSuccessful;

	EnterCriticalSection(&PerfDataCriticalSection);

	if (Index < ProcessCount)
	{
		memcpy(pIoCounters, &pPerfData[Index].IOCounters, sizeof(IO_COUNTERS));
		bSuccessful = TRUE;
	}
	else
		bSuccessful = FALSE;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return bSuccessful;
}

ULONG CPerformanceHelper::PerfDataGetCommitChargeTotalK(void)
{
	ULONG  Total;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	Total = SystemPerfInfo.CommittedPages;
	PageSize = SystemBasicInfo.PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	Total = Total * (PageSize / 1024);

	return Total;
}

ULONG CPerformanceHelper::PerfDataGetCommitChargeLimitK(void)
{
	ULONG  Limit;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	Limit = SystemPerfInfo.CommitLimit;
	PageSize = SystemBasicInfo.PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	Limit = Limit * (PageSize / 1024);

	return Limit;
}

ULONG CPerformanceHelper::PerfDataGetCommitChargePeakK(void)
{
	ULONG  Peak;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	Peak = SystemPerfInfo.PeakCommitment;
	PageSize = SystemBasicInfo.PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	Peak = Peak * (PageSize / 1024);

	return Peak;
}

ULONG CPerformanceHelper::PerfDataGetKernelMemoryTotalK(void)
{
	ULONG  Total;
	ULONG  Paged;
	ULONG  NonPaged;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	Paged = SystemPerfInfo.PagedPoolPages;
	NonPaged = SystemPerfInfo.NonPagedPoolPages;
	PageSize = SystemBasicInfo.PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	Paged = Paged * (PageSize / 1024);
	NonPaged = NonPaged * (PageSize / 1024);

	Total = Paged + NonPaged;

	return Total;
}

ULONG CPerformanceHelper::PerfDataGetKernelMemoryPagedK(void)
{
	ULONG  Paged;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	Paged = SystemPerfInfo.PagedPoolPages;
	PageSize = SystemBasicInfo.PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	Paged = Paged * (PageSize / 1024);

	return Paged;
}

ULONG CPerformanceHelper::PerfDataGetKernelMemoryNonPagedK(void)
{
	ULONG  NonPaged;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	NonPaged = SystemPerfInfo.NonPagedPoolPages;
	PageSize = SystemBasicInfo.PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	NonPaged = NonPaged * (PageSize / 1024);

	return NonPaged;
}

ULONG CPerformanceHelper::PerfDataGetPhysicalMemoryTotalK(void)
{
	ULONG  Total;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	Total = SystemBasicInfo.NumberOfPhysicalPages;
	PageSize = SystemBasicInfo.PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	Total = Total * (PageSize / 1024);

	return Total;
}

ULONG CPerformanceHelper::PerfDataGetPhysicalMemoryAvailableK(void)
{
	ULONG  Available;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	Available = SystemPerfInfo.AvailablePages;
	PageSize = SystemBasicInfo.PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	Available = Available * (PageSize / 1024);

	return Available;
}

ULONG CPerformanceHelper::PerfDataGetPhysicalMemorySystemCacheK(void)
{
	ULONG  SystemCache;
	ULONG  PageSize;

	EnterCriticalSection(&PerfDataCriticalSection);

	PageSize = SystemBasicInfo.PageSize;
	SystemCache = SystemCacheInfo.CurrentSizeIncludingTransitionInPages * PageSize;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return SystemCache / 1024;
}

ULONG CPerformanceHelper::PerfDataGetSystemHandleCount(void)
{
	ULONG  HandleCount;

	EnterCriticalSection(&PerfDataCriticalSection);

	HandleCount = SystemHandleInfo.NumberOfHandles;

	LeaveCriticalSection(&PerfDataCriticalSection);

	return HandleCount;
}

ULONG CPerformanceHelper::PerfDataGetTotalThreadCount(void)
{
	ULONG  ThreadCount = 0;
	ULONG  i;

	EnterCriticalSection(&PerfDataCriticalSection);

	for (i=0; i<ProcessCount; i++)
	{
		ThreadCount += pPerfData[i].ThreadCount;
	}

	LeaveCriticalSection(&PerfDataCriticalSection);

	return ThreadCount;
}

BOOL CPerformanceHelper::PerfDataGet(ULONG Index, PPERFDATA *lppData)
{
	BOOL  bSuccessful = FALSE;

	EnterCriticalSection(&PerfDataCriticalSection);
	if (Index < ProcessCount)
	{
		*lppData = pPerfData + Index;
		bSuccessful = TRUE;
	}
	LeaveCriticalSection(&PerfDataCriticalSection);
	return bSuccessful;
}
PERFDATA* CPerformanceHelper::PerfGetData()
{
	return pPerfData;
}
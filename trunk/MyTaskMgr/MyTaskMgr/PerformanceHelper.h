#pragma once

#include "extypes.h"
#include "afxwin.h"
#include <ntstatus.h>
#include <AclAPI.h>

typedef struct _PERFDATA
{
	WCHAR				ImageName[MAX_PATH];
	HANDLE				ProcessId;
	WCHAR				UserName[MAX_PATH];
	ULONG				SessionId;
	ULONG				CPUUsage;
	LARGE_INTEGER		CPUTime;
	ULONG				WorkingSetSizeBytes;
	ULONG				PeakWorkingSetSizeBytes;
	ULONG				WorkingSetSizeDelta;
	ULONG				PageFaultCount;
	ULONG				PageFaultCountDelta;
	ULONG				VirtualMemorySizeBytes;
	ULONG				PagedPoolUsagePages;
	ULONG				NonPagedPoolUsagePages;
	ULONG				BasePriority;
	ULONG				HandleCount;
	ULONG				ThreadCount;
	ULONG				USERObjectCount;
	ULONG				GDIObjectCount;
	IO_COUNTERS			IOCounters;
	LARGE_INTEGER		UserTime;
	LARGE_INTEGER		KernelTime;
} PERFDATA, *PPERFDATA;

class CPerformanceHelper
{
public:	
	CPerformanceHelper(void);
	~CPerformanceHelper(void);
	void SidToUserName(PSID Sid, LPWSTR szBuffer, DWORD BufferSize);
	void PerfDataRefresh(void);
	ULONG PerfDataGetProcessCount(void);
	ULONG PerfDataGetProcessorUsage(void);
	ULONG PerfDataGetProcessorSystemUsage(void);
	BOOL PerfDataGetImageName(ULONG Index, LPWSTR lpImageName, int nMaxCount);
	int PerfGetIndexByProcessId(DWORD dwProcessId);
	ULONG PerfDataGetProcessId(ULONG Index);
	BOOL PerfDataGetUserName(ULONG Index, LPWSTR lpUserName, int nMaxCount);
	ULONG PerfDataGetSessionId(ULONG Index);
	ULONG PerfDataGetCPUUsage(ULONG Index);
	LARGE_INTEGER PerfDataGetCPUTime(ULONG Index);
	ULONG PerfDataGetWorkingSetSizeBytes(ULONG Index);
	ULONG PerfDataGetPeakWorkingSetSizeBytes(ULONG Index);
	ULONG PerfDataGetWorkingSetSizeDelta(ULONG Index);
	ULONG PerfDataGetPageFaultCount(ULONG Index);
	ULONG PerfDataGetPageFaultCountDelta(ULONG Index);
	ULONG PerfDataGetVirtualMemorySizeBytes(ULONG Index);
	ULONG PerfDataGetPagedPoolUsagePages(ULONG Index);
	ULONG PerfDataGetNonPagedPoolUsagePages(ULONG Index);
	ULONG PerfDataGetBasePriority(ULONG Index);
	ULONG PerfDataGetHandleCount(ULONG Index);
	ULONG PerfDataGetThreadCount(ULONG Index);
	ULONG PerfDataGetUSERObjectCount(ULONG Index);
	ULONG PerfDataGetGDIObjectCount(ULONG Index);
	BOOL PerfDataGetIOCounters(ULONG Index, PIO_COUNTERS pIoCounters);
	ULONG PerfDataGetCommitChargeTotalK(void);
	ULONG PerfDataGetCommitChargeLimitK(void);
	ULONG PerfDataGetCommitChargePeakK(void);
	ULONG PerfDataGetKernelMemoryTotalK(void);
	ULONG PerfDataGetKernelMemoryPagedK(void);
	ULONG PerfDataGetKernelMemoryNonPagedK(void);
	ULONG PerfDataGetPhysicalMemoryTotalK(void);
	ULONG PerfDataGetPhysicalMemoryAvailableK(void);
	ULONG PerfDataGetPhysicalMemorySystemCacheK(void);
	ULONG PerfDataGetSystemHandleCount(void);
	ULONG PerfDataGetTotalThreadCount(void);
	BOOL PerfDataGet(ULONG Index, PPERFDATA *lppData);
	PERFDATA* PerfGetData(); 
private:
	CRITICAL_SECTION                           PerfDataCriticalSection;
	PPERFDATA                                  pPerfDataOld;    /* Older perf data (saved to establish delta values) */
	PPERFDATA                                  pPerfData;    /* Most recent copy of perf data */
	ULONG                                      ProcessCountOld;
	ULONG                                      ProcessCount;
	double                                     dbIdleTime;
	double                                     dbKernelTime;
	double                                     dbSystemTime;
	LARGE_INTEGER                              liOldIdleTime;
	double                                     OldKernelTime;
	LARGE_INTEGER                              liOldSystemTime;
	SYSTEM_PERFORMANCE_INFORMATION             SystemPerfInfo;
	SYSTEM_BASIC_INFORMATION                   SystemBasicInfo;
	SYSTEM_FILECACHE_INFORMATION               SystemCacheInfo;
	SYSTEM_HANDLE_INFORMATION                  SystemHandleInfo;
	PSYSTEM_PROCESSOR_PERFORMANCE_INFORMATION  SystemProcessorTimeInfo;
	PSID                                       SystemUserSid;

	typedef NTSTATUS (WINAPI * PNtQuerySystemInformation)(SYSTEM_INFORMATION_CLASS,PVOID,ULONG,PULONG);

	double Li2Double(LARGE_INTEGER x)
	{
		return (double)x.HighPart * 4.294967296E9 + (double)x.LowPart;
	}

	BOOL GetNtQuerySystemInformation(PNtQuerySystemInformation *p)
	{
		*p = (PNtQuerySystemInformation)GetProcAddress(GetModuleHandleW(L"Ntdll.dll"), "NtQuerySystemInformation");
		if(p)
			return TRUE;
		return FALSE;
	}

	void ShowError(LPCTSTR title)
	{   
		//获得信息   
		LPVOID   lpMsgBuf;   //Windows   will   allocate     
		::FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER|FORMAT_MESSAGE_FROM_SYSTEM,0,   GetLastError(),MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),   //默认语言   
			(LPTSTR)&lpMsgBuf, 0, NULL);   

		//显示   
		::MessageBox(0, (LPCTSTR)lpMsgBuf, title, MB_OK|MB_ICONINFORMATION);   
		//lpMsgBuf中是你要的错误提示.   

		//释放内存   
		::LocalFree(lpMsgBuf);   
	}
};


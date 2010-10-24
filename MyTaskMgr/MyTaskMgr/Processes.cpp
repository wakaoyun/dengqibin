// Processes.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "Processes.h"
#include "afxdialogex.h"


// CProcesses dialog
int CProcesses::sortColum = 0;
BOOL CProcesses::isASC = FALSE;
IMPLEMENT_DYNAMIC(CProcesses, CDialogEx)

CProcesses::CProcesses(CWnd* pParent /*=NULL*/)
	: CDialogEx(CProcesses::IDD, pParent)
{
	columns = columnMgr.GetColumns();
}

CProcesses::~CProcesses()
{
	CTaskSetting taskSetting;
	taskSetting.SetSortColumn(isASC,sortColum);
}

void CProcesses::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_Process_LIST, m_Process);
	DDX_Control(pDX, IDC_btnEndProcess, m_EndProcess);
	DDX_Control(pDX, IDC_CHECK, m_ShowAll);
}


BEGIN_MESSAGE_MAP(CProcesses, CDialogEx)
	ON_WM_SIZE()
	ON_WM_TIMER()
	ON_NOTIFY(LVN_GETDISPINFO, IDC_Process_LIST, &CProcesses::OnLvnGetdispinfoProcessList)
	ON_NOTIFY(HDN_ITEMCLICK, 0, &CProcesses::OnHdnItemclickProcessList)
	ON_NOTIFY(NM_RCLICK, IDC_Process_LIST, &CProcesses::OnNMRClickProcessList)
END_MESSAGE_MAP()


// CProcesses message handlers


BOOL CProcesses::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	hProcPageListCtrl = ::GetDlgItem(this->m_hWnd, IDC_Process_LIST);
	columnMgr.AddColumns(hProcPageListCtrl);
	m_Process.SetExtendedStyle(m_Process.GetExtendedStyle()|LVS_EX_FULLROWSELECT);
	CTaskSetting taskSetting;
	sortColum = taskSetting.GetSortColumn();
	isASC = taskSetting.IsASC();

	CreateThread(NULL, 0, ProcPageRefreshThread, NULL, 0, NULL);
	ListView_SortItems(hProcPageListCtrl,ProcessPageCompareFunc,NULL);

	SetTimer(1,1000,NULL);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CProcesses::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);
	if(m_Process.m_hWnd!=NULL)
	{
		m_Process.MoveWindow(15, 15, cx - 28, cy - 60);
	}

	CRect rectBtn;
	if(m_EndProcess.m_hWnd!=NULL)
	{
		m_EndProcess.GetClientRect(&rectBtn);
		cx = cx - 15 - rectBtn.Width();
	
		m_EndProcess.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());
	}

	if(m_ShowAll.m_hWnd!=NULL)
	{
		m_ShowAll.GetClientRect(&rectBtn);	
		m_ShowAll.MoveWindow(15, cy - 36, rectBtn.Width(), rectBtn.Height());
	}
}

DWORD CProcesses::ProcPageRefreshThread(void *lpParameter)
{
	/* Create the event */
	hProcPageEvent = CreateEventW(NULL, TRUE, TRUE, NULL);

	/* If we couldn't create the event then exit the thread */
	if (!hProcPageEvent)
		return 0;

	while (1)
	{
		DWORD   dwWaitVal;

		/* Wait on the event */
		dwWaitVal = WaitForSingleObject(hProcPageEvent, INFINITE);

		/* If the wait failed then the event object must have been */
		/* closed and the task manager is exiting so exit this thread */
		if (dwWaitVal == WAIT_FAILED)
			return 0;

		if (dwWaitVal == WAIT_OBJECT_0)
		{
			/* Reset our event */
			ResetEvent(hProcPageEvent);
			perfHelper.PerfDataRefresh();
			PERFDATA *perfData = perfHelper.PerfGetData();
			RefreshProc(perfData); 
		}
	}
	return 0;
}

void CProcesses::RefreshProc(PERFDATA *p)
{
	PPERFDATA  pPerfData = NULL;
    LV_ITEM    item;
	LV_COLUMN  colum;					
    int        i;
    BOOL       bAlreadyInList = FALSE;

    memset(&item, 0, sizeof(LV_ITEM));

    /* Remove exit Process from our list */
	for (i=0; i<ListView_GetItemCount(hProcPageListCtrl); i++)
	{
		memset(&item, 0, sizeof(LV_ITEM));
		item.mask = LVIF_PARAM;
		item.iItem = i;
		(void)ListView_GetItem(hProcPageListCtrl, &item);

		pPerfData = (PPERFDATA)item.lParam;
		if(!ProcessRunning((DWORD)pPerfData->ProcessId))
		{
			(void)ListView_DeleteItem(hProcPageListCtrl, i);
			HeapFree(GetProcessHeap(), 0, pPerfData);
		}
		else
		{
			PERFDATA *pTemp = p;
			int count  = perfHelper.PerfDataGetProcessCount();
			for(int j = 0; j<count;j++,pTemp++)
			{
				if(pTemp->ProcessId == pPerfData->ProcessId)
				{
					/*check the data is had changed*/
					if(!(pPerfData->CPUUsage==pTemp->CPUUsage
						&&pPerfData->CPUTime.HighPart==pTemp->CPUTime.HighPart
						&&pPerfData->CPUTime.LowPart==pTemp->CPUTime.LowPart))
					{
						memset(pPerfData,0,sizeof(PERFDATA));
						memcpy(pPerfData,pTemp,sizeof(PERFDATA));

						/* Update the item */
						memset(&item, 0, sizeof(LV_ITEM));
						item.mask = LVIF_TEXT|LVIF_PARAM;			
						item.pszText = pPerfData->ImageName;
						item.iItem = i;
						item.lParam = (LPARAM)pPerfData;
						ListView_SetItem(hProcPageListCtrl, &item);	
					}
					break;
				}
			}
		}
	}
    
	int count = 1;
	/*add the processes to our list*/
	while(count++ != perfHelper.PerfDataGetProcessCount())
	{
		LV_ITEM item;
		p++;
		bAlreadyInList = FALSE;
		PPERFDATA pPData = (PPERFDATA)HeapAlloc(GetProcessHeap(), 0, sizeof(PERFDATA));

		/* Check to see if it's already in our list */
		for (i=0; i<ListView_GetItemCount(hProcPageListCtrl); i++)
		{
			memset(&item, 0, sizeof(LV_ITEM));
			item.mask = LVIF_PARAM;
			item.iItem = i;
			ListView_GetItem(hProcPageListCtrl, &item);

			pPData = (PPERFDATA)item.lParam;
			if(pPData->ProcessId == p->ProcessId)
			{
				bAlreadyInList = TRUE;
				break;
			}
		}
		
		if(bAlreadyInList)
			continue;
		
		pPData = (PPERFDATA)HeapAlloc(GetProcessHeap(), 0, sizeof(PERFDATA));
		memcpy(pPData,p,sizeof(PERFDATA));
		
		/* Add the item to the list */
		memset(&item, 0, sizeof(LV_ITEM));
		item.mask = LVIF_TEXT|LVIF_PARAM;			
		item.pszText = p->ImageName;
		item.iItem = ListView_GetItemCount(hProcPageListCtrl);
		item.lParam = (LPARAM)pPData;
		ListView_InsertItem(hProcPageListCtrl, &item);	
	}
}

BOOL CProcesses::ProcessRunning(DWORD ProcessId) 
{
	HANDLE hProcess;
	DWORD exitCode;

	if (ProcessId == 0 || ProcessId == 4) {
		return TRUE;
	}

	hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, ProcessId);
	if (hProcess == NULL) {
		hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, FALSE, ProcessId);
		if (hProcess == NULL) {
			return FALSE;
		}
	}

	if (GetExitCodeProcess(hProcess, &exitCode)) {
		CloseHandle(hProcess);
		return (exitCode == STILL_ACTIVE);
	}

	CloseHandle(hProcess);
	return FALSE;
}


void CProcesses::OnTimer(UINT_PTR nIDEvent)
{
	SetEvent(hProcPageEvent);

	CDialogEx::OnTimer(nIDEvent);
}


void CProcesses::OnLvnGetdispinfoProcessList(NMHDR *pNMHDR, LRESULT *pResult)
{
	NMLVDISPINFO *pDispInfo = reinterpret_cast<NMLVDISPINFO*>(pNMHDR);
	LVCOLUMN  col;
	col.mask = LVCF_SUBITEM;
	/*Get the colum Info*/
	ListView_GetColumn(hProcPageListCtrl,pDispInfo->item.iSubItem,&col);

	if(columns[col.iSubItem].isShow)
	{
		PERFDATA *pPerfData = (PERFDATA *)pDispInfo->item.lParam;
		/*Set the Column Data*/
		switch(columns[col.iSubItem].nId)
		{
		case COLUMN_IMAGENAME:  
			break;
		case COLUMN_PID:
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->ProcessId);
			break;
		case COLUMN_USERNAME: 
			pDispInfo->item.pszText = pPerfData->UserName;
			break;
		case COLUMN_SESSIONID:
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->SessionId);
			break;
		case COLUMN_CPUUSAGE: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%02d"),pPerfData->CPUUsage);
			break;
		case COLUMN_CPUTIME:
			DWORD dwHours;
			DWORD dwMinutes;
			DWORD dwSeconds;

			GetHMSFromLargeInt(pPerfData->CPUTime, &dwHours, &dwMinutes, &dwSeconds);
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%02d:%02d:%02d"),dwHours,dwMinutes,dwSeconds);
			break;
		case COLUMN_MEMORYUSAGE: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->WorkingSetSizeBytes / 1024);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			wcscat(pDispInfo->item.pszText,_T(" K"));
			break;
		case COLUMN_PEAKMEMORYUSAGE: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d")
				,pPerfData->PeakWorkingSetSizeBytes / 1024);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			wcscat(pDispInfo->item.pszText,_T(" K"));
			break;
		case COLUMN_MEMORYUSAGEDELTA: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->WorkingSetSizeDelta / 1024);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			wcscat(pDispInfo->item.pszText,_T(" K"));
			break;
		case COLUMN_PAGEFAULTS: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->PageFaultCount);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_PAGEFAULTSDELTA: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->PageFaultCountDelta);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_VIRTUALMEMORYSIZE: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->VirtualMemorySizeBytes / 1024);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			wcscat(pDispInfo->item.pszText,_T(" K"));
			break;
		case COLUMN_PAGEDPOOL: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->PagedPoolUsagePages / 1024);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			wcscat(pDispInfo->item.pszText,_T(" K"));
			break;
		case COLUMN_NONPAGEDPOOL: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->NonPagedPoolUsagePages / 1024);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			wcscat(pDispInfo->item.pszText,_T(" K"));
			break;
		case COLUMN_BASEPRIORITY:
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->BasePriority);
			break;
		case COLUMN_HANDLECOUNT: 
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->HandleCount);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_THREADCOUNT:
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->ThreadCount);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_USEROBJECTS:
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->USERObjectCount);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_GDIOBJECTS:
			swprintf_s(pDispInfo->item.pszText,pDispInfo->item.cchTextMax,_T("%d"),pPerfData->GDIObjectCount);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_IOREADS:
			_ui64tow(pPerfData->IOCounters.ReadOperationCount,pDispInfo->item.pszText,10);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_IOWRITES:
			_ui64tow(pPerfData->IOCounters.WriteOperationCount,pDispInfo->item.pszText,10);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_IOOTHER: 
			_ui64tow(pPerfData->IOCounters.OtherOperationCount,pDispInfo->item.pszText,10);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_IOREADBYTES: 
			_ui64tow(pPerfData->IOCounters.ReadTransferCount,pDispInfo->item.pszText,10);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_IOWRITEBYTES: 
			_ui64tow(pPerfData->IOCounters.WriteTransferCount,pDispInfo->item.pszText,10);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		case COLUMN_IOOTHERBYTES: 
			_ui64tow(pPerfData->IOCounters.OtherTransferCount,pDispInfo->item.pszText,10);
			SeparateNumber(pDispInfo->item.pszText,pDispInfo->item.cchTextMax);
			break;
		}
	}

	*pResult = 0;
}

int CALLBACK CProcesses::ProcessPageCompareFunc(LPARAM lParam1, LPARAM lParam2, LPARAM lParamSort)
{
	PERFDATA* Param1;
	PERFDATA* Param2;
	int result;
	if (isASC) {
		Param1 = (PERFDATA *)lParam1;
		Param2 = (PERFDATA *)lParam2;
	} else {
		Param1 = (PERFDATA *)lParam2;
		Param2 = (PERFDATA *)lParam1;
	}

	switch(sortColum)
	{
	case COLUMN_IMAGENAME:
		result = _wcsicmp(Param1->ImageName, Param2->ImageName);
		break;
	case COLUMN_PID:
		result = CMP(Param1->ProcessId, Param2->ProcessId);
		break;
	case COLUMN_USERNAME: 
		result = _wcsicmp(Param1->UserName, Param2->UserName);
		break;
	case COLUMN_SESSIONID:
		result = CMP(Param1->SessionId, Param2->SessionId);
		break;
	case COLUMN_CPUUSAGE: 
		result = CMP(Param1->CPUUsage, Param2->CPUUsage);
		break;
	case COLUMN_CPUTIME:
		result = 0;
		break;
	case COLUMN_MEMORYUSAGE: 
		result = CMP(Param1->WorkingSetSizeBytes, Param2->WorkingSetSizeBytes);
		break;
	case COLUMN_PEAKMEMORYUSAGE: 
		result = CMP(Param1->PeakWorkingSetSizeBytes, Param2->PeakWorkingSetSizeBytes);
		break;
	case COLUMN_MEMORYUSAGEDELTA:
		result = CMP(Param1->WorkingSetSizeDelta, Param2->WorkingSetSizeDelta);
		break;
	case COLUMN_PAGEFAULTS: 
		result = CMP(Param1->PageFaultCount, Param2->PageFaultCount);
		break;
	case COLUMN_PAGEFAULTSDELTA: 
		result = CMP(Param1->PageFaultCountDelta, Param2->PageFaultCountDelta);
		break;
	case COLUMN_VIRTUALMEMORYSIZE: 
		result = CMP(Param1->VirtualMemorySizeBytes, Param2->VirtualMemorySizeBytes);
		break;
	case COLUMN_PAGEDPOOL:
		result = CMP(Param1->PagedPoolUsagePages, Param2->PagedPoolUsagePages);
		break;
	case COLUMN_NONPAGEDPOOL:
		result = CMP(Param1->NonPagedPoolUsagePages, Param2->NonPagedPoolUsagePages);
		break;
	case COLUMN_BASEPRIORITY:
		result = CMP(Param1->BasePriority, Param2->BasePriority);
		break;
	case COLUMN_HANDLECOUNT: 
		result = CMP(Param1->HandleCount, Param2->HandleCount);
		break;
	case COLUMN_THREADCOUNT:
		result = CMP(Param1->ThreadCount, Param2->ThreadCount);
		break;
	case COLUMN_USEROBJECTS:
		result = CMP(Param1->USERObjectCount, Param2->USERObjectCount);
		break;
	case COLUMN_GDIOBJECTS:
		result = CMP(Param1->GDIObjectCount, Param2->GDIObjectCount);
		break;
	case COLUMN_IOREADS:
		result = CMP(Param1->IOCounters.ReadOperationCount, Param2->IOCounters.ReadOperationCount);
		break;
	case COLUMN_IOWRITES:
		result = CMP(Param1->IOCounters.WriteOperationCount, Param2->IOCounters.WriteOperationCount);
		break;
	case COLUMN_IOOTHER: 
		result = CMP(Param1->IOCounters.OtherOperationCount, Param2->IOCounters.OtherOperationCount);
		break;
	case COLUMN_IOREADBYTES: 
		result = CMP(Param1->IOCounters.ReadTransferCount, Param2->IOCounters.ReadTransferCount);
		break;
	case COLUMN_IOWRITEBYTES: 
		result = CMP(Param1->IOCounters.WriteTransferCount, Param2->IOCounters.WriteTransferCount);
		break;
	case COLUMN_IOOTHERBYTES: 
		result = CMP(Param1->IOCounters.OtherTransferCount, Param2->IOCounters.OtherTransferCount);
		break;
	}

	return result;
}

void CProcesses::OnHdnItemclickProcessList(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMHEADER phdr = reinterpret_cast<LPNMHEADER>(pNMHDR);
	
	LVCOLUMN  col;
	col.mask = LVCF_SUBITEM;
	/*Get the colum Info*/
	ListView_GetColumn(hProcPageListCtrl,phdr->iItem,&col);

	sortColum = col.iSubItem;
	isASC = !isASC;

	ListView_SortItems(hProcPageListCtrl,ProcessPageCompareFunc,NULL);

	*pResult = 0;
}

inline void CProcesses::GetHMSFromLargeInt(LARGE_INTEGER time,DWORD *dwHours, DWORD *dwMinutes, DWORD *dwSeconds)
{
#ifdef _MSC_VER
	*dwHours = (DWORD)(time.QuadPart / 36000000000L);
	*dwMinutes = (DWORD)((time.QuadPart % 36000000000L) / 600000000L);
	*dwSeconds = (DWORD)(((time.QuadPart % 36000000000L) % 600000000L) / 10000000L);
#else
	*dwHours = (DWORD)(time.QuadPart / 36000000000LL);
	*dwMinutes = (DWORD)((time.QuadPart % 36000000000LL) / 600000000LL);
	*dwSeconds = (DWORD)(((time.QuadPart % 36000000000LL) % 600000000LL) / 10000000LL);
#endif
}

void CProcesses::SeparateNumber(LPWSTR strNumber, int nMaxCount)
{
	WCHAR  temp[260];
	UINT   i, j, k;

	for (i=0,j=0; i<(wcslen(strNumber) % 3); i++, j++)
		temp[j] = strNumber[i];
	for (k=0; i<wcslen(strNumber); i++,j++,k++) {
		if ((k % 3 == 0) && (j > 0))
			temp[j++] = L',';
		temp[j] = strNumber[i];
	}
	temp[j] = L'\0';
	wcsncpy(strNumber, temp, nMaxCount);
}

void CProcesses::OnNMRClickProcessList(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMITEMACTIVATE pNMItemActivate = reinterpret_cast<LPNMITEMACTIVATE>(pNMHDR);
	

	*pResult = 0;
}

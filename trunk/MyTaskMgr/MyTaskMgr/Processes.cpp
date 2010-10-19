// Processes.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "Processes.h"
#include "afxdialogex.h"


// CProcesses dialog
	
IMPLEMENT_DYNAMIC(CProcesses, CDialogEx)

CProcesses::CProcesses(CWnd* pParent /*=NULL*/)
	: CDialogEx(CProcesses::IDD, pParent)
{
	columns = columnMgr.GetColumns();
}

CProcesses::~CProcesses()
{

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
END_MESSAGE_MAP()


// CProcesses message handlers


BOOL CProcesses::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	hProcPageListCtrl = ::GetDlgItem(this->m_hWnd, IDC_Process_LIST);
	columnMgr.AddColumns(hProcPageListCtrl);
	m_Process.SetExtendedStyle(m_Process.GetExtendedStyle()|LVS_EX_FULLROWSELECT);

	CreateThread(NULL, 0, ProcPageRefreshThread, NULL, 0, NULL);
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
		/*wcscpy(pPData->ImageName, p->ImageName);
		pPData->ProcessId = p->ProcessId;*/
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
		return FALSE;
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
	
	if(columns[pDispInfo->item.iSubItem].isShow)
	{
		PERFDATA *pPerfData = (PERFDATA *)pDispInfo->item.lParam;
		switch(columns[pDispInfo->item.iSubItem].nId)
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
			break;
		case COLUMN_PEAKMEMORYUSAGE:  
			break;
		case COLUMN_MEMORYUSAGEDELTA:  
			break;
		case COLUMN_PAGEFAULTS: 
			break;
		case COLUMN_PAGEFAULTSDELTA: 
			break;
		case COLUMN_VIRTUALMEMORYSIZE:   
			break;
		case COLUMN_PAGEDPOOL: 
			break;
		case COLUMN_NONPAGEDPOOL: 
			break;
		case COLUMN_BASEPRIORITY: 
			break;
		case COLUMN_HANDLECOUNT: 
			break;
		case COLUMN_THREADCOUNT: 
			break;
		case COLUMN_USEROBJECTS:   
			break;
		case COLUMN_GDIOBJECTS: 
			break;
		case COLUMN_IOREADS:  
			break;
		case COLUMN_IOWRITES: 
			break;
		case COLUMN_IOOTHER:  
			break;
		case COLUMN_IOREADBYTES: 
			break;
		case COLUMN_IOWRITEBYTES: 
			break;
		case COLUMN_IOOTHERBYTES:  
			break;
		}
	}

	*pResult = 0;
}


void CProcesses::OnHdnItemclickProcessList(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMHEADER phdr = reinterpret_cast<LPNMHEADER>(pNMHDR);
	// TODO: Add your control notification handler code here
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

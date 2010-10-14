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
END_MESSAGE_MAP()


// CProcesses message handlers


BOOL CProcesses::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	m_Process.InsertColumn(0, _T("Image Name"), LVCFMT_LEFT, 100, -1);
	hProcPageListCtrl = ::GetDlgItem(this->m_hWnd, IDC_Process_LIST);

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
	perfHelper.PerfGetData();
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
			PERFDATA *perfData = perfHelper.PerfGetData();
			RefreshProc(perfData); 
		}
	}
	return 0;
}

void CProcesses::RefreshProc(PERFDATA *p)
{
	PPERFDATA  pPerfData = NULL;
    LV_ITEM                       item;
	LV_COLUMN					  colum;					
    int                           i;
    BOOL                          bAlreadyInList = FALSE;

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
	}
    
	int count = 1;
	while(count++ != perfHelper.PerfDataGetProcessCount())
	{
		LV_ITEM                       item;
		p++;
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

		wcscpy(pPData->ImageName, p->ImageName);
		pPData->ProcessId = p->ProcessId;

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

	if (ProcessId == 0) {
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

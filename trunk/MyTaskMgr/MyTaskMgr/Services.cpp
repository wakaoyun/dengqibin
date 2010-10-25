// Services.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "Services.h"
#include "afxdialogex.h"


// CServices dialog

IMPLEMENT_DYNAMIC(CServices, CDialogEx)

CServices::CServices(CWnd* pParent /*=NULL*/)
	: CDialogEx(CServices::IDD, pParent)
{

}

CServices::~CServices()
{
}

void CServices::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_Services_LIST, m_Services);
	DDX_Control(pDX, IDC_btnServices, m_btnServices);
}


BEGIN_MESSAGE_MAP(CServices, CDialogEx)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CServices message handlers


BOOL CServices::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	m_Services.InsertColumn(0, _T("Name"), LVCFMT_LEFT, 80, -1);
	m_Services.InsertColumn(1, _T("PID"), LVCFMT_LEFT, 50, -1);
	m_Services.InsertColumn(2, _T("Description"), LVCFMT_LEFT, 85, -1);
	m_Services.InsertColumn(3, _T("Status"), LVCFMT_LEFT, 50, -1);
	m_Services.InsertColumn(4, _T("Group"), LVCFMT_LEFT, 80, -1);
	hServicePageListCtrl = ::GetDlgItem(this->m_hWnd, IDC_Services_LIST);
	m_Services.SetExtendedStyle(m_Services.GetExtendedStyle()|LVS_EX_FULLROWSELECT);

	CreateThread(NULL, 0, ServicePageRefreshThread, NULL, 0, NULL);
	//ListView_SortItems(hServicePageListCtrl,ServicePageCompareFunc,NULL);

	SetTimer(1,1000,NULL);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CServices::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);

	if(m_Services.m_hWnd!=NULL)
	{
		m_Services.MoveWindow(15, 15, cx - 28, cy - 60);
	}
	CRect rectBtn;
	if(m_btnServices.m_hWnd!=NULL)
	{
		m_btnServices.GetClientRect(&rectBtn);
		cx = cx - 15 - rectBtn.Width();
		if(m_btnServices.m_hWnd!=NULL)
		{
			m_btnServices.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());
		}
	}
}


DWORD CALLBACK CServices::ServicePageRefreshThread(void *lpParameter)
{
	/* Create the event */
	hServicePageEvent = CreateEventW(NULL, TRUE, TRUE, NULL);

	/* If we couldn't create the event then exit the thread */
	if (!hServicePageEvent)
		return 0;

	while (1)
	{
		DWORD   dwWaitVal;

		/* Wait on the event */
		dwWaitVal = WaitForSingleObject(hServicePageEvent, INFINITE);

		/* If the wait failed then the event object must have been */
		/* closed and the task manager is exiting so exit this thread */
		if (dwWaitVal == WAIT_FAILED)
			return 0;

		if (dwWaitVal == WAIT_OBJECT_0)
		{
			/* Reset our event */
			ResetEvent(hServicePageEvent);
			GetServiceInfo();
		}
	}
	return 0;
}

void CServices::GetServiceInfo()
{
	SC_HANDLE sc = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS);
	if(sc==NULL)
		return;

	SC_HANDLE hSCCervice;

	DWORD ret=0;
	DWORD size=0;
	DWORD dwError=0;
	LPENUM_SERVICE_STATUS st;
	LPQUERY_SERVICE_CONFIG lpsc;
	CServices::PSERVICEINFO pServiceInfo;
	if(!EnumServicesStatus(sc,SERVICE_WIN32,SERVICE_STATE_ALL, NULL,0,&size,&ret, NULL))
	{		
		st = (LPENUM_SERVICE_STATUS)LocalAlloc(LPTR,(SIZE_T)size);
		EnumServicesStatus(sc,SERVICE_WIN32,SERVICE_STATE_ALL, st,size,&size,&ret,NULL);
	}
	pServiceInfo = (CServices::PSERVICEINFO)LocalAlloc(LPTR,(SIZE_T)(ret * sizeof(SERVICEINFO)));
	for (int i=0; i<ret; i++)
	{
		hSCCervice = OpenService(sc, st[i].lpServiceName, SERVICE_QUERY_CONFIG);
		if(hSCCervice == NULL)
		{
			CloseServiceHandle(sc);
			return;
		}
		if(!QueryServiceConfig(hSCCervice, NULL, 0, &size))
		{
			dwError = GetLastError();
			if(ERROR_INSUFFICIENT_BUFFER == dwError)
			{
				lpsc = (LPQUERY_SERVICE_CONFIG)LocalAlloc(LPTR,(SIZE_T)size);
				QueryServiceConfig(hSCCervice, lpsc, size, &size);
			}else{
				goto cleanup;
			}
		}
		pServiceInfo->ServiceStatus = st[i];
		wcscpy(pServiceInfo->OrderGroup,lpsc->lpLoadOrderGroup);
		pServiceInfo++;
	}	

cleanup:
	CloseServiceHandle(sc);
	CloseServiceHandle(hSCCervice);
}
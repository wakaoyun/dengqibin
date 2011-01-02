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
	ON_NOTIFY(LVN_GETDISPINFO, IDC_Services_LIST, &CServices::OnLvnGetdispinfoServicesList)
	ON_NOTIFY(HDN_ITEMCLICK, 0, &CServices::OnHdnItemclickServicesList)
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CServices message handlers


BOOL CServices::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	m_Services.InsertColumn(0, _T("Name"), LVCFMT_LEFT, 70, 0);
	m_Services.InsertColumn(1, _T("PID"), LVCFMT_LEFT, 37, 1);
	m_Services.InsertColumn(2, _T("Description"), LVCFMT_LEFT, 100, 2);
	m_Services.InsertColumn(3, _T("Status"), LVCFMT_LEFT, 52, 3);
	m_Services.InsertColumn(4, _T("Group"), LVCFMT_LEFT, 70, 4);
	hServicePageListCtrl = ::GetDlgItem(this->m_hWnd, IDC_Services_LIST);
	m_Services.SetExtendedStyle(m_Services.GetExtendedStyle()|LVS_EX_FULLROWSELECT);

	CreateThread(NULL, 0, ServicePageRefreshThread, NULL, 0, NULL);

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
	LPENUM_SERVICE_STATUS_PROCESS st;
	LPQUERY_SERVICE_CONFIG lpsc;
	CServices::PSERVICEINFO pServiceInfo;
	/*得到所需要的大小*/
	if(!EnumServicesStatusEx(sc,SC_ENUM_PROCESS_INFO, SERVICE_WIN32,SERVICE_STATE_ALL, NULL,0,&size,&ret, NULL, NULL))
	{		
		st = (LPENUM_SERVICE_STATUS_PROCESS)LocalAlloc(LPTR,(SIZE_T)size);
		EnumServicesStatusEx(sc,SC_ENUM_PROCESS_INFO,SERVICE_WIN32,SERVICE_STATE_ALL, (LPBYTE)st,size,&size,&ret,NULL, NULL);
	}
	pServiceInfo = (CServices::PSERVICEINFO)LocalAlloc(LPTR,(SIZE_T)(ret * sizeof(SERVICEINFO)));
	CServices::PSERVICEINFO pTemp = pServiceInfo;
	/*得到每一个服务的有关信息*/
	for (int i=0; i<ret; i++)
	{
		hSCCervice = OpenService(sc, st[i].lpServiceName, SERVICE_QUERY_CONFIG);
		if(hSCCervice == NULL)
		{
			CloseServiceHandle(sc);
			return;
		}
		/*得到服务信息所需大小*/
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
		pTemp->ServiceStatus = st[i];
		/*所属组为exe文件路径中"－k "之后的字符*/
		WCHAR *p = wcsstr(lpsc->lpBinaryPathName, TEXT("-k"));
		/*没有则说明不属于任何组*/
		if(NULL == p)
		{
			wcscpy(pTemp->OrderGroup, TEXT("N/A"));
		}
		else
		{
			wcscpy(pTemp->OrderGroup, p+3);
		}
		
		pTemp++;
	}	

	RefreshSevice(pServiceInfo, ret);

cleanup:
	CloseServiceHandle(sc);
	CloseServiceHandle(hSCCervice);
}

void CServices::RefreshSevice(SERVICEINFO* pService, DWORD count)
{
	LV_ITEM    item;
	LV_COLUMN  colum;
	BOOL bAlreadyInList = FALSE;
	int i = 0;
	while(i!=count)
	{
		/*查看服务是否已经在列表中*/
		for (int j=0; j<ListView_GetItemCount(hServicePageListCtrl); j++)
		{
			item.mask = LVIF_PARAM;
			item.iItem = j;
			ListView_GetItem(hServicePageListCtrl, &item);

			PSERVICEINFO pS = (PSERVICEINFO)item.lParam;
			if(!wcscmp(pS->ServiceStatus.lpServiceName,pService->ServiceStatus.lpServiceName))
			{
				bAlreadyInList = TRUE;
				/*判断是否有更新*/
				if (pS->ServiceStatus.ServiceStatusProcess.dwProcessId!=
					pService->ServiceStatus.ServiceStatusProcess.dwProcessId 
					|| pS->ServiceStatus.ServiceStatusProcess.dwCurrentState!=
					pService->ServiceStatus.ServiceStatusProcess.dwCurrentState)
				{
					memcpy(pS, pService, sizeof(SERVICEINFO));
					memset(&item, 0, sizeof(LV_ITEM));
					item.mask = LVIF_TEXT|LVIF_PARAM;			
					item.pszText = pS->ServiceStatus.lpServiceName;
					item.iItem = j;
					item.lParam = (LPARAM)pS;
					ListView_SetItem(hServicePageListCtrl, &item);	
				}
				break;
			}
		}
		/*如果不存在则加入列表*/
		if(!bAlreadyInList)
		{
			PSERVICEINFO temp = (PSERVICEINFO)HeapAlloc(GetProcessHeap(), 0, sizeof(SERVICEINFO));
			memcpy(temp, pService, sizeof(SERVICEINFO));
			memset(&item, 0, sizeof(LV_ITEM));
			item.mask = LVIF_TEXT|LVIF_PARAM;	
			item.pszText = temp->ServiceStatus.lpServiceName;
			item.lParam = (LPARAM)temp;
			item.iItem = ListView_GetItemCount(hServicePageListCtrl);
			ListView_InsertItem(hServicePageListCtrl, &item);
		}
		pService++;
		i++;
	}
}

void CServices::OnLvnGetdispinfoServicesList(NMHDR *pNMHDR, LRESULT *pResult)
{
	NMLVDISPINFO *pDispInfo = reinterpret_cast<NMLVDISPINFO*>(pNMHDR);
	
	PSERVICEINFO pService = (PSERVICEINFO)pDispInfo->item.lParam;
	/*服务对应的进程*/
	if(pDispInfo->item.iSubItem == 1 && pService->ServiceStatus.ServiceStatusProcess.dwCurrentState != SERVICE_STOPPED)
	{
		swprintf(pDispInfo->item.pszText, TEXT("%d"), pService->ServiceStatus.ServiceStatusProcess.dwProcessId);
	}
	/*服务描述信息*/
	if(pDispInfo->item.iSubItem == 2)
	{
		wcscpy(pDispInfo->item.pszText, pService->ServiceStatus.lpDisplayName);				
	}
	/*服务运行状态*/
	if(pDispInfo->item.iSubItem == 3)
	{
		if (pService->ServiceStatus.ServiceStatusProcess.dwCurrentState != SERVICE_STOPPED)
		{
			wcscpy(pDispInfo->item.pszText, TEXT("Running"));
		}
		else
		{
			wcscpy(pDispInfo->item.pszText, TEXT("Stopped"));
		}		
	}
	/*服务所属组*/
	if(pDispInfo->item.iSubItem == 4)
	{
		wcscpy(pDispInfo->item.pszText,pService->OrderGroup);
	}
	*pResult = 0;
}


void CServices::OnHdnItemclickServicesList(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMHEADER phdr = reinterpret_cast<LPNMHEADER>(pNMHDR);
	
	LVCOLUMN  col;
	col.mask = LVCF_SUBITEM;
	/*Get the colum Info*/
	ListView_GetColumn(hServicePageListCtrl,phdr->iItem,&col);

	serviceColumn = col.iSubItem;
	ListView_SortItems(hServicePageListCtrl,ServiceCompareFunc,NULL);
	serviceIsASC = !serviceIsASC;

	*pResult = 0;
}

int CALLBACK ServiceCompareFunc(LPARAM lParam1, LPARAM lParam2, LPARAM lParamSort)
{
	CServices::PSERVICEINFO param1;
	CServices::PSERVICEINFO param2;
	
	if (serviceIsASC) 
	{
		param1 = (CServices::PSERVICEINFO)lParam1;
		param2 = (CServices::PSERVICEINFO)lParam2;
	} else {
		param1 = (CServices::PSERVICEINFO)lParam2;
		param2 = (CServices::PSERVICEINFO)lParam1;
	}
	int result = 0;
	switch(serviceColumn)
	{
	case 0:
		result = wcscmp(param1->ServiceStatus.lpServiceName, param2->ServiceStatus.lpServiceName);
		break;
	case 1:		
		result = CMP(param1->ServiceStatus.ServiceStatusProcess.dwProcessId, param2->ServiceStatus.ServiceStatusProcess.dwProcessId);
		break;
	case 2:
		result = wcscmp(param1->ServiceStatus.lpDisplayName, param2->ServiceStatus.lpDisplayName);
		break;
	case 3:
		result = CMP(param1->ServiceStatus.ServiceStatusProcess.dwCurrentState, param2->ServiceStatus.ServiceStatusProcess.dwCurrentState);
		break;
	case 4:
		result = wcscmp(param1->OrderGroup, param2->OrderGroup);
		break;
	}
	return result;
}


void CServices::OnTimer(UINT_PTR nIDEvent)
{
	SetEvent(hServicePageEvent);

	CDialogEx::OnTimer(nIDEvent);
}

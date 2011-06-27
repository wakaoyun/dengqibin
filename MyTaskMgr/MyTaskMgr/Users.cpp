// Users.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "Users.h"
#include "afxdialogex.h"


// CUsers dialog

IMPLEMENT_DYNAMIC(CUsers, CDialogEx)

CUsers::CUsers(CWnd* pParent /*=NULL*/)
	: CDialogEx(CUsers::IDD, pParent)
{
 
}

CUsers::~CUsers()
{
}

void CUsers::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_Users_LIST, m_UsersList);
	DDX_Control(pDX, IDC_btnDisconnect, m_btnDisconnect);
	DDX_Control(pDX, IDC_btnLogoff, m_btnLogoff);
	DDX_Control(pDX, IDC_btnSendMsg, m_btnSendMsg);
}


BEGIN_MESSAGE_MAP(CUsers, CDialogEx)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CUsers message handlers


BOOL CUsers::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	m_UsersList.InsertColumn(0, _T("User"), LVCFMT_LEFT, 80, 0);
	m_UsersList.InsertColumn(1, _T("ID"), LVCFMT_LEFT, 35, 1);
	m_UsersList.InsertColumn(2, _T("Status"), LVCFMT_LEFT, 50, 2);
	m_UsersList.InsertColumn(3, _T("Client Name"), LVCFMT_LEFT, 100, 3);
	m_UsersList.InsertColumn(4, _T("Session"), LVCFMT_LEFT, 80, 4);
	hUsersPageListCtrl = ::GetDlgItem(this->m_hWnd, IDC_Users_LIST);
	m_UsersList.SetExtendedStyle(m_UsersList.GetExtendedStyle()|LVS_EX_FULLROWSELECT);

	CreateThread(NULL, 0, UsersPageRefreshThread, NULL, 0, NULL);

	SetTimer(1,1000,NULL);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CUsers::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);

	if(m_UsersList.m_hWnd!=NULL)
	{
		m_UsersList.MoveWindow(15, 15, cx - 28, cy - 60);
	}

	CRect rectBtn;
	if(m_btnSendMsg.m_hWnd!=NULL)
	{
		m_btnSendMsg.GetClientRect(&rectBtn);
		cx = cx - 15 - rectBtn.Width();
	
		m_btnSendMsg.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());	

		if(m_btnDisconnect.m_hWnd!=NULL)
		{
			m_btnDisconnect.GetClientRect(&rectBtn);
			cx = cx - 5 - rectBtn.Width();
	
			m_btnDisconnect.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());
		}
		cx = cx - 5 - rectBtn.Width();
		if(m_btnLogoff.m_hWnd!=NULL)
		{
			m_btnLogoff.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());
		}
	}
}



DWORD CALLBACK CUsers::UsersPageRefreshThread(void *lpParameter)
{
	/* Create the event */
	hUsersPageEvent = CreateEventW(NULL, TRUE, TRUE, NULL);

	EnumerateLogonSessions LsaEnumerateLogonSessions = (EnumerateLogonSessions)GetProcAddress(LoadLibrary(L"Secur32.dll"), "LsaEnumerateLogonSessions");
	GetLogonSessionData LsaGetLogonSessionData = (GetLogonSessionData)GetProcAddress(LoadLibrary(L"Secur32.dll"), "LsaGetLogonSessionData");
	FreeReturnBuffer LsaFreeReturnBuffer  = (FreeReturnBuffer)GetProcAddress(LoadLibrary(L"Secur32.dll"), "LsaGetLogonSessionData");

	/* If we couldn't create the event then exit the thread */
	if (!hUsersPageEvent)
		return 0;

	while (1)
	{
		DWORD   dwWaitVal;

		/* Wait on the event */
		dwWaitVal = WaitForSingleObject(hUsersPageEvent, INFINITE);

		/* If the wait failed then the event object must have been */
		/* closed and the task manager is exiting so exit this thread */
		if (dwWaitVal == WAIT_FAILED)
			return 0;

		if (dwWaitVal == WAIT_OBJECT_0)
		{
			/* Reset our event */
			ResetEvent(hUsersPageEvent);
			GetUsersInfo(LsaEnumerateLogonSessions, LsaGetLogonSessionData, LsaFreeReturnBuffer);
		}
	}
	return 0;
}


void CUsers::GetUsersInfo(EnumerateLogonSessions LsaEnumerateLogonSessions,
						GetLogonSessionData LsaGetLogonSessionData,
						FreeReturnBuffer LsaFreeReturnBuffer)
{
	ULONG count = 0;
	PLUID pLuid;
	LV_ITEM    item;
	LV_COLUMN  colum;
	BOOL bAlreadyInList = FALSE;

	if (LsaEnumerateLogonSessions(&count, &pLuid) == STATUS_SUCCESS)
	{
		/*枚举每一个用户的信息*/
		for (int i=0; i<count; i++)
		{
			PSECURITY_LOGON_SESSION_DATA pLogonData;
			if(LsaGetLogonSessionData(pLuid, &pLogonData)==STATUS_SUCCESS)
			{
				/*交互式登入、远程桌面才显示*/
				if(pLogonData->LogonType == 2 || pLogonData->LogonType == 10)
				{
					/*判断列表中是否已经添加了该用户*/
					for (int j=0; j<ListView_GetItemCount(hUsersPageListCtrl); j++)
					{
						item.mask = LVIF_PARAM;
						item.iItem = j;
						ListView_GetItem(hUsersPageListCtrl, &item);

						PUSERINFO pUserInfo = (PUSERINFO)item.lParam;
						if(pUserInfo->UserID==(int)pLogonData->Session)
						{
							bAlreadyInList = TRUE;
							break;
						}
					}
					if(!bAlreadyInList)
					{
						PUSERINFO temp = (PUSERINFO)HeapAlloc(GetProcessHeap(), 0, sizeof(USERINFO));
						temp->UserID = (int)pLogonData->Session;
						wcsncpy(temp->UserName,pLogonData->UserName.Buffer,pLogonData->UserName.Length);
						memset(&item, 0, sizeof(LV_ITEM));
						item.mask = LVIF_TEXT|LVIF_PARAM;	
						item.pszText = temp->UserName;
						item.lParam = (LPARAM)temp;
						item.iItem = ListView_GetItemCount(hUsersPageListCtrl);
						ListView_InsertItem(hUsersPageListCtrl, &item);
					}
				}
				//LsaFreeReturnBuffer(pLogonData);
			}
			pLuid++;
		}
	}
}

void CUsers::OnTimer(UINT_PTR nIDEvent)
{
	SetEvent(hUsersPageEvent);

	CDialogEx::OnTimer(nIDEvent);
}
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
	m_UsersList.InsertColumn(0, _T("User"), LVCFMT_LEFT, 80, -1);
	m_UsersList.InsertColumn(1, _T("ID"), LVCFMT_LEFT, 35, -1);
	m_UsersList.InsertColumn(2, _T("Status"), LVCFMT_LEFT, 50, -1);
	m_UsersList.InsertColumn(3, _T("Client Name"), LVCFMT_LEFT, 100, -1);
	m_UsersList.InsertColumn(4, _T("Session"), LVCFMT_LEFT, 80, -1);

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

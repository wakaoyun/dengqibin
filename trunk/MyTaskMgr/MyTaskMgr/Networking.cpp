// Networking.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "Networking.h"
#include "afxdialogex.h"


// CNetworking dialog

IMPLEMENT_DYNAMIC(CNetworking, CDialogEx)

CNetworking::CNetworking(CWnd* pParent /*=NULL*/)
	: CDialogEx(CNetworking::IDD, pParent)
{

}

CNetworking::~CNetworking()
{
}

void CNetworking::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_NetworkingLIST, m_NetworkingList);
	DDX_Control(pDX, IDC_STATIC_Local, m_LoacalStatic);
}


BEGIN_MESSAGE_MAP(CNetworking, CDialogEx)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CNetworking message handlers


BOOL CNetworking::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	m_NetworkingList.InsertColumn(0, _T("Adapter Name"), LVCFMT_LEFT, 100, -1);
	m_NetworkingList.InsertColumn(1, _T("Network Utilization"), LVCFMT_LEFT, 100, -1);
	m_NetworkingList.InsertColumn(2, _T("Link Speed"), LVCFMT_LEFT, 80, -1);
	m_NetworkingList.InsertColumn(3, _T("State"), LVCFMT_LEFT, 70, -1);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CNetworking::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);
	cx = cx - 10;
	cy = cy * (3 / 4.0F)- 10;
	if(m_LoacalStatic.m_hWnd!=NULL)
	{
		m_LoacalStatic.MoveWindow(5, 5, cx, cy);
	}
	if(m_NetworkingList.m_hWnd!=NULL)
	{
		m_NetworkingList.MoveWindow(5,cy + 13, cx, cy / 3);
	}
}

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

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CServices::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);

	m_Services.MoveWindow(15, 15, cx - 28, cy - 60);
	CRect rectBtn;
	m_btnServices.GetClientRect(&rectBtn);
	cx = cx - 15 - rectBtn.Width();
	m_btnServices.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());
}

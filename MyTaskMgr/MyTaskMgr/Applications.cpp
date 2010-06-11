// Applications.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "Applications.h"
#include "afxdialogex.h"


// CApplications dialog

IMPLEMENT_DYNAMIC(CApplications, CDialogEx)

CApplications::CApplications(CWnd* pParent /*=NULL*/)
	: CDialogEx(CApplications::IDD, pParent)
{

}

CApplications::~CApplications()
{
}

void CApplications::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_Application_LIST, m_Application);
	DDX_Control(pDX, IDC_btnEndTask, m_EndTask);
	DDX_Control(pDX, IDC_btnSwitchTo, m_SwitchTo);
	DDX_Control(pDX, IDC_btnNewTask, m_NewTask);
}


BEGIN_MESSAGE_MAP(CApplications, CDialogEx)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CApplications message handlers


BOOL CApplications::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	m_Application.InsertColumn(0, _T("Task"), LVCFMT_LEFT, 260, -1);
	m_Application.InsertColumn(1, _T("Status"), LVCFMT_LEFT, 60, -1);
	
	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CApplications::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);

	m_Application.MoveWindow(15, 15, cx - 28, cy - 60);

	CRect rectBtn;
	m_EndTask.GetClientRect(&rectBtn);
	cx = cx - 15 - rectBtn.Width();
	m_NewTask.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());

	cx = cx - 5 - rectBtn.Width();
	m_SwitchTo.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());

	cx = cx - 5 - rectBtn.Width();
	m_EndTask.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());
}

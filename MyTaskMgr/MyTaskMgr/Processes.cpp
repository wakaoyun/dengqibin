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
END_MESSAGE_MAP()


// CProcesses message handlers


BOOL CProcesses::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	m_Process.InsertColumn(0, _T("Image Name"), LVCFMT_LEFT, 100, -1);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CProcesses::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);
	m_Process.MoveWindow(15, 15, cx - 28, cy - 60);

	CRect rectBtn;
	m_EndProcess.GetClientRect(&rectBtn);
	cx = cx - 15 - rectBtn.Width();
	m_EndProcess.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());

	m_ShowAll.GetClientRect(&rectBtn);
	m_ShowAll.MoveWindow(15, cy - 36, rectBtn.Width(), rectBtn.Height());
}

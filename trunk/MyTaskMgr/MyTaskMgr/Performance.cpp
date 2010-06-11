// Performance.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "Performance.h"
#include "afxdialogex.h"


// CPerformance dialog

IMPLEMENT_DYNAMIC(CPerformance, CDialogEx)

CPerformance::CPerformance(CWnd* pParent /*=NULL*/)
	: CDialogEx(CPerformance::IDD, pParent)
{

}

CPerformance::~CPerformance()
{
}

void CPerformance::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_CPUUsage, m_CPUUsageStatic);
	DDX_Control(pDX, IDC_STATIC_CPUUsageHistory, m_CPUUsageHistoryStatic);
	DDX_Control(pDX, IDC_STATIC_Memory, m_MemoryStatic);
	DDX_Control(pDX, IDC_STATIC_MemoryHistory, m_MemoryHistoryStatic);
	DDX_Control(pDX, IDC_STATIC_PhysicalMemory, m_PhysicalMemoryStatic);
	DDX_Control(pDX, IDC_STATIC_KernelMemory, m_KernelMemoryStatic);
	DDX_Control(pDX, IDC_STATIC_System, m_SystemStatic);
	DDX_Control(pDX, IDC_btnRCMonitor, m_btnRCMonitor);
}


BEGIN_MESSAGE_MAP(CPerformance, CDialogEx)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CPerformance message handlers


BOOL CPerformance::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CPerformance::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);
	
	m_PhysicalMemoryStatic.MoveWindow(15, cy - 145, MINWIN_X / 2 - 38, 80);
	m_KernelMemoryStatic.MoveWindow(15, cy - 60, MINWIN_X / 2 - 38, 50);

	m_SystemStatic.MoveWindow(MINWIN_X / 2 - 12, cy -145, 172, 103);
	m_btnRCMonitor.MoveWindow(MINWIN_X / 2 - 12, cy - 37, 172, 23);

	cy = cy / 2 - 82;
	m_CPUUsageStatic.MoveWindow(15, 10, 90, cy);
	m_CPUUsageHistoryStatic.MoveWindow(120, 10, cx - 135, cy);
	m_MemoryStatic.MoveWindow(15, cy + 15, 90, cy);
	m_MemoryHistoryStatic.MoveWindow(120, cy + 15, cx - 135, cy);
}

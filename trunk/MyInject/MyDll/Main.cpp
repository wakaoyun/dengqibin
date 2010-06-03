// Main.cpp : implementation file
//

#include "stdafx.h"
#include "MyDll.h"
#include "Main.h"
#include "Personal.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMain dialog


CMain::CMain(CWnd* pParent /*=NULL*/)
	: CDialog(CMain::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMain)
		// NOTE: the ClassWizard will add member initialization here	
	//}}AFX_DATA_INIT
}


void CMain::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMain)
	DDX_Control(pDX, IDC_TAB_Info, m_myTab);
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CMain, CDialog)
	//{{AFX_MSG_MAP(CMain)
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB_Info, OnSelchangeTABInfo)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMain message handlers

BOOL CMain::OnInitDialog()
{
	CDialog::OnInitDialog();
	m_myTab.InsertItem(0,_T("基本信息"));
	m_myTab.InsertItem(1,_T("技能信息"));
	m_myTab.InsertItem(2,_T("物品信息"));
	m_PagePerson.Create(IDD_DIALOG_Personal,GetDlgItem(IDC_TAB_Info));
	m_PageSkill.Create(IDD_DIALOG_Skill,GetDlgItem(IDC_TAB_Info));
	m_PageGoods.Create(IDD_DIALOG_Goods,GetDlgItem(IDC_TAB_Info));
	CRect rec;
	m_myTab.GetClientRect(&rec);
	rec.top += 25;  

	m_PagePerson.MoveWindow(&rec);
	m_PagePerson.ShowWindow(SW_SHOW);

	m_PageSkill.MoveWindow(&rec);
	m_PageSkill.ShowWindow(SW_HIDE);

	m_PageGoods.MoveWindow(&rec);
	m_PageGoods.ShowWindow(SW_HIDE);

	m_myTab.SetCurSel(0);
	return TRUE;
}

void CMain::OnSelchangeTABInfo(NMHDR* pNMHDR, LRESULT* pResult) 
{
	int curSel = m_myTab.GetCurSel();
	switch(curSel)
	{
	case 0:
		m_PagePerson.ShowWindow(SW_SHOW);
		m_PageSkill.ShowWindow(SW_HIDE);
		m_PageGoods.ShowWindow(SW_HIDE);
		break;
	case 1:
		m_PageSkill.ShowWindow(SW_SHOW);
		m_PageGoods.ShowWindow(SW_HIDE);
		m_PagePerson.ShowWindow(SW_HIDE);
		break;
	case 2:
		m_PageGoods.ShowWindow(SW_SHOW);
		m_PagePerson.ShowWindow(SW_HIDE);
		m_PageSkill.ShowWindow(SW_HIDE);
	default:
		break;
	}
	*pResult = 0;
}

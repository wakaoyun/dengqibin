// ApplicationPP.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "ApplicationPP.h"
#include "afxdialogex.h"


// CApplicationPP dialog

IMPLEMENT_DYNAMIC(CApplicationPP, CPropertyPage)

CApplicationPP::CApplicationPP()
	: CPropertyPage(CApplicationPP::IDD)
{
	
}

CApplicationPP::~CApplicationPP()
{
}

void CApplicationPP::DoDataExchange(CDataExchange* pDX)
{
	CPropertyPage::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_Application_LIST, m_Application_List);
}


BEGIN_MESSAGE_MAP(CApplicationPP, CPropertyPage)
	ON_BN_CLICKED(IDC_btnEndTask, &CApplicationPP::OnBnClickedbtnendtask)
END_MESSAGE_MAP()


// CApplicationPP message handlers


BOOL CApplicationPP::OnInitDialog()
{
	CPropertyPage::OnInitDialog();

	m_Application_List.InsertColumn(0,_T("Task"),LVCFMT_LEFT,250,-1);
	m_Application_List.InsertColumn(1,_T("Status"),LVCFMT_LEFT,50,-1);

	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CApplicationPP::OnBnClickedbtnendtask()
{
	MessageBox(_T("test"));
}

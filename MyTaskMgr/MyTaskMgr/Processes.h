#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CProcesses dialog

class CProcesses : public CDialogEx
{
	DECLARE_DYNAMIC(CProcesses)

public:
	CProcesses(CWnd* pParent = NULL);   // standard constructor
	virtual ~CProcesses();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Processes };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CListCtrl m_Process;
	virtual BOOL OnInitDialog();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	CButton m_EndProcess;
	CButton m_ShowAll;
};

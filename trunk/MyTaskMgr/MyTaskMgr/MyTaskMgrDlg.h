
// MyTaskMgrDlg.h : header file
//

#pragma once
#include "afxcmn.h"
#include "Applications.h"
#include "Processes.h"


// CMyTaskMgrDlg dialog
class CMyTaskMgrDlg : public CDialogEx
{
// Construction
public:
	CMyTaskMgrDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_MYTASKMGR_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnGetMinMaxInfo(MINMAXINFO* lpMMI);
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	CTabCtrl m_MyTab;
	CApplications m_Application;
	CProcesses m_Process;
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnFileExittaskmanager();
	afx_msg void OnHelpAbouttaskmanager();
	afx_msg void OnSelchangeTab(NMHDR *pNMHDR, LRESULT *pResult);
	void HideAll(void);
};


// GDITestDlg.h : header file
//

#pragma once
#include "afxwin.h"
#include "mystatic.h"


// CGDITestDlg dialog
class CGDITestDlg : public CDialogEx
{
// Construction
public:
	CGDITestDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_GDITEST_DIALOG };

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
	CStatic m_a;
	CMyStatic m_MyStatic;
};

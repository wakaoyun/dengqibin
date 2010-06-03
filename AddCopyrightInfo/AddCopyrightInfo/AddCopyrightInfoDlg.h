
// AddCopyrightInfoDlg.h : header file
//

#pragma once
#include "afxwin.h"


// CAddCopyrightInfoDlg dialog
class CAddCopyrightInfoDlg : public CDialogEx
{
// Construction
public:
	CAddCopyrightInfoDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_ADDCOPYRIGHTINFO_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	BOOL MyFindFile(TCHAR* path, char* copyright);
	void WriteCopyright(TCHAR* filePath,TCHAR* sourceFile, char* copyright);
	CString m_Copyright;
	afx_msg void OnEnChangeCopyrightInfo();
	CString m_Path;
	afx_msg void OnEnChangePath();
	afx_msg void OnBnClickedSubmit();	
};

// MyInjectDlg.h : header file
//

#if !defined(AFX_MYINJECTDLG_H__D3278078_C365_4216_94BA_1D973936E7E3__INCLUDED_)
#define AFX_MYINJECTDLG_H__D3278078_C365_4216_94BA_1D973936E7E3__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CMyInjectDlg dialog

class CMyInjectDlg : public CDialog
{
// Construction
public:
	CMyInjectDlg(CWnd* pParent = NULL);	// standard constructor
	DWORD GetProcessID(char* lpProcessName);
	BOOL UpLevel(void);
	HWND GetWindowHandleByPID(DWORD dwProcessID);

// Dialog Data
	//{{AFX_DATA(CMyInjectDlg)
	enum { IDD = IDD_MYINJECT_DIALOG };
	CString	m_ProcessName;
	DWORD processID;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMyInjectDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CMyInjectDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInject();
	afx_msg void OnChangeEDITProcess();
	afx_msg void OnBUTTONConcel();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MYINJECTDLG_H__D3278078_C365_4216_94BA_1D973936E7E3__INCLUDED_)

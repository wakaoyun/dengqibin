#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CApplications dialog

class CApplications : public CDialogEx
{
	DECLARE_DYNAMIC(CApplications)

public:
	CApplications(CWnd* pParent = NULL);   // standard constructor
	virtual ~CApplications();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Applications };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CListCtrl m_Application;
	CButton m_EndTask;
	CButton m_SwitchTo;
	CButton m_NewTask;
	afx_msg void OnSize(UINT nType, int cx, int cy);
};

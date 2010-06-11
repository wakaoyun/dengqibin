#pragma once
#include "afxcmn.h"


// CApplicationPP dialog

class CApplicationPP : public CPropertyPage
{
	DECLARE_DYNAMIC(CApplicationPP)

public:
	CApplicationPP();
	virtual ~CApplicationPP();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Application };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CListCtrl m_Application_List;
	afx_msg void OnBnClickedbtnendtask();
};

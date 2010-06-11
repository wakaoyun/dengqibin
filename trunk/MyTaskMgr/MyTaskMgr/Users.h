#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CUsers dialog

class CUsers : public CDialogEx
{
	DECLARE_DYNAMIC(CUsers)

public:
	CUsers(CWnd* pParent = NULL);   // standard constructor
	virtual ~CUsers();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Users };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	CListCtrl m_UsersList;
	CButton m_btnDisconnect;
	CButton m_btnLogoff;
	CButton m_btnSendMsg;
};

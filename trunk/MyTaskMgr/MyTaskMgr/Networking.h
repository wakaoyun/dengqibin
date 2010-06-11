#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CNetworking dialog

class CNetworking : public CDialogEx
{
	DECLARE_DYNAMIC(CNetworking)

public:
	CNetworking(CWnd* pParent = NULL);   // standard constructor
	virtual ~CNetworking();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Networking };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	CListCtrl m_NetworkingList;
	CStatic m_LoacalStatic;
};

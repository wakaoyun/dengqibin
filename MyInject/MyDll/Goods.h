#pragma once
#include "afxcmn.h"


// CGoods dialog

class CGoods : public CDialog
{
	DECLARE_DYNAMIC(CGoods)

public:
	CGoods(CWnd* pParent = NULL);   // standard constructor
	virtual ~CGoods();
	BOOL OnInitDialog();

// Dialog Data
	enum { IDD = IDD_DIALOG_Goods };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/ADV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButtonFresh();
	CListCtrl m_List_BagGoods;
	// 获取包囊物品
	void GetBagGoods(void);
	CString m_1;
	CString m_2;
};

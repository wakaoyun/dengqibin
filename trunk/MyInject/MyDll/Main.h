#if !defined(AFX_MAIN_H__8F79C16B_4E70_4B5D_ADCB_A9F93D0F30DD__INCLUDED_)
#define AFX_MAIN_H__8F79C16B_4E70_4B5D_ADCB_A9F93D0F30DD__INCLUDED_

#include "Personal.h"	// Added by ClassView
#include "Skill.h"		// Added by ClassView
#include "Goods.h"		// Added by ClassView
#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// Main.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CMain dialog

class CMain : public CDialog
{
// Construction
public:
	CSkill m_PageSkill;
	CPersonal m_PagePerson;
	CGoods m_PageGoods;
	BOOL OnInitDialog();
	CMain(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CMain)
	enum { IDD = IDD_DIALOG_Property };
	CTabCtrl	m_myTab;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMain)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CMain)
	afx_msg void OnSelchangeTABInfo(NMHDR* pNMHDR, LRESULT* pResult);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MAIN_H__8F79C16B_4E70_4B5D_ADCB_A9F93D0F30DD__INCLUDED_)

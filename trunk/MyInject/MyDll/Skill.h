#include "afxwin.h"
#if !defined(AFX_SKILL_H__FEA1FD63_0772_4C37_A163_9BA56B1C7901__INCLUDED_)
#define AFX_SKILL_H__FEA1FD63_0772_4C37_A163_9BA56B1C7901__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// Skill.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CSkill dialog

class CSkill : public CDialog
{
// Construction
public:
	/*VOID GetMonsterList();*/
	VOID GetPlayerSkillList();
	BOOL OnInitDialog();
	CSkill(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CSkill)
	enum { IDD = IDD_DIALOG_Skill };
	
	CListBox	m_List_Skill;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSkill)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CSkill)
	afx_msg void OnBUTTONUseSkill();
	afx_msg void OnSelchangeLISTSkill();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedButton1();
	/*void SelectMonster(DWORD monsterID);*/
	afx_msg void OnBnClickedButtonRefresh();
	afx_msg void OnLvnColumnclickListMonster(NMHDR *pNMHDR, LRESULT *pResult);
	// 按与人的距离的降序排
	/* 地面物品列表 */
	void GetItemList(void);
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SKILL_H__FEA1FD63_0772_4C37_A163_9BA56B1C7901__INCLUDED_)

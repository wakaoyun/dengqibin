#if !defined(AFX_PERSONAL_H__2E9791BE_51CE_4B52_989D_8FFBB164E33B__INCLUDED_)
#define AFX_PERSONAL_H__2E9791BE_51CE_4B52_989D_8FFBB164E33B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// Personal.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CPersonal dialog

class CPersonal : public CDialog
{
// Construction
public:
	VOID SelectMonster(DWORD monsterID);
	VOID MoveCall(CString x,CString y);
	CPersonal(CWnd* pParent = NULL);   // standard constructor
	BOOL OnInitDialog();

// Dialog Data
	//{{AFX_DATA(CPersonal)
	enum { IDD = IDD_DIALOG_Personal };
	CString	m_MoveX;
	CString	m_MoveY;
	CString	m_ID;
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPersonal)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CPersonal)
	afx_msg void OnBUTTONPerson();
	afx_msg void OnChangeEdit();
	afx_msg void OnBUTTONUse();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PERSONAL_H__2E9791BE_51CE_4B52_989D_8FFBB164E33B__INCLUDED_)

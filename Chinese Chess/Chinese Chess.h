// Chinese Chess.h : main header file for the CHINESE CHESS application
//

#if !defined(AFX_CHINESECHESS_H__AE24D52B_4421_46DC_BF84_AFDC738DE27A__INCLUDED_)
#define AFX_CHINESECHESS_H__AE24D52B_4421_46DC_BF84_AFDC738DE27A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CChineseChessApp:
// See Chinese Chess.cpp for the implementation of this class
//

class CChineseChessApp : public CWinApp
{
public:
	CChineseChessApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CChineseChessApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CChineseChessApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CHINESECHESS_H__AE24D52B_4421_46DC_BF84_AFDC738DE27A__INCLUDED_)

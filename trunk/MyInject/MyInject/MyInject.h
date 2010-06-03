// MyInject.h : main header file for the MYINJECT application
//

#if !defined(AFX_MYINJECT_H__E96500C3_6D79_4718_8BBC_88CC20E91957__INCLUDED_)
#define AFX_MYINJECT_H__E96500C3_6D79_4718_8BBC_88CC20E91957__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMyInjectApp:
// See MyInject.cpp for the implementation of this class
//

class CMyInjectApp : public CWinApp
{
public:
	CMyInjectApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMyInjectApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CMyInjectApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MYINJECT_H__E96500C3_6D79_4718_8BBC_88CC20E91957__INCLUDED_)

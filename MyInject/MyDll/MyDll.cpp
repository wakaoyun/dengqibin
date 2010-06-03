// MyDll.cpp : Defines the initialization routines for the DLL.
//

#include "stdafx.h"
#include "MyDll.h"
#include "Main.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif
#pragma data_seg("mydata") 
	HHOOK g_hHook;
#pragma data_seg()
//
//	Note!
//
//		If this DLL is dynamically linked against the MFC
//		DLLs, any functions exported from this DLL which
//		call into MFC must have the AFX_MANAGE_STATE macro
//		added at the very beginning of the function.
//
//		For example:
//
//		extern "C" BOOL PASCAL EXPORT ExportedFunction()
//		{
//			AFX_MANAGE_STATE(AfxGetStaticModuleState());
//			// normal function body here
//		}
//
//		It is very important that this macro appear in each
//		function, prior to any calls into MFC.  This means that
//		it must appear as the first statement within the 
//		function, even before any object variable declarations
//		as their constructors may generate calls into the MFC
//		DLL.
//
//		Please see MFC Technical Notes 33 and 58 for additional
//		details.
//

/////////////////////////////////////////////////////////////////////////////
// CMyDllApp

BEGIN_MESSAGE_MAP(CMyDllApp, CWinApp)
	//{{AFX_MSG_MAP(CMyDllApp)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMyDllApp construction

CMyDllApp::CMyDllApp()
{
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CMyDllApp object

CMyDllApp theApp;
HINSTANCE g_h;
CMain *g_lphMain;
HWND g_MyhWnd;		
LRESULT CALLBACK KeyboardProc(
  int code,       // hook code
  WPARAM wParam,  // virtual-key code
  LPARAM lParam   // keystroke-message information
);
BOOL CMyDllApp::InitInstance() 
{
	g_h=AfxGetInstanceHandle();
	return CWinApp::InitInstance();
}

BOOL CMyDllApp::SetHook(HWND hWnd)
{
	DWORD threadID;
	DWORD processID;
	g_MyhWnd=hWnd;
	processID=GetCurrentProcessId();
	threadID=::GetWindowThreadProcessId(hWnd,&processID);
	g_hHook=::SetWindowsHookEx(WH_KEYBOARD,KeyboardProc,g_h,threadID);
	if(g_hHook!=NULL)
	{
		return true;
	}
	return false;
}
void CMyDllApp::UnHook()
{
	UnhookWindowsHookEx(g_hHook);
	g_hHook=NULL;
}

void ShowMyWind()
{
	if(NULL==g_lphMain)
	{
		AFX_MANAGE_STATE(AfxGetStaticModuleState());
        g_lphMain = new CMain(0);
        g_lphMain->Create(IDD_DIALOG_Property);
        g_lphMain->SetParent(0);
    }
    g_lphMain->ShowWindow(g_lphMain->IsWindowVisible()?SW_HIDE:SW_SHOW);
}

LRESULT CALLBACK KeyboardProc(
  int code,       // hook code
  WPARAM wParam,  // virtual-key code
  LPARAM lParam   // keystroke-message information
)
{	
	if(wParam == VK_MULTIPLY && code>=0 && code == HC_ACTION && (lParam & (1<<31)))
	{
		ShowMyWind();
	}	
	return CallNextHookEx(g_hHook, code, wParam, lParam);
}

int CMyDllApp::ExitInstance() 
{
	delete g_lphMain;
	return CWinApp::ExitInstance();
}

VOID CMyDllApp::MyFreeLibrary()
{
	FreeLibrary(GetModuleHandle("MyDll.dll"));
}

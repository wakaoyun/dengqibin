#pragma once
#include "afxcmn.h"
#include "afxwin.h"


// CApplications dialog

class CApplications : public CDialogEx
{
	DECLARE_DYNAMIC(CApplications)

public:
	CApplications(CWnd* pParent = NULL);   // standard constructor
	virtual ~CApplications();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Applications };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	CListCtrl m_Application;
	CButton m_EndTask;
	CButton m_SwitchTo;
	CButton m_NewTask;
	CImageList m_ImageList;
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnGetdispinfoApplicationList(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnItemclickApplicationList(NMHDR *pNMHDR, LRESULT *pResult);
};

typedef struct
{
	HWND    hWnd;
	WCHAR   szTitle[260];
	HICON   hIcon;
	BOOL    bHung;
} APPLICATION_PAGE_LIST_ITEM, *LPAPPLICATION_PAGE_LIST_ITEM;

static HANDLE   hApplicationPageEvent = NULL;   /* When this event becomes signaled then we refresh the app list */
DWORD WINAPI    ApplicationPageRefreshThread(void *lpParameter);
BOOL CALLBACK   EnumWindowsProc(HWND hWnd, LPARAM lParam);
void            AddOrUpdateHwnd(HWND hWnd, WCHAR *szTitle, HICON hIcon, BOOL bHung);
int CALLBACK    ApplicationPageCompareFunc(LPARAM lParam1, LPARAM lParam2, LPARAM lParamSort);
static BOOL     bSortAscending = TRUE;
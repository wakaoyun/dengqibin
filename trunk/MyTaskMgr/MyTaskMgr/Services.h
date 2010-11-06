#pragma once
#include "afxcmn.h"
#include "afxwin.h"
#include <WinSvc.h>
#define CMP(x1, x2) (x1 < x2 ? -1 : (x1 > x2 ? 1 : 0))

// CServices dialog

class CServices : public CDialogEx
{
public:
	typedef struct _tagServiceInfo
	{
		ENUM_SERVICE_STATUS_PROCESS ServiceStatus;
		WCHAR						OrderGroup[128];
	}SERVICEINFO, *PSERVICEINFO;
	DECLARE_DYNAMIC(CServices)

public:
	CServices(CWnd* pParent = NULL);   // standard constructor
	virtual ~CServices();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Services };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	CListCtrl m_Services;
	CButton m_btnServices;
private:
	static DWORD WINAPI ServicePageRefreshThread(void *lpParameter);
	static void GetServiceInfo();
	static void RefreshSevice(SERVICEINFO* pService, DWORD count);
public:
	afx_msg void OnLvnGetdispinfoServicesList(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnHdnItemclickServicesList(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
};
static int serviceColumn = 0;
static BOOL serviceIsASC = TRUE;
static HANDLE   hServicePageEvent = NULL;
static HWND hServicePageListCtrl;
int CALLBACK ServiceCompareFunc(LPARAM lParam1, LPARAM lParam2, LPARAM lParamSort);

#pragma once
#include "afxcmn.h"
#include "afxwin.h"
#include <WinSvc.h>


// CServices dialog

class CServices : public CDialogEx
{
	typedef struct _tagServiceInfo
	{
		HANDLE				 ProcessId;
		ENUM_SERVICE_STATUSW ServiceStatus;
		LPTSTR				 lpLoadOrderGroup;
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
};
static HANDLE   hServicePageEvent = NULL;
static HWND hServicePageListCtrl;

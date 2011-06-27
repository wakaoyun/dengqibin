#pragma once
#include "afxcmn.h"
#include "afxwin.h"

#define STATUS_SUCCESS ((NTSTATUS)0x00000000L)
// CUsers dialog
 
class CUsers : public CDialogEx
{
public:
	typedef struct  
	{
		WCHAR UserName[32];
		int UserID;
		WCHAR Status[32];
		WCHAR ClientName[64];
		WCHAR Session[32];
	}USERINFO,*PUSERINFO;

	DECLARE_DYNAMIC(CUsers)

public:
	CUsers(CWnd* pParent = NULL);   // standard constructor
	virtual ~CUsers();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Users };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	CListCtrl m_UsersList;
	CButton m_btnDisconnect;
	CButton m_btnLogoff;
	CButton m_btnSendMsg;

	typedef NTSTATUS(NTAPI *GetLogonSessionData)(PLUID, PSECURITY_LOGON_SESSION_DATA *);
	typedef NTSTATUS(NTAPI *EnumerateLogonSessions)(PULONG, PLUID *);
	typedef NTSTATUS(NTAPI *FreeReturnBuffer)(PVOID);

private:
	static DWORD WINAPI UsersPageRefreshThread(void *lpParameter);
	static void GetUsersInfo(EnumerateLogonSessions,GetLogonSessionData,FreeReturnBuffer);
	
};

static HANDLE   hUsersPageEvent = NULL;
static HWND hUsersPageListCtrl;

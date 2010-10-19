#pragma once
#include "afxcmn.h"
#include "afxwin.h"
#include "PerformanceHelper.h"
#include "ColumnMgr.h"


// CProcesses dialog

class CProcesses : public CDialogEx
{
	DECLARE_DYNAMIC(CProcesses)

public:
	CProcesses(CWnd* pParent = NULL);   // standard constructor
	virtual ~CProcesses();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Processes };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	CListCtrl m_Process;
	virtual BOOL OnInitDialog();
	CButton m_EndProcess;
	CButton m_ShowAll;
private:	
	static DWORD WINAPI ProcPageRefreshThread(void *lpParameter);
	static void RefreshProc(PERFDATA *P);
	static BOOL ProcessRunning(DWORD ProcessId);
	void GetHMSFromLargeInt(LARGE_INTEGER time,DWORD *dwHours, DWORD *dwMinutes, DWORD *dwSeconds);
public:
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnLvnGetdispinfoProcessList(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnHdnItemclickProcessList(NMHDR *pNMHDR, LRESULT *pResult);
private:
	CColumnMgr columnMgr;
	const CColumnMgr::ColumnEntry *columns;
};
static HANDLE   hProcPageEvent = NULL;
static HWND hProcPageListCtrl;
static CPerformanceHelper perfHelper;
#pragma once
#include "afxcmn.h"
#include "afxwin.h"
#include "PerformanceHelper.h"
#include "ColumnMgr.h"
#include "TaskSetting.h"


#define CMP(x1, x2) (x1 < x2 ? -1 : (x1 > x2 ? 1 : 0))
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
	static int CALLBACK ProcessPageCompareFunc(LPARAM lParam1, LPARAM lParam2, LPARAM lParamSort);
	void GetHMSFromLargeInt(LARGE_INTEGER time,DWORD *dwHours, DWORD *dwMinutes, DWORD *dwSeconds);
	void SeparateNumber(LPWSTR strNumber, int nMaxCount);
public:
	afx_msg void OnSize(UINT nType, int cx, int cy);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnLvnGetdispinfoProcessList(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnHdnItemclickProcessList(NMHDR *pNMHDR, LRESULT *pResult);
private:
	CColumnMgr columnMgr;
	const CColumnMgr::ColumnEntry *columns;
	static int sortColum;
	static BOOL isASC;
public:
	afx_msg void OnNMRClickProcessList(NMHDR *pNMHDR, LRESULT *pResult);
};
static HANDLE   hProcPageEvent = NULL;
static HWND hProcPageListCtrl;
static CPerformanceHelper perfHelper;
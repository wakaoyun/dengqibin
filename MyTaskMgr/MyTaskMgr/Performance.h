#pragma once
#include "afxwin.h"


// CPerformance dialog

class CPerformance : public CDialogEx
{
	DECLARE_DYNAMIC(CPerformance)

public:
	CPerformance(CWnd* pParent = NULL);   // standard constructor
	virtual ~CPerformance();

// Dialog Data
	enum { IDD = IDD_PROPPAGE_Performance };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	virtual BOOL OnInitDialog();
	afx_msg void OnSize(UINT nType, int cx, int cy);
	CStatic m_CPUUsageStatic;
	CStatic m_CPUUsageHistoryStatic;
	CStatic m_MemoryStatic;
	CStatic m_MemoryHistoryStatic;
	CStatic m_PhysicalMemoryStatic;
	CStatic m_KernelMemoryStatic;
	CStatic m_SystemStatic;
	CButton m_btnRCMonitor;
};

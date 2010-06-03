// Chinese ChessDlg.h : header file
//

#if !defined(AFX_CHINESECHESSDLG_H__1D410165_7469_462E_A35F_5ADC2208219B__INCLUDED_)
#define AFX_CHINESECHESSDLG_H__1D410165_7469_462E_A35F_5ADC2208219B__INCLUDED_

#include "Chess.h"	// Added by ClassView
#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CChineseChessDlg dialog

class CChineseChessDlg : public CDialog
{
// Construction
public:
	static int SearchEngine(CChess::CHESS chess[10][9], int alph, int beta, int deep);
	static DWORD WINAPI ThreadProc(LPVOID lpParameter);
	void DrawChessBord(HDC dc);
	CChineseChessDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CChineseChessDlg)
	enum { IDD = IDD_CHINESECHESS_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CChineseChessDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	virtual void DrawChessImage(int x, int y, HDC dc);
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CChineseChessDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnExit();
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
private:
	bool IsComputer;	
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CHINESECHESSDLG_H__1D410165_7469_462E_A35F_5ADC2208219B__INCLUDED_)

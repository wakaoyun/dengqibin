#pragma once
#include "afxwin.h"
class CMyStatic :
	public CButton
{
public:
	CMyStatic(void);
	~CMyStatic(void);
	virtual void DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct);
	DECLARE_MESSAGE_MAP()
	afx_msg void OnPaint();
	CStatic m_static;
	int GetXCoord();
	void MoveLeft();
	int GetLength();
	void SetLength(int iLength);
	BOOL FillDataSource(BOOL bUpdateData = TRUE);
protected:
	virtual void PreSubclassWindow();
	afx_msg void OnTimer(UINT nIDEvent);
private:
	CToolTipCtrl m_tooltip;
	CDC *pDC;
	CDC memDC;
	int m_now;			// 当前位置，画横竖线时用于定位X坐标的量，产生向左移的效果
	int *m_pData;
	int m_DataStartPos;	// 读取数据源的起始位置
	int m_DataOffPos;	// 相对于起始位置的偏移量
	int m_DataLength;	// 数据长度

	int m_test;
	void DrawWave(CDC *pDC, CRect rect);
};


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
	int m_now;			// ��ǰλ�ã���������ʱ���ڶ�λX������������������Ƶ�Ч��
	int *m_pData;
	int m_DataStartPos;	// ��ȡ����Դ����ʼλ��
	int m_DataOffPos;	// �������ʼλ�õ�ƫ����
	int m_DataLength;	// ���ݳ���

	int m_test;
	void DrawWave(CDC *pDC, CRect rect);
};


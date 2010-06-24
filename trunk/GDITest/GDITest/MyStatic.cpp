#include "StdAfx.h"
#include "MyStatic.h"


CMyStatic::CMyStatic(void)
{
	m_DataOffPos = 10;
	m_test = 370;
	m_now = 20;
}


CMyStatic::~CMyStatic(void)
{
	delete m_pData;
}

void CMyStatic::DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct)
{
	CButton::DrawItem(lpDrawItemStruct);
	CPaintDC dc(this);
	CRgn rgn;
	rgn.CreateRectRgn(10,10,20,20);
	dc.SelectObject(&rgn);
}
BEGIN_MESSAGE_MAP(CMyStatic, CStatic)
	//ON_WM_PAINT()
	ON_WM_TIMER()
END_MESSAGE_MAP()


void CMyStatic::OnPaint()
{
	CPaintDC dc(this); 
	//dc.DrawText(_T("gfdgfdgfd"),5,CRect(0,0,50,50),88);
}

void CMyStatic::PreSubclassWindow() 
{
	// TODO: Add your specialized code here and/or call the base class

	CButton::PreSubclassWindow();

	//ModifyStyle(0, BS_OWNERDRAW);
	SetTimer(0, 100, NULL);

	CRect rect;
	GetClientRect(&rect);
	SetLength(rect.Width()-20);

	// 初始化数组长度
	m_pData = new int[GetLength()];
	FillDataSource(FALSE);

	srand((unsigned int)time(NULL));
	FillDataSource();

}

void CMyStatic::OnTimer(UINT nIDEvent) 
{
	// TODO: Add your message handler code here and/or call default

	// 获得GDI绘图环境
	CRect rect;
	GetClientRect(&rect);	
	pDC = this->GetDC();
	this->Invalidate();

	// 内在绘图
	CBitmap memBitmap;
	CBitmap *pOldBmp = NULL;
	memDC.CreateCompatibleDC(pDC);
	memBitmap.CreateCompatibleBitmap(pDC, rect.right, rect.bottom);
	pOldBmp = memDC.SelectObject(&memBitmap);
	memDC.BitBlt(rect.left, rect.top, rect.right, rect.bottom, pDC, 0, 0, SRCCOPY);

	rect.top+=10;
	rect.bottom-=10;
	rect.right-=10;
	rect.left+=10;
	DrawWave(&memDC, rect);
	rect.top-=10;
	rect.bottom+=10;
	rect.right+=10;
	rect.left-=10;
	pDC->BitBlt(rect.left, rect.top, rect.right, rect.bottom, &memDC, 0, 0, SRCCOPY);

	memDC.SelectObject(pOldBmp);
	memDC.DeleteDC();
	memBitmap.DeleteObject();

	FillDataSource();

	CButton::OnTimer(nIDEvent);
}

void CMyStatic::DrawWave(CDC *pDC, CRect rect)
{	
	rect.top+=10;
	rect.bottom-=10;
	rect.right-=10;
	rect.left+=10;
	pDC->Rectangle(rect);	
	CBrush brush(BLACK_BRUSH);	
	pDC->FillRect(rect, &brush);

	// 创建浅绿色画笔
	CPen *pPenGreen = new CPen();
	pPenGreen->CreatePen(PS_SOLID, 1, RGB(0, 128, 64));
	
	CGdiObject *pOldPen = pDC->SelectObject(pPenGreen);
	
	// 画竖线
	for (int i = GetXCoord(); i < rect.right; i += 12)
	{
		pDC->MoveTo(i, 20);
		pDC->LineTo(i, rect.bottom);
	}
	// 画横线
	for (int i = 20; i < rect.bottom; i += 12)
	{
		pDC->MoveTo(20, i);
		pDC->LineTo(rect.right, i);
	}

	// 创建深绿色画笔，画波形图
	CPen *pPenDeepGreen = new CPen();
	pPenDeepGreen->CreatePen(PS_SOLID, 1, RGB(0, 255, 0));
	pDC->SelectObject(pPenDeepGreen);

	int j = m_DataStartPos;
	pDC->MoveTo(0, m_pData[j]);

	for (int i = 0; i < rect.right; i++)
	{
		if (m_pData[j] < 0)
			break;
		pDC->LineTo(i, m_pData[j]);
		j = ++j % GetLength();
	}

	pDC->SelectObject(pOldPen);

	delete pPenGreen;
	delete pPenDeepGreen;
}

BOOL CMyStatic::FillDataSource(BOOL bUpdateData)
{
	if (bUpdateData)
	{
		// 整个数组填满了数据起始点才能自增
		if (m_DataOffPos == GetLength())
		{
			m_pData[m_DataStartPos] = rand() % 370;
			m_DataStartPos = ++m_DataStartPos % GetLength();
			MoveLeft();			
		}
		else
		{
			m_pData[m_DataOffPos] = rand() % 370;
			m_DataOffPos++;
		}
	}
	else
	{
		// 数组初始化，将第一个元素填充数据，并设置起始位置
		for (int i = 1; i < GetLength(); i++)	m_pData[i] = -1;
		m_pData[0] = rand() % 370;
		m_DataStartPos = 10;
		m_DataOffPos = 11;
	}
	/*m_test = (0 == --m_test ? 370 : m_test);

	CString str;
	CRect rect;
	GetClientRect(&rect);
	str.Format("m_DataStartPos = %d, m_DataOffPos = %d, rect.Width() = %d, GetLength() = %d",
				m_DataStartPos, m_DataOffPos, rect.Width(), GetLength());
	m_tooltip.UpdateTipText(str, this);*/
	return TRUE;
}

void CMyStatic::SetLength(int iLength)
{
	m_DataLength = 300/*iLength*/; 
}

int CMyStatic::GetLength()
{
	return m_DataLength;
}

void CMyStatic::MoveLeft()
{
	m_now = (m_now == 20 ? 31 : --m_now);
}

int CMyStatic::GetXCoord()
{
	return m_now;
}

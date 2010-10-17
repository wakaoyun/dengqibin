// Chinese ChessDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Chinese Chess.h"
#include "Chinese ChessDlg.h"
#include <mmsystem.h>
#include <direct.h>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CChineseChessDlg dialog

CChess::CHESS chess[10][9]={
	{{2,false},{3,false},{6,false},{5,false},{1,false},{5,false},{6,false},{3,false},{2,false}},
	{{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false}},
	{{0,false},{4,false},{0,false},{0,false},{0,false},{0,false},{0,false},{4,false},{0,false}},
	{{7,false},{0,false},{7,false},{0,false},{7,false},{0,false},{7,false},{0,false},{7,false}},
	{{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false}},
	{{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false}},
	{{14,false},{0,false},{14,false},{0,false},{14,false},{0,false},{14,false},{0,false},{14,false}},
	{{0,false},{11,false},{0,false},{0,false},{0,false},{0,false},{0,false},{11,false},{0,false}},
	{{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false},{0,false}},
	{{9,false},{10,false},{13,false},{12,false},{8,false},{12,false},{13,false},{10,false},{9,false}}
};
CChess::CHESS TempChess[10][9];

CChess::MOVELIST BestMove;

CChess chessInstance;

CChineseChessDlg::CChineseChessDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CChineseChessDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CChineseChessDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	IsComputer = true;
	chessInstance = CChess();
}

void CChineseChessDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CChineseChessDlg)
		// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CChineseChessDlg, CDialog)
	//{{AFX_MSG_MAP(CChineseChessDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_COMMAND(IDR_Exit, OnExit)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CChineseChessDlg message handlers

BOOL CChineseChessDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CChineseChessDlg::OnPaint() 
{
	CPaintDC dc(this);
	DrawChessBord(dc);
	for(int i=0;i<10;i++)
	{
		for (int j=0;j<9;j++)
		{
			DrawChessImage(j,i,(HDC)dc);
		}
	}
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
		
	}
	else
	{
		//CPaintDC dc(GetDlgItem(IDC_STATIC_Chess));		
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CChineseChessDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CChineseChessDlg::DrawChessImage(int x, int y, HDC dc)
{
	HBITMAP hbmp;
	char name[3];
	char path[255];
	GetModuleFileName(NULL,path,255);
	(_tcsrchr(path,_T('\\')))[1] = 0;
	strcat(path,_T("\\Sourse\\"));
	if (chess[y][x].chessType != 0)
	{
		if (chess[y][x].isSelect)
		{
			strcat(path,_T("x"));
		}
		itoa(chess[y][x].chessType,name,10);
		strcat(path,name);
		strcat(path,_T(".bmp"));
		hbmp = (HBITMAP)LoadImage(AfxGetInstanceHandle(),path,
			IMAGE_BITMAP,50,50,LR_LOADFROMFILE);
		HBRUSH brush = CreatePatternBrush(hbmp); 
		HGDIOBJ hOld = SelectObject(dc,brush);
		RECT rec;
		rec.left = x * 50 ;
		rec.top = y * 50 ;
		rec.bottom = rec.top + 50;
		rec.right = rec.left + 50;
		FillRect(dc,&rec,brush);

		//»Ö¸´Ô­ÏÈ×´Ì¬
		SelectObject(dc, hOld);

		DeleteObject(brush);
		DeleteObject(hOld);
	}
}

void CChineseChessDlg::DrawChessBord(HDC dc)
{
	HBITMAP hbmp;
	char path[255];
	GetModuleFileName(NULL,path,255);
	(_tcsrchr(path,_T('\\')))[1] = 0;
	strcat(path,_T("\\Sourse\\chessbord.bmp"));
	hbmp = (HBITMAP)LoadImage(AfxGetInstanceHandle(),path,IMAGE_BITMAP,0,0,LR_LOADFROMFILE);
	HBRUSH brush = CreatePatternBrush(hbmp); 
	HGDIOBJ hOld = SelectObject(dc,brush);
	RECT rec;
	rec.left = 0;
	rec.top = 0;
	rec.bottom = 500;
	rec.right = 450;
	FillRect(dc,&rec,brush);

	SelectObject(dc, hOld);
	DeleteObject(hOld);
	DeleteObject(brush);
}

void CChineseChessDlg::OnExit() 
{
	exit(0);	
}

void CChineseChessDlg::OnLButtonDown(UINT nFlags, CPoint point) 
{
	CPoint k=CPoint(point.x/50,point.y/50);
	RECT rec;
	if(chessInstance.GetCurrentSide())
	{
		if(chessInstance.IsSelect(chess))
		{
			if(chess[k.y][k.x].chessType>0&&chess[k.y][k.x].chessType<8)
			{
				chess[chessInstance.GetSelectPoint().y][chessInstance.GetSelectPoint().x].isSelect=false;
				chess[k.y][k.x].isSelect=true; 			
				rec.left = k.x * 50;
				rec.top = k.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
				rec.left = chessInstance.GetSelectPoint().x * 50;
				rec.top = chessInstance.GetSelectPoint().y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
			}
		}
		else
		{
			if(chess[k.y][k.x].chessType>0&&chess[k.y][k.x].chessType<8)
			{
				chess[k.y][k.x].isSelect=true;
				rec.left = k.x * 50;
				rec.top = k.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
			}
		}
	}
	else
	{
		if(chessInstance.IsSelect(chess))
		{
			if(chess[k.y][k.x].chessType>7&&chess[k.y][k.x].chessType<15)
			{
				chess[chessInstance.GetSelectPoint().y][chessInstance.GetSelectPoint().x].isSelect=false;
				chess[k.y][k.x].isSelect=true; 			
				rec.left = k.x * 50;
				rec.top = k.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
				rec.left = chessInstance.GetSelectPoint().x * 50;
				rec.top = chessInstance.GetSelectPoint().y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
			}
		}
		else
		{
			if(chess[k.y][k.x].chessType>7&&chess[k.y][k.x].chessType<15)
			{
				chess[k.y][k.x].isSelect=true;
				rec.left = k.x * 50;
				rec.top = k.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
			}
		}
	}
	CDialog::OnLButtonDown(nFlags, point);
}

int CChineseChessDlg::SearchEngine(CChess::CHESS chess[10][9], int alph, int beta, int deep)
{
	int Count, i, score, chesstype;
	bool x;
	i = chessInstance.IsGameOver(chess, deep);
	if (i != 0)
		return i;
	if ((chessInstance.maxDeep - deep) % 2 == 0)
		x = !chessInstance.GetCurrentSide();
	else
		x = chessInstance.GetCurrentSide();
	if (deep <= 0)
		return chessInstance.Caculate(chess, x);
	Count = chessInstance.PosibleMove(chess, x, deep);
	for (i = 1; i < Count; i++)
	{
		chesstype = chessInstance.MakeMove(chessInstance.moveList[deep][i], chess);
		score = -SearchEngine(chess, -beta, -alph, deep - 1);
		chessInstance.UnMakeMove(chessInstance.moveList[deep][i], chesstype, chess);
		if (score > alph)
		{
			alph = score;
			if (deep == chessInstance.maxDeep)
			{
				BestMove.from = chessInstance.moveList[deep][i].from;
				BestMove.to = chessInstance.moveList[deep][i].to;
			}
		}
		if (alph >= beta)
			break;
	}
	return alph;
}

bool MyInvalidateRect(CONST RECT *lpRect,BOOL bErase)
{
	return InvalidateRect(NULL,lpRect,bErase);
}

void GameOver()
{
	PlaySound(_T("GAMEOVER.WAV"),NULL,SND_ASYNC);
	MessageBox(AfxGetMainWnd()->m_hWnd,_T("Game Over"),NULL,MB_OK);
}

DWORD WINAPI CChineseChessDlg::ThreadProc(LPVOID lpParameter)
{
	memcpy(TempChess,chess,sizeof(chess));	
	SearchEngine(TempChess, -20000, 20000, chessInstance.maxDeep);
	chess[BestMove.to.y][BestMove.to.x].chessType = chess[BestMove.from.y][BestMove.from.x].chessType;
	chess[BestMove.from.y][BestMove.from.x].chessType = 0;
	chessInstance.SetCurrentSide(!chessInstance.GetCurrentSide());   
	RECT rec;
	rec.left = BestMove.from.x * 50;
	rec.top = BestMove.from.y * 50;
	rec.bottom = rec.top + 50;
	rec.right = rec.left + 50;
	MyInvalidateRect(&rec,FALSE);
	rec.left = BestMove.to.x * 50;
	rec.top = BestMove.to.y * 50;
	rec.bottom = rec.top + 50;
	rec.right = rec.left + 50;
	MyInvalidateRect(&rec,FALSE);
	if (chessInstance.IsGameOver(chess, 0) != 0)
	{
		GameOver();
    }
	return 0;
}

void CChineseChessDlg::OnLButtonUp(UINT nFlags, CPoint point) 
{
	CPoint TargetP = CPoint(point.x/50,point.y/50);
	CPoint p = chessInstance.GetSelectPoint();
	RECT rec;
	if(!IsComputer)
	{
		if (chessInstance.GetCurrentSide() && chessInstance.IsSelect(chess))
		{
			if (chessInstance.CanMove(chess, p.x,p.y, TargetP.x, TargetP.y))
			{
				chess[TargetP.y][TargetP.x].chessType = chess[p.y][p.x].chessType;
				chess[p.y][p.x].chessType = 0;
				chessInstance.SetCurrentSide(!chessInstance.GetCurrentSide());
				rec.left = p.x * 50;
				rec.top = p.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
				rec.left = TargetP.x * 50;
				rec.top = TargetP.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
				
				if (chessInstance.IsGameOver(chess, 0) != 0)
				{
					GameOver();
				}
			}
		}
		else if (!chessInstance.GetCurrentSide() && chessInstance.IsSelect(chess))
		{
			
			if (chessInstance.CanMove(chess, p.x, p.y, TargetP.x, TargetP.y))
			{
				chess[TargetP.y][TargetP.x].chessType = chess[p.y][p.x].chessType;
				chess[p.y][p.x].chessType = 0;
				chessInstance.SetCurrentSide(!chessInstance.GetCurrentSide());
				rec.left = p.x * 50;
				rec.top = p.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
				rec.left = TargetP.x * 50;
				rec.top = TargetP.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
				
				if (chessInstance.IsGameOver(chess, 0) != 0)
				{
					GameOver();
				}
			}
		}
	}
	else
	{
		if(chessInstance.IsSelect(chess))
		{
			if (chessInstance.CanMove(chess, p.x,p.y, TargetP.x, TargetP.y))
			{
				chess[TargetP.y][TargetP.x].chessType = chess[p.y][p.x].chessType;
				chess[p.y][p.x].chessType = 0;
				chessInstance.SetCurrentSide(!chessInstance.GetCurrentSide());
				rec.left = p.x * 50;
				rec.top = p.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);
				rec.left = TargetP.x * 50;
				rec.top = TargetP.y * 50;
				rec.bottom = rec.top + 50;
				rec.right = rec.left + 50;
				InvalidateRect(&rec,FALSE);

				if (chessInstance.IsGameOver(chess, 0) != 0)
				{
					GameOver();
				}
				else
				{
					HANDLE hThread = CreateThread(NULL,0,ThreadProc,NULL,CREATE_SUSPENDED,NULL);
					SetThreadPriority(hThread,THREAD_PRIORITY_HIGHEST);
					ResumeThread(hThread);
				}
			}
		}
	}
	CDialog::OnLButtonUp(nFlags, point);
}

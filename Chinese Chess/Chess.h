#if !defined(AFX_CHESS_H__03FD8124_C67F_48B8_A7E2_09EAF7916E3F__INCLUDED_)
#define AFX_CHESS_H__03FD8124_C67F_48B8_A7E2_09EAF7916E3F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// Chess.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CChess window
#define BLACK 0
#define RED 1
class CChess 
{
// Construction
public:
	CChess();

// Attributes
public:
	typedef struct tagChess
	{
		int chessType;
		bool isSelect;		
	}CHESS,*PCHESS;
	typedef struct tagMovelist
	{
		CPoint from,to;
	}MOVELIST,*PMOVELIST;

// Operations
public:
// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CChess)
	//}}AFX_VIRTUAL

// Implementation
public:	
	void SetCurrentSide(bool flag);
	bool GetCurrentSide();
	CPoint GetSelectPoint();
	int Caculate(CHESS chess[10][9], bool side);
	inline void GetRelation(CHESS chess[10][9], int i ,int j);
	int PosibleMove(CHESS chess[10][9], bool side, int deep);
	inline CPoint GetKingPos(CHESS chess[10][9],bool isBlack=true);
	bool CanMove(CHESS chess[10][9], int FromX, int FromY, int ToX, int ToY);
	bool IsSelect(CHESS chess[10][9]);
	inline bool IsSameSide(int from, int to);
	inline int AddMove(CPoint from, CPoint to, int layer);
	inline bool IsBlack(int chessType);
	inline void AddPoint(CPoint p);
	int IsGameOver(CHESS chess[10][9], int deep);
	void UnMakeMove(MOVELIST move, int chessType, CHESS chess[10][9]);
	inline int GetPawnValue(CHESS c[10][9], int x, int y);
	int MakeMove(MOVELIST move, CHESS chess[10][9]);
	virtual ~CChess();

	// Generated message map functions
protected:
public:
	bool currentSide;
	int ActiveValuePos[10][9];
	int ChessValue[10][9];
	int Guard[10][9];
	int Attack[10][9];
	CPoint selectPoint;
	int moveCount;
	MOVELIST moveList[8][80];
	int relationCount;
	CPoint relation[20];
	bool isBlackFirst;
	unsigned short maxDeep;
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CHESS_H__03FD8124_C67F_48B8_A7E2_09EAF7916E3F__INCLUDED_)

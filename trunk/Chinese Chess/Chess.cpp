// Chess.cpp : implementation file
//

#include "stdafx.h"
#include "Chinese Chess.h"
#include "Chess.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CChess

int Additional[10][9]=
{
	{0,0,0,0,0,0,0,0,0},
	{90,90,110,120,120,120,110,90,90},
	{90,90,110,120,120,120,110,90,90},
	{70,90,110,110,110,110,110,90,70},
	{70,70,70,70,70,70,70,70,70},
	{0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0},
};
//������ֵ
int BaseValue[]={10000,500,350,350,250,250,350,10000,500,350,350,250,250,350};
//��Ծ��ֵ
int ActiveValue[]={0,6,12,6,1,1,15,0,6,12,6,1,1,15};

CChess::CChess()
{
	currentSide = true;
	maxDeep = 4;
	isBlackFirst = false;
}

CChess::~CChess()
{
}

//��ñ�������λ��ֵ
inline int CChess::GetPawnValue(tagChess c[10][9], int x, int y)
{
	if(y<=4)
	{
		return Additional[y][x];
	}
	else
	{
		return Additional[((y-4)<<1)-1][x];
	}
}

//�ƶ�����
int CChess::MakeMove(MOVELIST move, CHESS chess[10][9])
{
	int chessType = chess[move.to.y][move.to.x].chessType;
	chess[move.to.y][move.to.x].chessType = chess[move.from.y][move.from.x].chessType;
	chess[move.from.y][move.from.x].chessType = 0;
    return chessType;
}

//ȡ���ƶ�����
void CChess::UnMakeMove(MOVELIST move, int chessType, CHESS chess[10][9])
{
	chess[move.from.y][move.from.x].chessType = chess[move.to.y][move.to.x].chessType;
    chess[move.to.y][move.to.x].chessType = chessType;
}

//�Ƿ����0�����
int CChess::IsGameOver(CHESS chess[10][9], int deep)
{
	bool RedLive = false;
	bool BlackLive = false;
	int result = 0;
	for(int i = 0; i < 3; i++)
	{
		for(int j = 3; j < 6; j++)
		{
			if(1 == chess[i][j].chessType)
			{
				BlackLive = true;
			}
			if(8 == chess[i][j].chessType)
			{
				RedLive = true;
			}
		}
	}
	for(int i = 7; i < 10; i++)
	{
		for(int j = 3; j < 6; j++)
		{
			if (8 == chess[i][j].chessType)
			{
				RedLive = true;
			}
			if (1 == chess[i][j].chessType)
			{
				BlackLive = true;
			}
		}
	}
    bool temp = (bool)((maxDeep - deep ) % 2);          
    if(!BlackLive)
	{
        if(temp == isBlackFirst)
            result = 18888 + deep;
        else
            result = -18888 - deep;
	}
	else if(!RedLive)
	{
		if (temp == isBlackFirst)
			result = -18888 - deep;
		else
			result = 18888 + deep;
	}
    return result;
}

inline void CChess::AddPoint(CPoint p)
{
	relation[relationCount].x=p.x;
	relation[relationCount].y=p.y;
	relationCount++;
}

inline bool CChess::IsBlack(int chessType)
{
	if(chessType <= 7 && chessType >= 1)
		return true;
	else
		return false;
}

inline int CChess::AddMove(CPoint from, CPoint to, int layer)
{
	moveList[layer][moveCount].from=from;
	moveList[layer][moveCount].to=to;
	moveCount++;
	return moveCount;
}

inline bool CChess::IsSameSide(int from, int to)
{
	bool result = false;
	if((IsBlack(from)&&IsBlack(to))||(!IsBlack(from)&&!IsBlack(to)))
	{
		result = true;
	}
	return result;
}

bool CChess::IsSelect(CHESS chess[10][9])
{
	for(int i = 0; i < 10; i++)
	{
		for(int j = 0; j < 9; j++)
		{
			if(chess[i][j].isSelect)
			{
				selectPoint = CPoint(j, i);
				return true;
			}
		}
	}
    return false;
}

bool CChess::CanMove(CHESS chess[][9], int FromX, int FromY, int ToX, int ToY)
{
	int i,j;
	int MovechessType,TargetchessType;
	if(FromX==ToX&&FromY==ToY)
		return false;//û�ƶ�����
	MovechessType=chess[FromY][FromX].chessType;
	TargetchessType=chess[ToY][ToX].chessType;
	if(TargetchessType!=0 && IsSameSide(MovechessType,TargetchessType))
		return false;//���Լ�����
	switch(MovechessType)
	{
		case 1://�ڽ�
		{
			if(TargetchessType==8)//�Ƿ�����
			{
				if(FromX!=ToX)
					return false;//����ͬһ������
				for(i=FromY+1;i<ToY;i++)
					if(chess[i][FromX].chessType!=0)
						return false;//�м��������
			}
			else
			{
				if(ToX<3||ToX>5||ToY>2)
					return false;//���Ź�
				if(abs(FromX-ToX)+abs(FromY-ToY)>1)
					return false;//ֻ����һ��ֱ��
			}
			break;
		}
		case 8://��˧
		{
			if(TargetchessType==1)//�Ƿ�����
			{
				if(FromX!=ToX)
					return false;//����ͬһ������
				for(i=FromY-1;i>ToY;i--)
					if(chess[i][FromX].chessType!=0)
						return false;//�м��������
			}
			else
			{
				if(ToX<3||ToX>5||ToY<7)
					return false;//���Ź�
				if(abs(FromX-ToX)+abs(FromY-ToY)>1)
					return false;//ֻ����һ��ֱ��
			}
			break;
		}
		case 6://����
		{
			if(ToY>4)
				return false;//�󲻹���
			if(abs(FromX-ToX)!=2||abs(FromY-ToY)!=2)
				return false;//��������
			if(chess[(FromY+ToY)>>1][(FromX+ToX)>>1].chessType!=0)
				return false;//����
			break;
		}
		case 13://����
		{
			if(ToY<5)
				return false;//�಻����
			if(abs(FromX-ToX)!=2||abs(FromY-ToY)!=2)
				return false;//��������
			if(chess[(FromY+ToY)>>1][(FromX+ToX)>>1].chessType!=0)
				return false;//����
			break;
		}
		case 5://��ʿ
		{
			if(ToX<3||ToX>5||ToY>2)
				return false;//���Ź�
			if(abs(FromX-ToX)!=1||abs(FromY-ToY)!=1)
				return false;//ʿ��б��
			break;
		}
		case 12://����
		{
			if(ToX<3||ToX>5||ToY<7)
				return false;//���Ź�
			if(abs(FromX-ToX)!=1||abs(FromY-ToY)!=1)
				return false;//����б��
			break;
		}
		case 7://����
		{
			if(FromY>ToY)
				return false;//�䲻��ͷ
			if(FromY<5&&FromY==ToY)
				return false;//����ǰֻ��ǰ
			if(ToY-FromY+abs(FromX-ToX)>1)
				return false;//ֻ����һ��
			break;
		}
		case 14://���
		{
			if(FromY<ToY)
				return false;//������ͷ
			if(FromY>4&&FromY==ToY)
				return false;//����ǰֻ��ǰ
			if(FromY-ToY+abs(FromX-ToX)>1)
				return false;//ֻ����һ��
			break;
		}
		case 2:
		case 9://܇
		{
			if(FromX!=ToX&&FromY!=ToY)
				return false;//��ֱ��
			if(FromY==ToY)//����
			{
				if(FromX<ToX)//����
				{
					for(i=FromX+1;i<ToX;i++)
					{
						if(chess[FromY][i].chessType!=0)
							return false;//�м�������
					}
				}
				else//����
				{
					for(i=FromX-1;i>ToX;i--)
					{
						if(chess[FromY][i].chessType!=0)
							return false;//�м�������
					}
				}
			}
			else//����
			{
				if(FromY<ToY)//����
				{
					for(i=FromY+1;i<ToY;i++)
					{
						if(chess[i][FromX].chessType!=0)
							return false;//�м�������
					}
				}
				else//����
				{
					for(i=FromY-1;i>ToY;i--)
					{
						if(chess[i][FromX].chessType!=0)
							return false;//�м�������
					}
				}
			}
			break;
		}
		case 3:
		case 10://��
		{
			if(!((abs(FromX-ToX)==1&&abs(FromY-ToY)==2)||(abs(FromX-ToX)==2&&abs(FromY-ToY)==1)))
				return false;//��������
			if(FromX-ToX==2)//��
			{
				i=FromY;
				j=FromX-1;
			}
			else if(ToX-FromX==2)//��
			{
				i=FromY;
				j=FromX+1;
			}
			else if(FromY-ToY==2)//��
			{
				i=FromY-1;
				j=FromX;
			}
			else//��
			{
				i=FromY+1;
				j=FromX;
			}
			if(chess[i][j].chessType!=0)
				return false;//���
			break;
		}
		case 4:
		case 11://��
		{
			if(FromX!=ToX&&FromY!=ToY)
				return false;//��ֱ��
			if(chess[ToY][ToX].chessType!=0)//������
			{
				int count=0;
				if(FromY==ToY)//����
				{
					if(FromX<ToX)//����
					{
						for(i=FromX+1;i<ToX;i++)
						{
							if(chess[FromY][i].chessType!=0)
								count++;
						}
						if(count!=1)
							return false;//�м�û����
					}
					else//����
					{
						for(i=FromX-1;i>ToX;i--)
						{
							if(chess[FromY][i].chessType!=0)
								count++;
						}
						if(count!=1)
							return false;//�м�û����
					}
				}
				else//����
				{							
					if(FromY<ToY)//����
					{
						for(i=FromY+1;i<ToY;i++)
						{
							if(chess[i][FromX].chessType!=0)
								count++;
						}
						if(count!=1)
							return false;//�м�û����
					}
					else//����
					{
						for(i=FromY-1;i>ToY;i--)
						{
							if(chess[i][FromX].chessType!=0)
								count++;
						}
						if(count!=1)
							return false;//�м�û����
					}
				}
			}
			else//��������
			{
				if(FromY==ToY)//����
				{
					if(FromX<ToX)//����
					{
						for(i=FromX+1;i<ToX;i++)
						{
							if(chess[FromY][i].chessType!=0)
								return false;//�м�������
						}
					}
					else//����
					{
						for(i=FromX-1;i>ToX;i--)
						{
							if(chess[FromY][i].chessType!=0)
								return false;//�м�������
						}
					}
				}
				else//����
				{
					if(FromY<ToY)//����
					{
						for(i=FromY+1;i<ToY;i++)
						{
							if(chess[i][FromX].chessType!=0)
								return false;//�м�������
						}
					}
					else//����
					{
						for(i=FromY-1;i>ToY;i--)
						{
							if(chess[i][FromX].chessType!=0)
								return false;//�м�������
						}
					}
				}
			}
			break;
		}
		default:
			return false;				
	}
	return true;
}

inline CPoint CChess::GetKingPos(CHESS chess[10][9], bool isBlack)
{
	int start,end;
	if(isBlack)
	{
		start = 0;
		end = 2;
	} 
	else
	{
		start = 7;
		end = 9;
	}
	for(int y = start; y <= end; y++)
	{
		for(int x = 3; x < 6; x++)
		{
			if (chess[y][x].chessType == 1)
				return CPoint(x, y);
		}
	}
    return CPoint(0,0);
}

int CChess::PosibleMove(CHESS chess[10][9], bool side, int deep)
{
	int x,y,chessType,i,j;
	bool flag;
	moveCount=0;
	for(i=0;i<10;i++)
	{
		for(j=0;j<9;j++)
		{
			if(chess[i][j].chessType!=0)
			{
				chessType=chess[i][j].chessType;
				if(side&&IsBlack(chessType))
					continue;
				if(!side&&!IsBlack(chessType))
					continue;
				switch(chessType)
				{
					case 1://�ڽ�							
					{
						for(y=0;y<3;y++)
						{
							for(x=3;x<6;x++)
							{
								if(CanMove(chess,j,i,x,y))
									AddMove(CPoint(j,i),CPoint(x,y),deep);
							}
						}
                        CPoint p=GetKingPos(chess,false);
                        if(CanMove(chess,j,i,p.x,p.y))
                            AddMove(CPoint(j, i), p, deep);
						break;
					}
					case 8://��˧							
					{
						for(y=7;y<10;y++)
						{
							for(x=3;x<6;x++)
							{
								if(CanMove(chess,j,i,x,y))
									AddMove(CPoint(j,i),CPoint(x,y),deep);
							}
						}
                        CPoint p = GetKingPos(chess);
                        if (CanMove(chess, j, i, p.x, p.y))
                            AddMove(CPoint(j, i), p, deep);
						break;
					}
                    case 5://��ʿ
                    {
                        for (y = 0; y < 3; y++)
						{
                            for (x = 3; x < 6; x++)
							{
                                if (CanMove(chess, j, i, x, y))
                                    AddMove(CPoint(j, i), CPoint(x, y), deep);
							}
						}
                        break;
                    }
                    case 12://����
                    {
                        for (y = 7; y < 10; y++)
						{
                            for (x = 3; x < 6; x++)
							{
                                if (CanMove(chess, j, i, x, y))
                                    AddMove(CPoint(j, i), CPoint(x, y), deep);
							}
						}
                        break;
                    }
					case 6:
					case 13://��
					{
						x=j+2;
						y=i+2;
						if(x<9&&y<10&&CanMove(chess,i,y,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j+2;
						y=i-2;
						if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j-2;
						y=i+2;
						if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j-2;
						y=i-2;
						if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						break;
					}
					case 3:
					case 10://��
					{
						x=j+1;
						y=i+2;
						if(x<9&&y<10&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j-1;
						y=i+2;
						if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j+1;
						y=i-2;
						if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j-1;
						y=i-2;
						if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j+2;
						y=i+1;
						if(x<9&&y<10&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j-2;
						y=i+1;
						if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j+2;
						y=i-1;
						if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						x=j-2;
						y=i-1;
						if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						break;
					}
					case 2:
					case 9://܇
					{
						x=j;
						y=i+1;
						while(y<10)
						{
							if(chess[y][x].chessType==0)
								AddMove(CPoint(j,i),CPoint(x,y),deep);
							else 
							{
								if(!IsSameSide(chessType,chess[y][x].chessType))
									AddMove(CPoint(j,i),CPoint(x,y),deep);
								break;
							}									
							y++;
						}
						x=j;
						y=i-1;
						while(y>=0)
						{
							if(chess[y][x].chessType==0)
								AddMove(CPoint(j,i),CPoint(x,y),deep);
							else 
							{
								if(!IsSameSide(chessType,chess[y][x].chessType))
									AddMove(CPoint(j,i),CPoint(x,y),deep);
								break;
							}									
							y--;
						}
						x=j+1;
						y=i;
						while(x<9)
						{
							if(chess[y][x].chessType==0)
								AddMove(CPoint(j,i),CPoint(x,y),deep);
							else 
							{
								if(!IsSameSide(chessType,chess[y][x].chessType))
									AddMove(CPoint(j,i),CPoint(x,y),deep);
								break;
							}									
							x++;
						}
						x=j-1;
						y=i;
						while(x>=0)
						{
							if(chess[y][x].chessType==0)
								AddMove(CPoint(j,i),CPoint(x,y),deep);
							else 
							{
								if(!IsSameSide(chessType,chess[y][x].chessType))
									AddMove(CPoint(j,i),CPoint(x,y),deep);
								break;
							}									
							x--;
						}
						break;
					}
					case 4:
					case 11://��
					{
						flag=false;
						x=j;
						y=i+1;
						while(y<10)
						{
							if(chess[y][x].chessType==0)
							{
								if(!flag)
									AddMove(CPoint(j,i),CPoint(x,y),deep);
							}
							else
							{
								if(!flag)
									flag=true;
								else
								{
									if(!IsSameSide(chessType,chess[y][x].chessType))
										AddMove(CPoint(j,i),CPoint(x,y),deep);
									break;
								}
							}
							y++;
						}
						flag=false;
						y=i-1;
						while(y>=0)
						{
							if(chess[y][x].chessType==0)
							{
								if(!flag)
									AddMove(CPoint(j,i),CPoint(x,y),deep);
							}
							else
							{
								if(!flag)
									flag=true;
								else
								{
									if(!IsSameSide(chessType,chess[y][x].chessType))
										AddMove(CPoint(j,i),CPoint(x,y),deep);
									break;
								}
							}
							y--;
						}
						flag=false;
						x=j+1;
						y=i;
						while(x<9)
						{
							if(chess[y][x].chessType==0)
							{
								if(!flag)
									AddMove(CPoint(j,i),CPoint(x,y),deep);
							}
							else
							{
								if(!flag)
									flag=true;
								else
								{
									if(!IsSameSide(chessType,chess[y][x].chessType))
										AddMove(CPoint(j,i),CPoint(x,y),deep);
									break;
								}
							}
							x++;
						}
						flag=false;
						x=j-1;
						while(x>0)
						{
							if(chess[y][x].chessType==0)
							{
								if(!flag)
									AddMove(CPoint(j,i),CPoint(x,y),deep);
							}
							else
							{
								if(!flag)
									flag=true;
								else
								{
									if(!IsSameSide(chessType,chess[y][x].chessType))
										AddMove(CPoint(j,i),CPoint(x,y),deep);
									break;
								}
							}
							x--;
						}
						break;
					}
					case 7://����
					{
						x=j;
						y=i+1;
						if(y<10&&!IsSameSide(chessType,chess[y][x].chessType))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						if(i>4)
						{
							x=j+1;
							y=i;
							if(x<9&&!IsSameSide(chessType,chess[y][x].chessType))
								AddMove(CPoint(j,i),CPoint(x,y),deep);
							x=j-1;
							if(x>=0&&!IsSameSide(chessType,chess[y][x].chessType))
								AddMove(CPoint(j,i),CPoint(x,y),deep);
						}
						break;
					}
					case 14:
					{
						x=j;
						y=i-1;
						if(y>=0&&!IsSameSide(chessType,chess[y][x].chessType))
							AddMove(CPoint(j,i),CPoint(x,y),deep);
						if(i<5)
						{
							x=j+1;
							y=i;
							if(x<9&&!IsSameSide(chessType,chess[y][x].chessType))
								AddMove(CPoint(j,i),CPoint(x,y),deep);
							x=j-1;
							if(x>=0&&!IsSameSide(chessType,chess[y][x].chessType))
								AddMove(CPoint(j,i),CPoint(x,y),deep);
						}
						break;
					}
					default:
						break;
				}
			}
		}
	}
	return moveCount;
}

inline void CChess::GetRelation(CHESS chess[10][9], int i, int j)
{
	int x,y,chessType;
	bool flag;
	relationCount=0;
	chessType=chess[i][j].chessType;
	switch(chessType)
	{
		case 1://�ڽ�
		case 5://��ʿ
		{
			for(y=0;y<3;y++)
			{
				for(x=3;x<6;x++)
				{
					if(CanMove(chess,j,i,x,y))
						AddPoint(CPoint(x,y));
				}
			}
			break;
		}
		case 8://��˧
		case 12://����
		{
			for(y=7;y<10;y++)
			{
				for(x=3;x<6;x++)
				{
					if(CanMove(chess,j,i,x,y))
						AddPoint(CPoint(x,y));
				}
			}
			break;
		}
		case 6:
		case 13://��
		{
			x=j+2;
			y=i+2;
			if(x<9&&y<10&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j+2;
			y=i-2;
			if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j-2;
			y=i+2;
			if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j-2;
			y=i-2;
			if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			break;
		}
		case 3:
		case 10://��
		{
			x=j+1;
			y=i+2;
			if(x<9&&y<10&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j-1;
			y=i+2;
			if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j+1;
			y=i-2;
			if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j-1;
			y=i-2;
			if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j+2;
			y=i+1;
			if(x<9&&y<10&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j-2;
			y=i+1;
			if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j+2;
			y=i-1;
			if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			x=j-2;
			y=i-1;
			if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
				AddPoint(CPoint(x,y));
			break;
		}
		case 2:
		case 9://܇
		{
			x=j;
			y=i+1;
			while(y<10)
			{
				if(chess[y][x].chessType==0)
					AddPoint(CPoint(x,y));
				else 
				{
					if(!IsSameSide(chessType,chess[y][x].chessType))
						AddPoint(CPoint(x,y));
					break;
				}									
				y++;
			}
			x=j;
			y=i-1;
			while(y>=0)
			{
				if(chess[y][x].chessType==0)
					AddPoint(CPoint(x,y));
				else 
				{
					if(!IsSameSide(chessType,chess[y][x].chessType))
						AddPoint(CPoint(x,y));
					break;
				}									
				y--;
			}
			x=j+1;
			y=i;
			while(x<9)
			{
				if(chess[y][x].chessType==0)
					AddPoint(CPoint(x,y));
				else 
				{
					if(!IsSameSide(chessType,chess[y][x].chessType))
						AddPoint(CPoint(x,y));
					break;
				}									
				x++;
			}
			x=j-1;
			y=i;
			while(x>=0)
			{
				if(chess[y][x].chessType==0)
					AddPoint(CPoint(x,y));
				else 
				{
					if(!IsSameSide(chessType,chess[y][x].chessType))
						AddPoint(CPoint(x,y));
					break;
				}									
				x--;
			}
			break;
		}
		case 4:
		case 11://��
		{
			flag=false;
			x=j;
			y=i+1;
			while(y<10)
			{
				if(chess[y][x].chessType==0)
				{
					if(!flag)
						AddPoint(CPoint(x,y));
				}
				else
				{
					if(!flag)
						flag=true;
					else
					{
						if(!IsSameSide(chessType,chess[y][x].chessType))
							AddPoint(CPoint(x,y));
						break;
					}
				}
				y++;
			}
			flag=false;
			y=i-1;
			while(y>=0)
			{
				if(chess[y][x].chessType==0)
				{
					if(!flag)
						AddPoint(CPoint(x,y));
				}
				else
				{
					if(!flag)
						flag=true;
					else
					{
						if(!IsSameSide(chessType,chess[y][x].chessType))
							AddPoint(CPoint(x,y));
						break;
					}
				}
				y--;
			}
			flag=false;
			x=j+1;
			y=i;
			while(x<9)
			{
				if(chess[y][x].chessType==0)
				{
					if(!flag)
						AddPoint(CPoint(x,y));
				}
				else
				{
					if(!flag)
						flag=true;
					else
					{
						if(!IsSameSide(chessType,chess[y][x].chessType))
							AddPoint(CPoint(x,y));
						break;
					}
				}
				x++;
			}
			flag=false;
			x=j-1;
			while(x>0)
			{
				if(chess[y][x].chessType==0)
				{
					if(!flag)
						AddPoint(CPoint(x,y));
				}
				else
				{
					if(!flag)
						flag=true;
					else
					{
						if(!IsSameSide(chessType,chess[y][x].chessType))
							AddPoint(CPoint(x,y));
						break;
					}
				}
				x--;
			}
			break;
		}
		case 7://����
		{
			x=j;
			y=i+1;
			if(y<10&&!IsSameSide(chessType,chess[y][x].chessType))
				AddPoint(CPoint(x,y));
			if(i>4)
			{
				x=j+1;
				y=i;
				if(x<9&&!IsSameSide(chessType,chess[y][x].chessType))
					AddPoint(CPoint(x,y));
				x=j-1;
				if(x>=0&&!IsSameSide(chessType,chess[y][x].chessType))
					AddPoint(CPoint(x,y));
			}
			break;
		}
		case 14://���
		{
			x=j;
			y=i-1;
			if(y>=0&&!IsSameSide(chessType,chess[y][x].chessType))
				AddPoint(CPoint(x,y));
			if(i<5)
			{
				x=j+1;
				y=i;
				if(x<9&&!IsSameSide(chessType,chess[y][x].chessType))
					AddPoint(CPoint(x,y));
				x=j-1;
				if(x>=0&&!IsSameSide(chessType,chess[y][x].chessType))
					AddPoint(CPoint(x,y));
			}
			break;
		}
		default:
			break;
	}			
}

int CChess::Caculate(CHESS chess[10][9], bool side)
{
	int i,j,k,chessType,targettype;
	memset(Attack,0,sizeof(Attack));
	memset(Guard,0,sizeof(Guard));
	memset(ActiveValuePos,0,sizeof(ActiveValuePos));
	memset(ChessValue,0,sizeof(ChessValue));
	for(i=0;i<10;i++)
	{
		for(j=0;j<9;j++)
		{
			if(chess[i][j].chessType!=0)
			{
				chessType=chess[i][j].chessType;
				GetRelation(chess,i,j);
				for(k=0;k<relationCount;k++)
				{
					targettype=chess[relation[k].y][relation[k].x].chessType;
					if(targettype==0)//û��
						ActiveValuePos[i][j]++;
					else
					{
						if(IsSameSide(chessType,targettype))//�Լ���
							Guard[relation[k].y][relation[k].x]++;
						else
						{
							Attack[relation[k].y][relation[k].x]++;
							ActiveValuePos[i][j]++;
							switch(targettype)
							{
								case 1:
								{
                                    if (!side)
										return 18888;
									break;
								}
								case 8:
								{
									if(side)
										return 18888;
									break;
								}
								default:
								{
                                    Attack[relation[k].y][relation[k].x] += (30 + (BaseValue[targettype - 1] - BaseValue[chessType - 1]) / 10) / 10;
									break;
								}
							}
						}
					}
				}
			}
		}
	}
	for(i=0;i<10;i++)
	{
		for(j=0;j<9;j++)
		{
			if(chess[i][j].chessType!=0)
			{
				chessType=chess[i][j].chessType;
				ChessValue[i][j]++;
				ChessValue[i][j]+=ActiveValue[chessType-1]*ActiveValuePos[i][j];
				ChessValue[i][j]+=GetPawnValue(chess,j,i);
			}
		}
	}
	int HalfValue;
	for(i=0;i<10;i++)
	{
		for(j=0;j<9;j++)
		{
			if(chess[i][j].chessType!=0)
			{
				chessType=chess[i][j].chessType;
				HalfValue=BaseValue[chessType-1]>>4;
				ChessValue[i][j]+=BaseValue[chessType-1];
				if(IsBlack(chessType))
				{
					if(Attack[i,j]!=0)
					{
						if(side)
						{
							if(chessType==1)
								ChessValue[i][j]-=20;
							else
							{
                                ChessValue[i][j] -= HalfValue << 1;
								if(Guard[i][j]!=0)
                                    ChessValue[i][j] += HalfValue ;
							}
						}
						else
						{
							if(chessType==1)
								return 18888;
							else
							{
                                ChessValue[i][j] -= HalfValue  * 10;
                                if (Guard[i][j] != 0)
                                    ChessValue[i][j] += HalfValue * 9;
							}
						}
                        ChessValue[i][j] -= Attack[i][j];
					}
					else
					{
						if(Guard[i][j]!=0)
							ChessValue[i][j]+=1;
					}
				}
				else
				{
					if(Attack[i][j]!=0)
					{
						if(!side)
						{
							if(chessType==1)
								ChessValue[i][j]-=20;
							else
							{
                                ChessValue[i][j] -= HalfValue << 1;
								if(Guard[i][j]!=0)
									ChessValue[i][j]+=HalfValue;
							}
						}
						else
						{
							if(chessType==1)
								return 18888;
							else
							{
								ChessValue[i][j]-=HalfValue * 10;
								if(Guard[i][j]!=0)
									ChessValue[i][j]+=HalfValue * 9;
							}
						}
                        ChessValue[i][j] -= Attack[i][j];
					}
					else
					{
						if(Guard[i][j]!=0)
							ChessValue[i][j]+=1;
					}
				}
			}
		}
	}
	int RedValue=0;
	int BlackValue=0;
	for(i=0;i<10;i++)
	{
		for(j=0;j<9;j++)
		{
			chessType=chess[i][j].chessType;
			if(chess[i][j].chessType!=0)
			{
				if(IsBlack(chessType))
					BlackValue+=ChessValue[i][j];
				else
					RedValue+=ChessValue[i][j];
			}
		} 
	}
    if (!side)
        return BlackValue - RedValue;
    else
        return RedValue - BlackValue;
}

CPoint CChess::GetSelectPoint()
{
	return selectPoint;
}

bool CChess::GetCurrentSide()
{
	return currentSide;
}

void CChess::SetCurrentSide(bool flag)
{
	currentSide=flag;
}

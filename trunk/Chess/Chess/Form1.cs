using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Runtime.InteropServices;

namespace Chess
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>   

	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.Panel panel1;
		private Point p; //选中棋子坐标
		private int MoveCount;
		private static bool side=false;
        private static int c = 1;
        private int MaxDeep=5; //最大搜索深度
        private Movelist BestMove;
        public static bool emulant = true;
        [DllImport("Winmm")]
        public static extern bool PlaySound(string pszSound, IntPtr hmod, UInt32 fdwSound);
        private string path = Application.StartupPath + "\\GAMEOVER.WAV";
		public struct Chess
		{
			public int chesstype;
			public bool select;
			public Chess(int chesstype)
			{
				this.chesstype=chesstype;
				select=false;
			}
		}
		public struct Movelist
		{
			public Point from,to;
		}
		private Movelist[,] movelist=new Movelist[8,80];
		private int[] BaseValue={10000,500,350,350,250,250,350,10000,500,350,350,250,250,350};
		private int[] ActiveValue={0,6,12,6,1,1,15,0,6,12,6,1,1,15};
		private int[,] Attack;		
		private int[,] Guard;
		private int[,] ChessValue;		
		private int[,] ActiveValuePos;		
		private Point[] Relation=new Point[20];
		private int nRelation;
		private int[,] Additional1=
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
		private int[,] Additional2=
		{
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{70,70,70,70,70,70,70,70,70},
			{70,90,110,110,110,110,110,90,70},
			{90,90,110,120,120,120,110,90,90},
			{90,90,110,120,120,120,110,90,90},
			{0,0,0,0,0,0,0,0,0},
		};
        private Time.UserControl1 BlackSide;
        private Time.UserControl1 RedSide;
        private MenuItem menuItem5;
		private Chess[,] chess=
		{
			{new Chess(2),new Chess(3),new Chess(6),new Chess(5),new Chess(1),new Chess(5),new Chess(6),new Chess(3),new Chess(2)},
			{new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0)},
			{new Chess(0),new Chess(4),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(4),new Chess(0)},
			{new Chess(7),new Chess(0),new Chess(7),new Chess(0),new Chess(7),new Chess(0),new Chess(7),new Chess(0),new Chess(7)},
			{new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0)},
			{new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0)},
			{new Chess(14),new Chess(0),new Chess(14),new Chess(0),new Chess(14),new Chess(0),new Chess(14),new Chess(0),new Chess(14)},
			{new Chess(0),new Chess(11),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(11),new Chess(0)},
			{new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0),new Chess(0)},
			{new Chess(9),new Chess(10),new Chess(13),new Chess(12),new Chess(8),new Chess(12),new Chess(13),new Chess(10),new Chess(9)}
		};
        private Chess[,] TempChess = new Chess[10, 9];//电脑搜索时的临时棋盘
        //初始棋盘
        private int[,] chessman =
        {
            {2,3,6,5,1,5,6,3,2},
            {0,0,0,0,0,0,0,0,0},
            {0,4,0,0,0,0,0,4,0},
            {7,0,7,0,7,0,7,0,7},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {14,0,14,0,14,0,14,0,14},
            {0,11,0,0,0,0,0,11,0},
            {0,0,0,0,0,0,0,0,0},
            {9,10,13,12,8,12,13,10,9}
        };
		private int GetBingValue(Chess[,] chess,int x,int y)
		{
			if(chess[y,x].chesstype==7)
				return Additional1[y,x];
			if(chess[y,x].chesstype==14)
				return Additional2[y,x];
			return 0;
		}

        private int MakeMove(Movelist move,Chess[,] chess)
        {
            int chesstype = chess[move.to.Y, move.to.X].chesstype;
            chess[move.to.Y, move.to.X].chesstype = chess[move.from.Y, move.from.X].chesstype;
            chess[move.from.Y, move.from.X].chesstype = 0;
            return chesstype;
        }

        private void UnMakeMove(Movelist move, int chesstype,Chess[,] chess)
        {
            chess[move.from.Y, move.from.X].chesstype = chess[move.to.Y, move.to.X].chesstype;
            chess[move.to.Y, move.to.X].chesstype = chesstype;
        }

        private int IsGameOver(Chess[,] chess, int deep)
        {
            int i, j;
            bool RedLive = false, BlackLive = false;
            for(i = 0; i < 3; i++)
                for (j = 3; j < 6; j++)
                {
                    if (chess[i, j].chesstype == 1)
                        BlackLive = true;
                    if (chess[i, j].chesstype == 8)
                        RedLive = true;
                }
            for (i = 7; i < 10; i++)
                for (j = 3; j < 6; j++)
                {
                    if (chess[i, j].chesstype == 8)
                        RedLive = true;
                    if (chess[i, j].chesstype == 1)
                        BlackLive = true;
                }
            i = (MaxDeep - deep ) % 2;          
            if (!BlackLive)
                if (i==c)
                    return 18888 + deep;
                else
                    return -18888 - deep;
            if (!RedLive)
                if (i==c)
                    return -18888 - deep;
                else
                    return 18888 + deep;
            return 0;
        }

        private void AddPoint(int x, int y)
		{
			Relation[nRelation].X=x;
			Relation[nRelation].Y=y;
			nRelation++;
		}

		private bool IsBlack(int x)
		{
			if(x<=7&&x>=1)
				return true;
			else
				return false;
		}

		private int AddMove(Point from,Point to,int layer)
		{
			movelist[layer,MoveCount].from=from;
			movelist[layer,MoveCount].to=to;
			MoveCount++;
			return MoveCount;
		}

		private bool IsSameSide(int from,int to)
		{
            if (to == 0)
                return false;
			if((IsBlack(from)&&IsBlack(to))||(!IsBlack(from)&&!IsBlack(to)))
				return true;
			else 
				return false;
		}

		private Point ChangeCoordinate(MouseEventArgs e)
		{
			return new Point(e.X/50,e.Y/50);
		}

		private bool IsSelect(Chess[,]chess)
		{
			int i,j;
            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (chess[i, j].select)
                    {
                        p = new Point(j, i);
                        return true;
                    }
                }
            }
            return false;
		}

		private bool CanMove(Chess[,] chess,int FromX,int FromY,int ToX,int ToY)
		{
			int i,j;
			int Movechesstype,Targetchesstype;
			if(FromX==ToX&&FromY==ToY)
				return false;//没移动棋子
			Movechesstype=chess[FromY,FromX].chesstype;
			Targetchesstype=chess[ToY,ToX].chesstype;
			if(Targetchesstype!=0 && IsSameSide(Movechesstype,Targetchesstype))
				return false;//吃自己棋子
			switch(Movechesstype)
			{
				case 1://黑将
				{
					if(Targetchesstype==8)//是否死棋
					{
						if(FromX!=ToX)
							return false;//不在同一条线上
						for(i=FromY+1;i<ToY;i++)
							if(chess[i,FromX].chesstype!=0)
								return false;//中间隔有棋子
					}
					else
					{
						if(ToX<3||ToX>5||ToY>2)
							return false;//出九宫
						if(Math.Abs(FromX-ToX)+Math.Abs(FromY-ToY)>1)
							return false;//只能走一步直线
					}
					break;
				}
				case 8://红帅
				{
					if(Targetchesstype==1)//是否死棋
					{
						if(FromX!=ToX)
							return false;//不在同一条线上
						for(i=FromY-1;i>ToY;i--)
							if(chess[i,FromX].chesstype!=0)
								return false;//中间隔有棋子
					}
					else
					{
						if(ToX<3||ToX>5||ToY<7)
							return false;//出九宫
						if(Math.Abs(FromX-ToX)+Math.Abs(FromY-ToY)>1)
							return false;//只能走一步直线
					}
					break;
				}
				case 6://黑象
				{
					if(ToY>4)
						return false;//象不过河
					if(Math.Abs(FromX-ToX)!=2||Math.Abs(FromY-ToY)!=2)
						return false;//象走田字
					if(chess[(FromY+ToY)/2,(FromX+ToX)/2].chesstype!=0)
						return false;//象眼
					break;
				}
				case 13://红相
				{
					if(ToY<5)
						return false;//相不过河
					if(Math.Abs(FromX-ToX)!=2||Math.Abs(FromY-ToY)!=2)
						return false;//相走田字
					if(chess[(FromY+ToY)/2,(FromX+ToX)/2].chesstype!=0)
						return false;//相眼
					break;
				}
				case 5://黑士
				{
					if(ToX<3||ToX>5||ToY>2)
						return false;//出九宫
					if(Math.Abs(FromX-ToX)!=1||Math.Abs(FromY-ToY)!=1)
						return false;//士走斜线
					break;
				}
				case 12://红仕
				{
					if(ToX<3||ToX>5||ToY<7)
						return false;//出九宫
					if(Math.Abs(FromX-ToX)!=1||Math.Abs(FromY-ToY)!=1)
						return false;//仕走斜线
					break;
				}
				case 7://黑卒
				{
					if(FromY>ToY)
						return false;//卒不回头
					if(FromY<5&&FromY==ToY)
						return false;//过河前只向前
					if(ToY-FromY+Math.Abs(FromX-ToX)>1)
						return false;//只能走一步
					break;
				}
				case 14://红兵
				{
					if(FromY<ToY)
						return false;//兵不回头
					if(FromY>4&&FromY==ToY)
						return false;//过河前只向前
					if(FromY-ToY+Math.Abs(FromX-ToX)>1)
						return false;//只能走一步
					break;
				}
				case 2:
				case 9://車
				{
					if(FromX!=ToX&&FromY!=ToY)
						return false;//走直线
					if(FromY==ToY)//横向
					{
						if(FromX<ToX)//向右
						{
							for(i=FromX+1;i<ToX;i++)
								if(chess[FromY,i].chesstype!=0)
									return false;//中间有棋子
						}
						else//向左
						{
							for(i=FromX-1;i>ToX;i--)
								if(chess[FromY,i].chesstype!=0)
									return false;//中间有棋子
						}
					}
					else//纵向
					{
						if(FromY<ToY)//向下
						{
							for(i=FromY+1;i<ToY;i++)
								if(chess[i,FromX].chesstype!=0)
									return false;//中间有棋子
						}
						else//向上
						{
							for(i=FromY-1;i>ToY;i--)
								if(chess[i,FromX].chesstype!=0)
									return false;//中间有棋子
						}
					}
					break;
				}
				case 3:
				case 10://马
				{
					if(!((Math.Abs(FromX-ToX)==1&&Math.Abs(FromY-ToY)==2)||(Math.Abs(FromX-ToX)==2&&Math.Abs(FromY-ToY)==1)))
						return false;//马走日字
					if(FromX-ToX==2)//左
					{
						i=FromY;
						j=FromX-1;
					}
					else if(ToX-FromX==2)//右
					{
						i=FromY;
						j=FromX+1;
					}
					else if(FromY-ToY==2)//上
					{
						i=FromY-1;
						j=FromX;
					}
					else//下
					{
						i=FromY+1;
						j=FromX;
					}
					if(chess[i,j].chesstype!=0)
						return false;//马脚
					break;
				}
				case 4:
				case 11://炮
				{
					if(FromX!=ToX&&FromY!=ToY)
						return false;//走直线
					if(chess[ToY,ToX].chesstype!=0)//吃棋子
					{
						
						int count=0;
						if(FromY==ToY)//横向
						{
							if(FromX<ToX)//向右
							{
								for(i=FromX+1;i<ToX;i++)
									if(chess[FromY,i].chesstype!=0)
										count++;
								if(count!=1)
									return false;//中间没棋子
							}
							else//向左
							{
								for(i=FromX-1;i>ToX;i--)
									if(chess[FromY,i].chesstype!=0)
										count++;
								if(count!=1)
									return false;//中间没棋子
							}
						}
						else//纵向
						{							
							if(FromY<ToY)//向下
							{
								for(i=FromY+1;i<ToY;i++)
									if(chess[i,FromX].chesstype!=0)
										count++;
								if(count!=1)
									return false;//中间没棋子
							}
							else//向上
							{
								for(i=FromY-1;i>ToY;i--)
									if(chess[i,FromX].chesstype!=0)
										count++;
								if(count!=1)
									return false;//中间没棋子
							}
						}
					}
					else//不吃棋子
					{
						if(FromY==ToY)//横向
						{
							if(FromX<ToX)//向右
							{
								for(i=FromX+1;i<ToX;i++)
									if(chess[FromY,i].chesstype!=0)
										return false;//中间有棋子
							}
							else//向左
							{
								for(i=FromX-1;i>ToX;i--)
									if(chess[FromY,i].chesstype!=0)
										return false;//中间有棋子
							}
						}
						else//纵向
						{
							if(FromY<ToY)//向下
							{
								for(i=FromY+1;i<ToY;i++)
									if(chess[i,FromX].chesstype!=0)
										return false;//中间有棋子
							}
							else//向上
							{
								for(i=FromY-1;i>ToY;i--)
									if(chess[i,FromX].chesstype!=0)
										return false;//中间有棋子
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

        private Point GetBlackKingPos(Chess[,] chess)
        {
            int x,y;
            for (y = 0; y < 3; y++)
            {
                for (x = 3; x < 6; x++)
                    if (chess[y, x].chesstype == 1)
                        return new Point(x, y);
            }
            return new Point(0,0);
        }

        private Point GetRedKingPos(Chess[,] chess)
        {
            int x, y;
            for (y = 7; y <= 9; y++)
            {
                for (x = 3; x < 6; x++)
                    if (chess[y, x].chesstype == 1)
                        return new Point(x, y);
            }
            return new Point(0, 0);
        }
		private int PosibleMove(Chess[,] chess,bool side,int deep)
		{
			int x,y,chesstype,i,j;
			bool flag;
			MoveCount=0;
			for(i=0;i<10;i++)
				for(j=0;j<9;j++)
				{
					if(chess[i,j].chesstype!=0)
					{
						chesstype=chess[i,j].chesstype;
						if(side&&IsBlack(chesstype))
							continue;
						if(!side&&!IsBlack(chesstype))
							continue;
						switch(chesstype)
						{
							case 1://黑将							
							{
								for(y=0;y<3;y++)
									for(x=3;x<6;x++)
										if(CanMove(chess,j,i,x,y))
											AddMove(new Point(j,i),new Point(x,y),deep);
                                Point p=GetRedKingPos(chess);
                                if(CanMove(chess,j,i,p.X,p.Y))
                                    AddMove(new Point(j, i), p, deep);
								break;
							}
							case 8://红帅							
							{
								for(y=7;y<10;y++)
									for(x=3;x<6;x++)
										if(CanMove(chess,j,i,x,y))
											AddMove(new Point(j,i),new Point(x,y),deep);
                                Point p = GetBlackKingPos(chess);
                                if (CanMove(chess, j, i, p.X, p.Y))
                                    AddMove(new Point(j, i), p, deep);
								break;
							}
                            case 5://黑士
                            {
                                for (y = 0; y < 3; y++)
                                    for (x = 3; x < 6; x++)
                                        if (CanMove(chess, j, i, x, y))
                                            AddMove(new Point(j, i), new Point(x, y), deep);
                                break;
                            }
                            case 12://红仕
                            {
                                for (y = 7; y < 10; y++)
                                    for (x = 3; x < 6; x++)
                                        if (CanMove(chess, j, i, x, y))
                                            AddMove(new Point(j, i), new Point(x, y), deep);
                                break;
                            }
							case 6:
							case 13://象
							{
								x=j+2;
								y=i+2;
								if(x<9&&y<10&&CanMove(chess,i,y,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j+2;
								y=i-2;
								if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j-2;
								y=i+2;
								if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j-2;
								y=i-2;
								if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								break;
							}
							case 3:
							case 10://马
							{
								x=j+1;
								y=i+2;
								if(x<9&&y<10&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j-1;
								y=i+2;
								if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j+1;
								y=i-2;
								if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j-1;
								y=i-2;
								if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j+2;
								y=i+1;
								if(x<9&&y<10&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j-2;
								y=i+1;
								if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j+2;
								y=i-1;
								if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								x=j-2;
								y=i-1;
								if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
									AddMove(new Point(j,i),new Point(x,y),deep);
								break;
							}
							case 2:
							case 9://車
							{
								x=j;
								y=i+1;
								while(y<10)
								{
									if(chess[y,x].chesstype==0)
										AddMove(new Point(j,i),new Point(x,y),deep);
									else 
									{
										if(!IsSameSide(chesstype,chess[y,x].chesstype))
											AddMove(new Point(j,i),new Point(x,y),deep);
										break;
									}									
									y++;
								}
								x=j;
								y=i-1;
								while(y>=0)
								{
									if(chess[y,x].chesstype==0)
										AddMove(new Point(j,i),new Point(x,y),deep);
									else 
									{
										if(!IsSameSide(chesstype,chess[y,x].chesstype))
											AddMove(new Point(j,i),new Point(x,y),deep);
										break;
									}									
									y--;
								}
								x=j+1;
								y=i;
								while(x<9)
								{
									if(chess[y,x].chesstype==0)
										AddMove(new Point(j,i),new Point(x,y),deep);
									else 
									{
										if(!IsSameSide(chesstype,chess[y,x].chesstype))
											AddMove(new Point(j,i),new Point(x,y),deep);
										break;
									}									
									x++;
								}
								x=j-1;
								y=i;
								while(x>=0)
								{
									if(chess[y,x].chesstype==0)
										AddMove(new Point(j,i),new Point(x,y),deep);
									else 
									{
										if(!IsSameSide(chesstype,chess[y,x].chesstype))
											AddMove(new Point(j,i),new Point(x,y),deep);
										break;
									}									
									x--;
								}
								break;
							}
							case 4:
							case 11://炮
							{
								flag=false;
								x=j;
								y=i+1;
								while(y<10)
								{
									if(chess[y,x].chesstype==0)
									{
										if(!flag)
											AddMove(new Point(j,i),new Point(x,y),deep);
									}
									else
									{
										if(!flag)
											flag=true;
										else
										{
											if(!IsSameSide(chesstype,chess[y,x].chesstype))
												AddMove(new Point(j,i),new Point(x,y),deep);
											break;
										}
									}
									y++;
								}
								flag=false;
								y=i-1;
								while(y>=0)
								{
									if(chess[y,x].chesstype==0)
									{
										if(!flag)
											AddMove(new Point(j,i),new Point(x,y),deep);
									}
									else
									{
										if(!flag)
											flag=true;
										else
										{
											if(!IsSameSide(chesstype,chess[y,x].chesstype))
												AddMove(new Point(j,i),new Point(x,y),deep);
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
									if(chess[y,x].chesstype==0)
									{
										if(!flag)
											AddMove(new Point(j,i),new Point(x,y),deep);
									}
									else
									{
										if(!flag)
											flag=true;
										else
										{
											if(!IsSameSide(chesstype,chess[y,x].chesstype))
												AddMove(new Point(j,i),new Point(x,y),deep);
											break;
										}
									}
									x++;
								}
								flag=false;
								x=j-1;
								while(x>0)
								{
									if(chess[y,x].chesstype==0)
									{
										if(!flag)
											AddMove(new Point(j,i),new Point(x,y),deep);
									}
									else
									{
										if(!flag)
											flag=true;
										else
										{
											if(!IsSameSide(chesstype,chess[y,x].chesstype))
												AddMove(new Point(j,i),new Point(x,y),deep);
											break;
										}
									}
									x--;
								}
								break;
							}
							case 7://黑卒
							{
								x=j;
								y=i+1;
								if(y<10&&!IsSameSide(chesstype,chess[y,x].chesstype))
									AddMove(new Point(j,i),new Point(x,y),deep);
								if(i>4)
								{
									x=j+1;
									y=i;
									if(x<9&&!IsSameSide(chesstype,chess[y,x].chesstype))
										AddMove(new Point(j,i),new Point(x,y),deep);
									x=j-1;
									if(x>=0&&!IsSameSide(chesstype,chess[y,x].chesstype))
										AddMove(new Point(j,i),new Point(x,y),deep);
								}
								break;
							}
							case 14:
							{
								x=j;
								y=i-1;
								if(y>=0&&!IsSameSide(chesstype,chess[y,x].chesstype))
									AddMove(new Point(j,i),new Point(x,y),deep);
								if(i<5)
								{
									x=j+1;
									y=i;
									if(x<9&&!IsSameSide(chesstype,chess[y,x].chesstype))
										AddMove(new Point(j,i),new Point(x,y),deep);
									x=j-1;
									if(x>=0&&!IsSameSide(chesstype,chess[y,x].chesstype))
										AddMove(new Point(j,i),new Point(x,y),deep);
								}
								break;
							}
							default:
								break;
						}
					}
				}
			return MoveCount;

		}

		private void GetRelation(Chess[,] chess,int i,int j)
		{
			int x,y,chesstype;
			bool flag;
			nRelation=0;
			chesstype=chess[i,j].chesstype;
			switch(chesstype)
			{
				case 1://黑将
				case 5://黑士
				{
					for(y=0;y<3;y++)
						for(x=3;x<6;x++)
							if(CanMove(chess,j,i,x,y))
								AddPoint(x,y);
					break;
				}
				case 8://红帅
				case 12://红仕
				{
					for(y=7;y<10;y++)
						for(x=3;x<6;x++)
							if(CanMove(chess,j,i,x,y))
								AddPoint(x,y);
					break;
				}
				case 6:
				case 13://象
				{
					x=j+2;
					y=i+2;
					if(x<9&&y<10&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j+2;
					y=i-2;
					if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j-2;
					y=i+2;
					if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j-2;
					y=i-2;
					if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					break;
				}
				case 3:
				case 10://马
				{
					x=j+1;
					y=i+2;
					if(x<9&&y<10&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j-1;
					y=i+2;
					if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j+1;
					y=i-2;
					if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j-1;
					y=i-2;
					if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j+2;
					y=i+1;
					if(x<9&&y<10&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j-2;
					y=i+1;
					if(x>=0&&y<10&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j+2;
					y=i-1;
					if(x<9&&y>=0&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					x=j-2;
					y=i-1;
					if(x>=0&&y>=0&&CanMove(chess,j,i,x,y))
						AddPoint(x,y);
					break;
				}
				case 2:
				case 9://車
				{
					x=j;
					y=i+1;
					while(y<10)
					{
						if(chess[y,x].chesstype==0)
							AddPoint(x,y);
						else 
						{
							if(!IsSameSide(chesstype,chess[y,x].chesstype))
								AddPoint(x,y);
							break;
						}									
						y++;
					}
					x=j;
					y=i-1;
					while(y>=0)
					{
						if(chess[y,x].chesstype==0)
							AddPoint(x,y);
						else 
						{
							if(!IsSameSide(chesstype,chess[y,x].chesstype))
								AddPoint(x,y);
							break;
						}									
						y--;
					}
					x=j+1;
					y=i;
					while(x<9)
					{
						if(chess[y,x].chesstype==0)
							AddPoint(x,y);
						else 
						{
							if(!IsSameSide(chesstype,chess[y,x].chesstype))
								AddPoint(x,y);
							break;
						}									
						x++;
					}
					x=j-1;
					y=i;
					while(x>=0)
					{
						if(chess[y,x].chesstype==0)
							AddPoint(x,y);
						else 
						{
							if(!IsSameSide(chesstype,chess[y,x].chesstype))
								AddPoint(x,y);
							break;
						}									
						x--;
					}
					break;
				}
				case 4:
				case 11://炮
				{
					flag=false;
					x=j;
					y=i+1;
					while(y<10)
					{
						if(chess[y,x].chesstype==0)
						{
							if(!flag)
								AddPoint(x,y);
						}
						else
						{
							if(!flag)
								flag=true;
							else
							{
								if(!IsSameSide(chesstype,chess[y,x].chesstype))
									AddPoint(x,y);
								break;
							}
						}
						y++;
					}
					flag=false;
					y=i-1;
					while(y>=0)
					{
						if(chess[y,x].chesstype==0)
						{
							if(!flag)
								AddPoint(x,y);
						}
						else
						{
							if(!flag)
								flag=true;
							else
							{
								if(!IsSameSide(chesstype,chess[y,x].chesstype))
									AddPoint(x,y);
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
						if(chess[y,x].chesstype==0)
						{
							if(!flag)
								AddPoint(x,y);
						}
						else
						{
							if(!flag)
								flag=true;
							else
							{
								if(!IsSameSide(chesstype,chess[y,x].chesstype))
									AddPoint(x,y);
								break;
							}
						}
						x++;
					}
					flag=false;
					x=j-1;
					while(x>0)
					{
						if(chess[y,x].chesstype==0)
						{
							if(!flag)
								AddPoint(x,y);
						}
						else
						{
							if(!flag)
								flag=true;
							else
							{
								if(!IsSameSide(chesstype,chess[y,x].chesstype))
									AddPoint(x,y);
								break;
							}
						}
						x--;
					}
					break;
				}
				case 7://黑卒
				{
					x=j;
					y=i+1;
					if(y<10&&!IsSameSide(chesstype,chess[y,x].chesstype))
						AddPoint(x,y);
					if(i>4)
					{
						x=j+1;
						y=i;
						if(x<9&&!IsSameSide(chesstype,chess[y,x].chesstype))
							AddPoint(x,y);
						x=j-1;
						if(x>=0&&!IsSameSide(chesstype,chess[y,x].chesstype))
							AddPoint(x,y);
					}
					break;
				}
				case 14://红兵
				{
					x=j;
					y=i-1;
					if(y>=0&&!IsSameSide(chesstype,chess[y,x].chesstype))
						AddPoint(x,y);
					if(i<5)
					{
						x=j+1;
						y=i;
						if(x<9&&!IsSameSide(chesstype,chess[y,x].chesstype))
							AddPoint(x,y);
						x=j-1;
						if(x>=0&&!IsSameSide(chesstype,chess[y,x].chesstype))
							AddPoint(x,y);
					}
					break;
				}
				default:
					break;
			}			
		}

		private int Caculate(Chess[,] chess,bool side)
        {
			int i,j,k,chesstype,targettype;
			Attack=new int[10,9]
			{
				{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},
			};
			Guard=new int[10,9]
			{
				{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},
			};
			ChessValue=new int[10,9]
			{
				{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},
			};
			ActiveValuePos=new int[10,9]
			{
				{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0},
			};
			for(i=0;i<10;i++)
				for(j=0;j<9;j++)
				{
					if(chess[i,j].chesstype!=0)
					{
						chesstype=chess[i,j].chesstype;
						GetRelation(chess,i,j);
						for(k=0;k<nRelation;k++)
						{
							targettype=chess[Relation[k].Y,Relation[k].X].chesstype;
							if(targettype==0)//没棋
								ActiveValuePos[i,j]++;
							else
							{
								if(IsSameSide(chesstype,targettype))//自己棋
									Guard[Relation[k].Y,Relation[k].X]++;
								else
								{
									Attack[Relation[k].Y,Relation[k].X]++;
									ActiveValuePos[i,j]++;
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
                                            Attack[Relation[k].Y, Relation[k].X] += (30 + (BaseValue[targettype - 1] - BaseValue[chesstype - 1]) / 10) / 10;
											break;
										}
									}
								}
							}
						}
					}
				}
			for(i=0;i<10;i++)
				for(j=0;j<9;j++)
				{
					if(chess[i,j].chesstype!=0)
					{
						chesstype=chess[i,j].chesstype;
						ChessValue[i,j]++;
						ChessValue[i,j]+=ActiveValue[chesstype-1]*ActiveValuePos[i,j];
						ChessValue[i,j]+=GetBingValue(chess,j,i);
					}
				}
			int HalfValue;
			for(i=0;i<10;i++)
				for(j=0;j<9;j++)
				{
					if(chess[i,j].chesstype!=0)
					{
						chesstype=chess[i,j].chesstype;
						HalfValue=BaseValue[chesstype-1]/16;
						ChessValue[i,j]+=BaseValue[chesstype-1];
						if(IsBlack(chesstype))
						{
							if(Attack[i,j]!=0)
							{
								if(side)
								{
									if(chesstype==1)
										ChessValue[i,j]-=20;
									else
									{
                                        ChessValue[i, j] -= HalfValue * 2;
										if(Guard[i,j]!=0)
                                            ChessValue[i, j] += HalfValue ;
									}
								}
								else
								{
									if(chesstype==1)
										return 18888;
									else
									{
                                        ChessValue[i, j] -= HalfValue  * 10;
                                        if (Guard[i, j] != 0)
                                            ChessValue[i, j] += HalfValue * 9;
									}
								}
                                ChessValue[i, j] -= Attack[i, j];
							}
							else
							{
								if(Guard[i,j]!=0)
									ChessValue[i,j]+=1;
							}
						}
						else
						{
							if(Attack[i,j]!=0)
							{
								if(!side)
								{
									if(chesstype==1)
										ChessValue[i,j]-=20;
									else
									{
                                        ChessValue[i, j] -= HalfValue * 2;
										if(Guard[i,j]!=0)
											ChessValue[i,j]+=HalfValue;
									}
								}
								else
								{
									if(chesstype==1)
										return 18888;
									else
									{
										ChessValue[i,j]-=HalfValue * 10;
										if(Guard[i,j]!=0)
											ChessValue[i,j]+=HalfValue * 9;
									}
								}
                                ChessValue[i, j] -= Attack[i, j];
							}
							else
							{
								if(Guard[i,j]!=0)
									ChessValue[i,j]+=1;
							}
						}
					}
				}
			int RedValue=0;
			int BlackValue=0;
			for(i=0;i<10;i++)
				for(j=0;j<9;j++)
				{
					chesstype=chess[i,j].chesstype;
					if(chess[i,j].chesstype!=0)
					{
						if(IsBlack(chesstype))
							BlackValue+=ChessValue[i,j];
						else
							RedValue+=ChessValue[i,j];
					}
				}           
                if (!side)
                    return BlackValue - RedValue;
                else
                    return RedValue - BlackValue;          
		}

        private int SearchEngine(Chess[,] chess, int alph, int beta, int deep)
        {
            int Count, i, score, chesstype;
            bool x;
            i = IsGameOver(chess, deep);
            if (i != 0)
                return i;
             if ((MaxDeep - deep) % 2 == 0)
                x = !side;
            else
                x = side;
             if (deep <= 0)
                 return Caculate(chess, x);
             Count = PosibleMove(chess, x, deep);
             for (i = 1; i < Count; i++)
             {
                 chesstype = MakeMove(movelist[deep, i], chess);
                 score = -SearchEngine(chess, -beta, -alph, deep - 1);
                 UnMakeMove(movelist[deep, i], chesstype, chess);
                 if (score > alph)
                 {
                     alph = score;
                     if (deep == MaxDeep)
                     {
                         BestMove.from = movelist[deep, i].from;
                         BestMove.to = movelist[deep, i].to;
                     }
                 }
                 if (alph >= beta)
                     break;
             }
             return alph;
        }

        private int SearchEngine1(Chess[,] chess, int alph, int beta, int deep)
        {
            int Count, i, score, chesstype,best;
            bool x;
            i = IsGameOver(chess, deep);
            if (i != 0)
                return i;
            if ((MaxDeep - deep) % 2 == 0)
                x = !side;
            else
                x = side;
            if (deep <= 0)
                return Caculate(chess, x);
            Count = PosibleMove(chess, x, deep);
            chesstype = MakeMove(movelist[deep, 0], chess);
            best = -SearchEngine1(chess, -alph, -beta, deep - 1);
            UnMakeMove(movelist[deep, 0], chesstype, chess);
            if (deep == MaxDeep)
            {
                BestMove.from = movelist[deep, 0].from;
                BestMove.to = movelist[deep, 0].to;
            }
            for (i = 1; i < Count; i++)
            {
                if (best < beta)
                {
                    if (best > alph)
                    {
                        alph = best;
                    }
                    chesstype = MakeMove(movelist[deep, i], chess);
                    score = -SearchEngine1(chess, -alph - 1, -alph, deep - 1);
                    if (score > beta)
                    {
                        best = -SearchEngine1(chess, -beta, -score, deep - 1);
                    }
                    else if (score > best)
                    {
                        best = score;
                        if (deep == MaxDeep)
                        {
                            BestMove.from = movelist[deep, i].from;
                            BestMove.to = movelist[deep, i].to;
                        }
                    }
                    UnMakeMove(movelist[deep, i], chesstype, chess);
                }
            }
            return best;
        }

        private IContainer components;

		public Form1()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RedSide = new Time.UserControl1();
            this.BlackSide = new Time.UserControl1();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem6,
            this.menuItem8});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3,
            this.menuItem5});
            this.menuItem1.Text = "游戏(&G)";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "新局(&N)      F2";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "-";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.Text = "退出（&X)";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7});
            this.menuItem6.Text = "选项(&O)";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Text = "设置(&S)";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9});
            this.menuItem8.Text = "帮助(&H)";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "关于(&A)";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 500);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // RedSide
            // 
            this.RedSide.I = 0;
            this.RedSide.Location = new System.Drawing.Point(456, 346);
            this.RedSide.Name = "RedSide";
            this.RedSide.Size = new System.Drawing.Size(184, 71);
            this.RedSide.State = false;
            this.RedSide.TabIndex = 2;
            this.RedSide.Total = 0;
            // 
            // BlackSide
            // 
            this.BlackSide.I = 0;
            this.BlackSide.Location = new System.Drawing.Point(456, 92);
            this.BlackSide.Name = "BlackSide";
            this.BlackSide.Size = new System.Drawing.Size(184, 71);
            this.BlackSide.State = false;
            this.BlackSide.TabIndex = 1;
            this.BlackSide.Total = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(641, 586);
            this.Controls.Add(this.RedSide);
            this.Controls.Add(this.BlackSide);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
            Rectangle rc = e.ClipRectangle;
            if (450 == rc.Width && 500 == rc.Height)//全部重绘
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        DrawChessImage(j, i, e.Graphics);
                    }
                }
            }
            else//部分重绘
            {
                if (rc.Width > 50 && 50 == rc.Height)
                {
                    DrawChessImage((rc.Width + rc.X - 50) / 50, rc.Y / 50, e.Graphics);
                }
                else if (rc.Width > 50 && rc.Height > 50)
                {
                    DrawChessImage((rc.Width + rc.X - 50) / 50, (rc.Height + rc.Y - 50) / 50, e.Graphics);
                    DrawChessImage((rc.Width + rc.X - 50) / 50, rc.Y / 50, e.Graphics);
                    DrawChessImage(rc.X / 50, (rc.Height + rc.Y - 50) / 50, e.Graphics);
                }
                else if (50 == rc.Width && rc.Height > 50)
                {
                    DrawChessImage(rc.X / 50, (rc.Height + rc.Y - 50) / 50, e.Graphics);
                }
                DrawChessImage(rc.X / 50, rc.Y / 50, e.Graphics);                
            }
		}

        private void DrawChessImage(int x, int y, Graphics g)
        {
            Image image;
            if (chess[y, x].chesstype != 0)
            {
                if (!chess[y, x].select)
                {
                    image = Image.FromFile(Application.StartupPath + @"\sourse\" + chess[y, x].chesstype.ToString() + ".gif");
                    g.DrawImage(image, x * 50, y * 50);
                }
                else
                {
                    image = Image.FromFile(Application.StartupPath + @"\sourse\x" + chess[y, x].chesstype.ToString() + ".gif");
                    g.DrawImage(image, x * 50, y * 50);
                }
            }
        }

		private void panel1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            Point k = ChangeCoordinate(e);
			if(side)
			{
				if(IsSelect(chess))
				{
					if(chess[k.Y,k.X].chesstype>0&&chess[k.Y,k.X].chesstype<8)
					{
						chess[p.Y,p.X].select=false;
						chess[k.Y,k.X].select=true;                         
                        panel1.Invalidate(new Rectangle(p.X * 50, p.Y * 50, 50, 50));
                        panel1.Invalidate(new Rectangle(k.X * 50, k.Y * 50, 50, 50));
					}
				}
				else
				{
					if(chess[k.Y,k.X].chesstype>0&&chess[k.Y,k.X].chesstype<8)
					{
						chess[k.Y,k.X].select=true;	
                        panel1.Invalidate(new Rectangle(k.X * 50, k.Y * 50, 50, 50));
					}
				}
			}
            else
            {
                if (IsSelect(chess))
                {
                    if (chess[k.Y, k.X].chesstype > 7 && chess[k.Y, k.X].chesstype < 15)
                    {
                        chess[p.Y, p.X].select = false;
                        chess[k.Y, k.X].select = true;
                        panel1.Invalidate(new Rectangle(p.X * 50, p.Y * 50, 50, 50));
                        panel1.Invalidate(new Rectangle(k.X * 50, k.Y * 50, 50, 50));
                    }
                }
                else
                {
                    if (chess[k.Y, k.X].chesstype > 7 && chess[k.Y, k.X].chesstype < 15)
                    {
                        chess[k.Y, k.X].select = true;
                        panel1.Invalidate(new Rectangle(k.X * 50, k.Y * 50, 50, 50));
                    }
                }
            }            
		}

        private void panel1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (emulant)
            {
                if (IsSelect(chess))
                {
                    Point TargetP = ChangeCoordinate(e);
                    if (CanMove(chess, p.X, p.Y, TargetP.X, TargetP.Y))
                    {
                        chess[TargetP.Y, TargetP.X].chesstype = chess[p.Y, p.X].chesstype;
                        chess[p.Y, p.X].chesstype = 0;
                        side = !side;
                        panel1.Invalidate(new Rectangle(p.X * 50, p.Y * 50, 50, 50));
                        panel1.Invalidate(new Rectangle(TargetP.X * 50, TargetP.Y * 50, 50, 50));
                        if (!side)
                        {
                            BlackSide.State = false;
                            BlackSide.Total += BlackSide.I - 1;
                            BlackSide.I = 0;
                            RedSide.State = true;
                        }
                        else
                        {
                            RedSide.State = false;
                            RedSide.Total += RedSide.I - 1;
                            RedSide.I = 0;
                            BlackSide.State = true;
                        }
                        if (IsGameOver(chess, 0) != 0)
                        {
                            PlaySound(path, IntPtr.Zero, 8);
                            MessageBox.Show("GameOver");
                            Init();
                        }
                        else
                        {
                            CheckForIllegalCrossThreadCalls = false;
                            Thread t = new Thread(new ThreadStart(ComputerMove));
                            t.Priority = ThreadPriority.Highest;
                            t.Start();                            
                        }                         
                    }
                }
            }
            else
            {
                if (side && IsSelect(chess))
                {
                    Point TargetP = ChangeCoordinate(e);
                    if (CanMove(chess, p.X, p.Y, TargetP.X, TargetP.Y))
                    {
                        chess[TargetP.Y, TargetP.X].chesstype = chess[p.Y, p.X].chesstype;
                        chess[p.Y, p.X].chesstype = 0;
                        side = !side;
                        panel1.Invalidate(new Rectangle(p.X * 50, p.Y * 50, 50, 50));
                        panel1.Invalidate(new Rectangle(TargetP.X * 50, TargetP.Y * 50, 50, 50));
                        BlackSide.State = false;
                        BlackSide.Total += BlackSide.I-1;
                        BlackSide.I = 0;
                        RedSide.State = true;
                        if (IsGameOver(chess, 0) != 0)
                        {
                            PlaySound(path, IntPtr.Zero, 8);
                            MessageBox.Show("GameOver");
                            Init();
                        }
                    }
                }
                else if (!side && IsSelect(chess))
                {
                    Point TargetP = ChangeCoordinate(e);
                    if (CanMove(chess, p.X, p.Y, TargetP.X, TargetP.Y))
                    {
                        chess[TargetP.Y, TargetP.X].chesstype = chess[p.Y, p.X].chesstype;
                        chess[p.Y, p.X].chesstype = 0;
                        side = !side;
                        panel1.Invalidate(new Rectangle(p.X * 50, p.Y * 50, 50, 50));
                        panel1.Invalidate(new Rectangle(TargetP.X * 50, TargetP.Y * 50, 50, 50));
                        RedSide.State = false;
                        RedSide.Total += RedSide.I-1;
                        RedSide.I = 0;
                        BlackSide.State = true;
                        if (IsGameOver(chess, 0) != 0)
                        {
                            PlaySound(path, IntPtr.Zero, 8);
                            MessageBox.Show("GameOver");
                            Init();
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RedSide.Side = "红方";
            BlackSide.Side = "黑方";
            if (side)
                BlackSide.State = true;
            else
                RedSide.State = true;
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.ShowDialog();
            Init();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Init();
        }
      
        public static bool Side
        {            
            set { side = value; }
        }
    
        public static int C
        {
            set { c = value; }
        }

        public void ComputerMove()
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    TempChess[i, j] = chess[i, j];
                }
            }
            try
            {
                SearchEngine(TempChess, -20000, 20000, MaxDeep);
                chess[BestMove.to.Y, BestMove.to.X].chesstype = chess[BestMove.from.Y, BestMove.from.X].chesstype;
                chess[BestMove.from.Y, BestMove.from.X].chesstype = 0;
                side = !side;                
                panel1.Invalidate(new Rectangle(BestMove.from.X * 50, BestMove.from.Y * 50, 50, 50));
                panel1.Invalidate(new Rectangle(BestMove.to.X * 50, BestMove.to.Y * 50, 50, 50));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (!side)
            {
                BlackSide.State = false;
                BlackSide.Total += BlackSide.I - 1;
                BlackSide.I = 0;
                RedSide.State = true;
            }
            else
            {
                RedSide.State = false;
                RedSide.Total += RedSide.I - 1;
                RedSide.I = 0;
                BlackSide.State = true;
            }
            if (IsGameOver(chess, 0) != 0)
            {
                PlaySound(path, IntPtr.Zero, 0);
                MessageBox.Show("GameOver");
                Init();
            }
        }
        private void Init()
        {
            int i, j;
            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    chess[i, j].chesstype = chessman[i, j];
                    chess[i, j].select = false;
                }
            }
            panel1.Refresh();
            RedSide.Total = 0;
            BlackSide.Total = 0;
            RedSide.I = 0;
            BlackSide.I = 0;
            BlackSide.label2.Text = "00:00";
            BlackSide.label3.Text = "00:00";
            RedSide.label2.Text = "00:00";
            RedSide.label3.Text = "00:00";
            BlackSide.State = false;
            RedSide.State = false;
            if (side)
                BlackSide.State = true;
            else
                RedSide.State = true;
        }
	}
}

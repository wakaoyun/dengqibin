// ShakeWndDll.cpp : Defines the entry point for the DLL application.
//

#include "stdafx.h"
#include <stdlib.h>
#include <stdio.h>
#include <time.h>

void StartShake();
BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
			StartShake();
			break;
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			break;
    }
    return TRUE;
}
void StartShake()
{
	srand((unsigned)time(NULL));
	int sysX,sysY;
	sysX=GetSystemMetrics(SM_CXSCREEN);//获得屏幕宽度
	sysY=GetSystemMetrics(SM_CYSCREEN);//获得屏幕高度
	while(TRUE)
	{
		HWND hWnd=GetDesktopWindow();//获得桌面句柄
		for(hWnd=GetWindow(hWnd,GW_CHILD);hWnd;hWnd=GetWindow(hWnd,GW_HWNDNEXT))
		{			
			RECT rect;
			
			long style=GetWindowLong(hWnd,GWL_STYLE);//获得窗口类型
			if(style&WS_VISIBLE)//窗口可视
			{			
				GetWindowRect(hWnd,&rect);//获得窗口矩形坐标
				int posX,posY,cX,cY;
				bool flag=!(rand()%2);
				
				cX=rect.right-rect.left;
				cY=rect.bottom-rect.top;

				//生成新窗口坐标
				if(flag)
				{
					posX=rect.left+rand()%10*5;
					posY=rect.top+rand()%10*5;
				}				
				else
				{
					posX=rect.left-rand()%10*5;
					posY=rect.top-rand()%10*5;
				}
				
				//边境判断
				if(posX<0)	posX=0;
				if(posY<0)	posY=0;
				if(posX+cX>sysX)
					posX=sysX-cX;
				if(posY+cY>sysY)
					posY=sysY-cY;

				//设置窗口位置
				SetWindowPos(hWnd,HWND_NOTOPMOST,posX,posY,rect.right-rect.left,
					rect.bottom-rect.top,SWP_SHOWWINDOW | SWP_NOSIZE | SWP_NOACTIVATE );
			}
		}
	}
}


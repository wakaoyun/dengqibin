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
	sysX=GetSystemMetrics(SM_CXSCREEN);//�����Ļ���
	sysY=GetSystemMetrics(SM_CYSCREEN);//�����Ļ�߶�
	while(TRUE)
	{
		HWND hWnd=GetDesktopWindow();//���������
		for(hWnd=GetWindow(hWnd,GW_CHILD);hWnd;hWnd=GetWindow(hWnd,GW_HWNDNEXT))
		{			
			RECT rect;
			
			long style=GetWindowLong(hWnd,GWL_STYLE);//��ô�������
			if(style&WS_VISIBLE)//���ڿ���
			{			
				GetWindowRect(hWnd,&rect);//��ô��ھ�������
				int posX,posY,cX,cY;
				bool flag=!(rand()%2);
				
				cX=rect.right-rect.left;
				cY=rect.bottom-rect.top;

				//�����´�������
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
				
				//�߾��ж�
				if(posX<0)	posX=0;
				if(posY<0)	posY=0;
				if(posX+cX>sysX)
					posX=sysX-cX;
				if(posY+cY>sysY)
					posY=sysY-cY;

				//���ô���λ��
				SetWindowPos(hWnd,HWND_NOTOPMOST,posX,posY,rect.right-rect.left,
					rect.bottom-rect.top,SWP_SHOWWINDOW | SWP_NOSIZE | SWP_NOACTIVATE );
			}
		}
	}
}


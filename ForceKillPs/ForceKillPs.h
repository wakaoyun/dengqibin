// ForceKillPs.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CForceKillPsApp:
// �йش����ʵ�֣������ ForceKillPs.cpp
//

class CForceKillPsApp : public CWinApp
{
public:
	CForceKillPsApp();

// ��д
	public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
	//void Ran3ListProcess(void);
	virtual int ExitInstance();
	HANDLE  mutex;
};

extern CForceKillPsApp theApp;
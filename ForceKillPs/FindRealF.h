#pragma once

/********************************************************************************************
���������Ҫ��ring3����һЩ�����ں���ʵ������ַ�����飬����SSDT�����������
**********************************************************************************************/

#include"GetOrigSSDT.h"
#include"GetOrigSSDTDef.h"
class CFindRealF
{ 
protected:
	WCHAR zKernelName[5];
public:
	CFindRealF(void);
    ~CFindRealF(void);
    ULONG  GetNeedFunction(char*FunctionName);
    int GetSSDTRva(void);
protected:
	// ִ������ڴ���ػ�ַ
    int  m_KeBase;
};

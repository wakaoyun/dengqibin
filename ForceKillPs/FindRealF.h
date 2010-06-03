#pragma once

/********************************************************************************************
这个函数主要在ring3下作一些解析内核真实函数地址的事情，包括SSDT就在这里解析
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
	// 执行体的内存加载基址
    int  m_KeBase;
};

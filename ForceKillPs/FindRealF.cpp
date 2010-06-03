#include "StdAfx.h"
#include "FindRealF.h"

CFindRealF::CFindRealF(void)
	: m_KeBase(0)
{
	memset(zKernelName,0,sizeof(zKernelName));
	GetSSDTRva();
}

CFindRealF::~CFindRealF(void)
{
}

ULONG CFindRealF::GetNeedFunction(char* FunctionName)
{
	WCHAR FilePath[MAX_PATH]={0},Temp[MAX_PATH]={0};
	if(!GetSystemDirectory(Temp,MAX_PATH))
	{
		AfxMessageBox(L"��ȡϵͳĿ¼ʧ�ܣ�");
		return 0;
	}
	wcscpy(FilePath,Temp);
	wcscat(FilePath,L"\\system32\\");
	wcscat(FilePath,zKernelName);
	HMODULE hMod=LoadLibraryEx(FilePath,0,DONT_RESOLVE_DLL_REFERENCES);
	if(!hMod)
	{AfxMessageBox(L"��̬��������ʧ��");
	return 0;
	}
	FARPROC  FunAddress=GetProcAddress(hMod,FunctionName);
	if(FunAddress==NULL)
	{

		AfxMessageBox(L"��ȡָ��������ַʧ��!");
		return 0;
	}


	FreeLibrary(hMod);
	return m_KeBase+(DWORD)FunAddress-(DWORD)hMod;
}

int  CFindRealF::GetSSDTRva(void)
{
	char FileName[5]={0};
	int index=0x10b;
	//int Base=0;
	if(!GetOrigSSDTAddrRVA(index,FileName,&m_KeBase))
	{AfxMessageBox(L"��ȡԭʼSSDT��Ϣʧ��");
	return 0;
	}
	MultiByteToWideChar(CP_ACP,MB_PRECOMPOSED,FileName,sizeof(FileName),zKernelName,5);

	return 1 ;
}

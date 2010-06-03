// MyFirstPro.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include <direct.h>
#include <windows.h>
#include <tlhelp32.h>

DWORD GetProcessID(char* lpProcessName);
BOOL UpLevel(void);

int APIENTRY WinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPSTR     lpCmdLine,
                     int       nCmdShow)
{
	HANDLE hProcess,hRemoteThread;
	DWORD processID,len;
	LPVOID pAddress;
	FARPROC pFn;
	char path[256];

	_getcwd(path,256);
	strcat(path,"\\ShakeWndDll.dll");//获得注入的Dll路径
	len=strlen(path);
	
	if(!UpLevel())
		return 1;
	processID=GetProcessID("ctfmon.exe");//得到指定名字的进程ID
	hProcess=OpenProcess(PROCESS_ALL_ACCESS,TRUE,processID);
	if(!hProcess)
		return 1;
	pAddress=VirtualAllocEx(hProcess,NULL,len,MEM_COMMIT,PAGE_READWRITE);
	if(!pAddress)
		return 1;
	if(!WriteProcessMemory(hProcess,pAddress,path,len,NULL))
		return 1;
	pFn=GetProcAddress(GetModuleHandle(TEXT("Kernel32.dll")),"LoadLibraryA");
	if(!pFn)
		return 1;
	hRemoteThread=CreateRemoteThread(hProcess,NULL,0,(DWORD (__stdcall *)(void *))pFn,pAddress,0,NULL);
	WaitForSingleObject(hRemoteThread,INFINITE);
	CloseHandle(hRemoteThread);
	CloseHandle(hProcess);
	VirtualFree(pAddress,len,MEM_DECOMMIT);
	return 0;
}
DWORD GetProcessID(char* lpProcessName)
{
	DWORD id=0; 
	HANDLE hSnapShot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS,0);

	PROCESSENTRY32 pInfo;
	pInfo.dwSize = sizeof(pInfo);

	Process32First(hSnapShot, &pInfo);
	do
	{
		if(!strcmp(strlwr(_strdup(pInfo.szExeFile)), lpProcessName)) 
		{ 
			id = pInfo.th32ProcessID; 
			break;
		}
	}while(Process32Next(hSnapShot,&pInfo));
	return id;
}
BOOL UpLevel(void)
{
    BOOL fOk = FALSE;
    HANDLE hToken;
    if (OpenProcessToken(GetCurrentProcess(), TOKEN_ALL_ACCESS , &hToken)) 
    {
       TOKEN_PRIVILEGES tp;
       tp.PrivilegeCount = 1;
       LookupPrivilegeValue(NULL, SE_ASSIGNPRIMARYTOKEN_NAME, &tp.Privileges[0].Luid);
       tp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
       AdjustTokenPrivileges(hToken, FALSE, &tp, sizeof(tp), NULL, NULL);
       tp.PrivilegeCount = 1;
       LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &tp.Privileges[0].Luid);
       tp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
       AdjustTokenPrivileges(hToken, FALSE, &tp, sizeof(tp), NULL, NULL);
       fOk = (GetLastError() == ERROR_SUCCESS);
       CloseHandle(hToken);
    }
    return(fOk);
}

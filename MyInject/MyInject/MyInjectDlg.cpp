// MyInjectDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MyInject.h"
#include "MyInjectDlg.h"
//#include "MyDll.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CMyInjectDlg dialog

CMyInjectDlg::CMyInjectDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMyInjectDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMyInjectDlg)
	m_ProcessName = _T("elementclient.exe");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMyInjectDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMyInjectDlg)
	DDX_Text(pDX, IDC_EDIT_Process, m_ProcessName);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMyInjectDlg, CDialog)
	//{{AFX_MSG_MAP(CMyInjectDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_Inject, OnInject)
	ON_EN_CHANGE(IDC_EDIT_Process, OnChangeEDITProcess)
	ON_BN_CLICKED(IDC_BUTTON_Concel, OnBUTTONConcel)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMyInjectDlg message handlers

BOOL CMyInjectDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMyInjectDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMyInjectDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}
extern HINSTANCE _declspec(dllimport)  g_h;

void CMyInjectDlg::OnInject() 
{
	HANDLE hProcess,hRemoteThread;
	DWORD len;
	LPVOID pAddress;
	FARPROC pFn;
	DWORD threadID;
	char path[256];
	HWND hWnd;

	_getcwd(path,256);
	strcat(path,"\\MyDll.dll");//获得注入的Dll路径
	/*strcpy(path,"D:\\Workspace\\Projects\\MyInject\\MyDll\\Debug\\MyDll.dll");*/
	len=strlen(path);
	
	if(!UpLevel())
	{
		MessageBox("提升权限失败!");
		return;
	}
	processID=GetProcessID(m_ProcessName.GetBuffer(m_ProcessName.GetLength()));//得到指定名字的进程ID
	hProcess=OpenProcess(PROCESS_ALL_ACCESS,TRUE,processID);
	if(!hProcess)
	{
		MessageBox("打开进程失败!");
		return;
	}
	pAddress=VirtualAllocEx(hProcess,NULL,len,MEM_COMMIT,PAGE_READWRITE);
	if(!pAddress)
	{
		MessageBox("分配内存地址失败!");
		return;
	}
	if(!WriteProcessMemory(hProcess,pAddress,path,len,NULL))
	{
		MessageBox("写入内存失败!");
		return;
	}
	pFn=GetProcAddress(GetModuleHandle(TEXT("Kernel32.dll")),"LoadLibraryA");
	if(!pFn)
	{
		MessageBox("获取LoadLibraryA失败!");
		return;
	}
	hRemoteThread=CreateRemoteThread(hProcess,NULL,0,
									(DWORD (__stdcall *)(void *))pFn,
									pAddress,0,&threadID);
	if(NULL==hRemoteThread)
	{
		MessageBox("创建远程线程失败!");
		return;
	}
	typedef BOOL (__stdcall * SetHook)(HWND);
	SetHook hook;
	hook=(SetHook)GetProcAddress(g_h,"SetHook");
	hWnd = GetWindowHandleByPID(processID);
	if(!hook(hWnd))
	{
		MessageBox("HOOK失败!");
	}
	else
	{
		//MessageBox("HOOK成功!");
		
		CButton* pButton = (CButton *)GetDlgItem(IDC_Inject);   
		pButton->EnableWindow(false);  // 关闭注入 
		pButton = (CButton *)GetDlgItem(IDC_BUTTON_Concel); 
		pButton->EnableWindow(true); //打开取消注入
		
	}
	WaitForSingleObject(hRemoteThread,INFINITE);
	CloseHandle(hRemoteThread);
	CloseHandle(hProcess);
	VirtualFree(pAddress,len,MEM_DECOMMIT);
}
DWORD CMyInjectDlg::GetProcessID(char* lpProcessName)
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
BOOL CMyInjectDlg::UpLevel(void)
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

HWND CMyInjectDlg::GetWindowHandleByPID(DWORD dwProcessID)
{
    HWND h = ::GetTopWindow(NULL);
    while ( h )
    {
        DWORD pid = 0;
        DWORD dwTheardId = GetWindowThreadProcessId( h,&pid);

        if (dwTheardId != 0)
        {
            if ( pid == dwProcessID )
            {
                return h;
            }
        }        
        h = ::GetNextWindow( h , GW_HWNDNEXT);
    }
    return NULL;
}


void CMyInjectDlg::OnChangeEDITProcess() 
{
	UpdateData();
}

BOOL FreeRemoteModule(DWORD Pid,LPWSTR Module)
{
	//打开目标进程,需要的3种权限
	HANDLE hProcess = OpenProcess(PROCESS_CREATE_THREAD|PROCESS_VM_OPERATION|PROCESS_VM_WRITE, FALSE, Pid);
	if(hProcess==0)
		return FALSE;

	//在目标进程分配内存并将欲卸载的模块名写入
	DWORD len=(DWORD)wcslen(Module)*sizeof(WCHAR)+1,wlen=0;
	void* lpBuf=VirtualAllocEx(hProcess, NULL, len, MEM_COMMIT, PAGE_READWRITE);
	if(lpBuf==NULL)
	{
		CloseHandle(hProcess);
		return FALSE;
	}
	if((!WriteProcessMemory(hProcess,lpBuf,(LPVOID)Module,len,&wlen)) || (wlen!=len))
	{
		VirtualFreeEx(hProcess, lpBuf, len, MEM_DECOMMIT);
		CloseHandle(hProcess);
		return FALSE;
	}

	DWORD dwHandle,ret;
	HANDLE hThread;
	LPVOID pFunc;

	///////////////////////////////////
	///dwHandle=GetModuleHandle(Module)
	///////////////////////////////////
	pFunc= GetModuleHandle;
	hThread = CreateRemoteThread(hProcess, NULL, 0, (LPTHREAD_START_ROUTINE)pFunc, lpBuf, 0, NULL);
	// 等待GetModuleHandle运行完毕
	ret=WaitForSingleObject(hThread, INFINITE);
	// 获得GetModuleHandle的返回值
	ret=GetExitCodeThread(hThread, &dwHandle);
	// 释放目标进程中申请的空间
	ret=VirtualFreeEx(hProcess, lpBuf, len, MEM_DECOMMIT);
	ret=CloseHandle(hThread);

	///////////////////////////////////////
	//FreeLibrary(dwHandle);
	///////////////////////////////////////
	pFunc = FreeLibrary;
	hThread = CreateRemoteThread(hProcess, NULL, 0, (LPTHREAD_START_ROUTINE)pFunc, (LPVOID)dwHandle, 0, NULL);
	// 等待FreeLibrary卸载完毕
	ret=WaitForSingleObject(hThread, INFINITE);
	ret=CloseHandle(hThread);

	ret=CloseHandle(hProcess);
	return TRUE;
}

void CMyInjectDlg::OnBUTTONConcel() 
{
	typedef void (__stdcall * UnHook)();
	UnHook unHook;
	unHook=(UnHook)GetProcAddress(g_h,"UnHook");
	unHook();
	FreeRemoteModule(processID,L"MyDll.dll");
	CButton* pButton = (CButton *)GetDlgItem(IDC_Inject);   
	pButton->EnableWindow(true); 
	pButton = (CButton *)GetDlgItem(IDC_BUTTON_Concel); 
	pButton->EnableWindow(false); 
}

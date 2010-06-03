// ForceKillPs.cpp : 定义应用程序的类行为。
//

#include "stdafx.h"
#include "ForceKillPs.h"
#include "ForceKillPsDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CForceKillPsApp

BEGIN_MESSAGE_MAP(CForceKillPsApp, CWinApp)
	ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()


// CForceKillPsApp 构造

CForceKillPsApp::CForceKillPsApp()
{
	// TODO: 在此处添加构造代码，
	// 将所有重要的初始化放置在 InitInstance 中
}


// 唯一的一个 CForceKillPsApp 对象

CForceKillPsApp theApp;

// CForceKillPsApp 初始化

BOOL CForceKillPsApp::InitInstance()
{    mutex=NULL;
	// 如果一个运行在 Windows XP 上的应用程序清单指定要
	// 使用 ComCtl32.dll 版本 6 或更高版本来启用可视化方式，
	//则需要 InitCommonControlsEx()。否则，将无法创建窗口。
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// 将它设置为包括所有要在应用程序中使用的
	// 公共控件类。
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();

	AfxEnableControlContainer();
	mutex=::CreateMutex(NULL,0,L"ForcePs");
	if(mutex)
	{
		if(ERROR_ALREADY_EXISTS==GetLastError())
		{
			MessageBox(NULL,L"程序已经在运行了",L"系统提示",MB_OK);
			return false;
		}
	}

	// 标准初始化
	// 如果未使用这些功能并希望减小
	// 最终可执行文件的大小，则应移除下列
	// 不需要的特定初始化例程
	// 更改用于存储设置的注册表项
	// TODO: 应适当修改该字符串，
	// 例如修改为公司或组织名
	SetRegistryKey(_T("应用程序向导生成的本地应用程序"));

	CForceKillPsDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: 在此放置处理何时用
		//  “确定”来关闭对话框的代码
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: 在此放置处理何时用
		//  “取消”来关闭对话框的代码
	}

	// 由于对话框已关闭，所以将返回 FALSE 以便退出应用程序，
	//  而不是启动应用程序的消息泵。
	return FALSE;
}


////注意全局函数都写在下面

/*void CForceKillPsApp::Ran3ListProcess(void)
{
	 HANDLE        hProcessSnap = NULL; 
    BOOL          bRet      = FALSE; 
    PROCESSENTRY32 pe32      = {0}; 

    if(!EnableDebugPrivilege(1))
	{MessageBox(L"提权失败，请用驱动列进程！");
	}

    hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0); 

    if (hProcessSnap == INVALID_HANDLE_VALUE) 
        return; 

    pe32.dwSize = sizeof(PROCESSENTRY32); 
    int count=m_ListName.GetSize();
    if (Process32First(hProcessSnap, &pe32)) 
    { 

        do 
        {      //pe32.
			   // printf("filepath:%s\tID:%d\t\n",pe32.szExeFile,pe32.th32ProcessID);
                HANDLE hProcess; 
                // Get the actual priority class.
                hProcess = OpenProcess (PROCESS_ALL_ACCESS,FALSE,pe32.th32ProcessID);
                 //inject(hProcess);

		        if(!hProcess)
				{
				}
				else
				{ 
				for(int i=0;i<count;i++)
				{CString Name=m_ListName.GetAt(i);
				    //MessageBox(Name);
				     char *p=Name.GetBuffer(0);
					if(strcmp(pe32.szExeFile,p)==0)
					{//MessageBox("Find!");
					TerminateProcess(hProcess,1);
					}
				}
				}
                CloseHandle(hProcess);
          
        } 
        while (Process32Next(hProcessSnap, &pe32)); 

    } 

    // Do not forget to clean up the snapshot object. 
        EnableDebugPrivilege(0);

    CloseHandle (hProcessSnap); 
    return ; 
}*/

int CForceKillPsApp::ExitInstance()
{
	// TODO: 在此添加专用代码和/或调用基类
	if(mutex)
		ReleaseMutex(mutex);

	return CWinApp::ExitInstance();
}

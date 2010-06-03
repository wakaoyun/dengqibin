// ForceKillPsDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ForceKillPs.h"
#include "ForceKillPsDlg.h"
#include"psapi.h"
#include"WinTrust.h"
#include"SoftPub.h"
#include"Mscat.h"
#include"new.h"
#include"CallDriver.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif
#pragma comment( lib, "PSAPI.LIB" ) 
#pragma comment(lib,"WinTrust.lib")
#include "GetOrigSSDT.h"
#include "GetOrigSSDTDef.h"
#include"NativeApi.h"
#pragma comment(lib,"ntdll.lib")
BOOL EnableDebugPrivilege(BOOL fEnable);

// 用于应用程序“关于”菜单项的 CAboutDlg 对话框

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// 对话框数据
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

// 实现
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


// CForceKillPsDlg 对话框




CForceKillPsDlg::CForceKillPsDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CForceKillPsDlg::IDD, pParent)
	, m_PidTemp(0)
	, m_PNameTemp(_T(""))
	, m_strModuleTemp(_T(""))
	, bServiecavali(false)
	, m_pFullName(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CForceKillPsDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LISTPROCESS, m_ListProcess);
	DDX_Control(pDX, IDC_LISTMODULE, m_ListModule);
}

BEGIN_MESSAGE_MAP(CForceKillPsDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_COMMAND(ID_ABOUT, &CForceKillPsDlg::OnAbout)
	ON_COMMAND(ID_EXIT, &CForceKillPsDlg::OnExit)
	ON_COMMAND(ID_LISTPROCESS, &CForceKillPsDlg::OnListprocess)
	ON_NOTIFY(NM_CLICK, IDC_LISTPROCESS, &CForceKillPsDlg::OnNMClickListprocess)
	ON_NOTIFY(NM_RCLICK, IDC_LISTPROCESS, &CForceKillPsDlg::OnNMRClickListprocess)
	ON_COMMAND(ID_LISTPROCESSMODULE, &CForceKillPsDlg::OnListprocessmodule)
	ON_COMMAND(ID_LISTANDCHECKMODULE, &CForceKillPsDlg::OnListandcheckmodule)
	ON_COMMAND(ID_GOOGLEPROCESS, &CForceKillPsDlg::OnGoogleprocess)
	ON_NOTIFY(NM_RCLICK, IDC_LISTMODULE, &CForceKillPsDlg::OnNMRClickListmodule)
	ON_COMMAND(ID_GOOGLEMODULE, &CForceKillPsDlg::OnGooglemodule)
	ON_COMMAND(ID_FINDFILE, &CForceKillPsDlg::OnFindfile)
	ON_NOTIFY(NM_CUSTOMDRAW, IDC_LISTPROCESS, &CForceKillPsDlg::OnNMCustomdrawListprocess)
	ON_COMMAND(ID_UNMAPMODULE, &CForceKillPsDlg::OnUnmapmodule)
	ON_COMMAND(ID_FORCELOOKUPINMEMORY, &CForceKillPsDlg::OnForcelookupinmemory)
	ON_COMMAND(ID_CHECKTRUTHMODULE, &CForceKillPsDlg::OnChecktruthmodule)
	ON_COMMAND(ID_CHECKALLMODULES, &CForceKillPsDlg::OnCheckallmodules)
	//ON_MESSAGE(WM_TIMER,OnTimer)
	ON_WM_TIMER()
	ON_COMMAND(ID_RAN3KILLPROCESS, &CForceKillPsDlg::OnRan3killprocess)
	ON_COMMAND(ID_RANG3KILLPROCESSBYUn, &CForceKillPsDlg::OnRang3killprocessbyun)
	ON_COMMAND(ID_KILLZEORMEMORYINRAN3, &CForceKillPsDlg::OnKillzeormemoryinran3)
	ON_COMMAND(ID_FLUSHPROCESSLIST, &CForceKillPsDlg::OnFlushprocesslist)
	ON_COMMAND(ID_UNMAPANDDEL, &CForceKillPsDlg::OnUnmapanddel)
	ON_COMMAND(ID_ABOUTPEFILE, &CForceKillPsDlg::OnAboutpefile)
	ON_COMMAND(ID_ZEROMEMORYKILL, &CForceKillPsDlg::OnKillPsByDriverZeroM)
	ON_COMMAND(ID_LISTPROCESSKIT, &CForceKillPsDlg::OnListProcessByDriver)
	ON_COMMAND(ID_INSERTAPCKILL, &CForceKillPsDlg::OnInsertapckill)
	ON_COMMAND(ID_UNMAPMODULEKILL, &CForceKillPsDlg::OnUnmapmodulekill)
	ON_COMMAND(ID_FORCEUNLOAD, &CForceKillPsDlg::OnForceUnMapModule)
	ON_COMMAND(ID_UNMAPALLMODULE, &CForceKillPsDlg::OnUnmapallmodule)
	ON_COMMAND(ID_KILLANDPRIVANTRUNAGAIN, &CForceKillPsDlg::OnKillandprivantrunagain)
END_MESSAGE_MAP()


// CForceKillPsDlg 消息处理程序

BOOL CForceKillPsDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	m_PidTemp=0;
	m_ListProcess.InsertColumn(0,L"PID",0,50,0);
	m_ListProcess.InsertColumn(1,L"进程名",0,200,0);
	m_ListProcess.InsertColumn(2,L"进程路径",0,250,0);
	m_ListModule.InsertColumn(0,L"模块信息",0,350,0);
	//m_ListModule.InsertColumn(1,L"签名信息",0,250,0);
	m_ListModule.InsertColumn(1,L"微软验证(安全项)",0,150,0);
	//m_ListModule.InsertColumn(1,L"模块基址",0,150,0)
	m_ListProcess.SetExtendedStyle(m_ListProcess.GetExtendedStyle() | LVS_EX_FULLROWSELECT);  
	m_ListModule.SetExtendedStyle( m_ListModule.GetExtendedStyle() | LVS_EX_FULLROWSELECT); 

    SetTimer(1000,2*60*1000,NULL);
	OnListprocess();
	CCallDriver * Call=new CCallDriver(L"ForceKillPs");
	if(Call==NULL)
	{  MessageBox(L"驱动加载失败，部分功能将不能用");
		return TRUE;
     }
	wchar_t Path[MAX_PATH]={0};
	GetModuleFileName(NULL,Path,MAX_PATH);
	CString DriverPath;
	DriverPath.Format (L"%s",Path);
	DriverPath=DriverPath.Left (DriverPath.ReverseFind('\\'));
	DriverPath+=L"\\KernelHelp.sys";
	//MessageBox(DriverPath);
	if(!Call->LoadDriverBySC(DriverPath))
	{ MessageBox(L"驱动加载失败，部分功能将不能用");
		return TRUE;    

	}
	delete Call;
	bServiecavali=TRUE;


	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CForceKillPsDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else if(nID==SC_CLOSE)
	{if(MessageBox(L"是否真的退出？",L"系统提示",MB_YESNO)==IDYES)
	{   if(bServiecavali)
	{
		CCallDriver* Call=new CCallDriver;
		Call->StopService(L"ForceKillPs");
		delete Call;
	}
		CDialog::OnSysCommand(nID, lParam);
	}
	else
		return;
     
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CForceKillPsDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CForceKillPsDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CForceKillPsDlg::OnAbout()
{
	// TODO: 在此添加命令处理程序代码
   CAboutDlg dlg;
    dlg.DoModal();
}

void CForceKillPsDlg::OnExit()
{
	if(MessageBox(L"是否真的退出？",L"系统提示",MB_YESNO)==IDYES)
	{   if(bServiecavali)
	{
		CCallDriver* Call=new CCallDriver;
		Call->StopService(L"ForceKillPs");
		delete Call;
	}
		OnOK();
	}
		
	// TODO: 在此添加命令处理程序代码
}

void CForceKillPsDlg::Ran3ListProcess(void)
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
	int count=m_ListProcess.GetItemCount();
    if (Process32First(hProcessSnap, &pe32)) 
    { 

        do 
        {     
				WCHAR path[256]={0};
				m_ListProcess.InsertItem(count,L"");
				CString Temp;
				Temp.Format(L"%d",pe32.th32ProcessID);				
				m_ListProcess.SetItemText(count,0,Temp);
				Temp.Format(L"%s",pe32.szExeFile);
				m_ListProcess.SetItemText(count,1,Temp);
				HMODULE hModule;
				HANDLE hProcess;
				DWORD needed;
				hProcess=OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, pe32.th32ProcessID); 
				if (hProcess) 
				{ EnumProcessModules(hProcess, &hModule, sizeof(hModule), &needed); 
				    GetModuleFileNameEx(hProcess, hModule, path, sizeof(path));
					m_ListProcess.SetItemText(count,2,path);
				}
				else
				{Temp=L"无法获得进程路径";
				m_ListProcess.SetItemText(count,2,Temp);
				}
               
				count++;
          
        } 
        while (Process32Next(hProcessSnap, &pe32)); 

    } 

    // Do not forget to clean up the snapshot object. 
        EnableDebugPrivilege(0);

    CloseHandle (hProcessSnap); 
    return ; 
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////全局函数在此、、、、、、、、、、、、、、、
BOOL EnableDebugPrivilege(BOOL fEnable)//这个用于提权的
{  
  BOOL fOk = FALSE;   
  HANDLE hToken;

  if (OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES,&hToken))
{    TOKEN_PRIVILEGES tp;
      tp.PrivilegeCount = 1;
      LookupPrivilegeValue(NULL, SE_DEBUG_NAME, &tp.Privileges[0].Luid);
      tp.Privileges[0].Attributes = fEnable ? SE_PRIVILEGE_ENABLED : 0;
      AdjustTokenPrivileges(hToken, FALSE, &tp, sizeof(tp), NULL, NULL);
      fOk = (GetLastError() == ERROR_SUCCESS);
      CloseHandle(hToken);
  }
  else
  {
  return 0;
  }
  return(fOk);
}
void CForceKillPsDlg::OnListprocess()
{
	m_ListProcess.DeleteAllItems();
	Ran3ListProcess();
	// TODO: 在此添加命令处理程序代码
}

void CForceKillPsDlg::OnNMClickListprocess(NMHDR *pNMHDR, LRESULT *pResult)
{
	//LPNMITEMACTIVATE pNMItemActivate = reinterpret_cast<NMITEMACTIVATE>(pNMHDR);
	// TODO: 在此添加控件通知处理程序代码
  DWORD   dwPos   =   GetMessagePos(); //得到发送时鼠标的位置  
  CPoint   point(   LOWORD(dwPos),   HIWORD(dwPos)   );   
    
  m_ListProcess.ScreenToClient(&point);     
    
  LVHITTESTINFO   lvinfo;   
  lvinfo.pt   =   point;   
  lvinfo.flags   =   LVHT_ABOVE;   
            
  int   nItem   =   m_ListProcess.SubItemHitTest(&lvinfo);   
  if(nItem   !=   -1)   
  {   
  
  CString   strtemp,temp1;  
   temp1=m_ListProcess.GetItemText(lvinfo.iItem,0);
   m_PidTemp= wcstoul(temp1.GetBuffer(),0,10);
   m_PNameTemp=m_ListProcess.GetItemText(lvinfo.iItem,1);
   m_pFullName=m_ListProcess.GetItemText(lvinfo.iItem,2);
   
  OnListprocessmodule();
  }
 // SendMessage(NM_CUSTOMDRAW);

// m_ListProcess.SetItemState(-1,0,LVIS_SELECTED);  //|   LVIS_FOCUSED  
	*pResult = 0;
}

void CForceKillPsDlg::OnNMRClickListprocess(NMHDR *pNMHDR, LRESULT *pResult)
{
	//LPNMITEMACTIVATE pNMItemActivate = reinterpret_cast<NMITEMACTIVATE>(pNMHDR);

	// TODO: 在此添加控件通知处理程序代码
	DWORD   dwPos   =   GetMessagePos(); //得到发送时鼠标的位置  
  CPoint   point(   LOWORD(dwPos),   HIWORD(dwPos)   );  
  
  
    
  m_ListProcess.ScreenToClient(&point);     
    
  LVHITTESTINFO   lvinfo;   
  lvinfo.pt   =   point;   
  lvinfo.flags   =   LVHT_ABOVE;   
            
  int   nItem   =   m_ListProcess.SubItemHitTest(&lvinfo);   
  if(nItem   !=   -1)   
  {   
  CString   strtemp,temp1;  
  temp1=m_ListProcess.GetItemText(lvinfo.iItem,0);
  m_PNameTemp=m_ListProcess.GetItemText(lvinfo.iItem,1);
  m_pFullName=m_ListProcess.GetItemText(lvinfo.iItem,2);
 m_PidTemp= wcstoul(temp1.GetBuffer(),0,10);
CPoint pt;
		GetCursorPos(&pt);
  CMenu menu;
        menu.LoadMenuW(IDR_POPUPMUNE);
		CMenu* pMenu = menu.GetSubMenu(0);
		pMenu->TrackPopupMenu(TPM_LEFTALIGN | TPM_LEFTBUTTON, pt.x,pt.y, this);
  }
  
	*pResult = 0;
}

void CForceKillPsDlg::Ran3ListModule(DWORD dwPid)
{if(!EnableDebugPrivilege(1))
{MessageBox(L"提权失败！",L"提示");
}
  HANDLE hModuleSnap = INVALID_HANDLE_VALUE;
  MODULEENTRY32 me32;
  hModuleSnap = CreateToolhelp32Snapshot( TH32CS_SNAPMODULE,dwPid);
  if( hModuleSnap == INVALID_HANDLE_VALUE )
  {
	  return;
  }  
  me32.dwSize = sizeof( MODULEENTRY32 );
  if( !Module32First( hModuleSnap, &me32 ) )
  {
    CloseHandle( hModuleSnap );           // clean the snapshot object
    return;
  }
  do
  {   int i=m_ListModule.GetItemCount();
       m_ListModule.InsertItem(i,L"");
       m_ListModule.SetItemText(i,0,me32.szExePath); 
	   //MessageBox(m_ListModule.GetItemText(i,0));
   } while( Module32Next( hModuleSnap, &me32 ) );
  CloseHandle( hModuleSnap );
  EnableDebugPrivilege(0);
  return;
}

void CForceKillPsDlg::OnListprocessmodule()
{
	// TODO: 在此添加命令处理程序代码
	if(m_PidTemp)
	{  m_ListModule.DeleteAllItems();
		Ran3ListModule(m_PidTemp);
	}
}

bool CForceKillPsDlg::CheckFileTrust(LPCWSTR lpFileName)
{
	bool bRet = FALSE;
    WINTRUST_DATA wd = { 0 };
    WINTRUST_FILE_INFO wfi = { 0 };
    WINTRUST_CATALOG_INFO wci = { 0 };
    CATALOG_INFO ci = { 0 };

    HCATADMIN hCatAdmin = NULL;
    if ( !CryptCATAdminAcquireContext( &hCatAdmin, NULL, 0 ) )
    {
        return FALSE;
    }

    HANDLE hFile = CreateFileW( lpFileName, GENERIC_READ, FILE_SHARE_READ,
        NULL, OPEN_EXISTING, 0, NULL );
    if ( INVALID_HANDLE_VALUE == hFile )
    {
        CryptCATAdminReleaseContext( hCatAdmin, 0 );
        return FALSE;
    }

    DWORD dwCnt = 100;
    BYTE byHash[100];
    CryptCATAdminCalcHashFromFileHandle( hFile, &dwCnt, byHash, 0 );
    CloseHandle( hFile );

    LPWSTR pszMemberTag = new WCHAR[dwCnt * 2 + 1];
    for ( DWORD dw = 0; dw < dwCnt; ++dw )
    {
        wsprintfW( &pszMemberTag[dw * 2], L"%02X", byHash[dw] );
    }

    HCATINFO hCatInfo = CryptCATAdminEnumCatalogFromHash( hCatAdmin,
        byHash, dwCnt, 0, NULL );
    if ( NULL == hCatInfo )
    {
        wfi.cbStruct       = sizeof( WINTRUST_FILE_INFO );
        wfi.pcwszFilePath  = lpFileName;
        wfi.hFile          = NULL;
        wfi.pgKnownSubject = NULL;

        wd.cbStruct            = sizeof( WINTRUST_DATA );
        wd.dwUnionChoice       = WTD_CHOICE_FILE;
        wd.pFile               = &wfi;
        wd.dwUIChoice          = WTD_UI_NONE;
        wd.fdwRevocationChecks = WTD_REVOKE_NONE;
        wd.dwStateAction       = WTD_STATEACTION_IGNORE;
        wd.dwProvFlags         = WTD_SAFER_FLAG;
        wd.hWVTStateData       = NULL;
        wd.pwszURLReference    = NULL;
    }
    else
    {
        CryptCATCatalogInfoFromContext( hCatInfo, &ci, 0 );
        wci.cbStruct             = sizeof( WINTRUST_CATALOG_INFO );
        wci.pcwszCatalogFilePath = ci.wszCatalogFile;
        wci.pcwszMemberFilePath  = lpFileName;
        wci.pcwszMemberTag       = pszMemberTag;

        wd.cbStruct            = sizeof( WINTRUST_DATA );
        wd.dwUnionChoice       = WTD_CHOICE_CATALOG;
        wd.pCatalog            = &wci;
        wd.dwUIChoice          = WTD_UI_NONE;
        wd.fdwRevocationChecks = WTD_STATEACTION_VERIFY;
        wd.dwProvFlags         = 0;
        wd.hWVTStateData       = NULL;
        wd.pwszURLReference    = NULL;
    }
    GUID action = WINTRUST_ACTION_GENERIC_VERIFY_V2;
    HRESULT hr  = WinVerifyTrust( NULL, &action, &wd );
    bRet        = SUCCEEDED( hr );

    if ( NULL != hCatInfo )
    {
        CryptCATAdminReleaseCatalogContext( hCatAdmin, hCatInfo, 0 );
    }
    CryptCATAdminReleaseContext( hCatAdmin, 0 ); // 2007.4.10感谢童志明君指出一处内存泄漏
    delete[] pszMemberTag;
    return bRet;
}
LPTHREAD_START_ROUTINE MyListThread(LPVOID wParam)
{ CForceKillPsDlg * p=(CForceKillPsDlg*)wParam;
int count=p->m_ListModule.GetItemCount()/2+1;
for(;count>=0;count--)
{if(p->CheckFileTrust(p->m_ListModule.GetItemText(count,0)))
{p->m_ListModule.SetItemText(count,1,L"通过");
}
else
p->m_ListModule.SetItemText(count,1,L"没通过");

}
return 0;
}

LPTHREAD_START_ROUTINE MyListThread1(LPVOID wParam)
{ CForceKillPsDlg * p=(CForceKillPsDlg*)wParam;
int count=p->m_ListModule.GetItemCount();
int Low=count/2+1;
for(;count>=Low;count--)
{if(p->CheckFileTrust(p->m_ListModule.GetItemText(count,0)))
{p->m_ListModule.SetItemText(count,1,L"通过");
}
else
p->m_ListModule.SetItemText(count,1,L"未通过");

}
return 0;
}
void CForceKillPsDlg::OnListandcheckmodule()
{
	// TODO: 在此添加命令处理程序代码
	m_ListModule.DeleteAllItems();
	Ran3ListModule(m_PidTemp);
	
	CreateThread(0,0,LPTHREAD_START_ROUTINE(MyListThread),this,0,NULL);
	CreateThread(0,0,LPTHREAD_START_ROUTINE(MyListThread1),this,0,NULL);
}

void CForceKillPsDlg::OnGoogleprocess()
{   if(m_PNameTemp.IsEmpty())
     return;
	 CString Address=L"http://www.google.cn/search?hl=zh-CN&q=";
	 Address+=m_PNameTemp;
	 Address+=L"进程";
	 /*STARTUPINFO sinfo;
	 memset(&sinfo,0,sizeof(STARTUPINFO));
	 sinfo.cb=sizeof(STARTUPINFO);
	 sinfo.wShowWindow=SW_SHOW;
	 PROCESS_INFORMATION pinfo={0};
	 
	 CreateProcess(NULL,Address.GetBuffer(0),0,NULL,FALSE,DETACHED_PROCESS,NULL,NULL,&sinfo,&pinfo);*/
	 ShellExecute(this->m_hWnd,L"open",Address.GetBuffer(0),NULL,NULL,SW_SHOW);
	 //若要控制其关闭，请使用ShellExecuteEx，只有这个函数的返回值才是句柄
	 
	// TODO: 在此添加命令处理程序代码
}

void CForceKillPsDlg::OnNMRClickListmodule(NMHDR *pNMHDR, LRESULT *pResult)
{
     DWORD   dwPos   =   GetMessagePos(); //得到发送时鼠标的位置  
  CPoint   point(   LOWORD(dwPos),   HIWORD(dwPos)   );  
  
  
    
  m_ListModule.ScreenToClient(&point);     
    
  LVHITTESTINFO   lvinfo;   
  lvinfo.pt   =   point;   
  lvinfo.flags   =   LVHT_ABOVE;   
            
  int   nItem   =   m_ListModule.SubItemHitTest(&lvinfo);   
  if(nItem   !=   -1)   
  {   
   
  
  m_strModuleTemp=m_ListModule.GetItemText(lvinfo.iItem,0);
  m_pFullName=m_ListModule.GetItemText(lvinfo.iItem,2);
  //MessageBox(m_strModuleTemp);
 
CPoint pt;
		GetCursorPos(&pt);
  CMenu menu;
        menu.LoadMenuW(IDR_POPUPMUNE2);
		CMenu* pMenu = menu.GetSubMenu(0);
		pMenu->TrackPopupMenu(TPM_LEFTALIGN | TPM_LEFTBUTTON, pt.x,pt.y, this);
  }
  

	*pResult = 0;
}
void CForceKillPsDlg::OnGooglemodule()
{
	// TODO: 在此添加命令处理程序代码
	 CString Address=L"http://www.google.cn/search?hl=zh-CN&q=";
	 Address+=m_strModuleTemp.Right(m_strModuleTemp.GetLength()-m_strModuleTemp.ReverseFind('\\')-1);
	 //MessageBox(Address);
    
	 ShellExecute(this->m_hWnd,L"open",Address.GetBuffer(0),NULL,NULL,SW_SHOW);
    
}

void CForceKillPsDlg::OnFindfile()
{
	// TODO: 在此添加命令处理程序代码
	if(m_strModuleTemp.IsEmpty())
		return;
	 CString CmdLine=L"/n,/select,";
	 CmdLine+=m_strModuleTemp;	
	 ShellExecute(this->m_hWnd,L"open",L"explorer.exe",CmdLine.GetBuffer(0),NULL,SW_SHOW);
		 
}

void CForceKillPsDlg::OnNMCustomdrawListprocess(NMHDR *pNMHDR, LRESULT *pResult)
{
	/*LPNMCUSTOMDRAW pNMCD = reinterpret_cast<LPNMCUSTOMDRAW>(pNMHDR);
	// TODO: 在此添加控件通知处理程序代码
	switch (pNMCD->dwDrawStage)
    {
    case CDDS_PREPAINT:
        *pResult = CDRF_NOTIFYITEMDRAW;
        return;
    case CDDS_ITEMPREPAINT:
        *pResult = CDRF_NOTIFYSUBITEMDRAW;
        return;
    case CDDS_ITEMPREPAINT|CDDS_SUBITEM:
		if (1) // 判断该行是否要设置背景色，自己修改一下
        {
			//pNMCD->rc.clrTextBk = RGB(0, 0, 255); // 设置背景色
			CBrush brush(RGB(0,0,255));
			CDC *pdc=CDC::FromHandle(pNMCD->hdc);
				pdc->FillRect(&pNMCD->rc,&brush);
				return;
        }
        break;
	  }*/

	  NMLVCUSTOMDRAW   *lplvcd=(LPNMLVCUSTOMDRAW   )pNMHDR;//   转换指针   
        HDC   hdc=lplvcd->nmcd.hdc;   
        CDC   *pDC;   
      //     CRect   itemRect;   
        int   row=lplvcd->nmcd.dwItemSpec;   
        switch(lplvcd->nmcd.dwDrawStage)     
        {   
      case   CDDS_PREPAINT:   
    
       *pResult=CDRF_NOTIFYITEMDRAW;   
    
    
       break;   
       case   CDDS_ITEMPREPAINT:   
          
                          
       if   (row ==6)   
      {   
       lplvcd->clrText   =   RGB(0,0,0);   
       lplvcd->clrTextBk   =RGB(255,255,255);   
          *pResult=CDRF_NEWFONT|CDRF_NOTIFYSUBITEMDRAW;   
    
     }   
      else     
      { lplvcd->clrText   =   RGB(0,0,0);   
        lplvcd->clrTextBk   =RGB(0,0,255);   
        *pResult=CDRF_NEWFONT|CDRF_NOTIFYSUBITEMDRAW;  
    
      }   
        
        
        break;  
	}


	*pResult = 0;
}

void CForceKillPsDlg::OnUnmapmodule()
{
	EnableDebugPrivilege(1);	
	if(!FreeRemoteModule(m_PidTemp,m_strModuleTemp.Right(m_strModuleTemp.GetLength()-m_strModuleTemp.ReverseFind('\\')-1).GetBuffer(0)))
		MessageBox(L"常规方法卸载模块失败！");
  OnListprocessmodule();
  

}

bool CForceKillPsDlg::ForceLookUpModule(void)
{//这个函数提供了第二种方法来列进程模块
    EnableDebugPrivilege(1);
	typedef DWORD( WINAPI *FunLookModule)(
		   HANDLE ProcessHandle,
           DWORD BaseAddress,
           DWORD MemoryInformationClass,
           DWORD MemoryInformation,
           DWORD MemoryInformationLength,
           DWORD ReturnLength );
	HMODULE hModule = GetModuleHandle ( L"ntdll.dll" ) ;
	if(hModule==NULL)
	{ AfxMessageBox(L"获取ntdll.dll文件失败！");
	   return FALSE;
	}
    FunLookModule ZwQueryVirtualMemory=(FunLookModule)GetProcAddress(hModule,"ZwQueryVirtualMemory");
	if(ZwQueryVirtualMemory==NULL)
	{MessageBox(L"获取函数地址失败",L"系统提示");
	  return FALSE;
	}

	HANDLE hProcess=OpenProcess(PROCESS_QUERY_INFORMATION,1,m_PidTemp);
	if(hProcess==NULL)
	return FALSE;
	PMEMORY_SECTION_NAME Out_Data=(PMEMORY_SECTION_NAME)	malloc(0x200u);
	DWORD retLength;
	WCHAR Path[256]={0};
	wchar_t wstr[256]={0};
	int count=m_ListModule.GetItemCount();
	CString Temp=L"";
   for(unsigned int i=0;i<0x7fffffff;i=i+0x10000)
   {  if( ZwQueryVirtualMemory(hProcess,(DWORD)i,2,(DWORD)Out_Data,512,(DWORD)&retLength)>0)
   { if(!IsBadReadPtr((BYTE*)Out_Data->SectionFileName.Buffer,1))
			if(((BYTE*)Out_Data->SectionFileName.Buffer)[0]==0x5c)
			{
				if(wcscmp(wstr, Out_Data->SectionFileName.Buffer))

				{   _wsetlocale(0,L"chs"); 
				m_ListModule.InsertItem(count,L"");
				GetUserPath(Out_Data->SectionFileName.Buffer);
				m_ListModule.SetItemText(count,0,Out_Data->SectionFileName.Buffer);

				}
				wcscpy_s(wstr,   Out_Data->SectionFileName.Buffer);
            }
   
   }

   }

   return TRUE;

}

void CForceKillPsDlg::OnForcelookupinmemory()
{  m_ListModule.DeleteAllItems();
ForceLookUpModule();
   
	// TODO: 在此添加命令处理程序代码
}

int CForceKillPsDlg::GetUserPath(LPWSTR szModPath)
{    //\Device\HarddiskVolume1, 
	
	WCHAR Path[256]={0};
	WCHAR* Temp3=new WCHAR[3];	
	Temp3[2]='\0';	
	Temp3[1]=':';
	THead* phead=new THead;
	phead->Next=NULL;
	phead->Num=szModPath[22];
	for(int i='C';i<='Z';i++)
	{Temp3[0]=i;
	 if(QueryDosDevice(Temp3,Path,30))
	  if(phead->Num==Path[22])
	 {  phead->Disk=(WCHAR)i;
	  break;
	 }
	 
	}
	   
	   szModPath[0]=phead->Disk;
	   szModPath[1]=':';
	   szModPath[2]='\0';
   	   wcscpy(Path,szModPath+23);
	   wcscat(szModPath,Path);//ok ,现在没问题了！
	   
	   
	   delete phead;
	   delete Temp3;
	   

	 
	
	return 0;
}

void CForceKillPsDlg::OnChecktruthmodule()
{  int i=m_ListModule.GetNextItem(-1,LVNI_ALL | LVNI_SELECTED);
	CString Temp=m_ListModule.GetItemText(i,0);
	if(Temp.IsEmpty())
	{MessageBox(L"遇到不可预期的异常！");
	 return ;
	}
	if(CheckFileTrust(Temp.GetBuffer(0)))
		m_ListModule.SetItemText(i,1,L"通过");
	else
	m_ListModule.SetItemText(i,1,L"未通过");
	// TODO: 在此添加命令处理程序代码
}

void CForceKillPsDlg::OnCheckallmodules()
{   if(m_ListModule.GetItemCount()>0)
{
	CreateThread(0,0,LPTHREAD_START_ROUTINE(MyListThread),this,0,NULL);
	CreateThread(0,0,LPTHREAD_START_ROUTINE(MyListThread1),this,0,NULL);
}
	// TODO: 在此添加命令处理程序代码
}

int CForceKillPsDlg::GetModuleSize(LPWSTR  szFilePath)
{  int ret=0;
try
{
	CFile File(szFilePath,CFile::modeRead);
		if(File.m_hFile==NULL)
	   return ret;
   else
	   return File.GetLength();
}
catch(CFileException e)
{
 return 0;
}
   
	
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
void CForceKillPsDlg::FunCallBackOfTimer(HWND a, UINT b, UINT c, UINT d)
{
	OnListprocess();
}


void CForceKillPsDlg::OnTimer(UINT_PTR nIDEvent)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
     OnListprocess();
	 
	CDialog::OnTimer(nIDEvent);
}

void CForceKillPsDlg::OnRan3killprocess()
{
	// TODO: 在此添加命令处理程序代码
	EnableDebugPrivilege(1);
	HANDLE hProcess=OpenProcess(PROCESS_ALL_ACCESS,0,m_PidTemp);
	if(IDYES==MessageBox(L"是否真的要结束进程",L"系统提示",MB_YESNO))
		TerminateProcess(hProcess,0);
	WaitForSingleObject(hProcess,800);
	DWORD ExitCode;
	 if(GetExitCodeProcess(hProcess,&ExitCode))
	 if(ExitCode==STILL_ACTIVE)
		 MessageBox(L"结束进程失败，请尝试其他方法！");

	CloseHandle(hProcess);
	 OnListprocess();
	OnListprocessmodule();
}

void CForceKillPsDlg::OnRang3killprocessbyun()
{
	// TODO: 在此添加命令处理程序代码
	EnableDebugPrivilege(1);
	HANDLE hProcess=OpenProcess(PROCESS_VM_OPERATION|PROCESS_QUERY_INFORMATION,0,m_PidTemp);
	if(IDYES!=MessageBox(L"是否真的要结束进程",L"系统提示",MB_YESNO))
		return;
	DWORD ExitCode;
	BOOL ret=FALSE;
	LONG_PTR BaseAddress=0x0;
	if(hProcess)
	do
	{
     BaseAddress+=0x1000;//PageSize
	 if(GetExitCodeProcess(hProcess,&ExitCode))
	 if(ExitCode!=STILL_ACTIVE) 
	 {ret=TRUE;
	   break;
	  }
	 
	 ret|=VirtualFreeEx(hProcess,(LPVOID)BaseAddress,0,MEM_RELEASE);
	}while(BaseAddress<0x7ffff000);
	if(ret==FALSE)
		MessageBox(L"结束进程失败，请尝试其他方法！");
	Sleep(10);
	m_PidTemp=-1;
	m_PNameTemp=L"";
	UpdateData();
	WaitForSingleObject(hProcess,30);
	CloseHandle(hProcess);
		OnListprocess();
	OnListprocessmodule();
}


DWORD GetPidByName (WCHAR* szName)

{

HANDLE hProcessSnap = INVALID_HANDLE_VALUE;

PROCESSENTRY32 pe32 = {0};

DWORD dwRet=0;

 

hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);

if (hProcessSnap == INVALID_HANDLE_VALUE)

   return 0;

 

pe32.dwSize = sizeof(PROCESSENTRY32);

 

if (Process32First (hProcessSnap, &pe32)) {

   do {

    if (wcscmp(szName, pe32.szExeFile) == 0) 
	 {

     dwRet=pe32.th32ProcessID;

     break;

    }

   }while (Process32Next(hProcessSnap,&pe32));

}

else {

   return 0;

}

 

if(hProcessSnap !=INVALID_HANDLE_VALUE) {

   CloseHandle(hProcessSnap);

}

 

return dwRet;

}
void CForceKillPsDlg::OnKillzeormemoryinran3()
{
	EnableDebugPrivilege(1);
	// TODO: 在此添加命令处理程序代码
	if(IDYES!=MessageBox(L"是否真的要结束进程",L"系统提示",MB_YESNO))
		return;
HMODULE hNTDLL   =   GetModuleHandle(L"ntdll.dll");
HANDLE     ph, h_dup; 
PSYSTEM_HANDLE_INFORMATION     h_info; 

PROCESS_BASIC_INFORMATION      pbi;

// 得到 csrss.exe 进程的PID

HANDLE     csrss_id    = (HANDLE) GetPidByName (L"csrss.exe");

CLIENT_ID client_id;

client_id.UniqueProcess   = csrss_id;

client_id.UniqueThread    = 0;

 

// 初始化对象结构体

OBJECT_ATTRIBUTES               attr;

attr.Length                     = sizeof(OBJECT_ATTRIBUTES);

attr.RootDirectory     = 0;

attr.ObjectName      = 0;

attr.Attributes      = 0;

attr.SecurityDescriptor    = 0;

attr.SecurityQualityOfService   = 0;

 

 

////////////////////////////////////////////////////////////////////////////////////
// 获得这些函数的实际地址 

ZWQUERYSYSTEMINFORMATION ZwQuerySystemInformation = 

   (ZWQUERYSYSTEMINFORMATION) GetProcAddress (hNTDLL, "ZwQuerySystemInformation");

ZWOPENPROCESS ZwOpenProcess = 

   (ZWOPENPROCESS) GetProcAddress (hNTDLL, "ZwOpenProcess"); 

ZWDUPLICATEOBJECT ZwDuplicateObject =

   (ZWDUPLICATEOBJECT) GetProcAddress (hNTDLL, "ZwDuplicateObject"); 

ZWQUERYINFORMATIONPROCESS ZwQueryInformationProcess = 

   (ZWQUERYINFORMATIONPROCESS) GetProcAddress (hNTDLL, "ZwQueryInformationProcess");

ZWALLOCATEVIRTUALMEMORY   ZwAllocateVirtualMemory =

   (ZWALLOCATEVIRTUALMEMORY) GetProcAddress (hNTDLL, "ZwAllocateVirtualMemory"); 

ZWPROTECTVIRTUALMEMORY ZwProtectVirtualMemory = 

   (ZWPROTECTVIRTUALMEMORY) GetProcAddress (hNTDLL, "ZwProtectVirtualMemory"); 

ZWWRITEVIRTUALMEMORY ZwWriteVirtualMemory = 

   (ZWWRITEVIRTUALMEMORY) GetProcAddress (hNTDLL, "ZwWriteVirtualMemory");

ZWFREEVIRTUALMEMORY   ZwFreeVirtualMemory = 

   (ZWFREEVIRTUALMEMORY) GetProcAddress (GetModuleHandle(L"ntdll.dll"),   "ZwFreeVirtualMemory"); 

ZWCLOSE   ZwClose = 

   (ZWCLOSE) GetProcAddress (hNTDLL, "ZwClose");
////////////////////////////////////////////////////////////////////////////////////

////Copy From Internet!
	ZwOpenProcess (&ph, PROCESS_ALL_ACCESS, &attr, &client_id);

	ULONG   bytesIO = 0x400000;
	PVOID buf     = 0;

	ZwAllocateVirtualMemory (GetCurrentProcess(), &buf, 0, &bytesIO, MEM_COMMIT, PAGE_READWRITE);

	// 为 ZwQuerySystemInformation 函数传递16号参数.获得系统句柄信息保存在buff中
	// buff的开始出保存的是系统句柄的数量.偏移4才是句柄信息
	ZwQuerySystemInformation (SystemHandleInformation, buf, 0x400000, &bytesIO);
	ULONG NumOfHandle = (ULONG) buf;
	h_info = (PSYSTEM_HANDLE_INFORMATION)((ULONG)buf+4);

	for (ULONG i= 0 ; i<NumOfHandle; i++, h_info++)
	{ 
	  
        if(!IsBadReadPtr(h_info,sizeof(SYSTEM_HANDLE_INFORMATION)))///这个是我自己改进的，防止异常
		{

		if ((h_info->ProcessId == (ULONG)csrss_id) && (h_info->ObjectTypeNumber == 5))
		{
			// 复制句柄
			if (ZwDuplicateObject(
				ph,
				(PHANDLE)h_info->Handle,
				(HANDLE)-1,
				&h_dup,
				0,
				0, 
				DUPLICATE_SAME_ACCESS) == (LONG)0x0000000L) 
			{

				ZwQueryInformationProcess(h_dup, (PROCESSINFOCLASS)0,(PVOID) &pbi, sizeof(pbi), &bytesIO);
			}//用这种方法在某种程度上可以列出隐藏进程…………

                
			if (pbi.UniqueProcessId == m_PidTemp)
			{
				//MessageBox( L"目标已确定!", L"!", MB_OK);

				for (i = 0x10000; i < 0x8000000; i = i + 0x1000)//0x80000000,起始地址是0X10000，1000是分页大小
				{
					PVOID pAddress = (PVOID) i;
					ULONG sz = 0x1000;
					ULONG oldp;

					if (ZwProtectVirtualMemory (h_dup, &pAddress, &sz, PAGE_EXECUTE_READWRITE, &oldp) == (LONG)0x0000000L) {              
						ZwWriteVirtualMemory(h_dup, pAddress, buf, 0x1000, &oldp);
					}          
				}

				MessageBox(L"任务已完成！");
				//   ZwClose(h_dup);     
				break;
			}
		}
		}
		else
		{  MessageBox(L"进程开了保护，结束进程失败！");
			break;
		}
	
	}

	bytesIO = 0;
	ZwFreeVirtualMemory(GetCurrentProcess(), &buf, &bytesIO, MEM_RELEASE);
	 OnListprocess();
	OnListprocessmodule();
}




void CForceKillPsDlg::OnFlushprocesslist()
{    OnListprocess();
	// TODO: 在此添加命令处理程序代码
}

void CForceKillPsDlg::OnUnmapanddel()
{
	// TODO: 在此添加命令处理程序代码
	if(!FreeRemoteModule(m_PidTemp,m_strModuleTemp.Right(m_strModuleTemp.GetLength()-m_strModuleTemp.ReverseFind('\\')-1).GetBuffer(0)))
	{MessageBox(L"常规方法卸载模块失败！");
	 OnListprocessmodule();
	return;
	}
	if(CheckFileTrust(m_strModuleTemp.GetBuffer(0)))
	{MessageBox(L"不能删除信任模块，请手工删除！");
	OnFindfile();
    OnListprocessmodule();
	
	return;
	}
	if(DeleteFile(m_strModuleTemp))
	{MessageBox(L"卸载并且删除文件完成！");
	}
}

void CForceKillPsDlg::OnAboutpefile()
	{
	// TODO: 在此添加命令处理程序代码
	ShellExecute(this->m_hWnd,L"open",L"AboutPe.exe",NULL,NULL,SW_SHOW);
	}

bool CForceKillPsDlg::ListProcessByQueryHandle(void)
	{
	
	return true;
	}

void CForceKillPsDlg::OnKillPsByDriverZeroM()
	{
		// TODO: 在此添加命令处理程序代码
		if(!bServiecavali)
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return ;
		}
		CCallDriver * Call=new CCallDriver;
		if(!Call->OpenKernelDevice(L"\\\\.\\KernelModule"))
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return ;
			
		}
		DWORD Pid=m_PidTemp^5;
		Call->KillProcessByZeroMemory(Pid);//哎，这个也是没办法了，有些软件会监视DeviceIoControl，进程pid加密传输
		delete Call;
		 OnListprocess();
	OnListprocessmodule();
		return;
		
	}

bool CForceKillPsDlg::GetProcessDataFromDriver(void)
	{  if(!bServiecavali)
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return false;
		}
		CCallDriver * Call=new CCallDriver;
		if(!Call->OpenKernelDevice(L"\\\\.\\KernelModule"))
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return false;
			
		}
		KillTimer(1000);
		ULONG size=sizeof(PROCESSDATA)*50;//开始时分配50个进程
		void * Buffer=malloc(size);
		  memset(Buffer,0,size);
		  Call->GetProcessData(Buffer,size);
		while(!Call->GetProcessData(Buffer,size))
		{ free(Buffer);
		  size=size+size/2;
		  Buffer=malloc(size);
		  memset(Buffer,0,size);
		}
		PROCESSDATA *p=(PROCESSDATA*)Buffer;
		m_ListProcess.DeleteAllItems();
		int i=0;
		CString Temp;
		WCHAR Name[20]={0};
		p=p++;////在驱动里第一个返回的是总进程数
		while(p->id)
		{
		m_ListProcess.InsertItem(i,L"");
		Temp.Format(L"%d",p->id);
		m_ListProcess.SetItemText(i,0,Temp);
		MultiByteToWideChar(CP_ACP,MB_PRECOMPOSED,(char*)p->name,16,Name,20);
		Temp.Format(L"%s",Name);
		m_ListProcess.SetItemText(i,1,Temp);
		GetUserPath(p->fullname);
		Temp.Format(L"%s",p->fullname);
		m_ListProcess.SetItemText(i,2,Temp);
		i++;
		p++;
		}
		free(Buffer);
		delete Call;
  
		

		return true;
	}

void CForceKillPsDlg::OnListProcessByDriver()
	{
		// TODO: 在此添加命令处理程序代码
	GetProcessDataFromDriver();
			
	}

void CForceKillPsDlg::OnInsertapckill()
{    if(!bServiecavali)
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return ;
		}
		CCallDriver * Call=new CCallDriver;
		if(!Call->OpenKernelDevice(L"\\\\.\\KernelModule"))
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return ;
			
		}
		DWORD Pid=m_PidTemp^5;
		if(!Call->KillProcessByApc(Pid))
			MessageBox(L"失败！");
		delete Call;
		 OnListprocess();
	OnListprocessmodule();
	// TODO: 在此添加命令处理程序代码
}

unsigned long CForceKillPsDlg::GetMiUnMapViewOfAddress(void)
{   PUCHAR pFunc = NULL;
    HMODULE hMod = NULL;
    ULONG offset = 0;
    int flag = 0;
	char szKrnlName[30];
	int pBase = 0;
	int rva = GetOrigSSDTAddrRVA(0x10b,szKrnlName,&pBase);	
	char FilePath[MAX_PATH]={0},Temp[MAX_PATH]={0};
	 if(!GetSystemDirectoryA(Temp,MAX_PATH))
		 {
		 AfxMessageBox(L"获取系统目录失败！");
		  return 0;
		 }
	 strcpy(FilePath,Temp);
	 strcat(FilePath,"\\");
	 strcat(FilePath,szKrnlName);
	// MessageBoxA(NULL,FilePath,"s",MB_OK);
	hMod=LoadLibraryExA(FilePath,0,DONT_RESOLVE_DLL_REFERENCES);
	 if(hMod==NULL)
		 {AfxMessageBox(L"动态解析函数失败");
	 return 0;
		 }
	pFunc = (PUCHAR)((ULONG)hMod + rva);
	int i=0;
	for (  i = 0 ; i < 0x100 ; pFunc++)
		{
		if ( *pFunc == 0xC9 && *(pFunc+1) == 0xC2 )
			{
		
			break;
			}
		}
	for ( i = 0 ; i < 0x100 ; pFunc-- )
		{
		if ( *pFunc == 0xE8 )
			{
			flag += 1;
			}
		if ( flag == 2 )
			{
			offset = *((ULONG *)(pFunc+1));
			offset = offset + (ULONG)pFunc + 5;
			break;
			}
		}
	offset = offset - (ULONG)hMod;
	FreeLibrary(hMod);
    return offset+pBase;
}

bool CForceKillPsDlg::SendFunction(void)
{   
	if(!bServiecavali)
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return false ;
		}
		CCallDriver * Call=new CCallDriver;
		if(!Call->OpenKernelDevice(L"\\\\.\\KernelModule"))
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return false ;
			
		}
		DWORD Address=GetMiUnMapViewOfAddress();
		if(Address==0)
			return false;
	if(Call->SendFunction(Address))
	{delete Call;
	 return true;
	}
			
		delete Call;
		return false;
}

void CForceKillPsDlg::OnUnmapmodulekill()
{   
	// TODO: 在此添加命令处理程序代码
	if(!bServiecavali)
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return ;
		}
	if(SendFunction()==false)
	{
		MessageBox(L"函数地址传递失败");
		return;
	}
	CString Temp=m_strModuleTemp;
	
		CCallDriver * Call=new CCallDriver;
		if(!Call->OpenKernelDevice(L"\\\\.\\KernelModule"))
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return ;
			
		}

	
	ULONG Base=0;
	Call->UnMapViewOfModule(Base,m_PidTemp);
	delete Call;
	OnForcelookupinmemory();
	 
	OnListprocessmodule();

}

ULONG CForceKillPsDlg::GetModBase(WCHAR* ModuleName)
{   EnableDebugPrivilege(1);
    NTSTATUS status;
	HMODULE hMod=GetModuleHandle(L"ntdll.dll");
	RTLCREATEQUERYDEBUGBUFFER RtlCreateQueryDebugBuffer=(RTLCREATEQUERYDEBUGBUFFER )GetProcAddress(hMod,"RtlCreateQueryDebugBuffer");
	RTLQUERYPROCESSDEBUGINFORMATION RtlQueryProcessDebugInformation=(RTLQUERYPROCESSDEBUGINFORMATION)GetProcAddress(hMod,"RtlQueryProcessDebugInformation");
	RTLDESTROYDEBUGBUFFER RtlDestroyQueryDebugBuffer =(RTLDESTROYDEBUGBUFFER )GetProcAddress(hMod,"RtlDestroyQueryDebugBuffer");
	if((hMod==NULL)||(RtlDestroyQueryDebugBuffer==NULL)||(RtlQueryProcessDebugInformation==NULL)||(RtlCreateQueryDebugBuffer==NULL))
	{
		MessageBox(L"函数定位失败！");
		return NULL;
	}
    
	char Name[MAX_PATH]={0};
	
	WideCharToMultiByte(CP_ACP,WC_NO_BEST_FIT_CHARS,ModuleName,wcslen(ModuleName),Name,MAX_PATH,NULL,NULL);
	PDEBUG_BUFFER Buffer=RtlCreateQueryDebugBuffer(0,FALSE);
	status=RtlQueryProcessDebugInformation(m_PidTemp,PDI_MODULES ,Buffer);
	if(!NT_SUCCESS(status))
	{   MessageBox(L"RtlQueryProcessDebugInformation函数调用失败，进程开了保护");
		return 0;
	}
	ULONG count=*(PULONG)(Buffer->ModuleInformation);
	ULONG hModule=NULL;
	PDEBUG_MODULE_INFORMATION ModuleInfo=(PDEBUG_MODULE_INFORMATION)((ULONG)Buffer->ModuleInformation+4);
	for(long i=0;i<count;i++)
	{
		if(!strcmp(ModuleInfo->ImageName+ModuleInfo->ModuleNameOffset,Name))
		{
			hModule=ModuleInfo->Base;
			break;
		}
		ModuleInfo++;
	}

    RtlDestroyQueryDebugBuffer(Buffer);	

    return hModule;
}

void CForceKillPsDlg::OnForceUnMapModule()
{  //MessageBox(m_strModuleTemp);
	// TODO: 在此添加命令处理程序代码
	if(!bServiecavali)
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return ;
		}
	if(SendFunction()==false)
	{
		MessageBox(L"函数地址传递失败");
		return;
	}
	CString Temp=m_strModuleTemp;
	
		CCallDriver * Call=new CCallDriver;
		if(!Call->OpenKernelDevice(L"\\\\.\\KernelModule"))
		{
			MessageBox(L"由于无法打开驱动，此功能不能用");
			return ;
			
		}

	Temp=Temp.Right(Temp.GetLength()-Temp.ReverseFind('\\')-1);
	ULONG Base=GetModBase(Temp.GetBuffer(0));
	if(Base==0)
	{
		return ;
	}

	Call->UnMapViewOfModule(Base,m_PidTemp);
	OnForcelookupinmemory();
	
		delete Call;//一定要删除，不然第二次调用驱动总是会失败
		DeleteFile(m_strModuleTemp);
		 OnListprocess();
	OnListprocessmodule();
	

   
}

void CForceKillPsDlg::OnUnmapallmodule()
{   if(!EnableDebugPrivilege(1))
{
	MessageBox(L"权限太低，提权失败");
	return;
}
    
	// TODO: 在此添加命令处理程序代码
   DWORD SelfPid=GetCurrentProcessId();
   HANDLE        hProcessSnap = NULL; 
    BOOL          bRet      = FALSE; 
    PROCESSENTRY32 pe32      = {0}; 
    hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0); 

    if (hProcessSnap == INVALID_HANDLE_VALUE) 
        return; 

    pe32.dwSize = sizeof(PROCESSENTRY32); 
	
    if (Process32First(hProcessSnap, &pe32)) 
    { 

        do 
        {     
				   if((pe32.th32ProcessID!=SelfPid)&&(pe32.th32ProcessID>4))
					FreeRemoteModule(pe32.th32ProcessID,m_strModuleTemp.Right(m_strModuleTemp.GetLength()-m_strModuleTemp.ReverseFind('\\')-1).GetBuffer(0));
			
				               
				        
        } 
        while (Process32Next(hProcessSnap, &pe32)); 

    } 

    // Do not forget to clean up the snapshot object. 
        EnableDebugPrivilege(0);

    CloseHandle (hProcessSnap); 
	 OnListprocess();
	OnListprocessmodule();
   

}

void CForceKillPsDlg::OnKillandprivantrunagain()
{//这里想使用进程DNA识别，不是单纯靠路径或进程名来判断，可是又少不了要对SSDT动手脚，所以这个功能就不提供了,HOOK SSDT 也只是防君子^_^
	//MessageBox(L"不想HOOKSSDT，暂时不提供此功能");
	
	CString FileName=m_pFullName;
	FileName=FileName.Left(FileName.ReverseFind('.'));
	//MessageBox(FileName);
	FileName+=L".manifest";
	CreateFile(FileName,GENERIC_WRITE,0,NULL,CREATE_ALWAYS,FILE_ATTRIBUTE_HIDDEN|FILE_ATTRIBUTE_READONLY|FILE_ATTRIBUTE_SYSTEM,NULL);
	OnInsertapckill();
    OnListprocess();
	OnListprocessmodule();
	


}

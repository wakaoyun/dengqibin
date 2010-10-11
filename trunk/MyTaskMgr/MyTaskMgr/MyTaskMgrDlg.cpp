
// MyTaskMgrDlg.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "MyTaskMgrDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CAboutDlg dialog used for App About

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// Dialog Data
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

// Implementation
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CMyTaskMgrDlg dialog




CMyTaskMgrDlg::CMyTaskMgrDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CMyTaskMgrDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMyTaskMgrDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_TAB, m_MyTab);
}

BEGIN_MESSAGE_MAP(CMyTaskMgrDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_GETMINMAXINFO()
	ON_WM_CREATE()
	ON_WM_SIZE()
	ON_COMMAND(ID_FILE_EXITTASKMANAGER, &CMyTaskMgrDlg::OnFileExittaskmanager)
	ON_COMMAND(ID_HELP_ABOUTTASKMANAGER, &CMyTaskMgrDlg::OnHelpAbouttaskmanager)
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB, &CMyTaskMgrDlg::OnSelchangeTab)
END_MESSAGE_MAP()
static UINT BASED_CODE indicators[] =
{
	10001,
	10002,
	10003, 
};


// CMyTaskMgrDlg message handlers

BOOL CMyTaskMgrDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	ShowWindow(SW_NORMAL);

	InitMyTab();

	m_StatusBar.Create(this);
	m_StatusBar.SetIndicators(indicators,3);
	m_StatusBar.SetPaneInfo(0,10001, SBPS_NORMAL,150); 
	/*m_StatusBar.SetPaneInfo(1,1002, SBPS_NORMAL,150);
	m_StatusBar.SetPaneInfo(2,1003, SBPS_NORMAL,150);*/
	m_StatusBar.SetPaneText(0,_T("Welcome come here"),TRUE); //set new pane1 text
	//m_StatusBar.SetPaneText(1,_T("You can get a sweet felling"),TRUE); //set new pane2 text
	RepositionBars(AFX_IDW_CONTROLBAR_FIRST,AFX_IDW_CONTROLBAR_LAST,10002); 



	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMyTaskMgrDlg::OnSysCommand(UINT nID, LPARAM lParam)
{	
	CDialogEx::OnSysCommand(nID, lParam);
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMyTaskMgrDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

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
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMyTaskMgrDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CMyTaskMgrDlg::OnGetMinMaxInfo(MINMAXINFO* lpMMI)
{
	lpMMI->ptMinTrackSize.x = MINWIN_X;
	lpMMI->ptMinTrackSize.y = MINWIN_Y;

	CDialogEx::OnGetMinMaxInfo(lpMMI);
}


int CMyTaskMgrDlg::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CDialogEx::OnCreate(lpCreateStruct) == -1)
		return -1;
	
	return 0;
}


void CMyTaskMgrDlg::OnSize(UINT nType, int cx, int cy)
{	
	CDialogEx::OnSize(nType, cx, cy);
	if(m_MyTab.m_hWnd!=NULL)
	{
		m_MyTab.MoveWindow(5, 5, cx - 10, cy - 40);
	}
	if(m_Application.m_hWnd!=NULL)
	{
		m_Application.MoveWindow(1, 25, cx - 14, cy - 68);
	}
	if(m_Process.m_hWnd!=NULL)
	{
		m_Process.MoveWindow(1, 25, cx - 14, cy - 68);
	}
	if(m_Services.m_hWnd!=NULL)
	{
		m_Services.MoveWindow(1, 25, cx - 14, cy - 68);
	}
	if(m_Performance.m_hWnd!=NULL)
	{
		m_Performance.MoveWindow(1, 25, cx - 14, cy - 68);
	}
	if(m_Networking.m_hWnd!=NULL)
	{
		m_Networking.MoveWindow(1, 25, cx - 14, cy - 68);
	}
	if(m_Users.m_hWnd!=NULL)
	{
		m_Users.MoveWindow(1, 25, cx - 14, cy - 68);
	}
	RepositionBars(AFX_IDW_CONTROLBAR_FIRST,AFX_IDW_CONTROLBAR_LAST,10002); 
}


void CMyTaskMgrDlg::OnFileExittaskmanager()
{
	SendMessage(WM_CLOSE);
}


void CMyTaskMgrDlg::OnHelpAbouttaskmanager()
{
	CAboutDlg dlgAbout;
	dlgAbout.DoModal();
}


void CMyTaskMgrDlg::OnSelchangeTab(NMHDR *pNMHDR, LRESULT *pResult)
{
	int curSel = m_MyTab.GetCurSel();
	switch(curSel)
	{
	case 0:
		HideAll();
		m_Application.ShowWindow(SW_SHOW);		
		break;
	case 1:
		HideAll();
		m_Process.ShowWindow(SW_SHOW);
		break;
	case 2:
		HideAll();
		m_Services.ShowWindow(SW_SHOW);
		break;
	case 3:
		HideAll();
		m_Performance.ShowWindow(SW_SHOW);
		break;
	case 4:
		HideAll();
		m_Networking.ShowWindow(SW_SHOW);
		break;
	case 5:
		HideAll();
		m_Users.ShowWindow(SW_SHOW);
		break;
	default:
		HideAll();
		break;
	}
	*pResult = 0;
}


void CMyTaskMgrDlg::HideAll(void)
{
	m_Application.ShowWindow(SW_HIDE);
	m_Process.ShowWindow(SW_HIDE);
	m_Services.ShowWindow(SW_HIDE);
	m_Performance.ShowWindow(SW_HIDE);
	m_Networking.ShowWindow(SW_HIDE);
	m_Users.ShowWindow(SW_HIDE);
}


void CMyTaskMgrDlg::InitMyTab(void)
{
	m_MyTab.MoveWindow(5 ,5, MINWIN_X - 26, MINWIN_Y - 98);
	m_MyTab.InsertItem(0,_T("Applications"));
	m_MyTab.InsertItem(1,_T("Processes"));
	m_MyTab.InsertItem(2,_T("Services"));
	m_MyTab.InsertItem(3,_T("Performance"));
	m_MyTab.InsertItem(4,_T("Networking"));
	m_MyTab.InsertItem(5,_T("Users"));

	CRect rect;
	m_MyTab.GetClientRect(&rect);
	rect.top += 25;
	rect.left += 1;
	rect.bottom -= 6;
	rect.right -= 4;
	//应该程序选项卡
	m_Application.Create(IDD_PROPPAGE_Applications, GetDlgItem(IDC_TAB));
	m_Application.MoveWindow(&rect);
	m_Application.ShowWindow(SW_SHOW);
	//进程选项卡
	m_Process.Create(IDD_PROPPAGE_Processes, GetDlgItem(IDC_TAB));
	m_Process.MoveWindow(&rect);
	m_Process.ShowWindow(SW_HIDE);
	//服务选项卡
	m_Services.Create(IDD_PROPPAGE_Services, GetDlgItem(IDC_TAB));
	m_Services.MoveWindow(&rect);
	m_Services.ShowWindow(SW_HIDE);
	//性能选项卡
	m_Performance.Create(IDD_PROPPAGE_Performance, GetDlgItem(IDC_TAB));
	m_Performance.MoveWindow(&rect);
	m_Performance.ShowWindow(SW_HIDE);	
	//网络选项卡
	m_Networking.Create(IDD_PROPPAGE_Networking, GetDlgItem(IDC_TAB));
	m_Networking.MoveWindow(&rect);
	m_Networking.ShowWindow(SW_HIDE);
	//用户选项卡
	m_Users.Create(IDD_PROPPAGE_Users, GetDlgItem(IDC_TAB));
	m_Users.MoveWindow(&rect);
	m_Users.ShowWindow(SW_HIDE);
}

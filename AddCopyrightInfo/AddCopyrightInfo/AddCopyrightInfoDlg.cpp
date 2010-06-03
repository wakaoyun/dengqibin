
// AddCopyrightInfoDlg.cpp : implementation file
//

#include "stdafx.h"
#include "AddCopyrightInfo.h"
#include "AddCopyrightInfoDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif
// CAddCopyrightInfoDlg dialog

CAddCopyrightInfoDlg::CAddCopyrightInfoDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CAddCopyrightInfoDlg::IDD, pParent)
	, m_Path(_T(""))
{
	m_Copyright.Append(L"//-------------------------------------------------------------------\r\n");
	m_Copyright.Append(L"//版权所有：版权所有(C) \r\n");
	m_Copyright.Append(L"//系统名称：\r\n");
	m_Copyright.Append(L"//作    者：$username$\r\n");
	m_Copyright.Append(L"//邮箱地址：\r\n");
	m_Copyright.Append(L"//创建日期：$time$\r\n");
	m_Copyright.Append(L"//功能说明：\r\n");
	m_Copyright.Append(L"//-------------------------------------------------------------------\r\n");
	m_Copyright.Append(L"//修改记录：\r\n");
	m_Copyright.Append(L"//修改人：\r\n");
	m_Copyright.Append(L"//修改时间：\r\n");
	m_Copyright.Append(L"//修改内容：\r\n");
	m_Copyright.Append(L"//-------------------------------------------------------------------");
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CAddCopyrightInfoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_CopyrightInfo, m_Copyright);
	DDX_Text(pDX, IDC_Path, m_Path);
}

BEGIN_MESSAGE_MAP(CAddCopyrightInfoDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_EN_CHANGE(IDC_CopyrightInfo, &CAddCopyrightInfoDlg::OnEnChangeCopyrightInfo)
	ON_EN_CHANGE(IDC_Path, &CAddCopyrightInfoDlg::OnEnChangePath)
	ON_BN_CLICKED(IDSubmit, &CAddCopyrightInfoDlg::OnBnClickedSubmit)
END_MESSAGE_MAP()


// CAddCopyrightInfoDlg message handlers

BOOL CAddCopyrightInfoDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

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

void CAddCopyrightInfoDlg::OnPaint()
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
HCURSOR CAddCopyrightInfoDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

char* CStringToCharArray(CString str) 
{ 
	char *ptr; 
	#ifdef _UNICODE 
		LONG    len; 
		len = WideCharToMultiByte(CP_UTF8, 0, str, -1, NULL, 0, NULL, NULL); 
		ptr = new char [len+1]; 
		memset(ptr,0,len + 1); 
		WideCharToMultiByte(CP_UTF8, 0, str, -1, ptr, len + 1, NULL, NULL); 
	#else 
		ptr = new char [str.GetAllocLength()+1]; 
		sprintf(ptr,"%s",str); 
	#endif 
	return ptr;
}

BOOL CAddCopyrightInfoDlg::MyFindFile(TCHAR* path, char* copyright)
{
	WIN32_FIND_DATA findFileData;
	TCHAR szFind[MAX_PATH] = {0};
	TCHAR szFindTemp[MAX_PATH] = {0};

	wcsncpy_s(szFind,path,wcslen(path));
    wcsncat_s(szFind,L"\\*.*",4);
	HANDLE h = FindFirstFile(szFind, &findFileData);
	if(INVALID_HANDLE_VALUE==h)
	{
		MessageBox(_T("请检查路径是否正确"));
		return FALSE;
	}
	while(true)
	{
		if(findFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
		{
			if(findFileData.cFileName[0]!='.')
			{
				wcsncpy_s(szFindTemp,path,wcslen(path));
				wcsncat_s(szFindTemp,L"\\",1);
				wcsncat_s(szFindTemp,findFileData.cFileName,sizeof(findFileData.cFileName));
				MyFindFile(szFindTemp, copyright);
			}
		}
		else
		{
			TCHAR exName[_MAX_EXT];
			_wsplitpath(findFileData.cFileName, NULL, NULL, NULL, exName); 
			if(wcscmp(exName,_T(".cs"))==0)//只有是.cs文件的才写
			{	
				TCHAR filePath[MAX_PATH];
				wcsncpy_s(filePath,path,wcslen(path));
				wcsncat_s(filePath,L"\\",1);
				wcsncat_s(filePath,findFileData.cFileName,sizeof(findFileData.cFileName));

				TCHAR fileName[MAX_PATH];
				wcsncpy_s(fileName, filePath, wcslen(filePath)-3);//去除.cs
				wcsncat_s(fileName,L".bak",4);
				CopyFile(filePath,fileName,TRUE);//备份文件
				
				WriteCopyright(filePath, fileName, copyright);//写版权信息
			}
		}
		if(!FindNextFile(h,&findFileData))    
			break;
	}
	FindClose(h);
	return TRUE;
}

void CAddCopyrightInfoDlg::WriteCopyright(TCHAR* filePath, TCHAR* sourceFile, char* copyright)
{
	CFile file(sourceFile, CFile::modeRead | CFile::typeBinary);	
	
	long int size = file.GetLength();
	char* buf = new char[size];
	memset(buf, 0, size);	
	file.Seek(3, CFile::begin);//前三个未知内容
	file.Read(buf, size);
	file.Close();	

	int len = strlen(copyright);	
	char* fileBuf = new char[size + len + 1];
	memset(fileBuf, 0, size + len);
	strncpy(fileBuf, copyright, len);
	strncat(fileBuf, buf, size);
	
	if(file.Open(filePath,CFile::modeReadWrite | CFile::typeBinary, NULL))
	{
		file.SetLength(0);//清空文件
		file.Seek(0, CFile::begin);
		file.Write(fileBuf, strlen(fileBuf));
		file.Close();	
	}
	
	if(buf != NULL)
	{
		delete[] buf;
		buf = NULL;
	}
	if(fileBuf != NULL)
	{
		delete[] fileBuf;
		fileBuf = NULL;
	}
}

void CAddCopyrightInfoDlg::OnEnChangeCopyrightInfo()
{
	UpdateData();
}

void CAddCopyrightInfoDlg::OnEnChangePath()
{
	UpdateData();
}


void CAddCopyrightInfoDlg::OnBnClickedSubmit()
{
	if(m_Path.Trim()=="")
	{
		MessageBox(_T("请输入路径！"));
		return;
	}
	if(m_Copyright.Trim().GetLength()!=0)
	{
		m_Copyright.Append(L"\r\n");//添加换行
	}
	TCHAR pathItem[MAX_PATH];
	TCHAR pathProject[MAX_PATH];
	wcsncpy_s(pathItem, m_Path.GetBuffer(m_Path.GetLength()), m_Path.GetLength());
	wcsncpy_s(pathProject, pathItem, wcslen(pathItem));
	wcsncat_s(pathItem,L"\\Common7\\IDE\\ItemTemplatesCache", 31);
	wcsncat_s(pathProject, L"\\Common7\\IDE\\ProjectTemplatesCache", 34);
	char* copyright = NULL;
	copyright = CStringToCharArray(m_Copyright);
	if(MyFindFile(pathItem, copyright) && MyFindFile(pathProject, copyright))
	{
		MessageBox(L"添加成功");
	}
	if(copyright!=NULL)
	{
		delete copyright;
		copyright = NULL;
	}
	UpdateData();
}

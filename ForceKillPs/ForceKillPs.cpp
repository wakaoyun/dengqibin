// ForceKillPs.cpp : ����Ӧ�ó��������Ϊ��
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


// CForceKillPsApp ����

CForceKillPsApp::CForceKillPsApp()
{
	// TODO: �ڴ˴���ӹ�����룬
	// ��������Ҫ�ĳ�ʼ�������� InitInstance ��
}


// Ψһ��һ�� CForceKillPsApp ����

CForceKillPsApp theApp;

// CForceKillPsApp ��ʼ��

BOOL CForceKillPsApp::InitInstance()
{    mutex=NULL;
	// ���һ�������� Windows XP �ϵ�Ӧ�ó����嵥ָ��Ҫ
	// ʹ�� ComCtl32.dll �汾 6 ����߰汾�����ÿ��ӻ���ʽ��
	//����Ҫ InitCommonControlsEx()�����򣬽��޷��������ڡ�
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	// ��������Ϊ��������Ҫ��Ӧ�ó�����ʹ�õ�
	// �����ؼ��ࡣ
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();

	AfxEnableControlContainer();
	mutex=::CreateMutex(NULL,0,L"ForcePs");
	if(mutex)
	{
		if(ERROR_ALREADY_EXISTS==GetLastError())
		{
			MessageBox(NULL,L"�����Ѿ���������",L"ϵͳ��ʾ",MB_OK);
			return false;
		}
	}

	// ��׼��ʼ��
	// ���δʹ����Щ���ܲ�ϣ����С
	// ���տ�ִ���ļ��Ĵ�С����Ӧ�Ƴ�����
	// ����Ҫ���ض���ʼ������
	// �������ڴ洢���õ�ע�����
	// TODO: Ӧ�ʵ��޸ĸ��ַ�����
	// �����޸�Ϊ��˾����֯��
	SetRegistryKey(_T("Ӧ�ó��������ɵı���Ӧ�ó���"));

	CForceKillPsDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: �ڴ˷��ô����ʱ��
		//  ��ȷ�������رնԻ���Ĵ���
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: �ڴ˷��ô����ʱ��
		//  ��ȡ�������رնԻ���Ĵ���
	}

	// ���ڶԻ����ѹرգ����Խ����� FALSE �Ա��˳�Ӧ�ó���
	//  ����������Ӧ�ó������Ϣ�á�
	return FALSE;
}


////ע��ȫ�ֺ�����д������

/*void CForceKillPsApp::Ran3ListProcess(void)
{
	 HANDLE        hProcessSnap = NULL; 
    BOOL          bRet      = FALSE; 
    PROCESSENTRY32 pe32      = {0}; 

    if(!EnableDebugPrivilege(1))
	{MessageBox(L"��Ȩʧ�ܣ����������н��̣�");
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
	// TODO: �ڴ����ר�ô����/����û���
	if(mutex)
		ReleaseMutex(mutex);

	return CWinApp::ExitInstance();
}

#include "StdAfx.h"
#include"winsvc.h"
#include "CallDriver.h"
#pragma comment(lib,"advapi32.lib")

CCallDriver::CCallDriver(CString Name)
	{
	 m_strSerName=Name;
	 m_hDevice=NULL;

	}

CCallDriver::~CCallDriver(void)
	{ 
		if(m_hDevice)
			CloseHandle(m_hDevice);
	}

bool CCallDriver::LoadDriverBySC(CString m_DriverPath)//��һ�ַ�����������
	{ //char aPath[1024];
      //char aCurrentDirectory[515];
      SC_HANDLE sh = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);
      if(!sh)
      {     AfxMessageBox(L"�򿪷��������ʧ�ܣ�����Ƿ�װ����������ǽ!");
            return false;
      }
 
SC_HANDLE rh = CreateService(sh,
                                   m_strSerName,
                                   m_strSerName,
                                   SERVICE_ALL_ACCESS,
                                   SERVICE_KERNEL_DRIVER,
                                   SERVICE_DEMAND_START,
                                   SERVICE_ERROR_NORMAL,
                                   m_DriverPath,
                                   NULL,
                                   NULL,
                                   NULL,
                                   NULL,
                                   NULL);
   if(!rh)
      {
            if (GetLastError() == ERROR_SERVICE_EXISTS)
            {
                  // service exists
                  rh = OpenService(sh,
                                   m_strSerName,
                                   SERVICE_ALL_ACCESS);
                  if(!rh)
                  {
                        CloseServiceHandle(sh);
                        AfxMessageBox(L"�򿪷���ʱ��������");
                        return false;       
                   }
           }
            else
            {
                  CloseServiceHandle(sh);
                AfxMessageBox(L"�򿪷���ʧ�ܣ�����Ƿ�װ����������ǽ!");
                  return false;
            }
      }
      // start the drivers
         if(rh)
         {
            if(0 == StartService(rh, 0, NULL))
            {
                  if(ERROR_SERVICE_ALREADY_RUNNING == GetLastError())
                  {
                        // no real problem
                  }
                  else
                  {
                        CloseServiceHandle(sh);
                        CloseServiceHandle(rh);
						CString Mes;
						Mes.Format(L"��ʼ����ʱ��������%d",GetLastError());
						AfxMessageBox(Mes);
                        return false;
                  }
            }
            CloseServiceHandle(sh);
            CloseServiceHandle(rh);
      }
      return true;
	
	}

bool CCallDriver::LoadDriverBySetInfo(CString m_DriverPath)//�ڶ��ַ�����������
	{//������Ϊ�������Ԥ���ģ���������Ҳûʲô��


	return true;
	}

bool CCallDriver::KillProcessByZeroMemory(unsigned long Pid)
{    DWORD cbRet;
	BOOL ret=DeviceIoControl(m_hDevice,IOCTL_KPSBYZEROM,&Pid,sizeof(DWORD),NULL,0,&cbRet,NULL);
	return ret;
}

bool CCallDriver::OpenKernelDevice(WCHAR* DeviceName)
{  m_hDevice=CreateFile(DeviceName,GENERIC_READ|GENERIC_WRITE,0,NULL,OPEN_EXISTING,FILE_ATTRIBUTE_NORMAL,NULL);
if(m_hDevice==INVALID_HANDLE_VALUE)
{AfxMessageBox(L"��������ϵʧ��");
   return false;
}	return true;
}

bool CCallDriver::GetProcessData(void * Buffer, ULONG cbBuffer)
{   
	if(!m_hDevice)
		return false;
	DWORD cbRet;
	bool ret=DeviceIoControl(m_hDevice,IOCTL_GETPROCESS,NULL,0,Buffer,cbBuffer,&cbRet,NULL);
	if(*(DWORD*)Buffer==0)		
		return false;

	return TRUE;
}

bool CCallDriver::StopService(CString m_strService)
{   
	
	SC_HANDLE hServiceMsg=OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS);
	if(hServiceMsg==NULL)
	{
		AfxMessageBox(L"ж������ʧ�ܣ����ֶ�ɾ��");
		
		return false;
	}
	SC_HANDLE hService=OpenService(hServiceMsg,m_strService,SERVICE_ALL_ACCESS);
	if(hService==NULL)
	{	
		AfxMessageBox(L"ж������ʧ�ܣ����ֶ�ɾ��");
		CloseServiceHandle(hServiceMsg);
		return false;
	}
	SERVICE_STATUS Svstatus;
	if(!ControlService(hService,SERVICE_CONTROL_STOP,&Svstatus))
	{   CloseServiceHandle(hServiceMsg);
	   CloseServiceHandle(hService);
		AfxMessageBox(L"ж������ʧ�ܣ����ֶ�ɾ��");
		return false;
	}
	if(!DeleteService(hService))
	{ CloseServiceHandle(hServiceMsg);
	   CloseServiceHandle(hService);
		AfxMessageBox(L"ж������ʧ�ܣ����ֶ�ɾ��");
		return false;
	}
   CloseServiceHandle(hServiceMsg);
	   CloseServiceHandle(hService);



	return true;
}

bool CCallDriver::KillProcessByApc(unsigned long Pid)
{if(!m_hDevice)
		return false;
	DWORD cbRet;
	return DeviceIoControl(m_hDevice,IOCTL_KPSBYAPC,&Pid,sizeof(DWORD),NULL,0,&cbRet,NULL);
	
}

bool CCallDriver::SendFunction(unsigned long Address)
{if(!m_hDevice)
		return false;
	DWORD cbRet;
	return DeviceIoControl(m_hDevice,IOCTL_SENDFUN,&Address,sizeof(DWORD),NULL,0,&cbRet,NULL);
	
}

bool CCallDriver::UnMapViewOfModule(unsigned long lBaseAddress, unsigned long Pid)
{
	if(!m_hDevice)
		return false;
	DWORD cbRet;
	SENDDATA Send;
	Send.Base=lBaseAddress;
	Send.Pid=Pid;
	return DeviceIoControl(m_hDevice,IOCTL_KPSBYUNMAP,&Send,sizeof(SENDDATA),NULL,0,&cbRet,NULL);
	
}

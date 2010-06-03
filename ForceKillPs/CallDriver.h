#pragma once
#include"..\Driver\KitIoctl.h"
#include"winioctl.h"
/*
this class is used to connection the driver 
*/
typedef struct{
	DWORD Base;
	DWORD Pid;
}SENDDATA;


class CCallDriver
{
public:
	CCallDriver(CString Name);
	CCallDriver()
	{
	};
	~CCallDriver(void);
	bool LoadDriverBySC(CString m_DriverPath);
	bool LoadDriverBySetInfo(CString m_DriverPath);
protected:
	CString m_strSerName;
public:
	bool KillProcessByZeroMemory(unsigned long Pid);
	bool OpenKernelDevice(WCHAR* DeviceName);
protected:
	HANDLE m_hDevice;
public:
	bool GetProcessData(void * Buffer, ULONG cbBuffer);
	bool StopService(CString m_strService);
	bool KillProcessByApc(unsigned long Pid);
	bool SendFunction(unsigned long Address);
	bool UnMapViewOfModule(unsigned long lBaseAddress, unsigned long Pid);
};

// ForceKillPsDlg.h : 头文件
//

#pragma once
#include "afxcmn.h"
#include <Aclapi.h>

//********************************************************************
typedef struct _Table
{WCHAR Num;
 WCHAR Disk;
 struct _Table *Next;
}THead;
BOOL FreeRemoteModule(DWORD Pid, LPWSTR Module);
typedef struct PROCESSDATA
{
	//ULONG addr;//这个只是在内核态才有意义
	int id;
	UCHAR name[16];
	WCHAR fullname[200];
}PROCESSDATA,*PPROCESSDATA;

class CForceKillPsDlg : public CDialog
{
// 构造
public:
	CForceKillPsDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_FORCEKILLPS_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnAbout();
	afx_msg void OnExit();
	// 第一个list的关联变量
	CListCtrl m_ListProcess;
	CListCtrl m_ListModule;
	void Ran3ListProcess(void);
	afx_msg void OnListprocess();
	afx_msg void OnNMClickListprocess(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnNMRClickListprocess(NMHDR *pNMHDR, LRESULT *pResult);
	DWORD m_PidTemp;
	void Ran3ListModule(DWORD dwPid);
	afx_msg void OnListprocessmodule();
	bool CheckFileTrust(LPCWSTR lpFileName);
	afx_msg void OnListandcheckmodule();
	afx_msg void OnGoogleprocess();
	CString m_PNameTemp;
	CString m_strModuleTemp;
	afx_msg void OnNMRClickListmodule(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnGooglemodule();
	afx_msg void OnFindfile();
	afx_msg void OnNMCustomdrawListprocess(NMHDR *pNMHDR, LRESULT *pResult);
	afx_msg void OnUnmapmodule();
	bool ForceLookUpModule(void);
	afx_msg void OnForcelookupinmemory();
	int GetUserPath(LPWSTR szModPath);
	afx_msg void OnChecktruthmodule();
	afx_msg void OnCheckallmodules();
	int GetModuleSize(LPWSTR  szFilePath);
	//afx_msg void  OnTimer(UINT nIDEvent);
	void FunCallBackOfTimer(HWND a, UINT b, UINT c, UINT d);
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnRan3killprocess();
	afx_msg void OnRang3killprocessbyun();
	afx_msg void OnKillzeormemoryinran3();
	afx_msg void OnFlushprocesslist();
	afx_msg void OnUnmapanddel();
	afx_msg void OnAboutpefile();
	bool ListProcessByQueryHandle(void);
protected:
	bool bServiecavali;
public:
	afx_msg void OnKillPsByDriverZeroM();
	bool GetProcessDataFromDriver(void);
	afx_msg void OnListProcessByDriver();
	afx_msg void OnInsertapckill();
	unsigned long GetMiUnMapViewOfAddress(void);
	bool SendFunction(void);
	afx_msg void OnUnmapmodulekill();
	ULONG GetModBase(WCHAR* Name);
	afx_msg void OnForceUnMapModule();
	afx_msg void OnUnmapallmodule();
	afx_msg void OnKillandprivantrunagain();
protected:
	CString m_pFullName;
};


typedef struct UNICODE_STRING
{
	USHORT Length;
	USHORT MaximumLength;
	PWSTR  Buffer;
}UNICODE_STRING,*PUNICODE_STRING;


typedef struct _MEMORY_SECTION_NAME { 
UNICODE_STRING SectionFileName;
} MEMORY_SECTION_NAME, *PMEMORY_SECTION_NAME;

//*****************************************************************************************////、
//下面是native API的定义部分
typedef long NTSTATUS; 
//typedef  ((NTSTATUS)0x00000000L) STATUS_SUCCESS;


typedef struct _OBJECT_ATTRIBUTES {
ULONG Length;
HANDLE RootDirectory;
PUNICODE_STRING ObjectName;
ULONG Attributes;
PSECURITY_DESCRIPTOR SecurityDescriptor;
PSECURITY_QUALITY_OF_SERVICE SecurityQualityOfService;
} OBJECT_ATTRIBUTES, *POBJECT_ATTRIBUTES;

typedef enum _SYSTEM_INFORMATION_CLASS {
    SystemBasicInformation,              // 0        Y        N
    SystemProcessorInformation,          // 1        Y        N
    SystemPerformanceInformation,        // 2        Y        N
    SystemTimeOfDayInformation,          // 3        Y        N
    SystemNotImplemented1,               // 4        Y        N
    SystemProcessesAndThreadsInformation, // 5       Y        N
    SystemCallCounts,                    // 6        Y        N
    SystemConfigurationInformation,      // 7        Y        N
    SystemProcessorTimes,                // 8        Y        N
    SystemGlobalFlag,                    // 9        Y        Y
    SystemNotImplemented2,               // 10       Y        N
    SystemModuleInformation,             // 11       Y        N
    SystemLockInformation,               // 12       Y        N
    SystemNotImplemented3,               // 13       Y        N
    SystemNotImplemented4,               // 14       Y        N
    SystemNotImplemented5,               // 15       Y        N
    SystemHandleInformation,             // 16       Y        N
    SystemObjectInformation,             // 17       Y        N
    SystemPagefileInformation,           // 18       Y        N
    SystemInstructionEmulationCounts,    // 19       Y        N
    SystemInvalidInfoClass1,             // 20
    SystemCacheInformation,              // 21       Y        Y
    SystemPoolTagInformation,            // 22       Y        N
    SystemProcessorStatistics,           // 23       Y        N
    SystemDpcInformation,                // 24       Y        Y
    SystemNotImplemented6,               // 25       Y        N
    SystemLoadImage,                     // 26       N        Y
    SystemUnloadImage,                   // 27       N        Y
    SystemTimeAdjustment,                // 28       Y        Y
    SystemNotImplemented7,               // 29       Y        N
    SystemNotImplemented8,               // 30       Y        N
    SystemNotImplemented9,               // 31       Y        N
    SystemCrashDumpInformation,          // 32       Y        N
    SystemExceptionInformation,          // 33       Y        N
    SystemCrashDumpStateInformation,     // 34       Y        Y/N
    SystemKernelDebuggerInformation,     // 35       Y        N
    SystemContextSwitchInformation,      // 36       Y        N
    SystemRegistryQuotaInformation,      // 37       Y        Y
    SystemLoadAndCallImage,              // 38       N        Y
    SystemPrioritySeparation,            // 39       N        Y
    SystemNotImplemented10,              // 40       Y        N
    SystemNotImplemented11,              // 41       Y        N
    SystemInvalidInfoClass2,             // 42
    SystemInvalidInfoClass3,             // 43
    SystemTimeZoneInformation,           // 44       Y        N
    SystemLookasideInformation,          // 45       Y        N
    SystemSetTimeSlipEvent,              // 46       N        Y
    SystemCreateSession,                 // 47       N        Y
    SystemDeleteSession,                 // 48       N        Y
    SystemInvalidInfoClass4,             // 49
    SystemRangeStartInformation,         // 50       Y        N
    SystemVerifierInformation,           // 51       Y        Y
    SystemAddVerifier,                   // 52       N        Y
    SystemSessionProcessesInformation    // 53       Y        N
} SYSTEM_INFORMATION_CLASS;
typedef struct _CLIENT_ID {
    HANDLE UniqueProcess;
    HANDLE UniqueThread;
} CLIENT_ID;
typedef CLIENT_ID * PCLIENT_ID;
typedef enum _PROCESSINFOCLASS {
ProcessBasicInformation,            // 0    Y       N
ProcessQuotaLimits,                 // 1    Y       Y
ProcessIoCounters,                  // 2    Y       N
ProcessVmCounters,                  // 3    Y       N
ProcessTimes,                       // 4    Y       N
ProcessBasePriority,                // 5    N       Y
ProcessRaisePriority,               // 6    N       Y
ProcessDebugPort,                   // 7    Y       Y
ProcessExceptionPort,               // 8    N       Y
ProcessAccessToken,                 // 9    N       Y
ProcessLdtInformation,              // 10   Y       Y
ProcessLdtSize,                     // 11   N       Y
ProcessDefaultHardErrorMode,        // 12   Y       Y
ProcessIoPortHandlers,              // 13   N       Y
ProcessPooledUsageAndLimits,        // 14   Y       N
ProcessWorkingSetWatch,             // 15   Y       Y
ProcessUserModeIOPL,                // 16   N       Y
ProcessEnableAlignmentFaultFixup,   // 17   N       Y
ProcessPriorityClass,               // 18   N       Y
ProcessWx86Information,             // 19   Y       N
ProcessHandleCount,                 // 20   Y       N
ProcessAffinityMask,                // 21   N       Y
ProcessPriorityBoost,               // 22   Y       Y
ProcessDeviceMap,          
ProcessSessionInformation,          // 24   Y       Y
ProcessForegroundInformation,       // 25   N       Y
ProcessWow64Information             // 26   Y       N
} PROCESSINFOCLASS;

typedef struct _SYSTEM_HANDLE_INFORMATION { // Information Class 16
ULONG ProcessId;
UCHAR ObjectTypeNumber;
UCHAR Flags;  // 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
USHORT Handle;
PVOID Object;
ACCESS_MASK GrantedAccess;
} SYSTEM_HANDLE_INFORMATION, *PSYSTEM_HANDLE_INFORMATION;
//typedef  DWORD NTSTATUS;

typedef struct _PROCESS_BASIC_INFORMATION { // Information Class 0
DWORD ExitStatus;
DWORD PebBaseAddress;
DWORD AffinityMask;
DWORD BasePriority;
ULONG UniqueProcessId;
ULONG InheritedFromUniqueProcessId;
} PROCESS_BASIC_INFORMATION, *PPROCESS_BASIC_INFORMATION;

typedef DWORD (WINAPI*ZWCLOSE)(HANDLE );
typedef DWORD (WINAPI*ZWQUERYSYSTEMINFORMATION)(IN SYSTEM_INFORMATION_CLASS SystemInformationClass,
    OUT PVOID SystemInformation,
    IN ULONG SystemInformationLength,
    OUT PULONG ReturnLength 
    );
typedef DWORD (WINAPI*ZWOPENPROCESS)(OUT PHANDLE ProcessHandle,
    IN ACCESS_MASK DesiredAccess,
    IN POBJECT_ATTRIBUTES ObjectAttributes,
    IN PCLIENT_ID ClientId
       );


typedef DWORD (WINAPI*ZWDUPLICATEOBJECT)(

                  IN HANDLE SourceProcessHandle,
                  IN HANDLE SourceHandle,
                  IN HANDLE TargetProcessHandle,
                  OUT PHANDLE TargetHandle OPTIONAL,
                  IN ACCESS_MASK DesiredAccess,
                  IN ULONG Attributes,
                  IN ULONG Options
                   );
typedef DWORD (WINAPI*ZWQUERYINFORMATIONPROCESS)(HANDLE ProcessHandle,
     PROCESSINFOCLASS ProcessInformationClass,
     PVOID ProcessInformation,
     ULONG ProcessInformationLength,
     OUT PULONG ReturnLength
          );
typedef DWORD (WINAPI*ZWALLOCATEVIRTUALMEMORY)( IN HANDLE ProcessHandle,
    IN OUT PVOID *BaseAddress,
    IN ULONG ZeroBits,
    IN OUT PULONG AllocationSize,
    IN ULONG AllocationType,
    IN ULONG Protect
       );
typedef DWORD (WINAPI*ZWPROTECTVIRTUALMEMORY)(IN HANDLE ProcessHandle,
    IN OUT PVOID *BaseAddress,
    IN OUT PULONG ProtectSize,
    IN ULONG NewProtect,
    OUT PULONG OldProtect
        );
typedef DWORD (WINAPI*ZWWRITEVIRTUALMEMORY)(IN HANDLE ProcessHandle,
    IN PVOID BaseAddress,
    IN PVOID Buffer,
    IN ULONG BufferLength,
    OUT PULONG ReturnLength
              );
typedef DWORD (WINAPI*ZWFREEVIRTUALMEMORY )(IN HANDLE ProcessHandle,
    IN OUT PVOID *BaseAddress,
    IN OUT PULONG FreeSize,
    IN ULONG FreeType
             );


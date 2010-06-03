#define RVATOVA(base,offset) ((PVOID)((DWORD)(base)+(DWORD)(offset)))
#define ibaseDD *(PDWORD)&ibase
#define STATUS_INFO_LENGTH_MISMATCH      ((NTSTATUS)0xC0000004L)
#define NT_SUCCESS(Status) ((NTSTATUS)(Status) >= 0)
//#define SystemModuleInformation    11

typedef LONG NTSTATUS;

typedef struct {
    WORD    offset:12;
    WORD    type:4;
}IMAGE_FIXUP_ENTRY, *PIMAGE_FIXUP_ENTRY;

typedef struct _SYSTEM_MODULE_INFORMATION {//Information Class 11
    ULONG    Reserved[2];
    PVOID    Base;
    ULONG    Size;
    ULONG    Flags;
    USHORT    Index;
    USHORT    Unknown;
    USHORT    LoadCount;
    USHORT    ModuleNameOffset;
    CHAR    ImageName[256];
}SYSTEM_MODULE_INFORMATION,*PSYSTEM_MODULE_INFORMATION;

typedef struct {
    DWORD    dwNumberOfModules;
    SYSTEM_MODULE_INFORMATION    smi;
} MODULES, *PMODULES;

typedef struct _MY_DATA //猜猜这个结构体是干啥的:-)
{
ULONG index;
ULONG Addr;
} MY_DATA,*PMY_DATA;

extern "C"
NTSTATUS
WINAPI
NtQuerySystemInformation(    
    DWORD    SystemInformationClass,
    PVOID    SystemInformation,
    ULONG    SystemInformationLength,
    PULONG    ReturnLength
    );


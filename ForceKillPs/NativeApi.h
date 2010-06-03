#pragma once

typedef struct _DEBUG_MODULE_INFORMATION { 
ULONG Reserved[2];
ULONG Base;
ULONG Size;
ULONG Flags;
USHORT Index;
USHORT Unknown;
USHORT LoadCount;
USHORT ModuleNameOffset;
CHAR ImageName[256];
} DEBUG_MODULE_INFORMATION, *PDEBUG_MODULE_INFORMATION;
typedef struct _DEBUG_BUFFER {
HANDLE SectionHandle;
PVOID SectionBase;
PVOID RemoteSectionBase;
ULONG SectionBaseDelta;
HANDLE EventPairHandle;
ULONG Unknown[2];
HANDLE RemoteThreadHandle;
ULONG InfoClassMask;
ULONG SizeOfInfo;
ULONG AllocatedSize;
ULONG SectionSize;
PVOID ModuleInformation;
PVOID BackTraceInformation;
PVOID HeapInformation;
PVOID LockInformation;
PVOID Reserved[8];
} DEBUG_BUFFER, *PDEBUG_BUFFER;



typedef NTSTATUS (WINAPI *RTLQUERYPROCESSDEBUGINFORMATION)(IN ULONG ProcessId,
IN ULONG DebugInfoClassMask,
IN PDEBUG_BUFFER DebugBuffer);
typedef PDEBUG_BUFFER (WINAPI*RTLCREATEQUERYDEBUGBUFFER)(IN ULONG Size,
IN BOOLEAN EventPair);
typedef NTSTATUS (WINAPI * RTLDESTROYDEBUGBUFFER)(IN PDEBUG_BUFFER DebugBuffer);

ULONG PDI_MODULES =     0x01 ; // The loaded modules of the process
ULONG PDI_BACKTRACE =   0x02 ; // The heap stack back traces
ULONG PDI_HEAPS=        0x04 ; // The heaps of the process
ULONG PDI_HEAP_TAGS =   0x08 ; // The heap tags
ULONG PDI_HEAP_BLOCKS = 0x10 ; // The heap blocks
ULONG PDI_LOCKS =       0x20 ; // The locks created by the process

#include "stdafx.h"


#pragma comment(lib,"ntdll")

DWORD GetHeaders(PCHAR ibase,
                PIMAGE_FILE_HEADER *pfh,
                PIMAGE_OPTIONAL_HEADER *poh,
                PIMAGE_SECTION_HEADER *psh);
				
DWORD FindKiServiceTable(HMODULE hModule,DWORD dwKSDT);

int GetOrigSSDTAddrRVA(int index,char *Name,int *base);
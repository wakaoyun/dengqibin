#include"GetOrigSSDT.h"
#include "stdafx.h"
#include"GetOrigSSDTDef.h"
DWORD GetHeaders(PCHAR ibase,
                PIMAGE_FILE_HEADER *pfh,
                PIMAGE_OPTIONAL_HEADER *poh,
                PIMAGE_SECTION_HEADER *psh)

{
    PIMAGE_DOS_HEADER mzhead=(PIMAGE_DOS_HEADER)ibase;
    
    if((mzhead->e_magic!=IMAGE_DOS_SIGNATURE) ||        
        (ibaseDD[mzhead->e_lfanew]!=IMAGE_NT_SIGNATURE))
        return FALSE;
    
    *pfh=(PIMAGE_FILE_HEADER)&ibase[mzhead->e_lfanew];
    if (((PIMAGE_NT_HEADERS)*pfh)->Signature!=IMAGE_NT_SIGNATURE) 
        return FALSE;
		
    *pfh=(PIMAGE_FILE_HEADER)((PBYTE)*pfh+sizeof(IMAGE_NT_SIGNATURE));
    
    *poh=(PIMAGE_OPTIONAL_HEADER)((PBYTE)*pfh+sizeof(IMAGE_FILE_HEADER));
    if ((*poh)->Magic!=IMAGE_NT_OPTIONAL_HDR32_MAGIC)
        return FALSE;
    
    *psh=(PIMAGE_SECTION_HEADER)((PBYTE)*poh+sizeof(IMAGE_OPTIONAL_HEADER));
		return TRUE;
}


DWORD FindKiServiceTable(HMODULE hModule,DWORD dwKSDT)
{
    PIMAGE_FILE_HEADER    pfh;
    PIMAGE_OPTIONAL_HEADER    poh;
    PIMAGE_SECTION_HEADER    psh;
    PIMAGE_BASE_RELOCATION    pbr;
    PIMAGE_FIXUP_ENTRY    pfe;    
    
    DWORD    dwFixups=0,i,dwPointerRva,dwPointsToRva,dwKiServiceTable;
    BOOL    bFirstChunk;

    GetHeaders((char *)hModule,&pfh,&poh,&psh);

    // loop thru relocs to speed up the search
    if ((poh->DataDirectory[IMAGE_DIRECTORY_ENTRY_BASERELOC].VirtualAddress) &&
        (!((pfh->Characteristics)&IMAGE_FILE_RELOCS_STRIPPED))) {
        
        pbr=(PIMAGE_BASE_RELOCATION)RVATOVA(poh->DataDirectory[IMAGE_DIRECTORY_ENTRY_BASERELOC].VirtualAddress,hModule);

        bFirstChunk=TRUE;
        // 1st IMAGE_BASE_RELOCATION.VirtualAddress of ntoskrnl is 0
        while (bFirstChunk || pbr->VirtualAddress) {
            bFirstChunk=FALSE;

            pfe=(PIMAGE_FIXUP_ENTRY)((DWORD)pbr+sizeof(IMAGE_BASE_RELOCATION));

            for (i=0;i<(pbr->SizeOfBlock-sizeof(IMAGE_BASE_RELOCATION))>>1;i++,pfe++) {
                if (pfe->type==IMAGE_REL_BASED_HIGHLOW) {
                    dwFixups++;
                    dwPointerRva=pbr->VirtualAddress+pfe->offset;
                    // DONT_RESOLVE_DLL_REFERENCES flag means relocs aren't fixed
                    dwPointsToRva=*(PDWORD)((DWORD)hModule+dwPointerRva)-(DWORD)poh->ImageBase;

                    // does this reloc point to KeServiceDescriptorTable.Base?
                    if (dwPointsToRva==dwKSDT) {
                        // check for mov [mem32],imm32. we are trying to find 
                        // "mov ds:_KeServiceDescriptorTable.Base, offset _KiServiceTable"
                        // from the KiInitSystem.
                        if (*(PWORD)((DWORD)hModule+dwPointerRva-2)==0x05c7) {
                            // should check for a reloc presence on KiServiceTable here
                            // but forget it
                            dwKiServiceTable=*(PDWORD)((DWORD)hModule+dwPointerRva+4)-poh->ImageBase;
                            return dwKiServiceTable;
                        }
                    }
                    
                } 
            }
            *(PDWORD)&pbr+=pbr->SizeOfBlock;
        }
    }    
    return 0;
}


int GetOrigSSDTAddrRVA(int index,char *Name,int *base)
{
	HMODULE  hKernel;
    DWORD    dwKSDT;                // rva of KeServiceDescriptorTable
    DWORD    dwKiServiceTable;    // rva of KiServiceTable
    PMODULES    pModules=(PMODULES)&pModules;
    DWORD    dwNeededSize,rc;
    DWORD    dwKernelBase,dwServices=0;
    PCHAR    pKernelName;
    PDWORD    pService;
    PIMAGE_FILE_HEADER    pfh;
    PIMAGE_OPTIONAL_HEADER    poh;
    PIMAGE_SECTION_HEADER    psh;
    
    //通过NtQuerySystemInformation取得系统内核文件，判断为是ntoskrnl.exe ntkrnlmp.exe ntkrnlpa.exe
    rc=NtQuerySystemInformation(11,pModules,4,&dwNeededSize);
	if (rc==STATUS_INFO_LENGTH_MISMATCH) //如果内存不够
		{
        pModules=(PMODULES)GlobalAlloc(GPTR,dwNeededSize) ; //重新分配内存
        rc=NtQuerySystemInformation(11,pModules,dwNeededSize,NULL); //系统内核文件是总是在第一个，枚举1次
		} 
      
   if (!NT_SUCCESS(rc))
		{
		//MessageBox(NULL,"NtQuerySystemInformation() Failed !\n","",0); //NtQuerySystemInformation执行失败，检查当前进程权限
		MessageBoxA(NULL,"错误代码：01!\n","Error!",0);
		return 0;
		}
 
    dwKernelBase=(DWORD)pModules->smi.Base;   // imagebase
    *base = dwKernelBase;
    pKernelName=pModules->smi.ModuleNameOffset+pModules->smi.ImageName;
	//MessageBox (NULL,pKernelName,"pKernelName",0);
    memcpy(Name,pKernelName,30);
    hKernel=LoadLibraryExA(pKernelName,0,DONT_RESOLVE_DLL_REFERENCES);    // 映射ntoskrnl 
	if (!hKernel)
		{
        //MessageBox(NULL,"Failed to load \n","",0);
		MessageBoxA(NULL,"错误代码：02!\n","Error!",0);
        return 0;        
		}
    GlobalFree(pModules);
   if (!(dwKSDT=(DWORD)GetProcAddress(hKernel,"KeServiceDescriptorTable"))) //在内核文件中查找KeServiceDescriptorTable地址
		{
        //MessageBox (NULL,"Can't find KeServiceDescriptorTable\n","",0);
		MessageBoxA(NULL,"错误代码：03!\n","Error!",0);
        return 0;
		}

    dwKSDT-=(DWORD)hKernel;       // 获取 KeServiceDescriptorTable RVA
    if (!(dwKiServiceTable=FindKiServiceTable(hKernel,dwKSDT)))   // 获取KiServiceTable地址
		{
        //MessageBox (NULL,"Can't find KiServiceTable...\n","",0);
		MessageBoxA(NULL,"错误代码：04!\n","Error!",0);
        return 0;
		}

    GetHeaders((char *)hKernel,&pfh,&poh,&psh); 
   	//CString s;
	int dwIndex=0;
	pService = (PDWORD)((DWORD)hKernel+dwKiServiceTable);
	pService += index;
	if ( *pService - poh->ImageBase < poh->SizeOfImage )
		{
		//printf("The index 0x%X,Addr 0x%08X",index,*pService-poh->ImageBase+dwKernelBase);
		//return *pService-poh->ImageBase+dwKernelBase;
		  return *pService-poh->ImageBase;
		}
	else
		{
		//MessageBox (NULL,"Get Addr Error...\n","",0);
		MessageBoxA(NULL,"错误代码：05!\n","Error!",0);
		return 0;
		}
	/*for ( pService = (PDWORD)((DWORD)hKernel+dwKiServiceTable);
		 *pService - poh->ImageBase < poh->SizeOfImage;
         pService++,dwServices++,dwIndex++)
		{
		s.Format("0x%03X-0x%08X\n",dwIndex,*pService-poh->ImageBase+dwKernelBase);   //SSDT索引和地址，但这里的索引顺序不存在对应关系。 
		s.Format("0x%03X",dwIndex);  
		m_ListSSDT.InsertItem(0,s);
		s.Format("0x%08X\n",*pService-poh->ImageBase+dwKernelBase);   
		m_ListSSDT.SetItemText(0,1,s);
		 }*/
    FreeLibrary(hKernel);
	return 1;
}
// pe.cpp: implementation of the Cpe class.
//
//////////////////////////////////////////////////////////////////////
#include "pe.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

Cpe::Cpe()
{

}

Cpe::~Cpe()
{

}

void Cpe::CalcAddress(const void *base)
{
	IMAGE_DOS_HEADER *dos_header=(IMAGE_DOS_HEADER *)base;

	if(dos_header->e_magic!=IMAGE_DOS_SIGNATURE)
	{
		MessageBox(NULL,"Unknow type of file.",NULL,MB_OK);
		return;
	}

	peHeader *header;

	header=(peHeader *)((char *)dos_header + dos_header->e_lfanew);

	if(IsBadReadPtr(header,sizeof(*header)))
	{
		MessageBox(NULL,"No PE header, probable DOS executable.",NULL,MB_OK);
		return;
	}

	DWORD mods;
	char tmpstr[4]={0};

	if(strstr((const char *) header->section_header[0].Name,".text")!=NULL)
	{
		dwVirtSize=header->section_header[0].Misc.VirtualSize;
		dwPhysAddress=header->section_header[0].PointerToRawData;
		dwPhysSize=header->section_header[0].SizeOfRawData;
		dwPeAddress=dos_header->e_lfanew;

		dwSpace=dwPhysSize-dwVirtSize;
		dwProgRAV=header->opt_head.ImageBase;
		dwCodeOffset=header->opt_head.BaseOfCode-dwPhysAddress;
		dwEntryWrite=header->section_header[0].PointerToRawData + 
			header->section_header[0].Misc.VirtualSize;
		mods=dwEntryWrite%16;

		if(mods!=0)
		{
			dwEntryWrite+=16-mods;
		}
		dwOldEntryAddress=header->opt_head.AddressOfEntryPoint;
		dwNewEntryAddress=dwEntryWrite+dwCodeOffset;
		return;
	}
}

void Cpe::ModifyPe(CString strFileName, CString strMsg)
{	
	HANDLE hFile,hMapping;
	void *basepointer;

	hFile = CreateFile(strFileName,GENERIC_READ|GENERIC_WRITE,
		FILE_SHARE_READ|FILE_SHARE_WRITE,0,OPEN_EXISTING,
		FILE_FLAG_SEQUENTIAL_SCAN,0);

	if(hFile==INVALID_HANDLE_VALUE)
	{
		
		return;
	}

	hMapping=CreateFileMapping(hFile,NULL,PAGE_READONLY|SEC_COMMIT,0,0,NULL);

	if(!hMapping)
	{
		MessageBox(NULL,"Mapping failed.",NULL,MB_OK);
		CloseHandle(hFile);
		return;
	}

	if(!(basepointer=MapViewOfFile(hMapping,FILE_MAP_READ,0,0,0)))
	{
		MessageBox(NULL,"View failed.",NULL,MB_OK);
		CloseHandle(hMapping);
		CloseHandle(hFile);
		return;
	}

	CloseHandle(hMapping);
	CloseHandle(hFile);

	CalcAddress(basepointer);
	UnmapViewOfFile(basepointer);

	if(dwSpace<50)
	{
		MessageBox(NULL,"No room to write the data!",NULL,MB_OK);
	}
	else
	{
		WritePe(strFileName,strMsg);
	}

	hFile = CreateFile(strFileName,GENERIC_READ|GENERIC_WRITE,
	FILE_SHARE_READ|FILE_SHARE_WRITE,0,OPEN_EXISTING,
	FILE_FLAG_SEQUENTIAL_SCAN,0);

	if(hFile==INVALID_HANDLE_VALUE)
	{
		MessageBox(NULL,"Could not open file.",NULL,MB_OK);
		return;
	}

	CloseHandle(hFile);
}

void Cpe::WritePe(CString strFileName, CString strMsg)
{
	CString strAddress1,strAddress2;
	int ret;
	unsigned char waddress[4]={0};

	ret=_open(strFileName,_O_RDWR|_O_CREAT|_O_BINARY,_S_IREAD|_S_IWRITE);
	if(!ret)
	{
		MessageBox(NULL,"Error open.",NULL,MB_OK);
		return;
	}

	if(!WriteNewEntry(ret,(long)(dwPeAddress+40),dwNewEntryAddress))
	{
		_close(ret);
		return;
	}
	if(!WriteMessageBox(ret,(long)dwEntryWrite,TEXT("Test"),TEXT("We are the world!")))
	{
		_close(ret);
		return;
	}

	_close(ret);
}

BOOL Cpe::WriteNewEntry(int ret, long offset, DWORD dwAddress)
{
	CString strErrMsg;
	long retf;
	unsigned char waddress[4]={0};

	retf=_lseek(ret,offset,SEEK_SET);
	if(-1==retf)
	{
		MessageBox(NULL,"Error seek.",NULL,MB_OK);
		return FALSE;
	}

	memcpy(waddress,StrOfDWord(dwAddress),4);
	retf=_write(ret,waddress,4);

	if(-1==retf)
	{
		strErrMsg.Format("Error write:%d",GetLastError());
		MessageBox(NULL,strErrMsg,NULL,MB_OK);
		return FALSE;
	}

	return TRUE;
}

BOOL Cpe::WriteMessageBox(int ret, long offset, CString strCap, CString strTxt)
{
	CString strAddress1,strAddress2;
	unsigned char waddress[4]={0};
	DWORD dwAddress;

	HINSTANCE gLigMsg=LoadLibrary(TEXT("user32.dll"));
	dwMessageBoxAddress=(DWORD)GetProcAddress(gLigMsg,"MessageBoxA");

	int nLenCap1=strCap.GetLength()+1;
	int nLenTxt1=strTxt.GetLength()+1;
	int nTotLen=nLenCap1+nLenTxt1+24;

	dwAddress=dwMessageBoxAddress-(dwProgRAV+dwNewEntryAddress+nTotLen-5);
	strAddress2=StrOfDWord(dwAddress);

	unsigned char cHeader[2]={0x6a,0x40};
	unsigned char cDesCap[5]={0xe8,nLenCap1,0x00,0x00,0x00};
	unsigned char cDesTxt[5]={0xe8,nLenTxt1,0x00,0x00,0x00};
	unsigned char cFix[12]={0x6a,0x00,0xe8,0x00,0x00,0x00,
								0x00,0x00,0xe9,0x00,0x00,0x00};

	for(int i=0;i<4;i++)
	{
		cFix[3+i]=strAddress1.GetAt(i);
	}
	for(int i=0;i<4;i++)
	{
		cFix[8+i]=strAddress1.GetAt(i);
	}

	char *cMessageBox=new char[nTotLen];
	char *cMsg;
	memcpy((cMsg=cMessageBox),(char *)cHeader,2);
	memcpy((cMsg+=2),cDesCap,5);
	memcpy((cMsg+=5),strCap,nLenCap1);
	memcpy((cMsg+=nLenCap1),cDesTxt,5);
	memcpy((cMsg+=5),strTxt,nLenTxt1);
	memcpy((cMsg+=nLenTxt1),cFix,12);

	CString strErrMsg;
	long retf;
	retf=_lseek(ret,(long)dwEntryWrite,SEEK_SET);
	if(-1==retf)
	{
		delete[] cMessageBox;
		MessageBox(NULL,"Error seek.",NULL,MB_OK);
		return FALSE;
	}
	
	retf=_write(ret,cMessageBox,nTotLen);

	if(-1==retf)
	{
		delete[] cMessageBox;
		strErrMsg.Format("Error write:%d",GetLastError());
		MessageBox(NULL,strErrMsg,NULL,MB_OK);
		return FALSE;
	}
	
	delete[] cMessageBox;
	return TRUE;
}

CString Cpe::StrOfDWord(DWORD dwAddress)
{
	unsigned char waddress[4]={0};

	waddress[3]=(char)(dwAddress>>24)&0xFF;
	waddress[2]=(char)(dwAddress>>16)&0xFF;
	waddress[1]=(char)(dwAddress>>8)&0xFF;
	waddress[0]=(char)(dwAddress)&0xFF;

	return waddress;
}

// pe.h: interface for the Cpe class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_PE_H__EB15B25F_8A7E_4B8D_8590_3197A9E5E4C3__INCLUDED_)
#define AFX_PE_H__EB15B25F_8A7E_4B8D_8590_3197A9E5E4C3__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <stdio.h>
#include <STRING.H>
#include <Afx.h>
#include <windef.h>
#include <winnt.h>
#include <io.h>
#include <fcntl.h>
#include <sys\stat.h>


typedef struct PE_HEADER_MAP
{
	DWORD signature;
	IMAGE_FILE_HEADER _head;
	IMAGE_OPTIONAL_HEADER opt_head;
	IMAGE_SECTION_HEADER section_header[6];
} peHeader;

class Cpe  
{
public:
	CString StrOfDWord(DWORD dwAddress);
	DWORD dwSpace;
	DWORD dwEntryAddress;
	DWORD dwEntryWrite;
	DWORD dwProgRAV;
	DWORD dwOldEntryAddress;
	DWORD dwNewEntryAddress;
	DWORD dwCodeOffset;
	DWORD dwPeAddress;
	DWORD dwFlagAddress;
	DWORD dwVirtSize;
	DWORD dwPhysAddress;
	DWORD dwPhysSize;
	DWORD dwMessageBoxAddress;
	BOOL WriteMessageBox(int ret,long offset, CString strCap,CString strTxt);
	BOOL WriteNewEntry(int ret,long offset,DWORD dwAddress);
	void WritePe(CString strFileName,CString strMsg);
	void ModifyPe(CString strFileName,CString strMsg);
	void CalcAddress(const void *base);
	Cpe();
	virtual ~Cpe();

};

#endif // !defined(AFX_PE_H__EB15B25F_8A7E_4B8D_8590_3197A9E5E4C3__INCLUDED_)

#include "StdAfx.h"
#include "TaskSetting.h"


CTaskSetting::CTaskSetting(void)
{
}


CTaskSetting::~CTaskSetting(void)
{
}

BOOL CTaskSetting::GetColumnSetting(WCHAR *columnSetting)
{
	CRegistry reg;
	/*Load the show column info*/
	if(reg.Open(_T("Software\\Wakaoyun\\MyTask\\Proc\\")))
	{
		if(reg.Read(_T("Column"),columnSetting))
		{
			return TRUE;
		}
	}

	return FALSE;
}

BOOL CTaskSetting::SetColumnSetting(const WCHAR *columnSetting)
{
	CRegistry reg;
	/*save the column info*/
	if(!reg.Open(_T("Software\\Wakaoyun\\MyTask\\Proc")))
	{
		reg.CreateKey(_T("Software\\Wakaoyun\\MyTask\\Proc"));
	}
	
	return reg.Write(_T("Column"), columnSetting);
}

int CTaskSetting::GetSortColumn()
{
	CRegistry reg;
	DWORD column = 0;
	/*Load the sort column info*/
	if(reg.Open(_T("Software\\Wakaoyun\\MyTask\\Proc\\")))
	{
		reg.Read(_T("SortColumn"),&column);
	}
	
	return (int)column;
}

void CTaskSetting::SetSortColumn(const BOOL isASC /* = FALSE */,int sortColumn /*= 0*/)
{
	CRegistry reg;
	/*save the sort column info*/
	if(!reg.Open(_T("Software\\Wakaoyun\\MyTask\\Proc")))
	{
		reg.CreateKey(_T("Software\\Wakaoyun\\MyTask\\Proc"));
	}

	reg.Write(_T("IsASC"), (int)isASC);
	reg.Write(_T("SortColumn"), (int)sortColumn);
}

BOOL CTaskSetting::IsASC()
{
	CRegistry reg;
	DWORD isASC = 0;
	/*Load the sort column info*/
	if(reg.Open(_T("Software\\Wakaoyun\\MyTask\\Proc\\")))
	{
		reg.Read(_T("IsASC"),&isASC);
	}

	return (DWORD)isASC;
}

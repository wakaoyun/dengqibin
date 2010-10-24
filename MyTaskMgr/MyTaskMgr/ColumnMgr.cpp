#include "StdAfx.h"
#include "ColumnMgr.h"


CColumnMgr::ColumnEntry defaultColums[MAX_COLUM] =
{
	{_T("Image Name"),90,TRUE,COLUMN_IMAGENAME},
	{_T("PID"),50,FALSE,COLUMN_PID},
	{_T("User Name"),90,TRUE,COLUMN_USERNAME},
	{_T("Session ID"),35,FALSE,COLUMN_SESSIONID},
	{_T("CPU"),35,TRUE,COLUMN_CPUUSAGE},
	{_T("CPU Time"),70,FALSE,COLUMN_CPUTIME},
	{_T("Working Set(Memory)"),70,TRUE,COLUMN_MEMORYUSAGE},
	{_T("Peak Working Set(Memory)"),100,FALSE,COLUMN_PEAKMEMORYUSAGE},
	{_T("Working Set Delta(Memory)"),70,FALSE,COLUMN_MEMORYUSAGEDELTA},
	{_T("Page Faults"),70,FALSE,COLUMN_PAGEFAULTS},
	{_T("PF Delta"),70,FALSE,COLUMN_PAGEFAULTSDELTA},
	{_T("Commit Size"),70,FALSE,COLUMN_VIRTUALMEMORYSIZE},
	{_T("Page Pool"),70,FALSE,COLUMN_PAGEDPOOL},
	{_T("NP Pool"),70,FALSE,COLUMN_NONPAGEDPOOL},
	{_T("Base Pri"),60,FALSE,COLUMN_BASEPRIORITY},
	{_T("Handles"),60,FALSE,COLUMN_HANDLECOUNT},
	{_T("Threads"),60,FALSE,COLUMN_THREADCOUNT},
	{_T("User Objects"),60,FALSE,COLUMN_USEROBJECTS},
	{_T("GDI Objects"),60,FALSE,COLUMN_GDIOBJECTS},
	{_T("I/O Reads"),70,FALSE,COLUMN_IOREADS},
	{_T("I/O Writes"),70,FALSE,COLUMN_IOWRITES},
	{_T("I/O Other"),70,FALSE,COLUMN_IOOTHER},
	{_T("I/O Read Bytes"),70,FALSE,COLUMN_IOREADBYTES},
	{_T("I/O Write Bytes"),70,FALSE,COLUMN_IOWRITEBYTES},
	{_T("I/O Other Bytes"),70,FALSE,COLUMN_IOOTHERBYTES}
};

CColumnMgr::CColumnMgr(void)
{
	memcpy(columns, defaultColums, sizeof(ColumnEntry) * MAX_COLUM);
	CTaskSetting taskSetting;
	
	WCHAR data[2*MAX_COLUM+1] = {0};
	if(taskSetting.GetColumnSetting(data))
	{
		WCHAR *token;
		token = wcstok(data,_T("|"));
		int i = 0;
		while(token != NULL)
		{
			columns[i].isShow = *token - '0';
			token = wcstok(NULL,_T("|"));
			i++;
		}
	}
}


CColumnMgr::~CColumnMgr(void)
{
	CTaskSetting taskSetting;
	WCHAR pValue[2*MAX_COLUM+1],*p;
	p=pValue;
	for (int i=0; i<MAX_COLUM; i++)
	{
		swprintf(p++, _T("%s|"),columns[i].isShow ? "1" : "0");
		p++;
	}
	taskSetting.SetColumnSetting(pValue);
}

void CColumnMgr::AddColumns(HWND hWnd)
{
	for (int i = 0; i < MAX_COLUM; i++)
	{
		if(columns[i].isShow)
		{
			LVCOLUMN  col;
			col.mask = LVCF_TEXT | LVCF_SUBITEM;
			col.pszText = columns[i].name;
			col.iSubItem = i;

			if (columns[i].width != -1)
			{
				col.mask |= LVCF_WIDTH;
				col.cx = columns[i].width;
			}
			ListView_InsertColumn(hWnd, i, &col);
		}
	}
}

const CColumnMgr::ColumnEntry * CColumnMgr::GetColumns()
{
	return columns;
}

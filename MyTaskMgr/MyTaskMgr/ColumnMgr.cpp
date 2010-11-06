#include "StdAfx.h"
#include "ColumnMgr.h"


CColumnMgr::ColumnEntry defaultColums[MAX_COLUM] =
{
	{_T("Image Name"),90,TRUE,COLUMN_IMAGENAME, FALSE},
	{_T("PID"),50,FALSE,COLUMN_PID, FALSE},
	{_T("User Name"),90,TRUE,COLUMN_USERNAME, FALSE},
	{_T("Session ID"),35,FALSE,COLUMN_SESSIONID, FALSE},
	{_T("CPU"),35,TRUE,COLUMN_CPUUSAGE, FALSE},
	{_T("CPU Time"),70,FALSE,COLUMN_CPUTIME, FALSE},
	{_T("Working Set(Memory)"),70,TRUE,COLUMN_MEMORYUSAGE, TRUE},
	{_T("Peak Working Set(Memory)"),100,FALSE,COLUMN_PEAKMEMORYUSAGE, TRUE},
	{_T("Working Set Delta(Memory)"),70,FALSE,COLUMN_MEMORYUSAGEDELTA, TRUE},
	{_T("Page Faults"),70,FALSE,COLUMN_PAGEFAULTS, TRUE},
	{_T("PF Delta"),70,FALSE,COLUMN_PAGEFAULTSDELTA, TRUE},
	{_T("Commit Size"),70,FALSE,COLUMN_VIRTUALMEMORYSIZE, TRUE},
	{_T("Page Pool"),70,FALSE,COLUMN_PAGEDPOOL, TRUE},
	{_T("NP Pool"),70,FALSE,COLUMN_NONPAGEDPOOL, TRUE},
	{_T("Base Pri"),60,FALSE,COLUMN_BASEPRIORITY, TRUE},
	{_T("Handles"),60,FALSE,COLUMN_HANDLECOUNT, TRUE},
	{_T("Threads"),60,FALSE,COLUMN_THREADCOUNT, TRUE},
	{_T("User Objects"),60,FALSE,COLUMN_USEROBJECTS, TRUE},
	{_T("GDI Objects"),60,FALSE,COLUMN_GDIOBJECTS, TRUE},
	{_T("I/O Reads"),70,FALSE,COLUMN_IOREADS, TRUE},
	{_T("I/O Writes"),70,FALSE,COLUMN_IOWRITES, TRUE},
	{_T("I/O Other"),70,FALSE,COLUMN_IOOTHER, TRUE},
	{_T("I/O Read Bytes"),70,FALSE,COLUMN_IOREADBYTES, TRUE},
	{_T("I/O Write Bytes"),70,FALSE,COLUMN_IOWRITEBYTES, TRUE},
	{_T("I/O Other Bytes"),70,FALSE,COLUMN_IOOTHERBYTES, TRUE}
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
			if (columns[i].isRight)
			{
				col.mask |=LVCF_FMT;
				col.fmt = LVCFMT_RIGHT;
			}
			ListView_InsertColumn(hWnd, i, &col);
		}
	}
}

const CColumnMgr::ColumnEntry * CColumnMgr::GetColumns()
{
	return columns;
}

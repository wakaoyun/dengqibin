#include "StdAfx.h"
#include "ColumnMgr.h"


CColumnMgr::ColumnEntry defaultColums[MAX_COLUM] =
{
	{_T("Image Name"),90,TRUE,COLUMN_IMAGENAME},
	{_T("PID"),50,TRUE,COLUMN_PID},
	{_T("User Name"),90,TRUE,COLUMN_USERNAME},
	{_T("Session ID"),35,TRUE,COLUMN_SESSIONID},
	{_T("CPU"),35,TRUE,COLUMN_CPUUSAGE},
	{_T("CPU Time"),70,TRUE,COLUMN_CPUTIME},
	{_T(""),70,FALSE,COLUMN_MEMORYUSAGE},
	{_T(""),100,FALSE,COLUMN_PEAKMEMORYUSAGE},
	{_T(""),70,FALSE,COLUMN_MEMORYUSAGEDELTA},
	{_T(""),70,FALSE,COLUMN_PAGEFAULTS},
	{_T(""),70,FALSE,COLUMN_PAGEFAULTSDELTA},
	{_T(""),70,FALSE,COLUMN_VIRTUALMEMORYSIZE},
	{_T(""),70,FALSE,COLUMN_PAGEDPOOL},
	{_T(""),70,FALSE,COLUMN_NONPAGEDPOOL},
	{_T(""),60,FALSE,COLUMN_BASEPRIORITY},
	{_T(""),60,FALSE,COLUMN_HANDLECOUNT},
	{_T(""),60,FALSE,COLUMN_THREADCOUNT},
	{_T(""),60,FALSE,COLUMN_USEROBJECTS},
	{_T(""),60,FALSE,COLUMN_GDIOBJECTS},
	{_T(""),70,FALSE,COLUMN_IOREADS},
	{_T(""),70,FALSE,COLUMN_IOWRITES},
	{_T(""),70,FALSE,COLUMN_IOOTHER},
	{_T(""),70,FALSE,COLUMN_IOREADBYTES},
	{_T(""),70,FALSE,COLUMN_IOWRITEBYTES},
	{_T(""),70,FALSE,COLUMN_IOOTHERBYTES}
};

CColumnMgr::CColumnMgr(void)
{
	memcpy(columns, defaultColums, sizeof(ColumnEntry) * MAX_COLUM);
}


CColumnMgr::~CColumnMgr(void)
{
}

void CColumnMgr::AddColumns(HWND hWnd)
{
	for (int i = 0; i < MAX_COLUM; i++)
	{
		if(columns[i].isShow)
		{
			LVCOLUMN  column;
			column.mask = LVCF_TEXT;
			column.pszText = columns[i].name;

			if (columns[i].width != -1)
			{
				column.mask |= LVCF_WIDTH;
				column.cx = columns[i].width;
			}
			ListView_InsertColumn(hWnd, i, &column);
		}
	}
}

const CColumnMgr::ColumnEntry * CColumnMgr::GetColumns()
{
	return columns;
}

#include "StdAfx.h"
#include "ColumnMgr.h"


CColumnMgr::ColumnEntry defaultColums[MAX_COLUM] =
{
	{_T("Image Name"),100,TRUE},{_T("PID"),100,TRUE},{_T("User Name"),100,FALSE},{_T("Session ID"),100,FALSE},{_T("CPU"),100,FALSE},
	{_T("CPU Time"),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},
	{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},
	{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},
	{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE},{_T(""),100,FALSE}
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

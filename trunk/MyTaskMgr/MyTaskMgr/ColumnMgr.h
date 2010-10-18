#pragma once
#define MAX_COLUM 25

class CColumnMgr
{
public:
	CColumnMgr(void);
	~CColumnMgr(void);
	void AddColumns(HWND hWnd);
public:
	typedef struct
	{
		LPWSTR name;
		int width;
		BOOL isShow;
	}ColumnEntry;
private:
	ColumnEntry columns[MAX_COLUM];
	
};

extern CColumnMgr::ColumnEntry defaultColums[MAX_COLUM];
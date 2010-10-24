#pragma once
#include "Registry.h"
class CTaskSetting
{
public:
	CTaskSetting(void);
	~CTaskSetting(void);
	BOOL GetColumnSetting(WCHAR *columnSetting);
	BOOL SetColumnSetting(const WCHAR *columnSetting);
	int GetSortColumn();
	void SetSortColumn(const BOOL isASC = FALSE, int sortColumn = 0);
	BOOL IsASC();
private:
};


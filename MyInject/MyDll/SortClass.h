#pragma once
#ifndef  _CSORTCLASS_H_
#define _CSORTCLASS_H_
class CSortClass
{
public:

	CSortClass();
	virtual ~CSortClass();
	CSortClass(CListCtrl* _pWnd,const int _iCol,const 
		bool _bIsNumeric);
public:
	int iCol;
	CListCtrl* pWnd;
	bool bIsNumeric;

public:
	void Sort(const bool bAsc);

	static int CALLBACK CompareAsc(LPARAM 
		lParam1,LPARAM lParam2,LPARAM lParamSort);

	static int CALLBACK CompareDes(LPARAM 
		lParam1,LPARAM lParam2,LPARAM lParamSort);

	static int CALLBACK CompareAscI(LPARAM 
		lParam1,LPARAM lParam2,LPARAM lParamSort);

	static int CALLBACK CompareDesI(LPARAM 
		lParam1,LPARAM lParam2,LPARAM lParamSort);

public:
	class CSortItem
	{
	public:
		virtual ~CSortItem();

		CSortItem(const DWORD _dw,const CString& _txt);
		CString txt;
		DWORD dw;
	};
	class CSortItemInt
	{
	public:
		CSortItemInt(const DWORD _dw,CString _txt);

		int iInt;

		DWORD dw;
	};
};

#endif //~_CSORTCLASS_H_

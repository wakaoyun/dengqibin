// Goods.cpp : implementation file
//

#include "stdafx.h"
#include "MyDll.h"
#include "Goods.h"


// CGoods dialog

IMPLEMENT_DYNAMIC(CGoods, CDialog)

CGoods::CGoods(CWnd* pParent /*=NULL*/)
	: CDialog(CGoods::IDD, pParent)
	, m_1(_T(""))
	, m_2(_T(""))
{

}

CGoods::~CGoods()
{
}

void CGoods::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST_BagGoods, m_List_BagGoods);
	DDX_Text(pDX, IDC_STATIC_1, m_1);
	DDX_Text(pDX, IDC_STATIC_2, m_2);
}


BEGIN_MESSAGE_MAP(CGoods, CDialog)
	ON_BN_CLICKED(IDC_BUTTON_Fresh, &CGoods::OnBnClickedButtonFresh)
END_MESSAGE_MAP()
// CGoods message handlers

BOOL CGoods::OnInitDialog()
{
	CDialog::OnInitDialog();

	m_List_BagGoods.InsertColumn(0,_T("编号"),LVCFMT_LEFT,50,-1);
	m_List_BagGoods.InsertColumn(1,_T("数量"),LVCFMT_LEFT,50,-1);
	m_List_BagGoods.InsertColumn(2,_T("最大数量"),LVCFMT_LEFT,50,-1);
	m_List_BagGoods.InsertColumn(3,_T("类型"),LVCFMT_LEFT,50,-1);
	m_List_BagGoods.InsertColumn(4,_T("所需的等级"),LVCFMT_LEFT,50,-1);
	m_List_BagGoods.InsertColumn(5,_T("格子"),LVCFMT_LEFT,40,-1);
	m_List_BagGoods.Invalidate();

	//GetBagGoods();
	
	return TRUE;
}

void CGoods::OnBnClickedButtonFresh()
{
	// TODO: Add your control notification handler code here
	GetBagGoods();
	UpdateData(FALSE);
}

// 获取包囊物品
void CGoods::GetBagGoods(void)
{
	DWORD playerBaseAddr = GetPlayerBaseAddr(g_GameBaseAddr);
	DWORD count;
	DWORD cellBaseAddr;

	__asm
	{
		pushad
		mov eax,playerBaseAddr
		mov eax,[eax + 0xB18]
		mov edx,[eax + 0x14]
		mov count,edx
		mov ebx,[eax + 0x10]
		mov cellBaseAddr,ebx
		popad
	}

	m_1.Format("%d",count);
	m_2.Format("%x",cellBaseAddr);
	
	m_List_BagGoods.DeleteAllItems();
	PBAGDATA pBagData;
	CString temp;
	int itemCount = 0;
	DWORD aaa;						  
	for (UINT i = 0; i < count; ++i)
	{
		__asm
		{
			pushad
			mov eax,cellBaseAddr
			mov edx,i
			mov eax,[eax + edx * 4]
			mov pBagData,eax
			popad
		}

		if (pBagData != 0)//没物品的格子
		{
			temp.Format("%x",pBagData->sysNo);
			m_List_BagGoods.InsertItem(itemCount++,temp);
			temp.Format("%d",pBagData->nCount);
			m_List_BagGoods.SetItemText(itemCount - 1,1,temp);
			temp.Format("%d",pBagData->maxCount);
			m_List_BagGoods.SetItemText(itemCount - 1,2,temp);
			temp.Format("%d",pBagData->type);
			m_List_BagGoods.SetItemText(itemCount - 1,3,temp);
			temp.Format("%d",pBagData->needLevel);
			m_List_BagGoods.SetItemText(itemCount - 1,4,temp);
			temp.Format("%d",i);
			m_List_BagGoods.SetItemText(itemCount - 1,5,temp);
		}
	}
}

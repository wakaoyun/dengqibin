// Skill.cpp : implementation file
//

#include "stdafx.h"
#include "MyDll.h"
#include "Skill.h"
#include "SortClass.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CSkill dialog

CListCtrl	m_List_Monster;

CSkill::CSkill(CWnd* pParent /*=NULL*/)
	: CDialog(CSkill::IDD, pParent)
{
	//{{AFX_DATA_INIT(CSkill)
	//}}AFX_DATA_INIT 
}  

void CSkill::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSkill)
	DDX_Control(pDX, IDC_LIST_Monster, m_List_Monster);
	DDX_Control(pDX, IDC_LIST_Skill, m_List_Skill);
	//}}AFX_DATA_MAP
} 

BEGIN_MESSAGE_MAP(CSkill, CDialog)
	//{{AFX_MSG_MAP(CSkill)
	ON_BN_CLICKED(IDC_BUTTON_Use_Skill, OnBUTTONUseSkill)
	ON_LBN_SELCHANGE(IDC_LIST_Skill, OnSelchangeLISTSkill)
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BUTTON1, OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON_Refresh, &CSkill::OnBnClickedButtonRefresh)
	ON_NOTIFY(LVN_COLUMNCLICK, IDC_LIST_Monster, &CSkill::OnLvnColumnclickListMonster)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSkill message handlers
void SortMonster(MONSTERDATA *m, int low, int hight);
void UseSkill(DWORD skillID, DWORD monsterID);
void SelectMonster(DWORD monsterID);
void CSkill::OnBUTTONUseSkill() 
{
	DWORD playerBaseAddr = GetPlayerBaseAddr(g_GameBaseAddr);
	CString id;
	DWORD skill_ID;
	m_List_Skill.GetText(m_List_Skill.GetCurSel(),id);
	DWORD monsterID = pplayerInfo->selectedMonsterID;
	sscanf(id,"%x",&skill_ID);
	__asm
	{
		pushad
		push 0
		push skill_ID
		mov ecx,playerBaseAddr
		call g_GetSkillAddr //找回城技能找到的
		mov ecx,playerBaseAddr
		mov [ecx+0xb98],eax
		push 0
		push 0
		push monsterID		
		call g_SkillAddr
		popad
	}
}

void CSkill::OnSelchangeLISTSkill() 
{
	UpdateData();	
}

void /*CSkill::*/GetMonsterList()
{
	DWORD monsterCount;
	DWORD monsterArrayAddr;
	DWORD mosterBaseAddr;
	CString temp;
	DWORD index;
	int count = 0;
	/*m_List_Monster.DeleteAllItems();*/
	__asm
	{
		pushad
		mov eax,g_GameBaseAddr
		mov eax,[eax]
		mov eax,[eax+0x1c]		
		mov eax,[eax+0x8]
		mov eax,[eax+0x24]
		mov ebx,[eax+0x24]
		mov monsterCount,ebx
		mov ebx,[eax+0x18]
		mov monsterArrayAddr,ebx
		popad
	}
	/*temp.Format("%x",monsterArrayAddr);
	SetDlgItemText(IDC_STATIC_Addr,temp);*/
	//memset(monsterList,0xFF,sizeof(MONSTERDATA)*MAX_COUNT);
	PMONSTERDATA pMonsterData;
	for (UINT i=0; i < monsterCount; i++)
	{
		__asm
		{
			pushad
			mov eax,monsterArrayAddr
			mov edx,i	
			mov ecx,[eax+edx*4]
			test ecx,ecx
			jz AA
			mov ecx,[ecx+4]	  
		AA:	mov mosterBaseAddr,ecx
			mov pMonsterData,ecx
			popad
		}   	
		if (0==mosterBaseAddr)
		{
			continue;
		}
		if(pMonsterData->type == 6)
		{
			monsterList[count++] = *pMonsterData;
		}
		
		//temp.Format("%x",pMonsterData->id);
		//m_List_Monster.InsertItem(count++,temp);
		//temp.Format("%f",pMonsterData->dx);
		//m_List_Monster.SetItemText(count-1,1,temp);
		//temp.Format("%x",pMonsterData->type);
		//m_List_Monster.SetItemText(count-1,2,temp);
		//temp.Format("%x",mosterBaseAddr);
		//m_List_Monster.SetItemText(count-1,3,temp);		
	} 
	SortMonster(monsterList,0,count-1);
}

BOOL CSkill::OnInitDialog()
{	
	CDialog::OnInitDialog();
	m_List_Monster.InsertColumn(0,_T("怪ID"),LVCFMT_LEFT,80,-1);
	m_List_Monster.InsertColumn(1,_T("距离"),LVCFMT_LEFT,50,-1);
	m_List_Monster.InsertColumn(2,_T("怪物种类"),LVCFMT_LEFT,20,-1);
	m_List_Monster.InsertColumn(3,_T("基址"),LVCFMT_LEFT,80,-1);
	m_List_Monster.InsertColumn(4,_T("是否死完"),LVCFMT_LEFT,50,-1);
	m_List_Monster.Invalidate();

	GetPlayerSkillList();
	GetMonsterList();
	GetItemList();

	return TRUE;
}


VOID CSkill::GetPlayerSkillList()
{
	DWORD skillCount;
	DWORD skillID;
	CString temp;
	DWORD playerBaseAddr = GetPlayerBaseAddr(g_GameBaseAddr);
	PSKILL pSkill;
	__asm
	{
		pushad
		mov eax,playerBaseAddr
		mov eax,[eax + 0xbb8]
		mov skillCount,eax
		popad
	}
	for (UINT i=0; i < skillCount; i++)
	{
		__asm
		{
			pushad
			mov eax,playerBaseAddr
			mov eax,[eax+0xbb4]
			mov ebx,i
			mov eax,[eax+ebx*4]
			mov pSkill,eax
			/*mov eax,[eax+8]
			mov skillID,eax	*/
			popad
		}
		skillList[i] = *pSkill;
		temp.Format("%x",pSkill->id);
		m_List_Skill.InsertString(i,temp);
	}
}

DWORD WINAPI ThreadProc(LPVOID lpParameter)
{
	SKILL temp;
	temp = skillList[0];
	int i = 0;
	skillList[0] = /*skillList[m_List_Skill.GetCurSel()]*/skillList[1];
	/*skillList[m_List_Skill.GetCurSel()]*/skillList[1] = temp;
	//sscanf(id,"%x",&skill_ID);
	/*if (!flag)
	{*/
		flag = TRUE;
		while(flag)
		{
			if (monsterList[i].dx<=17/*skillList[0].dx*/)
			{
				SelectMonster(monsterList[i].id);
				Sleep(skillList[0].coolingTime);
				UseSkill(skillList[0].id,pplayerInfo->selectedMonsterID);
				Sleep(skillList[0].coolingTime);
				while (pplayerInfo->selectedMonsterID!= 0)//怪没死继续用技能
				{
					UseSkill(skillList[0].id,pplayerInfo->selectedMonsterID);
					Sleep(skillList[0].coolingTime);
				}
				i = i % 2;
			}
			GetMonsterList();
		}
	//}
	//else
	//{
	//	flag = FALSE;
	//}
	return 0;
}


void CSkill::OnBnClickedButton1()
{	/*
	CString ID;
	ID = m_List_Monster.GetItemText(m_List_Monster.GetSelectionMark(),0);
	DWORD monsterID;
	sscanf(ID,"%x",&monsterID);
	SelectMonster(monsterID); 
	CString id;
	DWORD skill_ID;
	m_List_Skill.GetText(m_List_Skill.GetCurSel(),id); */
	//SKILL temp;
	//temp = skillList[0];
	//skillList[0] = /*skillList[m_List_Skill.GetCurSel()]*/skillList[1];
	///*skillList[m_List_Skill.GetCurSel()]*/skillList[1] = temp;
	////sscanf(id,"%x",&skill_ID);
	//if (!flag)
	//{
	//	flag = TRUE;
	//	while(flag)
	//	{
	//		if (monsterList[0].dx<=18)
	//		{
	//			SelectMonster(monsterList[0].id);
	//			Sleep(300);
	//			UseSkill(skillList[0].id,pplayerInfo->selectedMonsterID);
	//			Sleep(skillList[0].coolingTime);
	//			while (pplayerInfo->selectedMonsterID != 0)//怪没死继续用技能
	//			{
	//				UseSkill(skillList[0].id,pplayerInfo->selectedMonsterID);
	//				Sleep(skillList[0].coolingTime);
	//			}
	//			GetMonsterList();
	//		}
	//		else
	//		{
	//			GetMonsterList();
	//		}
	//	}
	//}
	//else
	//{
	//	flag = FALSE;
	//}
	if (!flag)
	{
		flag = TRUE;
		SetDlgItemText(IDC_BUTTON1,_T("停止"));
		CreateThread(NULL,0,ThreadProc,NULL,0,NULL);
	}
	else
	{		
		flag = FALSE;
		SetDlgItemText(IDC_BUTTON1,_T("挂机"));
	}
	
}

void /*CSkill::*/SelectMonster(DWORD monsterID)
{
	__asm
	{		
		pushad
		mov eax, g_GameBaseAddr
		mov eax,[eax]
		mov ecx,[eax+0x20]
		add ecx,0xec
		push monsterID
		call g_SelectMonsterAddr
		popad
	}
}

void CSkill::OnBnClickedButtonRefresh()
{
	GetMonsterList();
	m_List_Monster.DeleteAllItems();
	CString temp;
	int count=0;
	for (int i=0;i<sizeof(monsterList) / sizeof(monsterList[0]);++i)
	{
		temp.Format("%x",monsterList[i].id);
		m_List_Monster.InsertItem(count++,temp);
		temp.Format("%f",monsterList[i].dx);
		m_List_Monster.SetItemText(count-1,1,temp);
		temp.Format("%x",monsterList[i].type);
		m_List_Monster.SetItemText(count-1,2,temp);
		temp.Format("%x",monsterList);
		m_List_Monster.SetItemText(count-1,3,temp);	
		temp.Format("%x",monsterList[i].isDie);
		m_List_Monster.SetItemText(count-1,4,temp);	
	}

	/*GetItemList(); */
}

void CSkill::OnLvnColumnclickListMonster(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMLISTVIEW pNMLV = reinterpret_cast<LPNMLISTVIEW>(pNMHDR);
	// TODO: Add your control notification handler code here
	CSortClass csc(&m_List_Monster,pNMLV->iSubItem,TRUE);
	csc.Sort(TRUE);
	*pResult = 0;
}

// 按与人的距离的降序排
void /*CSkill::*/SortMonster(MONSTERDATA *m, int low, int hight)
{
	int i(low),j(hight);
	float middle(0);
	MONSTERDATA iTemp;
	middle=m[(low+hight)/2].dx;//求中间值
	//middle=pData[(rand()%(right-left+1))+left];
	do{
		while((m[i].dx<middle)&&(i<hight))
			i++;
		while((m[j].dx>middle) && (j>low))
			j--;
		if(i<=j)
		{
			iTemp=m[j];
			m[j]=m[i];
			m[i]=iTemp;
			i++;
			j--;
		}
	}while(i<=j);

	if(low<j)
	{
		SortMonster(m,low,j);
	} 
	if(hight>i)
	{
		SortMonster(m,i,hight);
	}
}

void /*CSkill::*/UseSkill(DWORD skillID, DWORD monsterID)
{
	DWORD playerBaseAddr = GetPlayerBaseAddr(g_GameBaseAddr);
	__asm
	{
		pushad
		push 0
		push skillID
		mov ecx,playerBaseAddr
		call g_GetSkillAddr //找回城技能找到的
		mov ecx,playerBaseAddr
		mov [ecx+0xb98],eax
		push 0
		push 0
		push monsterID		
		call g_SkillAddr
		popad
	}
}

// 地面物品列表
void CSkill::GetItemList(void)
{
	DWORD itemCount;
	DWORD itemArrayAddr;
	DWORD itemBaseAddr;
	CString temp;
	DWORD index;
	int count = 0;
	m_List_Monster.DeleteAllItems();
	__asm
	{
		pushad
		mov eax,g_GameBaseAddr
		mov eax,[eax]
		mov eax,[eax+0x1c]		
		mov eax,[eax+0x8]
		mov eax,[eax+0x28]
		mov ebx,[eax+0x24]
		mov itemCount,ebx
		mov ebx,[eax+0x18]
		mov itemArrayAddr,ebx
		popad
	}
	temp.Format("%x",itemArrayAddr);
	SetDlgItemText(IDC_STATIC_Addr,temp);
	//memset(monsterList,0xFF,sizeof(MONSTERDATA)*MAX_COUNT);
	PITEMDATA pItemData;
	for (UINT i=0; i < itemCount; i++)
	{
		__asm
		{
			pushad
			mov eax,itemArrayAddr
			mov edx,i	
			mov ecx,[eax+edx*4]
			test ecx,ecx
			jz AA
			mov ecx,[ecx+4]	  
		AA:	mov itemBaseAddr,ecx
			mov pItemData,ecx
			popad
		}   	
		if (0==itemBaseAddr)
		{
			continue;
		}
		/*if(pMonsterData->type == 6)
		{
			monsterList[count++] = *pMonsterData;
		}*/
		
		temp.Format("%x",pItemData->id);
		m_List_Monster.InsertItem(count++,temp);
		temp.Format("%f",pItemData->dx);
		m_List_Monster.SetItemText(count-1,1,temp);
		temp.Format("%x",pItemData->sysNo);
		m_List_Monster.SetItemText(count-1,2,temp);
		temp.Format("%x",itemBaseAddr);
		m_List_Monster.SetItemText(count-1,3,temp);	
	}
}

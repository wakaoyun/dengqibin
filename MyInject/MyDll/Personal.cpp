// Personal.cpp : implementation file
//

#include "stdafx.h"
#include "MyDll.h"
#include "Personal.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CPersonal dialog

CPersonal::CPersonal(CWnd* pParent /*=NULL*/)
	: CDialog(CPersonal::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPersonal)
	m_MoveX = _T("");
	m_MoveY = _T("");
	m_Exper = _T("");
	m_HP = _T("");
	m_Level = _T("");
	m_MP = _T("");
	m_Location = _T("");
	m_BaseAddr = _T("");
	m_PlayerName = _T("");
	m_MonsterID = _T("");
	m_XX = _T("");
	m_YY = _T("");
	m_ID = _T("");
	//}}AFX_DATA_INIT
}

void CPersonal::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPersonal)
	DDX_Text(pDX, IDC_EDIT_X, m_MoveX);
	DDX_Text(pDX, IDC_EDIT_Y, m_MoveY);
	DDX_Text(pDX, IDC_EDIT1, m_ID);
	//}}AFX_DATA_MAP
}

DWORD GetTestInfo(DWORD playerBaseAddr)
{
	
	DWORD temp;
	__asm
	{
		pushad
		/*鼠标经过的怪ID
		mov eax,g_GameBaseAddr
		mov eax,[eax]
		add eax,0x1c
		mov eax,[eax]
		add eax,0x8
		mov eax,[eax] 
		add eax,0x1c
		mov eax,[eax]
		add eax,0x8
		mov eax,[eax]*/
		///////////////////////////////////////////////////
		//技能参数4值
		mov eax,playerBaseAddr
		add eax,0xb8c
		mov eax,[eax]
		add eax,0x14
		mov eax,[eax] 
		add eax,0x20
		mov eax,[eax]
//		lea eax,[eax]
		mov temp,eax
		popad
	}
	return temp;
}
VOID MyUpdateData(HWND hWnd)
{
	SetDlgItemText(hWnd, IDC_STATIC_HP, m_HP);
	SetDlgItemText(hWnd, IDC_STATIC_MP, m_MP);
	SetDlgItemText(hWnd, IDC_STATIC_Location, m_Location);
	SetDlgItemText(hWnd, IDC_STATIC_BaseAddr, m_BaseAddr);
	SetDlgItemText(hWnd, IDC_STATIC_Name, m_PlayerName);
	SetDlgItemText(hWnd, IDC_STATIC_Test,m_Test);
	SetDlgItemText(hWnd, IDC_STATIC_XX,m_XX);
	SetDlgItemText(hWnd, IDC_STATIC_YY,m_YY);
}

VOID GetPlayerInfo(HWND hWnd)
{	
	DWORD name1;
	DWORD name2;
	DWORD name3;
	DWORD name4;
	DWORD name5;
	DWORD name6;
	DWORD name7;
	DWORD name8;
	DWORD name9;
	DWORD name10;
	DWORD PlayerBaseAddr = GetPlayerBaseAddr(g_GameBaseAddr);
	__asm
	{
		pushad
		mov eax,PlayerBaseAddr
		mov pplayerInfo,eax
		add eax,0x5bc
		mov eax,[eax]
		mov name1,eax
		inc eax
		inc eax	
		mov name2,eax
		inc eax
		inc eax	
		mov name3,eax
		inc eax
		inc eax	
		mov name4,eax
		inc eax
		inc eax	
		mov name5,eax
		inc eax
		inc eax	
		mov name6,eax
		inc eax
		inc eax	
		mov name7,eax
		inc eax
		inc eax	
		mov name8,eax
		inc eax
		inc eax	
		mov name9,eax
		inc eax
		inc eax	
		mov name10,eax
		popad	
	}	
	
	m_PlayerName.Format("%s%s%s%s%s%s%s%s%s%s",name1,name2,name3,name4
						,name5,name6,name7,name8,name9,name10);
	m_Level.Format("%d",pplayerInfo->playLevel);
	m_Exper.Format("%d",pplayerInfo->experience);
	m_HP.Format("%d",pplayerInfo->HP);
	m_MP.Format("%d",pplayerInfo->MP);
	m_Location.Format("X:%d Y:%d H:%d",(int)(pplayerInfo->zX/10+400) + 1/*加1的误差*/,
					  (int)(pplayerInfo->zY/10+550),(int)pplayerInfo->zZ/10);
	m_BaseAddr.Format("%x",PlayerBaseAddr);
	m_XX.Format("%x",GetTestInfo(PlayerBaseAddr));
	m_YY.Format("%f",pplayerInfo->zY);
	m_Test.Format("%x",pplayerInfo->selectedMonsterID);
	MyUpdateData(hWnd);
}


VOID CALLBACK TimerProc(HWND hwnd, UINT uMsg, UINT idEvent, DWORD dwTime)
{
	GetPlayerInfo(hwnd);
}

BOOL CPersonal::OnInitDialog()
{
	CDialog::OnInitDialog();
	::SetTimer(this->m_hWnd,55,300,TimerProc);
	return TRUE;
}

BEGIN_MESSAGE_MAP(CPersonal, CDialog)
	//{{AFX_MSG_MAP(CPersonal)
	ON_BN_CLICKED(IDC_BUTTON_Person, OnBUTTONPerson)
	ON_EN_CHANGE(IDC_EDIT_X, OnChangeEdit)
	ON_EN_CHANGE(IDC_EDIT_Y, OnChangeEdit)
	ON_EN_CHANGE(IDC_EDIT1, OnChangeEdit)
	ON_BN_CLICKED(IDC_BUTTON_Use, OnBUTTONUse)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPersonal message handlers

void CPersonal::OnBUTTONPerson() 
{
	MoveCall(m_MoveX, m_MoveY);
}

void CPersonal::OnChangeEdit() 
{
	UpdateData();
}

VOID CPersonal::MoveCall(CString x, CString y)
{
	DWORD playerBaseAddr = GetPlayerBaseAddr(g_GameBaseAddr);
	float toX = (float)(atof(x.GetBuffer(x.GetLength()))-401) * 10;
	float toY = (float)(atof(y.GetBuffer(y.GetLength()))-550) * 10;
	DWORD monsterID = atol(x.GetBuffer(x.GetLength()));
	__asm
	{
		pushad
		mov esi,playerBaseAddr
		mov ecx,[esi+0xb8c]
		push 1
		call g_MoveAddress
		mov edi,eax
		lea eax,[esp+0x38]
		push eax
		push 0
		mov ecx,edi
		call g_MoveAddress1
		push 0
		push 1
		push edi
		mov ecx,[esi+0xb8c]
		push 1
		call g_MoveAddress2
		mov eax,playerBaseAddr
		mov eax,[eax+0xb8c]
		mov eax,[eax+0x30]
		mov ecx,[eax+0x4]
		mov eax,toX
		mov [ecx+0x20], eax
		mov eax, toY
		//mov [ecx+0x24],260//[ecx+0x24] z坐标
		mov [ecx+0x28], eax
		popad
	}
	SelectMonster(0x8010371D);
}

VOID CPersonal::SelectMonster(DWORD monsterID)
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

void CPersonal::OnBUTTONUse() 
{
	
}

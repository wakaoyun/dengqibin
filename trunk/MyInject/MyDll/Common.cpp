#include "StdAfx.h"

PPLAYERINFORMATION pplayerInfo;
CString	m_Exper;
CString	m_HP;
CString	m_Level;
CString	m_MP;
CString	m_Location;
CString	m_BaseAddr;
CString m_PlayerName;
CString m_MonsterID;
CString m_Test;
CString m_Test1;
CString m_Test2;
CString m_XX;
CString m_YY;
MONSTERDATA monsterList[MAX_COUNT];
SKILL skillList[30];
BOOL flag = FALSE;
/////////////////////////////////////////////////////////////////////////
//µÿ÷∑–≈œ¢															
DWORD g_GameBaseAddr = 0xA314DC;
DWORD g_MoveAddress = 0x45EE70;
DWORD g_MoveAddress1 = 0x466050;
DWORD g_MoveAddress2 = 0x45F270;	
DWORD g_SelectMonsterAddr = 0x005B2670;		
DWORD g_SkillAddr = 0x0045BAD0;
DWORD g_GetSkillAddr = 0x0045BD70;						
/////////////////////////////////////////////////////////////////////////

DWORD GetPlayerBaseAddr(DWORD gameBaseAddr)
{
	DWORD playerBaseAddr;
	__asm
	{
		pushad
		mov eax,gameBaseAddr
		mov eax,[eax]
		add eax,0x1c
		mov eax,[eax]
		add eax,0x20
		mov eax,[eax]
		mov playerBaseAddr,eax
		popad
	}
	return playerBaseAddr;
}

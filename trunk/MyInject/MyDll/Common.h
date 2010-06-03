#define MAX_COUNT 301

typedef struct tagPLAYERINFORMATION
{
	BYTE temp[0x3c] ;
	float zX;		//0x3c 
	float zZ;		//0x40 
	float zY;		//0x44 
	BYTE temp1[0x308];// 
	DWORD assistantStateBaseAddr;//0x350
	DWORD currentStateNum;//0x354
	BYTE temp2[0xe4];//
	DWORD playerID    ;//0x43c
	BYTE temp3[8];
	DWORD playLevel   ;//0x448 
	DWORD self_cultivation_period   ;//0x44c 
	DWORD HP    ;//0x450 
	DWORD MP    ;//0x454 
	DWORD experience    ;//0x458 
	DWORD spirit   ;//0x45c 
	DWORD unAlcPoint ;//0x460 
	DWORD Genki   ;//0x464 
	DWORD power   ;//0x468 
	DWORD Mana   ;//0x46c 
	DWORD Physical   ;//0x470 
	DWORD Agile   ;//0x474 
	DWORD maxHP   ;//0x478 
	DWORD maxMP   ;//0x47c 
	BYTE temp4[0x18] ;//  
	DWORD Accuracy   ;//0x498 
	DWORD complexAttackLower ;//0x49c 
	DWORD complexAttackUpper ;//0x4a0 
	BYTE temp5[0x30] ;//  
	DWORD franceAttackLower ;//0x4d4 
	DWORD franceAttackUpper ;//0x4d8 
	DWORD goldDefend   ;//0x4dc 
	DWORD woodDefend   ;//0x4e0 
	DWORD waterDefend   ;//0x4e4 
	DWORD fireDefend   ;//0x4e8 
	DWORD earthDefend   ;//0x4ec 
	DWORD anti_matter   ;//0x4f0 
	DWORD dodge   ;//0x4f4
	DWORD temp6   ;//0x4f8
	DWORD gold   ;//0x4fc
	BYTE temp7[0x5c] ;//  
	DWORD reputation   ;//0x55c
	BYTE temp8[0x5c] ;//
	DWORD nameAddr ;//0x5bc
	BYTE temp9[14] ;
	DWORD characterClass   ;//0x5d4 
	DWORD sex   ;//0x5d8 
	BYTE temp10[4] ;// 
	DWORD state   ;//0x5e0
	BYTE temp11[0x48] ;
	DWORD activeState ;//0x62c
	BYTE temp12[0x3b4] ;
	DWORD selectedMonsterID;//0x9e0
	
	float x;
	float y;
	float z;
	WCHAR name[64];
	
}PLAYERINFORMATION,*PPLAYERINFORMATION; 

typedef struct tagSKILL
{
	DWORD temp1;
	DWORD temp2;
	DWORD id;       //0x8=技能ID
	DWORD level;      //0xc=等级
	DWORD residualCoolingTime; //0x10=技能的剩余冷却时间(毫秒)
	DWORD coolingTime;    //0x14=技能的冷却时间
	DWORD isCanUsed;     //0x18=技能的冷却时间是否已过.0-可以使用,1-不能用
	byte temp3[0x14];
	float dx;			 //0x30施放距离???
	byte temp4[0x4];
}SKILL,*PSKILL;

typedef struct tagMONSTERDATA
{
	BYTE temp[0x3c];
	float x;     //0x3c    x;
	float z;     //0x40    z;
	float y;     //0x44    y;
	BYTE temp1[0x6c]; 
	DWORD type;    //0xB4    怪物种类 6为怪，7为NPC，为宠物A为GM;
	BYTE temp2[0x64]; 
	DWORD id;     //0x11C   怪物ID;
	DWORD sysNo;    //0x120   怪物编号;
	DWORD level;    //0x124   怪物等级;
	BYTE temp3[4];
	DWORD hp;     //0x12c   怪物当前血值;
	BYTE temp4[0x24]; 
	DWORD maxHp;    //0x154   怪物血值上限;
	BYTE temp5[0xc4]; 
	DWORD isDie;    //0x21c   是否已死亡;
	BYTE temp6[0x10]; 
	DWORD nameAddr;   //0x230   怪物名称首地址;
	BYTE temp7[0x20]; 
	float dx;     //0x254   人与怪物距离;
	BYTE temp8[0x30]; 
	DWORD state;    //0x288   怪物的D状态0,1,2=地上,空中,水中
	BYTE temp9[0x2c]; 
	DWORD akId;    //0x2b8   攻击目标ID	
}MONSTERDATA,*PMONSTERDATA;

typedef struct tagITEMDATA//地面物品数据结构
{
	BYTE temp[0x3c];
	float x;     //0x3C     物品X坐标   
	float z;     //0x40    物品的Z坐标
	float y;     //0x44     物品Y坐标
	BYTE temp1[0xc4];     
	DWORD id;     //0x10c    物品ID
	DWORD sysNo;    //0x110    物品编号
	BYTE temp2[0x3c]; 
	DWORD needLevel;   //0x150    如果是矿物,则采集它所需的级数
	float dx;     //0x154    人与物品距离
	BYTE temp3[0xc]; 
	DWORD nameAddr;   //0x164    物品名称首地址

	DWORD addr;    //物品的地址
	WCHAR name[32];   //物品的名字

}ITEMDATA,*PITEMDATA; 

typedef struct tagBAGDATA//包裹数据结构
{
	BYTE temp[4];
	DWORD type;   		//0x4 	类型
	DWORD sysNo;   		//0x8 	编号
	DWORD nCount;  		//0xc 	数量
	DWORD maxCount;		//0x10 	可以存放的最大数量
	DWORD price; 		//0x14 	单价
	DWORD temp1;   		//0x18 	
	BYTE temp2[0x28];	//
	DWORD needLevel;	//0x44 使用该物品所需的等级
	DWORD needPhysical;	//0x48 使用该物品所需的体力
	DWORD needAgile;	//0x4c 使用该物品所需的敏捷
	DWORD temp3;		//0x50 
	DWORD needPower;	//0x54 使用该物品所需的力量
	DWORD needMana;		//0x58 使用该物品所需的灵力
	DWORD indexGird;	//0x5c所在包裹的第几格	
}BAGDATA,*PBAGDATA;

extern PPLAYERINFORMATION pplayerInfo;
extern CString m_Exper;
extern CString m_HP;
extern CString m_Level;
extern CString m_MP;
extern CString m_Location;
extern CString m_BaseAddr;
extern CString m_PlayerName;
extern CString m_MonsterID;
extern CString m_Test;
extern CString m_Test1;
extern CString m_Test2;
extern CString m_XX;
extern CString m_YY;
extern MONSTERDATA monsterList[MAX_COUNT];
extern SKILL skillList[30];
extern BOOL flag;
/////////////////////////////////////////////////////////////////////////
//地址信息															
extern DWORD g_GameBaseAddr;
extern DWORD g_MoveAddress;
extern DWORD g_MoveAddress1;
extern DWORD g_MoveAddress2;	
extern DWORD g_SelectMonsterAddr;		
extern DWORD g_SkillAddr;
extern DWORD g_GetSkillAddr;						
/////////////////////////////////////////////////////////////////////////

extern DWORD GetPlayerBaseAddr(DWORD gameBaseAddr);


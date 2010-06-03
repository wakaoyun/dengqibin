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
	DWORD id;       //0x8=����ID
	DWORD level;      //0xc=�ȼ�
	DWORD residualCoolingTime; //0x10=���ܵ�ʣ����ȴʱ��(����)
	DWORD coolingTime;    //0x14=���ܵ���ȴʱ��
	DWORD isCanUsed;     //0x18=���ܵ���ȴʱ���Ƿ��ѹ�.0-����ʹ��,1-������
	byte temp3[0x14];
	float dx;			 //0x30ʩ�ž���???
	byte temp4[0x4];
}SKILL,*PSKILL;

typedef struct tagMONSTERDATA
{
	BYTE temp[0x3c];
	float x;     //0x3c    x;
	float z;     //0x40    z;
	float y;     //0x44    y;
	BYTE temp1[0x6c]; 
	DWORD type;    //0xB4    �������� 6Ϊ�֣�7ΪNPC��Ϊ����AΪGM;
	BYTE temp2[0x64]; 
	DWORD id;     //0x11C   ����ID;
	DWORD sysNo;    //0x120   ������;
	DWORD level;    //0x124   ����ȼ�;
	BYTE temp3[4];
	DWORD hp;     //0x12c   ���ﵱǰѪֵ;
	BYTE temp4[0x24]; 
	DWORD maxHp;    //0x154   ����Ѫֵ����;
	BYTE temp5[0xc4]; 
	DWORD isDie;    //0x21c   �Ƿ�������;
	BYTE temp6[0x10]; 
	DWORD nameAddr;   //0x230   ���������׵�ַ;
	BYTE temp7[0x20]; 
	float dx;     //0x254   ����������;
	BYTE temp8[0x30]; 
	DWORD state;    //0x288   �����D״̬0,1,2=����,����,ˮ��
	BYTE temp9[0x2c]; 
	DWORD akId;    //0x2b8   ����Ŀ��ID	
}MONSTERDATA,*PMONSTERDATA;

typedef struct tagITEMDATA//������Ʒ���ݽṹ
{
	BYTE temp[0x3c];
	float x;     //0x3C     ��ƷX����   
	float z;     //0x40    ��Ʒ��Z����
	float y;     //0x44     ��ƷY����
	BYTE temp1[0xc4];     
	DWORD id;     //0x10c    ��ƷID
	DWORD sysNo;    //0x110    ��Ʒ���
	BYTE temp2[0x3c]; 
	DWORD needLevel;   //0x150    ����ǿ���,��ɼ�������ļ���
	float dx;     //0x154    ������Ʒ����
	BYTE temp3[0xc]; 
	DWORD nameAddr;   //0x164    ��Ʒ�����׵�ַ

	DWORD addr;    //��Ʒ�ĵ�ַ
	WCHAR name[32];   //��Ʒ������

}ITEMDATA,*PITEMDATA; 

typedef struct tagBAGDATA//�������ݽṹ
{
	BYTE temp[4];
	DWORD type;   		//0x4 	����
	DWORD sysNo;   		//0x8 	���
	DWORD nCount;  		//0xc 	����
	DWORD maxCount;		//0x10 	���Դ�ŵ��������
	DWORD price; 		//0x14 	����
	DWORD temp1;   		//0x18 	
	BYTE temp2[0x28];	//
	DWORD needLevel;	//0x44 ʹ�ø���Ʒ����ĵȼ�
	DWORD needPhysical;	//0x48 ʹ�ø���Ʒ���������
	DWORD needAgile;	//0x4c ʹ�ø���Ʒ���������
	DWORD temp3;		//0x50 
	DWORD needPower;	//0x54 ʹ�ø���Ʒ���������
	DWORD needMana;		//0x58 ʹ�ø���Ʒ���������
	DWORD indexGird;	//0x5c���ڰ����ĵڼ���	
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
//��ַ��Ϣ															
extern DWORD g_GameBaseAddr;
extern DWORD g_MoveAddress;
extern DWORD g_MoveAddress1;
extern DWORD g_MoveAddress2;	
extern DWORD g_SelectMonsterAddr;		
extern DWORD g_SkillAddr;
extern DWORD g_GetSkillAddr;						
/////////////////////////////////////////////////////////////////////////

extern DWORD GetPlayerBaseAddr(DWORD gameBaseAddr);


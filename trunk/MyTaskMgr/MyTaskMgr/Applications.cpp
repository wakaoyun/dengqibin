// Applications.cpp : implementation file
//

#include "stdafx.h"
#include "MyTaskMgr.h"
#include "Applications.h"
#include "afxdialogex.h"

HWND            hApplicationPageListCtrl;       /* Application ListCtrl Window */
HWND			CurrenthWnd;
// CApplications dialog

IMPLEMENT_DYNAMIC(CApplications, CDialogEx)

CApplications::CApplications(CWnd* pParent /*=NULL*/)
	: CDialogEx(CApplications::IDD, pParent)
{

}

CApplications::~CApplications()
{
}

void CApplications::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_Application_LIST, m_Application);
	DDX_Control(pDX, IDC_btnEndTask, m_EndTask);
	DDX_Control(pDX, IDC_btnSwitchTo, m_SwitchTo);
	DDX_Control(pDX, IDC_btnNewTask, m_NewTask);
}


BEGIN_MESSAGE_MAP(CApplications, CDialogEx)
	ON_WM_SIZE()
	ON_NOTIFY(LVN_GETDISPINFO, IDC_Application_LIST, &CApplications::OnGetdispinfoApplicationList)
	ON_NOTIFY(HDN_ITEMCLICK, 0, &CApplications::OnItemclickApplicationList)
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CApplications message handlers


BOOL CApplications::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	SetBackgroundColor(RGB(255, 255,255));
	m_ImageList.Create(16, 16, ILC_COLORDDB|ILC_MASK, 0, 1);
	m_Application.InsertColumn(0, _T("Task"), LVCFMT_LEFT, 260, -1);
	m_Application.InsertColumn(1, _T("Status"), LVCFMT_LEFT, 60, -1);
	m_Application.SetImageList(&m_ImageList, LVSIL_SMALL);
	m_Application.SetExtendedStyle(m_Application.GetExtendedStyle()|LVS_EX_FULLROWSELECT);
	
	CurrenthWnd = this->GetParent()->GetParent()->m_hWnd;
	hApplicationPageListCtrl = ::GetDlgItem(this->m_hWnd, IDC_Application_LIST);	
	
	CreateThread(NULL, 0, ApplicationPageRefreshThread, NULL, 0, NULL);
	SetTimer(1,1000,NULL);
	
	return TRUE;  // return TRUE unless you set the focus to a control
	// EXCEPTION: OCX Property Pages should return FALSE
}


void CApplications::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);

	m_Application.MoveWindow(15, 15, cx - 28, cy - 60);

	CRect rectBtn;
	m_EndTask.GetClientRect(&rectBtn);
	cx = cx - 15 - rectBtn.Width();
	m_NewTask.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());

	cx = cx - 5 - rectBtn.Width();
	m_SwitchTo.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());

	cx = cx - 5 - rectBtn.Width();
	m_EndTask.MoveWindow(cx, cy - 37, rectBtn.Width(), rectBtn.Height());
}

DWORD WINAPI ApplicationPageRefreshThread(void *lpParameter)
{
    /* Create the event */
    hApplicationPageEvent = CreateEventW(NULL, TRUE, TRUE, NULL);

    /* If we couldn't create the event then exit the thread */
    if (!hApplicationPageEvent)
        return 0;

    while (1)
    {
        DWORD   dwWaitVal;

        /* Wait on the event */
        dwWaitVal = WaitForSingleObject(hApplicationPageEvent, INFINITE);

        /* If the wait failed then the event object must have been */
        /* closed and the task manager is exiting so exit this thread */
        if (dwWaitVal == WAIT_FAILED)
            return 0;

        if (dwWaitVal == WAIT_OBJECT_0)
        {
            /* Reset our event */
            ResetEvent(hApplicationPageEvent);
 
            EnumWindows(EnumWindowsProc, 0);            
        }
    }
}

BOOL CALLBACK EnumWindowsProc(HWND hWnd, LPARAM lParam)
{
    HICON   hIcon;
    WCHAR   szText[260];
    BOOL    bHung = FALSE;
    HICON*  xhIcon = (HICON*)&hIcon;

    typedef int (FAR __stdcall *IsHungAppWindowProc)(HWND);
    IsHungAppWindowProc IsHungAppWindow;

	/* Skip our window */
		if (hWnd == CurrenthWnd)
			return TRUE;

    GetWindowTextW(hWnd, szText, 260); /* Get the window text */

    /* Check and see if this is a top-level app window */
    if ((wcslen(szText) <= 0) ||
        !IsWindowVisible(hWnd) ||
        (GetParent(hWnd) != NULL) ||
        (GetWindow(hWnd, GW_OWNER) != NULL) ||
        (GetWindowLongPtrW(hWnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW))
    {
        return TRUE; /* Skip this window */
    }

    /* Get the icon for this window */
    hIcon = NULL;
    SendMessageTimeoutW(hWnd, WM_GETICON, ICON_SMALL, 0, 0, 1000, (PDWORD_PTR)xhIcon);

    if (!hIcon)
    {
		hIcon = (HICON)(LONG_PTR)GetClassLongPtrW(hWnd, GCL_HICONSM);
		if (!hIcon) hIcon = (HICON)(LONG_PTR)GetClassLongPtrW(hWnd, GCL_HICON);
		if (!hIcon) SendMessageTimeoutW(hWnd, WM_QUERYDRAGICON, 0, 0, 0, 1000, (PDWORD_PTR)xhIcon);
    }

	if (!hIcon)
		hIcon = LoadIconW(NULL, MAKEINTRESOURCEW(IDI_APPLICATION));

    bHung = FALSE;

    IsHungAppWindow = (IsHungAppWindowProc)(FARPROC)GetProcAddress(GetModuleHandleW(L"USER32.DLL"), "IsHungAppWindow");

    if (IsHungAppWindow)
        bHung = IsHungAppWindow(hWnd);

    AddOrUpdateHwnd(hWnd, szText, hIcon, bHung);

    return TRUE;
}

void AddOrUpdateHwnd(HWND hWnd, WCHAR *szTitle, HICON hIcon, BOOL bHung)
{
    LPAPPLICATION_PAGE_LIST_ITEM  pAPLI = NULL;
    HIMAGELIST                    hImageListSmall;
    LV_ITEM                       item;
	LV_COLUMN					  colum;					
    int                           i;
    BOOL                          bAlreadyInList = FALSE;
    BOOL                          bItemRemoved = FALSE;

    memset(&item, 0, sizeof(LV_ITEM));

    /* Get the image lists */
    hImageListSmall = ListView_GetImageList(hApplicationPageListCtrl, LVSIL_SMALL);

    /* Check to see if it's already in our list */
	for (i=0; i<ListView_GetItemCount(hApplicationPageListCtrl); i++)
	{
		memset(&item, 0, sizeof(LV_ITEM));
		item.mask = LVIF_IMAGE|LVIF_PARAM;
		item.iItem = i;
		(void)ListView_GetItem(hApplicationPageListCtrl, &item);

		pAPLI = (LPAPPLICATION_PAGE_LIST_ITEM)item.lParam;
		if (pAPLI->hWnd == hWnd)
		{
			bAlreadyInList = TRUE;
			break;
		}
	}

    /* If it is already in the list then update it if necessary */
    if (bAlreadyInList)
    {
        /* Check to see if anything needs updating */
        if ((pAPLI->hIcon != hIcon) ||
            (_wcsicmp(pAPLI->szTitle, szTitle) != 0) ||
            (pAPLI->bHung != bHung))
        {
            /* Update the structure */
            pAPLI->hIcon = hIcon;
            pAPLI->bHung = bHung;
            wcscpy(pAPLI->szTitle, szTitle);

            /* Update the image list */
            ImageList_ReplaceIcon(hImageListSmall, item.iItem, hIcon);

			pAPLI->hWnd = hWnd;
			pAPLI->hIcon = hIcon;
			pAPLI->bHung = bHung;
			wcscpy(pAPLI->szTitle, szTitle);
			
            /* Update the list view */
            (void)ListView_RedrawItems(hApplicationPageListCtrl, i, i+1/*ListView_GetItemCount(hApplicationPageListCtrl)*/);
            /* UpdateWindow(m_Application); */
            InvalidateRect(hApplicationPageListCtrl, NULL, 0);
        }
    }
    /* It is not already in the list so add it */
    else
    {
        pAPLI = (LPAPPLICATION_PAGE_LIST_ITEM)HeapAlloc(GetProcessHeap(), 0, sizeof(APPLICATION_PAGE_LIST_ITEM));

        pAPLI->hWnd = hWnd;
        pAPLI->hIcon = hIcon;
        pAPLI->bHung = bHung;
        wcscpy(pAPLI->szTitle, szTitle);

        /* Add the item to the list */
        memset(&item, 0, sizeof(LV_ITEM));
        item.mask = LVIF_TEXT|LVIF_IMAGE|LVIF_PARAM;
        item.iImage = ImageList_AddIcon(hImageListSmall, hIcon);
        item.pszText = szTitle;
        item.iItem = ListView_GetItemCount(hApplicationPageListCtrl);
        item.lParam = (LPARAM)pAPLI;
        (void)ListView_InsertItem(hApplicationPageListCtrl, &item);		
    }


    /* Check to see if we need to remove any items from the list */
    for (i=ListView_GetItemCount(hApplicationPageListCtrl)-1; i>=0; i--)
    {
        memset(&item, 0, sizeof(LV_ITEM));
        item.mask = LVIF_IMAGE|LVIF_PARAM;
        item.iItem = i;
        (void)ListView_GetItem(hApplicationPageListCtrl, &item);

        pAPLI = (LPAPPLICATION_PAGE_LIST_ITEM)item.lParam;
        if (!IsWindow(pAPLI->hWnd)||
            (wcslen(pAPLI->szTitle) <= 0) ||
            !IsWindowVisible(pAPLI->hWnd) ||
            (GetParent(pAPLI->hWnd) != NULL) ||
            (GetWindow(pAPLI->hWnd, GW_OWNER) != NULL) ||
            (GetWindowLongPtrW(hWnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW))
        {
            ImageList_Remove(hImageListSmall, item.iItem);

            (void)ListView_DeleteItem(hApplicationPageListCtrl, item.iItem);
            HeapFree(GetProcessHeap(), 0, pAPLI);
            bItemRemoved = TRUE;
        }
    }

    /*
     * If an item was removed from the list then
     * we need to resync all the items with the
     * image list
     */
    if (bItemRemoved)
    {
        for (i=0; i<ListView_GetItemCount(hApplicationPageListCtrl); i++)
        {
            memset(&item, 0, sizeof(LV_ITEM));
            item.mask = LVIF_IMAGE;
            item.iItem = i;
            item.iImage = i;
            (void)ListView_SetItem(hApplicationPageListCtrl, &item);
        }
    }
}

void CApplications::OnGetdispinfoApplicationList(NMHDR *pNMHDR, LRESULT *pResult)
{
	NMLVDISPINFO *pDispInfo = reinterpret_cast<NMLVDISPINFO*>(pNMHDR);

	LPAPPLICATION_PAGE_LIST_ITEM  pAPLI;	
	if(pDispInfo->item.iSubItem = 1)
	{
		pAPLI = (LPAPPLICATION_PAGE_LIST_ITEM) pDispInfo->item.lParam;
		if(pAPLI->bHung)
			wcsncpy(pDispInfo->item.pszText, _T("Not response"), pDispInfo->item.cchTextMax);
		else
			wcsncpy(pDispInfo->item.pszText, _T("Running"), pDispInfo->item.cchTextMax);
	}
	*pResult = 0;
}

int CALLBACK ApplicationPageCompareFunc(LPARAM lParam1, LPARAM lParam2, LPARAM lParamSort)
{
	LPAPPLICATION_PAGE_LIST_ITEM  Param1;
	LPAPPLICATION_PAGE_LIST_ITEM  Param2;

	if (bSortAscending) {
		Param1 = (LPAPPLICATION_PAGE_LIST_ITEM)lParam1;
		Param2 = (LPAPPLICATION_PAGE_LIST_ITEM)lParam2;
	} else {
		Param1 = (LPAPPLICATION_PAGE_LIST_ITEM)lParam2;
		Param2 = (LPAPPLICATION_PAGE_LIST_ITEM)lParam1;
	}
	return wcscmp(Param1->szTitle, Param2->szTitle);
}



void CApplications::OnItemclickApplicationList(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMHEADER phdr = reinterpret_cast<LPNMHEADER>(pNMHDR);
	m_Application.SortItems(ApplicationPageCompareFunc,NULL);
	bSortAscending = !bSortAscending;
	*pResult = 0;
}


void CApplications::OnTimer(UINT_PTR nIDEvent)
{
	SetEvent(hApplicationPageEvent);

	CDialogEx::OnTimer(nIDEvent);
}

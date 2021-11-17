using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class RoomListPanel : BasePanel
{
    private Button closeButton;
    private RectTransform battleRes;
    private RectTransform roomList;
    private VerticalLayoutGroup roomLayout;
    private GameObject roomItemPrefab;
    private ListRoomRequest listRoomRequest;
    private CreateRoomRequest createRoomRequest;
    private JoinRoomRequest joinRoomRequest;
    private List<UserData> udList = null;

    private UserData ud1;
    private UserData ud2;
    private void Start()
    {
        
        if (transform.Find("RoomList/CloseButton") == null)
            Debug.Log("CloseButton is null!");
        closeButton = transform.Find("RoomList/CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);

        battleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;

        transform.Find("RoomList/CreateRoomButton").GetComponent<Button>().onClick.AddListener(OnCreateRoomClick);
        transform.Find("RoomList/RefreshRoomButton").GetComponent<Button>().onClick.AddListener(OnRefreshClick);

        listRoomRequest = GetComponent<ListRoomRequest>();
        createRoomRequest = GetComponent<CreateRoomRequest>();
        joinRoomRequest = GetComponent<JoinRoomRequest>();
        //EnterAnimation();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        SetBattleRes();
        EnterAnimation();
        if (listRoomRequest == null)
            listRoomRequest = GetComponent<ListRoomRequest>();
        listRoomRequest.SendRequest();
    }
    private void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.2f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f);
        tweener.OnComplete(() => UIMng.PopPanel());

    }
    private void OnCreateRoomClick()
    {
        createRoomRequest.SetPanel(UIMng.PushPanel(UIPanelType.Room));
        createRoomRequest.SendRequest();
    }
    private void OnRefreshClick()
    {
        listRoomRequest.SendRequest();
    }
    public override void OnPause()
    {
        base.OnPause();
        HideAnimation();
    }
    public override void OnExit()
    {
        base.OnExit();
        //loginButton.onClick.RemoveAllListeners();
        HideAnimation();
    }
    public override void OnResume()
    {
        base.OnResume();
        EnterAnimation();
        listRoomRequest.SendRequest();
    }
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
    private void HideAnimation()
    {
        transform.DOScale(0, 0.2f);
        transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f).OnComplete(() => gameObject.SetActive(false));
    }
    private void SetBattleRes()
    {
        UserData ud = facade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = ud.Username;
        transform.Find("BattleRes/TotalCount").GetComponent<Text>().text = "总场数："+ud.TotalCount.ToString();

        transform.Find("BattleRes/Wincount").GetComponent<Text>().text = "胜场："+ud.WinCount.ToString();
    }
    public void OnUpdateResultResponse(int totalCount,int winCount)
    {
        facade.UpdateResult(totalCount,winCount);
        SetBattleRes();//TODO
    }
    private void Update()
    {
        if(udList!=null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
        if(ud1!=null&&ud2!=null)
        {
            BasePanel panel = UIMng.PushPanel(UIPanelType.Room);

            (panel as RoomPanel).SetAllPlayerResSync(ud1, ud2);
            ud1 = null;
            ud2 = null;
        }
    }
    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }
    public void LoadRoomItem(List<UserData> udList)
    {
        RoomItem[] riArray =  roomLayout.GetComponentsInChildren<RoomItem>();

        foreach(RoomItem ri in riArray)
        {
            ri.DestroySelf();
        }
        int count = udList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);

            roomItem.transform.SetParent(roomLayout.transform);
            roomItem.transform.localScale = roomItemPrefab.transform.localScale;
            //roomItem.transform.lossyScale = roomItemPrefab.transform.lossyScale;
            UserData ud = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInfo(ud.Id,ud.Username,ud.TotalCount,ud.WinCount,this);
        }
        int roomCount = GetComponentsInChildren<RoomItem>().Length;
        Vector2 size = roomLayout.GetComponent<RectTransform>().sizeDelta;
        roomLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x,
            roomCount * (roomItemPrefab.GetComponent<RectTransform>().sizeDelta.y + roomLayout.spacing));
    }

    public void OnJoinClick(int id)
    {
        joinRoomRequest.SendRequest(id);
    }
    public void OnJoinResponse(ReturnCode returnCode,UserData ud1,UserData ud2)
    {
        switch (returnCode)
        {
            case ReturnCode.NotFound:
                UIMng.ShowMessageSync("房间被销毁无法加入");
                break;
            case ReturnCode.Fail:
                UIMng.ShowMessageSync("房间已满，无法加入");
                break;
            case ReturnCode.Success:
                this.ud1 = ud1;
                this.ud2 = ud2;
                break;

        }
    }
}

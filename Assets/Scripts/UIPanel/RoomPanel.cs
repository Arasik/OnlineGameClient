using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class RoomPanel : BasePanel
{
    private Text localPlayerUsername;
    private Text localPlayerTotalCount;
    private Text localPlayerWinCount;

    private Text enemyPlayerUsername;
    private Text enemyPlayerTotalCount;
    private Text enemyPlayerWinCount;

    private Transform bluePanel;
    private Transform redPanel;
    private Transform startButton;
    private Transform exitButton;

    //private CreateRoomRequest createRoomRequest;
    private UserData ud = null;
    private UserData ud1 = null;
    private UserData ud2 = null;

    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;
    private bool IsPopPanel=false;

    private void Start()
    {
        localPlayerUsername = transform.Find("BluePanel/Username").GetComponent<Text>();
        localPlayerTotalCount = transform.Find("BluePanel/TotalCount").GetComponent<Text>();
        localPlayerWinCount = transform.Find("BluePanel/WinCount").GetComponent<Text>();

        enemyPlayerUsername = transform.Find("RedPanel/Username").GetComponent<Text>();
        enemyPlayerTotalCount = transform.Find("RedPanel/TotalCount").GetComponent<Text>();
        enemyPlayerWinCount = transform.Find("RedPanel/WinCount").GetComponent<Text>();

        bluePanel = transform.Find("BluePanel");
        redPanel = transform.Find("RedPanel");

        startButton= transform.Find("StartButton");
        exitButton= transform.Find("ExitButton");

        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnStartClick);
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitClick);

        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();
        //createRoomRequest = GetComponent<CreateRoomRequest>();
        OnEnter();
    }
    public void SetLocalPlayerResSync()//异步 或许是Async
    {
        ud = facade.GetUserData();
    }
    public void SetAllPlayerResSync(UserData ud1,UserData ud2)//异步 或许是Async
    {
        this.ud1 = ud1;
        this.ud2 = ud2;

    }
    private void Update()
    {
        if(ud != null)
        {
            SetLocalPlayerRes(ud.Username, ud.TotalCount.ToString(), ud.WinCount.ToString());
            ClearEnemyPlayerRes();
            ud = null;
        }
        if(ud1!=null)
        {
            SetLocalPlayerRes(ud1.Username, ud1.TotalCount.ToString(), ud1.WinCount.ToString());
            if (ud2 != null)
                SetEnemyPlayerRes(ud2.Username, ud2.TotalCount.ToString(), ud2.WinCount.ToString());
            else
                ClearEnemyPlayerRes();
            ud1 = null;
            ud2 = null;
        }
        if(IsPopPanel)
        {
            UIMng.PopPanel();
            IsPopPanel = false;
        }


    }
    public void SetLocalPlayerRes(string username,string totalCount,string winCount)
    {
        localPlayerUsername.text = username;
        localPlayerTotalCount.text ="总场数：" + totalCount;
        localPlayerWinCount.text ="胜利：" + winCount;
    }
    public void SetEnemyPlayerRes(string username,string totalCount,string winCount)
    {
        enemyPlayerUsername.text = username;
        enemyPlayerTotalCount.text ="总场数：" + totalCount;
        enemyPlayerWinCount.text ="胜利：" + winCount;
    }
    public void ClearEnemyPlayerRes()
    {
        enemyPlayerUsername.text = "";
        enemyPlayerTotalCount.text = "等待玩家加入.....";
        enemyPlayerWinCount.text ="";
    }
    private void OnStartClick()
    {
        startGameRequest.SendRequest();
    }
    public void OnStartResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Fail)
        {
            UIMng.ShowMessageSync("您不是房主，无法开始游戏!");
        }
        else
        {
            //TODO
            UIMng.PushPanelSync(UIPanelType.Game);
            facade.EnterPlayingSync();
        }
    }
    private void OnExitClick()
    {
        quitRoomRequest.SendRequest();
    }
    public void OnExitResponse()
    {
        IsPopPanel = true;
    }
    private void EnterAnim()
    {
        gameObject.SetActive(true);
        bluePanel.localPosition = new Vector3(-1000, 0, 0);
        bluePanel.DOLocalMoveX((float)(-195.1076), 0.4f);
        redPanel.localPosition = new Vector3(1000, 0, 0);
        redPanel.DOLocalMoveX(90, 0.4f);

        startButton.localScale = Vector3.zero;
        startButton.DOScale(1, 0.4f);

        exitButton.localScale = Vector3.zero;
        exitButton.DOScale(1, 0.4f);

    }
    private void ExitAnim()
    {
        bluePanel.DOLocalMoveX(-1000, 0.4f);
        redPanel.DOLocalMoveX(1000, 0.4f);
        startButton.DOScale(0, 0.4f);
        exitButton.DOScale(0, 0.4f).OnComplete(()=>gameObject.SetActive(false));
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (bluePanel == null ||
            redPanel == null ||
            startButton == null ||
            exitButton == null) 
                return;
        EnterAnim();
        //if(createRoomRequest!=null)
        //createRoomRequest.SendRequest();
    }

    public override void OnPause()
    {
        base.OnPause();
        ExitAnim();
    }

    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
    }

    public override void OnExit()
    {
        base.OnExit();
        ExitAnim();
    }
}

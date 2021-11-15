using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;
using Assets.Scripts;

public class LoginPanel : BasePanel
{
    
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;
    private Button closeButton;
    private Button loginButton;
    private Button registerButton;
    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();

        usernameIF = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();

        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        registerButton = transform.Find("RegisterButton").GetComponent<Button>();

        loginButton.onClick.AddListener(OnLoginClick);
        registerButton.onClick.AddListener(OnRegisterClick);
        closeButton.onClick.AddListener(OnCloseClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnimation();

        
    }


    private void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.2f);
        Tweener tweener =  transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f);
        tweener.OnComplete(()=>UIMng.PopPanel());

    }
    private void OnLoginClick()
    {
        PlayClickSound();
        string msg = "";
        if(string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if(string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if(msg!="")
        {
            UIMng.ShowMessage(msg);
        }
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }
    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            //TODO登录成功,进入房间列表
            UIMng.ShowMessageSync("登录成功");
            UIMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            UIMng.ShowMessageSync("用户名或者密码错误，无法登录");
        }
    }
    private void OnRegisterClick()
    {
        UIMng.PushPanel(UIPanelType.Register);
    }
    public override void OnPause()
    {
        base.OnPause();
        HideAnimation();
    }
    public override void OnExit()
    {
        base.OnExit();
        /*closeButton.onClick.RemoveAllListeners();
        loginButton.onClick.RemoveAllListeners();
        registerButton.onClick.RemoveAllListeners();*/
        HideAnimation();
    }
    public override void OnResume()
    {
        base.OnResume();
        EnterAnimation();
    }
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(CommonVar.SCENCE_CENTER, 0.2f);
    }
    private void HideAnimation()
    {
        transform.DOScale(0, 0.2f);
        transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f).OnComplete(()=> gameObject.SetActive(false));
    }
}

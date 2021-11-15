using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts;
using Common;

public class RegisterPanel : BasePanel
{
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    private Button RegisterButton;
    private Button CloseButton;
    private RegisterRequest registerRequest;
    private void Start()
    {
        base.OnEnter();
        registerRequest = GetComponent<RegisterRequest>();

        usernameIF = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        rePasswordIF = transform.Find("RepeatPasswordLabel/PasswordInput").GetComponent<InputField>();

        RegisterButton = transform.Find("RegisterButton").GetComponent<Button>();
        CloseButton = transform.Find("CloseButton").GetComponent<Button>();
        RegisterButton.onClick.AddListener(OnRegisterClick);
        CloseButton.onClick.AddListener(OnCloseClick);
    }
    public override void OnEnter()
    {
        gameObject.SetActive(true);

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(CommonVar.SCENCE_CENTER, 0.2f);
    }

    public override void OnExit()
    {
        base.OnExit();
        //gameObject.SetActive(false);
        //RegisterButton.onClick.RemoveAllListeners();
    }

    private void OnRegisterClick()
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
        if(passwordIF.text!=rePasswordIF.text)
        {
            msg += "密码不一致";
        }
        if (msg != "")
            UIMng.ShowMessage(msg);
        else
        {
            //进行注册
            registerRequest.SendRequest(usernameIF.text,passwordIF.text);
        }

    }
    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if(returnCode==ReturnCode.Success)
        {
            UIMng.ShowMessageSync("注册成功");
        }
        else
        {
            UIMng.ShowMessageSync("注册失败");
        }
    }
    private void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(()=>UIMng.PopPanel());
    }

}

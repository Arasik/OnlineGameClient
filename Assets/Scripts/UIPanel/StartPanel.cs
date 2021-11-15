using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StartPanel : BasePanel
{
    private Button loginButton;
    private Animator btnAnimator;
    private void Start()
    {
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        btnAnimator = loginButton.GetComponent<Animator>();
        loginButton.onClick.AddListener(OnLoginClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        
    }
    private void OnLoginClick()
    {
        PlayClickSound();
        UIMng.PushPanel(UIPanelType.Login);
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
}

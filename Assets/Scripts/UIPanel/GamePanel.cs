using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using Common; 
public class GamePanel : BasePanel
{
    private Button successButton;
    private Button failButton;
    private Text timer;
    private int time = -1;
    private void Start()
    {
        timer = transform.Find("Timer").GetComponent<Text>();
        timer.gameObject.SetActive(false);

        
        successButton = transform.Find("SuccessButton").GetComponent<Button>();
        failButton = transform.Find("FailButton").GetComponent<Button>();

        successButton.gameObject.SetActive(false);
        failButton.gameObject.SetActive(false);

        successButton.onClick.AddListener(OnResultClick);
        failButton.onClick.AddListener(OnResultClick);
    }
    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
        successButton.gameObject.SetActive(false);
        failButton.gameObject.SetActive(false);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
    }
    private void Update()
    {
        if (time > -1)
        {
            ShowTime(time);
            time = -1;
        }
    }
    private void OnResultClick()
    {
        UIMng.PopPanel();
        UIMng.PopPanel();
        facade.GameOver();
    }
    public void ShowTimeSync(int time)
    {
        this.time = time;
    }
    public void ShowTime(int time)
    {
        timer.gameObject.SetActive(true);
        timer.text = time.ToString();
        timer.transform.localScale = Vector3.one;
        Color tempColor = timer.color;
        tempColor.a = 1;
        timer.color = tempColor;

        timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        timer.DOFade(0,0.3f).SetDelay(0.3f).OnComplete(()=>timer.gameObject.SetActive(false));
        facade.PlayNormalSound(AudioManager.Sound_Alert);
    }
    public void OnGameOverResponse(ReturnCode returnCode)
    {
        Button tempBtn=null;
        switch(returnCode)
        {
            case ReturnCode.Success:
                tempBtn = successButton;
                
                break;
            case ReturnCode.Fail:
                tempBtn = failButton;
                break;
        }

        tempBtn.gameObject.SetActive(true);
        tempBtn.transform.localScale = Vector3.zero;
        tempBtn.transform.DOScale(1, 0.5f);
    }

}

using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
public class GamePanel : BasePanel
{
    private Text timer;
    private int time = -1;
    private void Start()
    {
        timer = transform.Find("Timer").GetComponent<Text>();
        timer.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(time>-1)
        {
            ShowTime(time);
            time = -1;
        }
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

}

using Common;

public class ShowTimerRequest : BaseRequest
{
    private GamePanel gamePanel;
    public override void Awake()
    {

        actionCode = ActionCode.ShowTimer;
        gamePanel = GetComponent<GamePanel>();
        base.Awake();
    }
    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        int time = int.Parse(data);
        gamePanel.ShowTimeSync(time);
    }
}

using Common;

public class CreateRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        //roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }
    public void SetPanel(BasePanel panel)
    {
        roomPanel = panel as RoomPanel;
    }
    public override void SendRequest()
    {

        base.SendRequest("r");

    }
    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if(returnCode==ReturnCode.Success)
        {
            //UserData ud = facade.GetUserData();
            roomPanel.SetLocalPlayerResSync();
            //roomPanel.ClearEnemyPlayerRes();
        }
    }
}

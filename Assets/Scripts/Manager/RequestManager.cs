using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class RequestManager : BaseManager
{
    public RequestManager(GameFacade gameFacade) : base(gameFacade) { }
    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();
    public void AddRequest(ActionCode actionCode,BaseRequest baseRequest)
    {
        requestDict.Add(actionCode, baseRequest);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }
    public void HandleResponse(ActionCode actionCode, string data)
    {
        BaseRequest baseRequest = requestDict.TryGet<ActionCode, BaseRequest>(actionCode);
        if(baseRequest == null)
        {
            Debug.LogWarning("�޷��õ�RequestCode[" + actionCode + "]��Ӧ��Request��");return;
        }
        baseRequest.OnResponse(data);
    }
}

using UnityEngine;
using Common;
using System.Collections.Generic;

public class PlayerManager : BaseManager
{
    private UserData userData;
    private Dictionary<RoleType, RoleData> roleDataDict = new Dictionary<RoleType, RoleData>();
    private Transform rolePositions;
    private RoleType currentRoleType;
    private GameObject currentRoleGameObject;
    private GameObject playerSyncRequest;
    private GameObject remoteRoleGameObject;

    public void SetCurrentRoleType(RoleType rt)
    {
        currentRoleType = rt;
    }
    public PlayerManager(GameFacade gameFacade) : base(gameFacade) { }

    public UserData UserData 
    {
        get => userData; 
        set => userData = value; 
    }
    public override void OnInit()
    {
        base.OnInit();
        rolePositions = GameObject.Find("RolePositions").transform;
        InitRoleDataDict();
    }
    private void InitRoleDataDict()
    {
        roleDataDict.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Hunter_BLUE", "Arrow_BLUE", rolePositions.Find("Position1")));
        roleDataDict.Add(RoleType.Red, new RoleData(RoleType.Red, "Hunter_RED", "Arrow_RED", rolePositions.Find("Position2")));
    }
    public void SpawnRoles()
    {
        foreach(RoleData rd in roleDataDict.Values)
        {
            GameObject go = GameObject.Instantiate(rd.RolePrefab, rd.SpawnPosition, Quaternion.identity);
            if (rd.RoleType == currentRoleType) 
            {
                currentRoleGameObject = go;
            }
            else
            {
                remoteRoleGameObject = go;
            }
            
        }
    }
    public GameObject GetCurrentRoleGameObject()
    {
        return currentRoleGameObject;
    }
    private RoleData GetRoleData(RoleType rt)
    {
        RoleData rd = null;
        roleDataDict.TryGetValue(rt,out rd);
        return rd;
    }
    public void AddControlScript()
    {
        currentRoleGameObject.AddComponent<PlayerMove>();
        PlayerAttack playerAttack = currentRoleGameObject.AddComponent<PlayerAttack>();
        RoleType rt = currentRoleGameObject.GetComponent<PlayerInfo>().roleType;
        RoleData rd = GetRoleData(rt);
        playerAttack.arrowPrefab = rd.ArrowPrefab;
    }
    public void CreateSyncRequest()
    {
        playerSyncRequest = new GameObject("PlayerSyncRequest");
        playerSyncRequest.AddComponent<MoveRequest>().SetLocalPlayer(
            currentRoleGameObject.transform,
            currentRoleGameObject.GetComponent<PlayerMove>()
            ).SetRemotePlayer(remoteRoleGameObject.transform);

    }
}

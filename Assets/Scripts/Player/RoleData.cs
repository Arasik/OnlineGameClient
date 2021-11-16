using Common;
using UnityEngine;

public class RoleData
{
    private const string PREFIX_PREFAB = "Prefabs/";
    public RoleType RoleType { get; private set; }
    public GameObject RolePrefab { get; private set; }
    public GameObject ArrowPrefab{get;private set;}
    public Vector3 SpawnPosition { get; private set; }


    public RoleData(RoleType roleType,string rolePath,string arrowPath,Transform spawnPos)
    {
        this.RoleType = roleType;
        this.RolePrefab = Resources.Load(PREFIX_PREFAB + rolePath) as GameObject;
        this.ArrowPrefab = Resources.Load(PREFIX_PREFAB + arrowPath) as GameObject;
        this.SpawnPosition = spawnPos.position;
    }
}

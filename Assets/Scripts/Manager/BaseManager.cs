using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager 
{
    protected GameFacade facade;
    public BaseManager(GameFacade gameFacade)
    {
        this.facade = gameFacade;
    }
    public virtual void OnInit() { }
    public virtual void Update() { }
    public virtual void OnDestroy() { }
}

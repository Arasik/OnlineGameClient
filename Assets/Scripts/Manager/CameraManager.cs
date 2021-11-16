using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : BaseManager
{
    private GameObject cameraGo;
    private Animator cameraAnim;
    private FllowTarget fllowTarget;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    public CameraManager(GameFacade gameFacade) : base(gameFacade) { }

    public override void OnInit()
    {
        base.OnInit();
        cameraGo = Camera.main.gameObject;
        cameraAnim = cameraGo.GetComponent<Animator>();
        fllowTarget = cameraGo.GetComponent<FllowTarget>();
        originalPosition = cameraGo.transform.position;
        originalRotation = cameraGo.transform.eulerAngles;
    }
    //public override void Update()
    //{
    //    base.Update();
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        FllowTarget(null);
    //    }
    //    if(Input.GetMouseButtonDown(1))
    //    {
    //        WalkthroughScene();
    //    }
    //}
    public void FllowRole()
    {
        fllowTarget.target = facade.GetCurrentRoleGameObject().transform;
        cameraAnim.enabled = false;
        Quaternion targetQuaternion = Quaternion.LookRotation(fllowTarget.target.position-cameraGo.transform.position);
        cameraGo.transform.DORotateQuaternion(targetQuaternion,1f).OnComplete(delegate() 
        {
            fllowTarget.enabled = true;
        });
        //fllowTarget.target = target;
        
        
        originalPosition = cameraGo.transform.position;
        originalRotation = cameraGo.transform.eulerAngles;
    }
    public void WalkthroughScene()
    {
        fllowTarget.enabled = false;
        cameraGo.transform.DOMove(originalPosition, 1f);
        cameraGo.transform.DORotate(originalRotation, 1f).OnComplete(delegate()
        {
            cameraAnim.enabled = true;
            //fllowTarget.enabled = false;

        });
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject arrowPrefab;
    private Animator anim;
    private Transform leftHandTrans;
    private Vector3 shootDir;
    private PlayerManager playerMng;
    void Start()
    {
        anim = GetComponent<Animator>();
        leftHandTrans = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
    }


    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool IsCollider = Physics.Raycast(ray, out hit);
                if (IsCollider)
                {
                    Vector3 point = hit.point;

                    //transform.LookAt(point);
                    point.y = transform.position.y;
                    shootDir = point - transform.position;
                    transform.rotation = Quaternion.LookRotation(shootDir);
                    anim.SetTrigger("Attack");

                    Invoke("Shoot", 0.5f);
                }
            }
        }
    }
    public void SetPlayerMng(PlayerManager playerManager)
    {
        this.playerMng = playerManager ;
    }
    private void Shoot()
    {
        
        playerMng.Shoot(arrowPrefab,leftHandTrans.position,Quaternion.LookRotation(shootDir));
        
        
    }
}

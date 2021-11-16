using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float forward = 0;
    private float speed = 3;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, v)*speed*Time.deltaTime,Space.World);
        if(h!=0.0f||v!=0.0f)
            transform.rotation = Quaternion.LookRotation(new Vector3(h,0,v));
        float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
        forward = res;
        animator.SetFloat("Forward", res);

    }
}

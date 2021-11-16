using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int speed=5;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.MovePosition(transform.position+transform.forward*speed*Time.deltaTime);
        //transform.Translate(Vector3.forward*speed*Time.deltaTime);
    }
}

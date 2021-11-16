using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowTarget : MonoBehaviour
{
    public Transform target;
    private Vector3 offset = new Vector3(0, 11.98111f, -14.10971f);
    private float smoothing = 2;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        transform.LookAt(target);
    }
}

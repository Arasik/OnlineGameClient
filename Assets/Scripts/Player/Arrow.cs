using Common;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public int speed=10;
    private Rigidbody rigidbody_;
    public RoleType roleType;
    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody_.MovePosition(transform.position+transform.forward*speed*Time.fixedDeltaTime);
        //transform.Translate(Vector3.forward*speed*Time.deltaTime);
        Debug.Log(rigidbody_.velocity.magnitude);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        GameObject.Destroy(gameObject);
    }
}

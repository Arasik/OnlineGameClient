using Common;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    public int speed=10;
    private Rigidbody rigidbody_;
    public RoleType roleType;
    public GameObject explosionEffect;
    public bool isLocal=false;
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
        //Debug.Log(rigidbody_.velocity.magnitude);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag.Equals("Player"))
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ShootPerson);
            if(isLocal)
            {
                bool playerIsLocal = collision.gameObject.GetComponent<PlayerInfo>().isLocal;
                if(isLocal!=playerIsLocal)
                {
                    GameFacade.Instance.SendAttack(Random.Range(10, 20));
                }
            }
        }
        else
        {
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Miss);
        }
        GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        GameObject.Destroy(gameObject);
    }
}

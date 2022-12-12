using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeBullet : MonoBehaviour
{
    [SerializeField] Item mirror;
    Item lastHitMiror;
    Rigidbody rb;
    public float lifeSpan = 1f;
    public float currentLifeTime = 0;
    [Space]
    public float speed = 5f;
    [Space]
    public int bounceAmount = 5;
    public int currentBounces = 0;
    [Space]
    public bool mirrorBounce = false;
    //public UnityEvent onPlayerShot;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided");
        if(collision.gameObject.GetComponent<Rigidbody>())
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 10f, collision.contacts[0].point, ForceMode.Impulse);//AddExplosionForce(50, collision.contacts[0].point, 1f);
        }

        if(collision.gameObject.GetComponent<CharacterController>() && mirrorBounce)
        {
            Debug.Log("ShotPlayer");
            lastHitMiror.GetComponent<MirrorView>().onShot.Invoke();
            lastHitMiror.GetComponent<HideInCovers>().ShowUntouched();
        }

        //If getComponent Explodable -> Explode and die
        if(collision.gameObject.GetComponent<Item>())
        {
            if (collision.gameObject.GetComponent<Item>().itemName == mirror.itemName)
            {
                mirrorBounce = true;
                lastHitMiror = collision.gameObject.GetComponent<Item>();
            }
            else
            {
                mirrorBounce = false;
                lastHitMiror = null;
            }
        }
        else
        {
            mirrorBounce = false;
            lastHitMiror = null;
        }


        currentBounces++;
        if (currentBounces > bounceAmount) Destroy(this.gameObject);
        Vector3 reflected = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
        transform.LookAt(transform.position + reflected);
        transform.position += transform.forward * .5f;

        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void LifeTick()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= lifeSpan) Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();


        LifeTick();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeBullet : MonoBehaviour
{

    public float lifeSpan = 1f;
    public float currentLifeTime = 0;
    [Space]
    public float speed = 5f;
    [Space]
    public int bounceAmount = 5;
    public int currentBounces = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        if(collision.gameObject.GetComponent<Rigidbody>())
        {
            collision.gameObject.GetComponent<Rigidbody>().AddExplosionForce(50, collision.contacts[0].point, 1f);
        }
        //If getComponent Explodable -> Explode and die

        currentBounces++;
        if (currentBounces > bounceAmount) Destroy(this.gameObject);
        transform.forward = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
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

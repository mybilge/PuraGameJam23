using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GumType gumType;
    Rigidbody rb;

    Vector3 dir;
    [SerializeField] float speed;
    [SerializeField] bool reflect;
    private Vector3 _velocity;
    private Vector3 oldvel;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
        
    }
    public void Fire(Vector3 dir){
        this.dir = dir;
        rb.velocity = (Time.fixedDeltaTime * speed * dir);
        oldvel = rb.velocity;
    }

    private void FixedUpdate() {
        
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<Collider>().isTrigger = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(gumType.act != null)
        {
            gumType.act();
        }
        
        if(!reflect)
        {
            return;
        }

        /*if(collision.gameObject.tag != "reflect")
        {
            oldvel = rb.velocity;
            return;
        }*/

        ReflectProjectile(rb, collision.contacts[0].normal);
    }

    private void ReflectProjectile(Rigidbody rb, Vector3 reflectVector)
    {
        _velocity = Vector3.Reflect(oldvel, reflectVector);
        _velocity.y = 0;
        rb.velocity = _velocity;
        oldvel = _velocity;
    }


}

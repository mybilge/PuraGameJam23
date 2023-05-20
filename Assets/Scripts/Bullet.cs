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
    [SerializeField] float canTime;

   public float power;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;

        switch (gumType.colors)
        {
            case Colors.Red:
                break;
            case Colors.Green:
                reflect = true;
                break;
            case Colors.Blue:
                break;
            case Colors.Orange:
                break;
            case Colors.Magenta:
                break;
            case Colors.Yellow:
                break;
        }
        
    }

    private void Update() {
        if(gumType.colors == Colors.Green)
        {
            canTime -= Time.deltaTime;
            if(canTime<=0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Fire(Vector3 dir){
        this.dir = dir;
        rb.velocity = (Time.fixedDeltaTime * speed * dir);
        oldvel = rb.velocity;
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<Collider>().isTrigger = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (gumType.colors)
        {
            case Colors.Red:
                break;
            case Colors.Green:
                ReflectProjectile(rb, collision.contacts[0].normal);
                if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    StartCoroutine(enemy.GeriSek(collision.GetContact(0).point, power,0.5f));
                }
                break;
            case Colors.Blue:
                break;
            case Colors.Orange:
                break;
            case Colors.Magenta:
                break;
            case Colors.Yellow:
                break;
        }        
    }

    private void ReflectProjectile(Rigidbody rb, Vector3 reflectVector)
    {
        _velocity = Vector3.Reflect(oldvel, reflectVector);
        _velocity.y = 0;
        rb.velocity = _velocity;
        oldvel = _velocity;
    }

    void GreenBullet()
    {

    }
}

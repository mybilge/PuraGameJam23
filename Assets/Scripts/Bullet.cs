using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GumType gumType;
    Rigidbody rb;
    public float power;
    Vector3 dir;
    [SerializeField] float speed;

    
    [Header("Green")]
    [SerializeField] float canTime;
    [SerializeField] float ittirGreen = 1f;
    private Vector3 _velocity;
    private Vector3 oldvel;

    [Header("Red")]
    [SerializeField] float boomRadiusBase = 1f;
    [SerializeField] float ittirRed = 1f;

    [Header("Blue")]
    [SerializeField] float sabitBase = 0.5f;

   
  

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
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
        switch (gumType.colors)
        {
            case Colors.Red:
                this.dir = dir;
                rb.velocity = (Time.fixedDeltaTime * speed * power * dir);
                break;
            case Colors.Green:
                this.dir = dir;
                rb.velocity = (Time.fixedDeltaTime * speed * power * dir);
                oldvel = rb.velocity;
                canTime*= power;
                break;
            case Colors.Blue:
                this.dir = dir;
                rb.velocity = (Time.fixedDeltaTime * speed * power * dir);
                break;
            case Colors.Cyan:
                break;
            case Colors.Magenta:
                break;
            case Colors.Yellow:
                break;
        }        
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<Collider>().isTrigger = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (gumType.colors)
        {
            case Colors.Red:
                RedBoom();
                break;
            case Colors.Green:
                ReflectProjectile(collision.contacts[0].normal);
                if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    StartCoroutine(enemy.GeriSek(collision.GetContact(0).point, power*ittirGreen,0.5f));
                }
                break;
            case Colors.Blue:
                if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyBlue))
                {
                    enemyBlue.Sabit(power*sabitBase);
                }         
                Destroy(gameObject);       
                break;
            case Colors.Cyan:
                break;
            case Colors.Magenta:
                break;
            case Colors.Yellow:
                break;
        }        
    }

    private void ReflectProjectile(Vector3 reflectVector)
    {
        _velocity = Vector3.Reflect(oldvel, reflectVector);
        _velocity.y = 0;
        rb.velocity = _velocity;
        oldvel = _velocity;
    }



    void RedBoom()
    {
        var hits = Physics.SphereCastAll(transform.position+Vector3.down*25, boomRadiusBase*power,Vector3.up,50);    

        foreach (var hit in hits)
        {
            Debug.Log(hit.transform.name);

            if(hit.transform.TryGetComponent<Enemy>(out var enemy))
            {
                if(hit.collider.isTrigger)
                {
                    continue;
                }
                StartCoroutine(enemy.GeriSek(hit.point, power*ittirRed, 0.5f));
            }


            if (hit.transform.TryGetComponent<Player>(out var player))
            {
                player.GeriSek(hit.point, power * ittirRed);
            }
        }   

        Destroy(gameObject); 
    }

}

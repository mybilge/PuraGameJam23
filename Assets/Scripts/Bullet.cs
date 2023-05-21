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


    [Header("Yellow")]
    public static float shoutgunMaxRangeStatic = 3f;
    public float maxRangeShotgun = 3f;
    float? shotgunAmount;
    [SerializeField] float ittirYellow = 1f;


    [Header("Cyan")]
    [SerializeField] float cekmeHiziBase = 1f;
    [SerializeField] float hookMaxRange = 3f;
    public static float hookMaxRangeStatic = 3f;
    bool hooked = false;
    Vector3 firstPos;



    [Header("Magenta")]
    [SerializeField] float zehirRadiusBase = 0.5f;
    [SerializeField] float zehirYavaslatmaBase = 1f;
    Transform zehirPrefab;

   
  

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
        shoutgunMaxRangeStatic = maxRangeShotgun;
        hookMaxRangeStatic = hookMaxRange;
    }

    private void Start() {
        zehirPrefab = Player.Instance.zehirPrefab;
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


        if(gumType.colors == Colors.Cyan)
        {
            if(Vector3.Distance(transform.position,firstPos)>=hookMaxRange && !hooked)
            {
                CyanHook(transform.position);
            }

        }
    }

    public void Fire(Vector3 dir, float? shotgunAmount){
        this.shotgunAmount = shotgunAmount;
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
                this.dir = dir;
                rb.velocity = (Time.fixedDeltaTime * speed * power * dir);
                firstPos = transform.position;
                break;
            case Colors.Magenta:
                this.dir = dir;
                rb.velocity = (Time.fixedDeltaTime * speed * power * dir);
                break;
            case Colors.Yellow:
                this.dir = dir;
                
                YellowShotgun();
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
                if(collision.transform.tag != "Bullet")
                {
                    RedBoom();
                }
                
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
                if(!hooked && collision.transform.tag != "Bullet")
                {
                    CyanHook(transform.position);
                }       
                break;
            case Colors.Magenta:
                if (collision.transform.tag != "Bullet" && collision.transform.tag != "Player")
                {
                    MagentaAlan();
                }
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


    void MagentaAlan()
    {
        Destroy(gameObject);
        Transform zehir = Instantiate(zehirPrefab,transform.position, Quaternion.identity);
        zehir.position = new Vector3(zehir.position.x,0,zehir.position.z);
        zehir.GetComponent<Zehir>().zehirAmount = zehirYavaslatmaBase*power;
        zehir.localScale *= zehirRadiusBase*power;
    }

    void CyanHook(Vector3 hookPos)
    {
        hooked = true;
        Player.Instance.Hook(hookPos, gameObject, cekmeHiziBase*power);
    }
    void YellowShotgun()
    {
        var hits = Physics.SphereCastAll(transform.position + Vector3.down * 25, maxRangeShotgun * shotgunAmount.Value, Vector3.up, 50);
        foreach (var hit in hits)
        {

            if (hit.transform.TryGetComponent<Enemy>(out var enemy))
            {
                if (hit.collider.isTrigger)
                {
                    continue;
                }
                Vector3 enemyDir = enemy.transform.position - transform.position;
                enemyDir.y = 0;
                if(Vector3.Angle(enemyDir, dir)< 180*shotgunAmount)
                {
                    StartCoroutine(enemy.GeriSek(hit.point, power * ittirYellow, 0.5f));
                }
            }
        }
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

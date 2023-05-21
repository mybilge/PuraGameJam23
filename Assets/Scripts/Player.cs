using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] int can = 3;
    Rigidbody rb;
   
    public static  Player Instance;
    LineRenderer lineRenderer;
    public bool isHooking;
    Vector3 hookPos;
    GameObject bullet;
    float cekmeHizi;
    public Transform zehirPrefab;

    

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    public void HasarAl()
    {
        can--;
        if(can<=0)
        {
            Die();
        }
    } 

    public void Die(){

    }

    private void Update() {
        if(isHooking)
        {
            transform.position = Vector3.Lerp(transform.position, hookPos, cekmeHizi*Time.deltaTime);
            lineRenderer.SetPosition(0,hookPos);
            lineRenderer.SetPosition(1, transform.position);

            if(Vector3.Distance(transform.position,hookPos)<0.5f)
            {
                isHooking = false;
                lineRenderer.enabled = false;
                
            }
        }
    }

    public void Hook(Vector3 hookPos, GameObject bullet, float cekmeHizi)
    {
        isHooking = true;
        this.hookPos = hookPos;
        this.bullet = bullet;
        this.cekmeHizi = cekmeHizi;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = true;
        Destroy(bullet);
        
    }


    public void GeriSek(Vector3 impactPoint, float force)
    {
        Vector3 dir = transform.TransformPoint(GetComponent<BoxCollider>().center) - impactPoint;
        dir.y = 0;
        Debug.Log(dir.normalized);
        rb.AddForce(force * dir.normalized, ForceMode.Impulse);        
    }



   


}

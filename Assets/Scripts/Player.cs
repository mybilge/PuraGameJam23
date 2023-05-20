using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int can = 3;
    Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
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


    public void GeriSek(Vector3 impactPoint, float force)
    {
        Vector3 dir = transform.TransformPoint(GetComponent<BoxCollider>().center) - impactPoint;
        dir.y = 0;
        Debug.Log(dir.normalized);
        rb.AddForce(force * dir.normalized, ForceMode.Impulse);        
    }
}

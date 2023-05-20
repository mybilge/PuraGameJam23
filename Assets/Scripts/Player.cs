using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int can = 3;


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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zehir : MonoBehaviour
{
    public float zehirAmount;

    List<Enemy> temasEnemy = new List<Enemy>();


    private void OnTriggerStay(Collider other) {
        if(other.TryGetComponent<Enemy>(out var enemy))
        {
            if (other.isTrigger)
            {
                return;
            }
            if(enemy.hizcarpan > 1 / zehirAmount)
            {
                enemy.hizcarpan = 1/zehirAmount;
            }            
            temasEnemy.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            if(other.isTrigger)
            {
                return;
            }
            enemy.hizcarpan = 1f;
            temasEnemy.Remove(enemy);
        }
    }

    private void OnDestroy() {
        foreach (var enemy in temasEnemy)
        {
            enemy.agent.speed = 1f;
        }
    }
}


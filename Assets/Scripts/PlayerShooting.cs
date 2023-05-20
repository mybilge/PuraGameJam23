using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct GumType
{
    public Sprite gumSprite;
    public Colors colors;
    public Action act;

    public GumType(Sprite sp, Colors cl, Action act){
        gumSprite = sp;
        colors = cl;
        this.act = act;
    }

    public GumType(Sprite sp, Colors cl)
    {
        gumSprite = sp;
        colors = cl;
        this.act = null;
    }
}

public enum Colors
{
    Red,
    Green,
    a,
    Blue,
    Magenta,
    Yellow,
    Orange
}

public class PlayerShooting : MonoBehaviour
{
    Queue<GumType> gumTypeQueue = new Queue<GumType>();
    [SerializeField] GumType[] gumTypeBase;
    

    [SerializeField] GameObject bulletPrefab;

    [SerializeField] int[] rands = { 0, 1, 3 };
    [SerializeField]  int[] olasilik = { 10, 10, 10 };


    private void Awake() {
        gumTypeQueue.Enqueue(new GumType(null, (Colors)RandColorsInt()));
        gumTypeQueue.Enqueue(new GumType(null, (Colors)RandColorsInt()));
    }


    int RandColorsInt()
    {
        

        int toplam = 0;
        for (int i = 0; i < olasilik.Length; i++)
        {
            toplam += olasilik[i];
        }

        int sec = UnityEngine.Random.Range(0, toplam);

        int temp = 0;
        

        for (int i = 0; i < olasilik.Length; i++)
        {
            temp+= olasilik[i];
            if(sec<temp)
            {
                return rands[i];
            }
        }

        return rands[0];

    }

    private void Update() {

        //Debug.Log((Colors)RandColorsInt());
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
            {
                Vector3 dir = hit.point -transform.position;
                dir.y = 0;

                Shoot(dir);

                gumTypeQueue.Enqueue(new GumType(null, (Colors)RandColorsInt()));
                //Debug.Log(gumTypeQueue.Peek().colors);
            }
        }
    }

    void Shoot(Vector3 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position,Quaternion.identity);
        bullet.GetComponent<Bullet>().gumType = Combine(gumTypeQueue.Dequeue(), gumTypeQueue.Peek());
        bullet.GetComponent<Bullet>().Fire(dir);
    }

    GumType Combine(GumType gumType1, GumType gumType2)
    {
        GumType retGumType = gumTypeBase[0];

        Colors c1 = gumType1.colors;
        Colors c2 = gumType2.colors;

        switch (((int)c1) + ((int)c2))
        {
            case 0:
                retGumType = gumTypeBase[ (int)Colors.Red];
                break;
            case 1:
                retGumType = gumTypeBase[ (int)Colors.Orange];
                break;
            case 2:
                retGumType = gumTypeBase[ (int)Colors.Green];
                break;
            case 3:
                retGumType = gumTypeBase[ (int)Colors.Magenta];
                break;
            case 4:
                retGumType = gumTypeBase[ (int)Colors.Yellow];
                break;
            case 6:
                retGumType = gumTypeBase[ (int)Colors.Blue];
                break;
            
        }
        
        //retGumType.act = Actaa;
        
        return retGumType;
    }
}

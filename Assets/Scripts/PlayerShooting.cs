using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct GumType
{
    public Sprite gumSprite;
    public Colors colors;

    public GumType(Sprite sp, Colors cl)
    {
        gumSprite = sp;
        colors = cl;
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

    [SerializeField] LayerMask zemin;
    [SerializeField] float exp = 3f;
    

    [SerializeField] GameObject bulletPrefab;

    [SerializeField] int[] rands = { 0, 1, 3 };
    [SerializeField]  int[] olasilik = { 10, 10, 10 };

    [SerializeField] float basiliTutMax = 1f;
    float tempBasiliTut = 0f;
    bool canFire = true;


    private void Awake() {
       // gumTypeQueue.Enqueue(new GumType(null, (Colors)RandColorsInt()));
        //gumTypeQueue.Enqueue(new GumType(null, (Colors)RandColorsInt()));

        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));

        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));

        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
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
        if(Input.GetMouseButton(0))
        {
            tempBasiliTut+= Time.deltaTime;
            if(tempBasiliTut> basiliTutMax)
            {
                canFire = false;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(canFire)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, zemin))
                {
                    Vector3 dir = hit.point - transform.position;
                    dir.y = 0;

                    float power = MathF.Pow((1+(tempBasiliTut/basiliTutMax)),exp);

                    Shoot(dir.normalized, power);

                    gumTypeQueue.Enqueue(new GumType(null, (Colors)RandColorsInt()));
                    //Debug.Log(gumTypeQueue.Peek().colors);
                }
            }            

            tempBasiliTut = 0f;
            canFire = true;
        }
    }

    void Shoot(Vector3 dir, float newPower)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position,Quaternion.identity);
        bullet.GetComponent<Bullet>().gumType = Combine(gumTypeQueue.Dequeue(), gumTypeQueue.Peek());
        bullet.GetComponent<Bullet>().Fire(dir);
        bullet.GetComponent<Bullet>().power = newPower;
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

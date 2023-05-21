using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    Cyan
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

    bool firstTimeClick = false;
    GumType? gumtypeTemp;

    [Header("Yellow")]
    [SerializeField] Image shotgunImage;
    [SerializeField] GameObject hookCursor;
    float? shotgunAmount = null;


    private void Awake() {
       // gumTypeQueue.Enqueue(new GumType(null, (Colors)RandColorsInt()));
        //gumTypeQueue.Enqueue(new GumType(null, (Colors)RandColorsInt()));

        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Blue));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Blue));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Green));

        gumTypeQueue.Enqueue(new GumType(null, Colors.Blue));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Red));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Red));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Red));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Red));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Red));

        gumTypeQueue.Enqueue(new GumType(null, Colors.Red));
        gumTypeQueue.Enqueue(new GumType(null, Colors.Red));

        UITemizle();
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
            if(!firstTimeClick)
            {
                var arr = gumTypeQueue.ToArray();
                gumtypeTemp = Combine(arr[0], arr[1]);
            }


            firstTimeClick = true;
            tempBasiliTut+= Time.deltaTime;
            if(tempBasiliTut> basiliTutMax && canFire)
            {
                GetComponent<Player>().HasarAl();
                canFire = false;
                // UI TEMİZLE
                UITemizle();

                return;
            }

            if(!canFire)
            {
                return;
            }

            if(gumtypeTemp.Value.colors == Colors.Yellow)
            {
                shotgunImage.enabled = true;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, zemin))
                {
                    Vector3 dir = hit.point-transform.position;
                    dir.y = 0;

                    //Debug.Log(Vector3.Angle(dir, transform.right));
                    ShotgunImageRotate(Vector3.SignedAngle(dir, transform.right,Vector3.up));
                    ShotgunImageSize((tempBasiliTut / basiliTutMax));
                    shotgunAmount = (tempBasiliTut / basiliTutMax);
                }                
            }

            if(gumtypeTemp.Value.colors == Colors.Cyan)
            {
                hookCursor.SetActive(true);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, zemin))
                {
                    Vector3 dir = hit.point - transform.position;
                    dir.y = 0;
                    dir.Normalize();
                    dir*= Bullet.hookMaxRangeStatic*(tempBasiliTut / basiliTutMax);
                    hookCursor.transform.position = dir + new Vector3(0,0.1f,0) + transform.position;

                }
            }
            


        }

        if(Input.GetMouseButtonUp(0))
        {
            UITemizle();
            
            firstTimeClick = false;
            gumtypeTemp = null;
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
            shotgunAmount = null;
            tempBasiliTut = 0f;
            canFire = true;
        }
    }


    void UITemizle()
    {
        shotgunImage.enabled = false;
        shotgunImage.fillAmount = 0f;
        shotgunImage.rectTransform.sizeDelta = Vector2.one;
        hookCursor.SetActive(false);
    }
    void Shoot(Vector3 dir, float newPower)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position,Quaternion.identity);
        bullet.GetComponent<Bullet>().gumType = Combine(gumTypeQueue.Dequeue(), gumTypeQueue.Peek());
        bullet.GetComponent<Bullet>().power = newPower;
        bullet.GetComponent<Bullet>().Fire(dir,shotgunAmount);
        bullet.GetComponent<Bullet>().mermiSprite.sprite = bullet.GetComponent<Bullet>().gumType.gumSprite;
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
                retGumType = gumTypeBase[ (int)Colors.Yellow];
                break;
            case 2:
                retGumType = gumTypeBase[ (int)Colors.Green];
                break;
            case 3:
                retGumType = gumTypeBase[ (int)Colors.Magenta];
                break;
            case 4:
                retGumType = gumTypeBase[ (int)Colors.Cyan];
                break;
            case 6:
                retGumType = gumTypeBase[ (int)Colors.Blue];
                break;
            
        }
        
        return retGumType;
    }





    public void ShotgunImageRotate(float angle)
    {
        float amount = shotgunImage.fillAmount;
        
        shotgunImage.rectTransform.localEulerAngles = new Vector3(0,0,(90f+angle + (180f*amount))%360);

       // Debug.Log(shotgunImage.rectTransform.localEulerAngles);
    }

    public void ShotgunImageSize(float range)
    {
        shotgunImage.rectTransform.sizeDelta = Mathf.Clamp(range* Bullet.shoutgunMaxRangeStatic,0f,Bullet.shoutgunMaxRangeStatic)
                                                *Vector2.one;
        shotgunImage.fillAmount = range/3;

    }
}

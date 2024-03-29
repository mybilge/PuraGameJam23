using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;


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

    [SerializeField] GameObject[] cans;

    [SerializeField] float levelTime;
    float levelTimeTemp;
    [SerializeField] Image siyah;
    [SerializeField] GameObject asansorKapıSol;
    [SerializeField] GameObject asansorKapıSağ;
    [SerializeField] GameObject asansorMain;

    [SerializeField] TextMeshProUGUI zamanText;
    bool kazandi;
    bool asansor;

    bool oldu;

    [SerializeField] Image kazanImage;
    [SerializeField] Image kaybetImage;

    

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    public void HasarAl()
    {
        if(kazandi)
        {
            return;
        }

        can--;
        cans[(can+300)%3].SetActive(false);
        if(can<=0)
        {
            oldu = true;
            Die();
        }
    } 

    public void Die(){
        Debug.Log("aaa");
        kaybetImage.gameObject.SetActive(true);
        siyah.DOFade(0, 10f).OnComplete(() =>
        {
            
            SceneManager.LoadScene("MainMenu");
        });
    }

    private void Update() {
        levelTimeTemp += Time.deltaTime;
        zamanText.text =(Mathf.Max(levelTime- levelTimeTemp,0)) .ToString("F2");
        if(levelTimeTemp>= levelTime && !asansor)
        {
            asansor = true;
            Kazan();
        }
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

    void Kazan()
    {
        asansorKapıSağ.transform.DOLocalMoveX(-1.5f,1f);
        asansorKapıSol.transform.DOLocalMoveX(1.5f, 1f).OnComplete(()=>{
            asansorMain.SetActive(true);
        });
    }

    public void YeniLevel()
    {
        if(oldu)
        {
            return;
        }
        kazandi = true;
        kazanImage.gameObject.SetActive(true);
        siyah.DOFade(0,10f).OnComplete(()=>{
            SceneManager.LoadScene("MainMenu");
        });
    }

    


}

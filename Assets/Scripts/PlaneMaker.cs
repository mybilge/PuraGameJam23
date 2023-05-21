using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMaker : MonoBehaviour
{
    Vector3 scale;
    [SerializeField] SpriteRenderer zemin   ;


    List<SpriteRenderer> allSprites = new List<SpriteRenderer>();
    private void Awake() {
        scale = transform.localScale;

        for (int i = 0; i < 3; i++)
        {
            var child = transform.GetChild(i);
            if(child.transform.localPosition.x != 0)
            {
                child.GetComponent<BoxCollider>().size= new Vector3(1,1,scale.z * 50);
            }
            else if(child.transform.localPosition.z != 0)
            {
                child.GetComponent<BoxCollider>().size = new Vector3(scale.x * 50, 1, 1);
            }
        }
    }

    private void Update() {
        foreach (var sprite in allSprites)
        {
            sprite.sortingOrder = (int)(sprite.gameObject.transform.position.z*-100);
        }
    }


    private void Start() {
        allSprites.AddRange(GameObject.FindObjectsOfType<SpriteRenderer>());

        allSprites.Remove(zemin);
    }


    public void AddSpriteList(SpriteRenderer spriteRenderer)
    {
        allSprites.Add(spriteRenderer);
    }
}

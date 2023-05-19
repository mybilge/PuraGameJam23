using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMaker : MonoBehaviour
{
    Vector3 scale;
    private void Awake() {
        scale = transform.localScale;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if(child.transform.localPosition.x != 0)
            {
                child.GetComponent<BoxCollider>().size= new Vector3(1,1,scale.z * 10);
            }
            else if(child.transform.localPosition.z != 0)
            {
                child.GetComponent<BoxCollider>().size = new Vector3(scale.x * 10, 1, 1);
            }
        }
    }
}

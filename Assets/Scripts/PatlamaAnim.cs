using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatlamaAnim : MonoBehaviour
{
    Animation animation;
    [SerializeField] AnimationClip[] anims;

    private void Awake() {
        animation = GetComponent<Animation>();
    }

    float temp = 0;
    private void Update() {
            temp+= Time.deltaTime;
            if(temp>1f)
            {
                Destroy(gameObject);
            }
    }
}

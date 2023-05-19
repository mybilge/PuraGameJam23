using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{
    int clickCount = 0;

    [SerializeField] List<Sprite> easterEggSprites;
    [SerializeField] Image easterEggImage;
    public void EasterEggOnClick()
    {
        clickCount++;

        if(clickCount >= 5)
        {
            easterEggImage.enabled = true;
            easterEggImage.sprite = easterEggSprites[Random.Range(0, easterEggSprites.Count)];
            clickCount = 0;
        }
    }


    private void OnEnable() {
        clickCount = 0;
        easterEggImage.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EasterEgg : MonoBehaviour
{
    int clickCount = 0;

    [SerializeField] List<Sprite> easterEggSprites;
    [SerializeField] Image easterEggImage;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button showBtn;

    int sayac = 0;

    private void Start() {
        clickCount = 0;
        easterEggImage.enabled = false;
        showBtn.enabled = false;
        inputField.text = "";
    }


    public void EasterEggOnClick()
    {
        clickCount++;

        if(clickCount >= 1)
        {
            Show();
            clickCount = 0;
        }
    }

    public void CheckText()
    {
        if(inputField.text == "minnosum")
        {
            showBtn.enabled = true;
        }
    }

    void Show()
    {
        easterEggImage.enabled = true;
        easterEggImage.sprite = easterEggSprites[(sayac++)%easterEggSprites.Count];
    }


    private void OnEnable() {
        clickCount = 0;
        easterEggImage.enabled = false;
        showBtn.enabled = false;
        inputField.text = "";
    }
}

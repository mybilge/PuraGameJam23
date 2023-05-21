using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NormalMainMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI usernameScoreText;

    [SerializeField] Sprite[] cutscenes;
    [SerializeField] Image cutsceneImage;

    int temp = 0;
    int max;

    [SerializeField] Button ilerleBtn;
    private void Start() {
        ilerleBtn.gameObject.SetActive(false);
        cutsceneImage.gameObject.SetActive(false);
        //ShowUsernameScore();
        max = cutscenes.Length;
    }
    void ShowUsernameScore()
    {
        usernameScoreText.text = "Username: " + PlayerPrefs.GetString("Username") 
                                + "\nBest Score: " + PlayerPrefs.GetInt("BestScore").ToString();
    }


    public void Ilerle()
    {
        PlayGame();
    }

    public void PlayGame()
    {
        cutsceneImage.gameObject.SetActive(true);
        ilerleBtn.gameObject.SetActive(true);
        if(temp >= max)
        {
            SceneManager.LoadScene("Level1");       
        }
        else{
            cutsceneImage.sprite = cutscenes[temp];
            temp++;
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnEnable() {
        ShowUsernameScore();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NormalMainMenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI usernameScoreText;


    private void Start() {
        ShowUsernameScore();
    }
    void ShowUsernameScore()
    {
        usernameScoreText.text = "Username: " + PlayerPrefs.GetString("Username") 
                                + "\nBest Score: " + PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnEnable() {
        ShowUsernameScore();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FirstPlayMenuManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInputField;
    [SerializeField] TextMeshProUGUI uyariText;
    [SerializeField] GameObject normalMainMenu;



    private void Awake() {
        usernameInputField.characterLimit = 20;
        uyariText.text = "";
        usernameInputField.ActivateInputField();
    }

    public void Next()
    {
        CheckUsername();        
    }

    void GoNext()
    {
        PlayerPrefs.SetString("Username", usernameInputField.text);
        LeaderboardManager.Instance.SetLeaderboardEntry(usernameInputField.text,0);
        usernameInputField.ActivateInputField();
        gameObject.SetActive(false);
        normalMainMenu.SetActive(true);
    }

    void CheckUsername()
    {
        usernameInputField.DeactivateInputField();
        uyariText.text = "";
        
        string typedName = usernameInputField.text;

        if(typedName == "")
        {
            uyariText.text = "Kullanıcı adı boş olamaz";
            Fail();
            return;
        }

        if (typedName.Contains(" ") ||  typedName.Contains("\n") || typedName.Contains("\t"))
        {
            uyariText.text = "Kullanıcı adı boşluk içeremez";
            Fail();
            return;
        }


        LeaderboardManager.Instance.IsUsernameAlreadyExist(typedName,GoNext,Fail);  
    }

    void Fail(bool fromLeaderboard = false){

        usernameInputField.ActivateInputField();
        if(fromLeaderboard)
        {
            uyariText.text = "Kullanıcı adı zaten alınmış";
        }
    }
}

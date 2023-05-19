using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


using Dan.Main;
using System;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> names;
    [SerializeField] List<TextMeshProUGUI> scores;

    
    public static LeaderboardManager Instance;


    private void Awake() {

        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;


        for (int i = 0; i < names.Count; i++)
        {
            names[i].text = "";
            scores[i].text = "";
        }
    }



    string leaderboardPublicKey = "d0d906b8541ca9ecec054408e257f07ea1dd1b322d570508f43a82563a54af8f";
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(leaderboardPublicKey,((msg)=>
        {
            int loopLength = (msg.Length<names.Count)?msg.Length:names.Count;
            //Debug.Log(msg.Length);
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(leaderboardPublicKey, username,score,((msg)=>
        {
            GetLeaderboard();
        }));
    }

    public void IsUsernameAlreadyExist(string username, Action goNext, Action<bool> fail)
    {

        LeaderboardCreator.GetLeaderboard(leaderboardPublicKey, ((msg) =>
        {
            bool isFail = false;
            for (int i = 0; i < msg.Length; i++)
            {
                if(msg[i].Username == username)
                {   
                    isFail = true;
                    break;
                }
            }

            if(!isFail)
            {
                goNext();
            }
            else{
                fail(true);
            }
        }));
    }

    


}

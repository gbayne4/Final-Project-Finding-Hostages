using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class RestVariables : MonoBehaviour
{
    // prepare my static variables 
    void Start()
    {
        ScoreManager.hostages = 16;
        ScoreManager.hostagesDeadByMonster = 0;
        ScoreManager.hostagesDeadByYou = 0;
        ScoreManager.hostagesSaved = 0;
        ScoreManager.score = 0;
        ScoreManager.timer = 0;
    }

    private void Update()
    {
        ScoreManager.timer++;
        ScoreManager.timer = (1 / ScoreManager.timer) * 100;

        if (ScoreManager.hostages <= 0)
        {
            { SceneManager.LoadScene("EndScene"); }
        }
    }
}

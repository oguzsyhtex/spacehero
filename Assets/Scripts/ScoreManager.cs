using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text textScore;

    private int score = 0;
    private int highScore = 0;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Text txtHighScore;

    public void UpdateScore(int amount)
    {
        score += amount;
        textScore.text = "SCORE : " + score;

        if (score % 10== 0 && score !=0)
        {
            playerController.UpdateLevel();
        }
    }



    public void SetHighScore()
    {
        if (score>=highScore)
        {
            PlayerPrefs.SetInt("HIGH_SCORE: ", score);
        }
    }
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGH_SCORE: ", 0);
        txtHighScore.text = "HIGHSCORE: " + highScore;
        UpdateScore(0);
    }

}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text textScore;

    private int score = 0;

    public void UpdateScore(int amount)
    {
        score += amount;
        textScore.text = "SCORE : " + score;
    }

    private void Start()
    {
        UpdateScore(0);
    }

}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecorder
{
    public Text scoreText;
    private int score = -1;
    public void resetScore()
    {
        score = -1;
    }
    public void addScore(int add)
    {
        score += add;
        scoreText.text = "Your Score:" + score;
    }

    public void setDisActive()
    {
        scoreText.text = "";
    }

    public void setActive()
    {
        scoreText.text = "Your Score:" + score;
    }
}


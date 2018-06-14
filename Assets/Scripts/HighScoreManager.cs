using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {
    
    [SerializeField]
    private int highScore;
    [SerializeField]
    private int currentScore;

    [SerializeField]
    private float lowestTime;
    [SerializeField]
    private float currentTime;

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text timeText;

    [SerializeField]
    Text highScoreText;
    [SerializeField]
    Text lowTimeText;


    private void Awake()
    {
        currentTime = 99999f;
    }
    // Use this for initialization
    void Start () {
        highScore = PlayerPrefs.GetInt("HighScore");
        if (highScoreText)
        {
            highScoreText.text = "HighScore: " + highScore.ToString();
        }

        lowestTime = PlayerPrefs.GetFloat("TimeScore");
        if (lowTimeText)
        {
            lowTimeText.text = "Lowest Time: " + lowestTime.ToString();
        }
    }

	// Update is called once per frame
	void Update ()
    {
		if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        if(currentTime < lowestTime)
        {
            lowestTime = currentTime;
            PlayerPrefs.SetFloat("TimeScore", currentTime);
        }

        if(scoreText && timeText)
        {
            timeText.text = "Your time: " + currentTime.ToString();
            scoreText.text = "Your Score: " + currentScore.ToString();
        }
	}
    
    public void setTime(float time)
    {
        currentTime = time;
    }

    public void setScore(int score)
    {
        currentScore += score;
    }

    public int[] getScore()
    {
        int[] items = { currentScore, highScore };
        return items;
    }

    public float[] getTime()
    {
        float[] items = { currentTime, lowestTime };
        return items;
    }

}

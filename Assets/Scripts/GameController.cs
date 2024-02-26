using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiText;
    public float initialSpeed = 1f;
    public float gameSpeed {get ;private set;}
    public float gameSpeedIncrease = 1f;
    public float score;
    public bool gameOver;

    void Start()
    {
        NewGame();
    }

    void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        if(gameOver)GameOver();
    }

    void NewGame()
    {
        score = 0;
        gameSpeed = initialSpeed;
        enabled = true;
        UpdateHighscore();
    }
    void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;
        gameSpeedIncrease = 0f;
        UpdateHighscore();
    }

    void UpdateHighscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore",0);
        if(score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }
        hiText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}

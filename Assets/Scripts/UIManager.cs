using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject LevelComplete, GameOver;
    public Text scoreText;
    public GameObject[] lives;

    private void Awake()
    {
        instance = this;
    }
    public void LevelCompletePanel(bool isTrue)
    {
        if (isTrue)
        {
            GameManager.instance.isInputOn = false;
            GameManager.instance.ResetBallPosition();
        }
        LevelComplete.SetActive(isTrue);


    }
    public void GameOverPanel(bool isTrue)
    {
        if (isTrue)
        {
            GameManager.instance.isInputOn = false;
            GameManager.instance.ResetBallPosition();
        }
        GameOver.SetActive(isTrue);
       
    }
    public void ScoreIncrement(int x)
    {
        scoreText.text = x.ToString();
    }
}

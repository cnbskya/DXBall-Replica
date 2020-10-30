using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.WSA;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject bulletPrefab;
    public Transform levelsParent;
    public GameObject currentlyActiveLevel;
    public GameObject livesSample;
    public Transform ParentBallAndPlayer,Ball,Player;
    public Transform fireLeft, fireRight;
    public GameObject[] bonus;
    private bool fireMode;
    [Space]
    public bool isGameOn;
    public bool isInputOn = true;
    int lives = 3;
    int totalScore;
    

    public void SpawnBonus(Transform BrokenBrick)
    {
        int rand = Random.Range(0, bonus.Length);
        Instantiate(bonus[5], BrokenBrick.position, transform.rotation);

    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentlyActiveLevel = GameObject.Find("Levels").transform.GetChild(0).gameObject;
        currentlyActiveLevel.SetActive(true);
    }

    [ContextMenu("Next")]
    public void NextLevel()
    {
        ToggleUltimate(false);
        isInputOn = true;
        isGameOn = false;
        ResetBallPosition();
        PlayerResetSize();
        currentlyActiveLevel.SetActive(false);
        int currentSiblingIndex = currentlyActiveLevel.transform.GetSiblingIndex();

        if (currentSiblingIndex + 1 == levelsParent.childCount)
        {
            Debug.Log("Oyun Bitti");
            return;
        }
        currentlyActiveLevel = levelsParent.GetChild(currentSiblingIndex + 1).gameObject;
        currentlyActiveLevel.SetActive(true);
        UIManager.instance.LevelCompletePanel(false);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetBallPosition()
    {
        BulletToggle(false);
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponent<SphereCollider>().enabled = false;
        Ball.parent = ParentBallAndPlayer;
        Ball.localPosition = new Vector3(0.27f, 0.217f, 0);
        ParentBallAndPlayer.position = Vector3.down * 3.7f;
    }
    public void UpdateScore(bool reset,bool isBonus)
    {
        if (reset)
        {
            totalScore = 0;
            UIManager.instance.ScoreIncrement(totalScore);
            return;
        }
        if (isBonus)
        {
            totalScore += Random.Range(0,501);
            UIManager.instance.ScoreIncrement(totalScore);
        }
        else
        {
            totalScore += 100;
            UIManager.instance.ScoreIncrement(totalScore);
        }
    }
    
    public void PlayerExchanceReduction()
    {
        if (Player.transform.localScale.x == 1.2f)
        {
            Player.transform.localScale = new Vector3(0.8f, 0.15f, 1f);
        }
        else if (Player.transform.localScale.x == 1.6f)
        {
            Player.transform.localScale = new Vector3(1.2f,0.15f,1f);
        }
    }
    public void PlayerExchanceMagnification()
    {
        if (Player.transform.localScale.x == 0.8f)
        {
            Player.transform.localScale = new Vector3(1.2f, 0.15f, 1f);
        }
        else if (Player.transform.localScale.x == 1.2f)
        {
            Player.transform.localScale = new Vector3(1.6f, 0.15f, 1f);
        }
        else if (Player.transform.localScale.x == 1.6f)
        {
            Player.transform.localScale = new Vector3(2f, 0.15f, 1f);
        }
    }
    public void PlayerResetSize()
    {
        Player.transform.localScale = new Vector3(1.2f, 0.15f, 1f);
    }

    public void BallFail()
    {
        ToggleUltimate(false);
        isGameOn = false;
        lives--;
        if (lives == 0)
        {
            UIManager.instance.GameOverPanel(true);
            UpdateScore(false, true);
            Destroy(UIManager.instance.lives[2]);
            ResetBallPosition();
        }
        else
        {
            if (lives == 2)
            {
                Destroy(UIManager.instance.lives[0]);
            }
            else if (lives == 1)
            {
                Destroy(UIManager.instance.lives[1]);
            }
            ResetBallPosition();
        }
    }

    public void UpdateHealt()
    {
        if(lives <= 3)
        {
            if (lives == 3)
            {
                return;
            }
            else if (lives == 2)
            {
                Instantiate(livesSample, GameObject.Find("Lives").transform);
                lives++;
            }
            else if (lives == 1)
            {
                Instantiate(livesSample, GameObject.Find("Lives").transform);
                lives++;
            }
            else
            {
                Instantiate(livesSample, GameObject.Find("Lives").transform);
                lives++;
            }
        }
    }

    IEnumerator FireEnumerator()
    {
        while (fireMode)
        {
            Instantiate(bulletPrefab, fireLeft.transform.position, Quaternion.identity);
            Instantiate(bulletPrefab, fireRight.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.6f);
        }
    }
    public void BulletToggle(bool toogle)
    {
        if (toogle)
        {
            if(fireMode == false)
            {
                fireMode = true;
                StartCoroutine(FireEnumerator());
            }
        }
        else
        {
            if (fireMode == true)
            {
                fireMode = false;
                StopCoroutine(FireEnumerator());
            }
        }
    }
    public void ToggleUltimate(bool toogle)
    {
        if (toogle)
        {
            Ball.GetComponent<BallMovement>().isUltimate = true;
            Ball.GetComponent<SphereCollider>().isTrigger = true;
        }
        else
        {
            Ball.GetComponent<BallMovement>().isUltimate = false;
            Ball.GetComponent<SphereCollider>().isTrigger = false;
        }
    }
}

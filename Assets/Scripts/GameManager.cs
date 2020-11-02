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
    public AudioClip[] soundFiles;
    private bool fireMode;
    [Space]
    public bool isGameOn;
    public bool isInputOn = true;
    int lives = 3;
    int totalScore;
    

    public void SpawnBonus(Transform BrokenBrick)
    {
        int rand = Random.Range(0, bonus.Length);
        Instantiate(bonus[rand], BrokenBrick.position, transform.rotation);
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
        BrickCollisionController.ToggleTrigger(false);
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
        Vector3 scale = Player.transform.localScale;

        if (Player.transform.localScale.x >= 1.2f)
        {
            scale -= Vector3.right * 0.4f;
            Player.transform.localScale = scale;
        }
    }
    public void PlayerExchanceMagnification()
    {
        Vector3 scale = Player.transform.localScale;

        if (Player.transform.localScale.x < 2)
        {
            scale += Vector3.right * 0.4f;
            Player.transform.localScale = scale;
        }
    }
    public void PlayerResetSize()
    {
        Player.transform.localScale = new Vector3(1.2f, 0.15f, 1f);
    }
    public void BallFail()
    {
        isGameOn = false;
        lives--;
        BallFailSound();
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
    public IEnumerator ClearBonusses()
    {
        yield return new WaitForEndOfFrame();

        BonusController[] bonusControllers = FindObjectsOfType<BonusController>();
        Debug.Log(bonusControllers);
        foreach (BonusController b in bonusControllers)
        {
            Destroy(b.gameObject);
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
        BrickCollisionController.ToggleTrigger(toogle);
    }
    public void BallFailSound()
    {
        GetComponent<AudioSource>().PlayOneShot(soundFiles[2], 1);
    }
    public void BoingSound()
    {
        GetComponent<AudioSource>().PlayOneShot(soundFiles[0], 1);
    }
    public void WallSound()
    {
        GetComponent<AudioSource>().PlayOneShot(soundFiles[4], 1);
    }
    public void BrickSound()
    {
        GetComponent<AudioSource>().PlayOneShot(soundFiles[1], 1);
    }
    public void IronSound()
    {
        GetComponent<AudioSource>().PlayOneShot(soundFiles[3], 1);
    }
}

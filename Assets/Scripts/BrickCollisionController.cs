using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickCollisionController : MonoBehaviour
{
    public int collCount = 2;
    public bool isIron;
    int randomBonus = 0;
    public bool isBonusOn;
    public static bool isTriggered;

    private void Start()
    {
        if(isIron)
        {
            GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
        }
    }
    
    public static void ToggleTrigger(bool toggle)
    {
        isTriggered = toggle;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            BallCollision();
        }
    }

    private void Update()
    {
        if (isTriggered)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.name == "Ball")
        {
            BallCollision();
        }
    }
    public void BallCollision()
    {
        if (isIron == false)
        {
            GameManager.instance.BrickSound();
            Destroy(gameObject);
            GetComponentInParent<LevelManagerLocal>().CounterBrick();
            GameManager.instance.UpdateScore(false, false);
            if (isBonusOn)
            {
                GameManager.instance.SpawnBonus(transform);
            }
        }

        if (isIron)
        {
            GameManager.instance.IronSound();
            if (collCount > 0)
            {
                collCount--;
                //Debug.Log(collCount);
            }
            if (collCount == 0)
            {
                Destroy(gameObject);
                GetComponentInParent<LevelManagerLocal>().CounterBrick();
                GameManager.instance.UpdateScore(false, false);
                if (isBonusOn)
                {
                    GameManager.instance.SpawnBonus(transform);
                }
            }
            if (collCount == 1)
            {
                GetComponent<Renderer>().material.color = new Color(105, 105, 105);
                GameManager.instance.UpdateScore(false, false);
            }
        }
    }
}

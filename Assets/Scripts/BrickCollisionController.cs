using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class BrickCollisionController : MonoBehaviour
{
    public int collCount = 2;
    public bool isIron;
    public bool isBonusOn;
    public static bool isTriggered;
    public ParticleSystem brokenBrickEffect;

    

    public void StartParticle()
    {
        
        ParticleSystem ps = Instantiate(brokenBrickEffect, transform.position, transform.rotation);
        ParticleSystem.MainModule mModule = ps.main;
        mModule.startColor = GetComponent<Renderer>().material.color;
    }

    private void Start()
    {
        StartTweenAnimation();
        if (isIron)
        {
            GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
        }
    }
    void StartTweenAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(1f, 1f, 1f), UnityEngine.Random.Range(0f, 2f)).SetDelay(UnityEngine.Random.Range(0f, 1f));
    }
    public static void ToggleTrigger(bool toggle)
    {
        isTriggered = toggle;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
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
        if (other.gameObject.name == "Ball")
        {
            BallCollision();
            BallCollision();
        }
        if (other.gameObject.CompareTag("Bullet"))
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
            StartParticle();
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
                StartParticle();
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

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

    private void Start()
    {
        if(isIron)
        {
            GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
        }
    }

    //private void Update()
    //{
    //    randomBonus = Random.Range(1, 2);
    //}
    

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            BallCollision();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            BallCollision();
        }
    }
    public void BallCollision()
    {
        if (isIron == false)
        {
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

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditorInternal;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Transform Player;
    public int ballSpeed;
    private float yLocalPosition;
    //public bool isUltimate;
    
    int collCount = 1;

    // Update is called once per frame
    void Update()
    {
        StartGame();
    }

    public void StartGame()
    {
        if (!GameManager.instance.isGameOn && Input.GetMouseButtonUp(0) && GameManager.instance.isInputOn)
        {
            GameManager.instance.isGameOn = true;
            GetComponent<SphereCollider>().enabled = true;
            transform.parent = null;
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            Vector3 ballVector = (transform.position - Player.transform.position).normalized;
            rb.velocity = ballVector * ballSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            Vector3 ballVector = (transform.position - collision.transform.position).normalized;
            rb.velocity = ballVector * ballSpeed;
        }

        if (collision.gameObject.CompareTag("Frame"))
        {
            if(Mathf.Abs(yLocalPosition - transform.position.y) < 0.001f)
            {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(1, -1, 0).normalized * ballSpeed;
            }
            yLocalPosition = transform.position.y;
        }

        if (collision.gameObject.CompareTag("GameOverFrame"))
        {
            GameManager.instance.BallFail();
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if ((collision.gameObject.CompareTag("Frame") || collision.gameObject.CompareTag("Player")) && isUltimate)
    //    {
    //        GetComponent<SphereCollider>().isTrigger = true;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if ((other.gameObject.CompareTag("Frame") || other.gameObject.CompareTag("Player")) && isUltimate)
    //    {
    //        GetComponent<SphereCollider>().isTrigger = false;
    //    }
    //}
}

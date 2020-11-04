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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            Vector3 ballVector = (transform.position - collision.transform.position).normalized;
            rb.velocity = ballVector * ballSpeed;
            GameManager.instance.BoingSound();
        }

        if (collision.gameObject.CompareTag("Frame"))
        {
            GameManager.instance.WallSound();
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
}

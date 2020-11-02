using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.SetParent(null);
        GetComponent<Rigidbody>().velocity = Vector3.up * 10;
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("IronObstacle"))
        {
            Destroy(gameObject);
        }
    }
}

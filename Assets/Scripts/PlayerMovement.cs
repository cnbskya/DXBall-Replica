using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform stick, stickParent, ball;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        HitFirstTime();
        if(Input.GetMouseButton(0) && GameManager.instance.isInputOn)
        {
            stickParent.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -3.7f, 0);
            float limit = 2.7f - stick.lossyScale.x / 2;
            if ((stickParent.position.x < -limit))
            {
                stickParent.position = new Vector3(-limit, -3.7f, 0);
            }

            if ((stickParent.position.x > limit))
            {
                stickParent.position = new Vector3(limit, -3.7f, 0);
            }
        }
    }

    public void HitFirstTime()
    {
        if (!GameManager.instance.isGameOn && Input.GetMouseButtonUp(0) && GameManager.instance.isInputOn)
        {
            GameManager.instance.isGameOn = true;
            ball.GetComponent<SphereCollider>().enabled = true;
            ball.parent = null;
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            Vector3 ballVector = (ball.position - stick.transform.position).normalized;
            rb.velocity = ballVector * 8;
        }
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform stick;
    void Start()
    {
        stick = transform.GetChild(1);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && GameManager.instance.isInputOn)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -3.7f, 0);
            float limit = 2.7f - stick.lossyScale.x / 2;
            if ((transform.position.x < -limit))
            {
                transform.position = new Vector3(-limit, -3.7f, 0);
            }

            if ((transform.position.x > limit))
            {
                transform.position = new Vector3(limit, -3.7f, 0);
            }
        }
    }
}

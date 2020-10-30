using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerLocal : MonoBehaviour
{
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void CounterBrick()
    {
        if (transform.childCount == 1)
        {
            UIManager.instance.LevelCompletePanel(true);
        }
    }
}

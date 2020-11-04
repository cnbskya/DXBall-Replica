using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManagerLocal : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }
    public void CounterBrick()
    {
        if (transform.childCount == 1)
        {
            StartCoroutine(GameManager.instance.ClearBonusses());
            UIManager.instance.LevelCompletePanel(true);
        }
    }
}

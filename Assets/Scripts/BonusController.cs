using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // PADDLE İLE ÇARPIŞINCA OLACAK OLANLAR 
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("BonusBrick"))
            {
                GameManager.instance.UpdateScore(false, true);
            }
            else if (gameObject.CompareTag("BonusHealt"))
            {
                GameManager.instance.UpdateHealt();
            }
            else if (gameObject.CompareTag("BonusSmallBrick"))
            {
                GameManager.instance.PlayerExchanceReduction();
            }
            else if (gameObject.CompareTag("BonusBigBrick"))
            {
                GameManager.instance.PlayerExchanceMagnification();
            }
            else if (gameObject.CompareTag("FireBrick"))
            {
                GameManager.instance.BulletToggle(true);
            }
            else if (gameObject.CompareTag("CloseTrigger"))
            {
                //GameManager.instance.CloseTrigger();
            }
            Debug.Log("Player ile çarpıştı");
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("GameOverFrame"))
        {
            // GAMEOVERFRAME İLE ÇARPIŞINCA OLACAK OLANLAR
            Debug.Log("GameOverFrame ile çarpıştı.");
            Destroy(gameObject);
        }
    }
}

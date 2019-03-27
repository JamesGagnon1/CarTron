using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupActions : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    public void HighSpeedStartAction()
    {
        playerController.speed *= 2;
        Debug.Log("Powerup works!!!");
    }

    public void HighSpeedEndAction()
    {
        playerController.speed = playerController.defaultSpeed;
        Debug.Log("Powerup ends!");
    }

    public void HealthIncreseStartAction()
    {
        playerController.health += 10;
        if (playerController.health >= playerController.defaultHealth)
        {
            playerController.health = playerController.defaultHealth;
        }
        Debug.Log("Health: " + playerController.health);
    }
}

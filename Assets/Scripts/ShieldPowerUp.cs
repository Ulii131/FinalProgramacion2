using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public float shieldDuration = 5.0f; // Duración del escudo en segundos
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            Debug.Log("Escudo recogido");

            // Activa el escudo en el jugador
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ActivateShield(shieldDuration); // Llama a ActivateShield en el PlayerController
            }

            // Destruye el objeto del poder
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public int dañoPorImpacto = 1; // Daño causado por cada impacto
    private bool hasCollided = false; // Flag para evitar múltiples colisiones
    public float puntajePorImpacto = 10f; // Puntaje por cada impacto
    public ScorePlayer scorePlayer; // Referencia al componente ScorePlayer

    private void Start()
    {
        if (scorePlayer == null) // Si no está asignado
        {
            scorePlayer = FindObjectOfType<ScorePlayer>(); // Busca el componente en la escena

            if (scorePlayer == null) // Si aún no se encuentra
            {
                Debug.LogError("No se encontró un componente ScorePlayer en la escena. Asegúrate de que exista.");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return; // Evita que se procese más de una colisión
        hasCollided = true; // Marca que ya se procesó una colisión

        EnemyScript enemigo = collision.gameObject.GetComponent<EnemyScript>();

        if (enemigo != null)
        {
            enemigo.RecibirDaño(dañoPorImpacto);
            // Incrementa el puntaje
            if (scorePlayer != null)
            {
                scorePlayer.IncrementScore(puntajePorImpacto);
                Debug.Log("Puntaje incrementado en: " + puntajePorImpacto);
            }
            else
            {
                Debug.LogError("ScorePlayer no asignado al Proyectil.");
            }
        }

        // Destruye el proyectil después de impactar
        Destroy(gameObject);
    }
}

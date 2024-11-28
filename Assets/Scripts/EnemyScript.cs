using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public int vidaInicial = 3; // La cantidad de impactos que puede resistir el enemigo
    private int vidaActual;
    private bool isDestroyed = false; // Flag para evitar múltiples destrucciones

    private void Start()
    {
        vidaActual = vidaInicial;
    }

    public void RecibirDaño(int cantidad)
    {
        if (isDestroyed) return; // Si ya está destruido, no hacemos nada

        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            DestruirEnemigo();
        }
    }

    private void DestruirEnemigo()
    {
        if (isDestroyed) return; // Evita que se ejecute más de una vez
        isDestroyed = true;

        // Llama al método del spawner
        EnemySpawner.Instance.EnemyDestroyed();

        // Destruye el objeto enemigo
        Destroy(gameObject);
    }
}
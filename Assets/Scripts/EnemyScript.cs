using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int vidaInicial = 2; // La cantidad de impactos que puede resistir el enemigo
    private int vidaActual;

    private void Start()
    {
        vidaActual = vidaInicial;
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            DestruirEnemigo();
        }
    }

    private void DestruirEnemigo()
    {
        // Llama al método del spawner para reducir el contador
        FindObjectOfType<EnemySpawner>().EnemyDestroyed();

        // Luego, destruye el objeto enemigo
        Destroy(gameObject);
    }
}

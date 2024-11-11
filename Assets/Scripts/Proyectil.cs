using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public int dañoPorImpacto = 1; // Daño causado por cada impacto

    private void OnCollisionEnter(Collision collision)
    {
        EnemyScript enemigo = collision.gameObject.GetComponent<EnemyScript>();

        if (enemigo != null)
        {
            enemigo.RecibirDaño(dañoPorImpacto);

            // Aquí puedes agregar algún efecto visual o sonoro de impacto si lo deseas

            // Después de impactar, destruye el proyectil
            Destroy(gameObject);
        }
    }
}

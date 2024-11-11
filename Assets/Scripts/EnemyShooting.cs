using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootingPoint; // Punto desde donde el enemigo lanza el proyectil
    public float shootingInterval = 2.0f; // Intervalo entre disparos
    private EnemyMovement enemyMovement; // Referencia al script EnemyMovement
    private bool canShoot = false; // Flag para controlar si el enemigo puede disparar

    private void Start()
    {
        // Intentamos obtener el componente EnemyMovement
        enemyMovement = GetComponent<EnemyMovement>();

        // Comenzamos el disparo solo cuando el enemigo llegue a su destino
        if (enemyMovement != null)
        {
            // Llamamos a la función que verifica cuando el enemigo llega a su destino
            enemyMovement.OnDestinationReached += EnableShooting;
        }
    }

    // Esta función se llama cuando el enemigo llega a su destino
    private void EnableShooting()
    {
        canShoot = true;
        InvokeRepeating("Shoot", 0, shootingInterval); // Comienza el disparo periódico
    }

    private void Shoot()
    {
        if (canShoot && projectilePrefab != null && shootingPoint != null)
        {
            // Instanciamos el proyectil en el punto de disparo
            Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
        }
    }

    private void OnDestroy()
    {
        // Aseguramos que si el enemigo se destruye, dejamos de escuchar el evento
        if (enemyMovement != null)
        {
            enemyMovement.OnDestinationReached -= EnableShooting;
        }
    }
}

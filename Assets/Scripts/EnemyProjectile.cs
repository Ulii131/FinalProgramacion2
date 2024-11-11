using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Velocidad del proyectil
    public int damage = 1; // Daño que causa el proyectil
    public Vector3 direction = Vector3.forward; // Dirección en la que el proyectil se moverá (hacia adelante)

    private void Start()
    {
        // Asegúrate de que la dirección esté en la dirección deseada (eje Z positivo en 3D)
        direction = transform.forward; // O puedes definir otra dirección si lo prefieres
    }

    private void Update()
    {
        // Mueve el proyectil hacia adelante en la dirección especificada
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);

        // Destruye el proyectil si sale de la pantalla
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el jugador tiene el escudo activo
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null && playerController.IsShieldActive())
        {
            // Si el escudo está activo, destruye el proyectil
            Destroy(gameObject);
            Debug.Log("Proyectil destruido por el escudo");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Si el proyectil colisiona con el jugador y el escudo no está activo, causa daño
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Llama al método de daño del jugador
            }
            Destroy(gameObject); // Destruye el proyectil después de colisionar
        }

        // Si el proyectil colisiona con un proyectil del jugador, destrúyelo
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            Destroy(collision.gameObject); // Destruye el proyectil del jugador
            Destroy(gameObject); // Destruye el proyectil enemigo
        }
    }
}

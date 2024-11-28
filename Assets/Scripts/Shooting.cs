using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootForce = 10.0f;
    public float shootInterval = 0.5f;
    public float projectileLifetime = 2.0f; // Tiempo en segundos antes de destruir el proyectil

    private float lastShootTime;
    private Vector3 shootingDirection;

    //Para el audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


    private void Update()
    {
        // Almacena la dirección de disparo
        shootingDirection = transform.forward;

        if (Input.GetButtonDown("Fire1") && Time.time - lastShootTime >= shootInterval)
        {
            Shoot();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    void Shoot()
    {
        lastShootTime = Time.time;

        // Crea una instancia del proyectil
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Aplica una fuerza al proyectil para moverlo en la dirección de disparo
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = shootingDirection * shootForce;

        // Destruye el proyectil después de un cierto tiempo (projectileLifetime)
        Destroy(newProjectile, projectileLifetime);
    }
}

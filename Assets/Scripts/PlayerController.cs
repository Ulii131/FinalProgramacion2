using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float tiltAmount = 30.0f;
    public float smooth = 5.0f;
    public GameObject shieldVisual; // Objeto visual del escudo (asignar en el Inspector)
    public AudioSource audioSource;

    private Transform visual; // Referencia al GameObject "Visual"
    private bool isShieldActive = false;
    private float shieldDuration = 10f;
    private float shieldTimer = 10f;

    private void Start()
    {
        visual = transform.Find("Visual"); // Encuentra el GameObject "Visual" por nombre
        shieldVisual.SetActive(false); // Asegura que el objeto visual del escudo esté desactivado al inicio
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Calcula la inclinación en base al movimiento horizontal
        float tilt = -horizontalInput * tiltAmount;

        // Aplica la inclinación gradualmente a "Visual" para que sea suave
        Quaternion targetRotation = Quaternion.Euler(0, 0, tilt);
        visual.rotation = Quaternion.Slerp(visual.rotation, targetRotation, Time.deltaTime * smooth);

        // Limita la posición del jugador dentro de los límites de la cámara
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;
        float xPosition = Mathf.Clamp(transform.position.x, -camWidth, camWidth);
        float zPosition = Mathf.Clamp(transform.position.z, -camHeight, camHeight);
        transform.position = new Vector3(xPosition, transform.position.y, zPosition);

        if (isShieldActive)
        {
            // Reduzca el temporizador del escudo
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0)
            {
                isShieldActive = false;
                shieldVisual.SetActive(false); // Desactiva el objeto visual del escudo
                Debug.Log("Escudo desactivado"); // Registro en la consola
            }
        }
    }
        public bool IsShieldActive()
    {
        return isShieldActive;
    }

    // Método para activar el escudo
        public void ActivateShield(float duration)
    {
        isShieldActive = true;
        shieldDuration = duration;
        shieldTimer = duration;
        shieldVisual.SetActive(true);// Activa el objeto visual del escudo
        audioSource.Play();
        Debug.Log("Escudo activado"); // Registro en la consola
    }

}

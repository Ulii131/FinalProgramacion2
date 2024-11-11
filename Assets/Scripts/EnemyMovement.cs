using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    private Transform targetDestination;

    // Evento para notificar cuando el enemigo ha llegado a su destino
    public delegate void DestinationReached();
    public event DestinationReached OnDestinationReached;

    // Configura el punto de destino para el enemigo
    public void SetDestination(Transform destination)
    {
        targetDestination = destination;
    }

    private void Update()
    {
        if (targetDestination != null)
        {
            // Calcula la dirección hacia el punto de destino
            Vector3 direction = (targetDestination.position - transform.position).normalized;

            // Mueve al enemigo en la dirección del punto de destino
            transform.Translate(direction * movementSpeed * Time.deltaTime);

            // Si el enemigo llega al punto de destino, dispara el evento
            if (Vector3.Distance(transform.position, targetDestination.position) < 0.1f)
            {
                targetDestination = null;

                // Llamamos al evento para indicar que el destino ha sido alcanzado
                OnDestinationReached?.Invoke();
            }
        }
    }
}

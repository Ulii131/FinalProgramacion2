using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float scrollSpeed = 2.0f;

    private void Update()
    {
        // Mueve el fondo en el eje Z (hacia el jugador)
        float offset = Time.time * scrollSpeed;
        transform.position = new Vector3(transform.position.x, transform.position.y, offset);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float rotateSpeed = 30.0f;

    private void Update()
    {
        // Rota al enemigo alrededor de su eje hacia un lado y otro
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}

// EnemySpawner.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> destinationPoints = new List<Transform>();
    public GameObject enemyPrefab;
    public float spawnInterval = 3.0f;
    public int maxEnemies = 15; // Límite máximo de enemigos
    private int currentEnemies = 0; // Número actual de enemigos
    private int enemigosDestruidos = 0; // Contador global de enemigos destruidos
    public static EnemySpawner Instance; // Singleton para acceso global


    private List<int> assignedDestinations = new List<int>();
    private EnemyFactory enemyFactory; // Instancia de EnemyFactory

    private void Start()
    {
        // Inicializa la fábrica de enemigos con el prefab proporcionado
        enemyFactory = new EnemyFactory(enemyPrefab);

        // Inicia una repetición para generar enemigos periódicamente
        InvokeRepeating("SpawnEnemy", 0, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Verifica que haya destinos disponibles y que el límite de enemigos no haya sido alcanzado
        if (destinationPoints.Count > 0 && currentEnemies < maxEnemies)
        {
            int randomIndex;

            // Encuentra un destino aleatorio que no haya sido asignado previamente
            do
            {
                randomIndex = Random.Range(0, destinationPoints.Count);
            } while (assignedDestinations.Contains(randomIndex) && assignedDestinations.Count < destinationPoints.Count);

            // Si no se encuentra un destino válido, retorna
            if (assignedDestinations.Count == destinationPoints.Count)
            {
                Debug.LogWarning("No hay destinos disponibles para asignar.");
                return;
            }

            // Asegúrate de que el índice sea válido
            if (randomIndex >= 0 && randomIndex < destinationPoints.Count)
            {
                Transform destination = destinationPoints[randomIndex];

                // Usa la fábrica para crear el enemigo
                GameObject enemy = enemyFactory.CreateEnemy(transform.position, Quaternion.identity, destination);

                // Incrementa el número de enemigos generados
                currentEnemies++;

                // Registra la creación del enemigo
                Debug.Log("Enemigo creado: " + enemy.name + " Destino: " + destination.name);

                // Asigna el punto de destino al enemigo
                EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                if (enemyMovement != null)
                {
                    enemyMovement.SetDestination(destination);
                }

                // Agrega el índice del destino asignado a la lista
                assignedDestinations.Add(randomIndex);

                // Elimina el destino de la lista para evitar repeticiones
                destinationPoints.RemoveAt(randomIndex);
            }
        }
        else
        {
            // Registra si no hay enemigos disponibles o si el número máximo ha sido alcanzado
            if (currentEnemies >= maxEnemies)
            {
                Debug.LogWarning("Límite de enemigos alcanzado. No se pueden generar más.");
            }
            else
            {
                Debug.LogWarning("No hay destinos disponibles para asignar.");
            }
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita duplicados en la escena
        }
    }

    // Añade una función para reducir el contador de enemigos cuando un enemigo sea destruido

    public void EnemyDestroyed()
    {
        currentEnemies--; // Reduce el contador de enemigos activos en la escena
        enemigosDestruidos++; // Incrementa el contador de enemigos destruidos

        // Muestra en la consola cuántos enemigos han sido destruidos
        Debug.Log("Enemigo destruido. Enemigos restantes: " + currentEnemies + ". Total destruidos: " + enemigosDestruidos);

        // Verifica si todos los enemigos han sido derrotados
        if (enemigosDestruidos >= maxEnemies)
        {
            Debug.Log("¡Todos los enemigos han sido derrotados! Cargando escena de victoria...");
            SceneManager.LoadScene("VictoryScene"); // Cambia a la escena de victoria
        }
    }

}

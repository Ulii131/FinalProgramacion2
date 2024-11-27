using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importa el namespace para manejar las escenas

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Salud máxima del jugador
    private int currentHealth;

    public delegate void OnPlayerDeath();
    public event OnPlayerDeath playerDeath;

    private bool isShieldActive = false; // Variable para verificar si el escudo está activo

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Método para activar el escudo
    public void ActivateShield()
    {
        isShieldActive = true;
    }

    // Método para desactivar el escudo
    public void DeactivateShield()
    {
        isShieldActive = false;
    }

    // Método que retorna el estado del escudo
    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    public void TakeDamage(int damage)
    {
        if (isShieldActive)
        {
            Debug.Log("Escudo bloqueando el daño.");
            return; // No se aplica daño si el escudo está activo
        }

        currentHealth -= damage;
        Debug.Log("Jugador ha recibido daño. Daño: " + damage + " | Salud restante: " + currentHealth); // Debug que muestra el daño recibido y la salud restante

        if (currentHealth <= 0)
        {
            // El jugador ha muerto
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Jugador muerto");
        
        // Llamamos al evento de muerte del jugador
        if (playerDeath != null)
        {
            playerDeath.Invoke();
        }

        // Aquí podrías agregar animaciones de muerte, efectos, etc.
        // Por ejemplo, podemos desactivar al jugador temporalmente
        gameObject.SetActive(false);

        SceneManager.LoadScene("DefeatScene");
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

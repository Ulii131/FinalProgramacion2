using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperShooting : MonoBehaviour
{
    public GameObject superProjectilePrefab;
    public float shootForce = 10.0f;
    public float chargeTime = 2.0f;
    public int numberOfProjectiles = 3;
    public float timeBetweenShots = 0.5f;

    private float chargeTimer;
    private bool isCharging;
    private Vector3 shootingDirection;

    private float cooldownTimer = 20.0f;
    private bool isOnCooldown = false;

    private Text cooldownText;

    //Para el audio
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        cooldownText = GameObject.Find("CooldownText").GetComponent<Text>();

        UpdateCooldownText();
    }

    private void Update()
    {
        shootingDirection = transform.forward;

        if (!isOnCooldown)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                StartCharging();
            }

            if (Input.GetButton("Fire2"))
            {
                ChargeSuperShot();
            }

            if (Input.GetButtonUp("Fire2"))
            {
                if (isCharging)
                {
                    StartCoroutine(ShootMultipleSuperShots());
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    StartCooldown();
                }
                StopCharging();
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0)
            {
                cooldownTimer = 0;
                isOnCooldown = false;
                UpdateCooldownText("Ráfaga Cargada");
            }
            else
            {
                UpdateCooldownText();
            }
        }
    }

    void StartCharging()
    {
        chargeTimer = 0;
        isCharging = true;
    }

    void ChargeSuperShot()
    {
        chargeTimer += Time.deltaTime;
    }

    void StopCharging()
    {
        isCharging = false;
    }

    IEnumerator ShootMultipleSuperShots()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Crea y dispara el super proyectil
            GameObject superProjectile = Instantiate(superProjectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = superProjectile.GetComponent<Rigidbody>();
            rb.velocity = shootingDirection * shootForce;

            // Destruye el super proyectil después de 2 segundos
            Destroy(superProjectile, 2.0f);

            // Espera el tiempo entre disparos
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    void StartCooldown()
    {
        cooldownTimer = 20.0f;
        isOnCooldown = true;
    }

    void UpdateCooldownText(string textToShow = "")
    {
        if (string.IsNullOrEmpty(textToShow))
        {
            cooldownText.text = "Recargando rafaga: " + cooldownTimer.ToString("F1") + "s";

        }
        else
        {
            cooldownText.text = textToShow;
        }
    }
}

using System.Collections;
using UnityEngine;

public class BarrelHealth : MonoBehaviour
{
    public int vida = 100; // Set initial health for the barrel
    private bool isAlive = true;

    public void BajarVida(int dañoRecibido)
    {
        if (!isAlive) return;

        vida -= dañoRecibido;
        Debug.Log("Barrel Health: " + vida); // Log current health

        if (vida <= 0)
        {
            MatarBarrel();
        }
    }

    void MatarBarrel()
    {
        if (!isAlive) return;
        isAlive = false; // Mark barrel as dead

        // Deactivate the barrel GameObject
        gameObject.SetActive(false); // Disable the barrel

        // Start the respawn coroutine
        StartCoroutine(RespawnBarrel());
    }

    private IEnumerator RespawnBarrel()
    {
        yield return new WaitForSeconds(30f); // Wait for 30 seconds

        // Reset the barrel's health
        vida = 100;
        isAlive = true; // Mark the barrel as alive again

        // Reactivate the barrel in the scene
        gameObject.SetActive(true);
    }
}

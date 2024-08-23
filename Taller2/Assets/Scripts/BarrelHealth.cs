using UnityEngine;

public class BarrelHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the barrel
    private int currentHealth;  // Current health of the barrel

    void Start()
    {
        // Initialize the barrel's health to the maximum value
        currentHealth = maxHealth;
    }

    // Method to decrease the barrel's health
    public void BajarVida(int damage)
    {
        // Decrease the barrel's health by the damage amount
        currentHealth -= damage;

        // Check if the barrel's health has reached zero
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            FindObjectOfType<GoldenBarrel>().OpenStore(); // Open the store when the barrel is destroyed
            gameObject.SetActive(false); // Deactivate the barrel
        }
    }
}

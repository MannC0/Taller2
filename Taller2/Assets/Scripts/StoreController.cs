using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldenBarrel : MonoBehaviour
{
    public int vida = 100; // Set initial health for the barrel
    public GameObject storeUI; // Reference to the store UI to display
    public Button healButton; // Reference to the healing button
    public int healAmount = 50; // Amount of health to heal
    public int itemCost = 100; // Cost of the healing item
    private bool isAlive = true;
    public TMP_Text messageText; // Reference to a TextMeshPro element for messages

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

        // Open the store UI here
        storeUI.SetActive(true); // Activate the store UI
        PauseGame(); // Pause the game

        // Assign the heal action to the button
        healButton.onClick.AddListener(() =>
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>(); // Find the player object
            if (player != null) // Check if the player object is found
            {
                if (player.dinero >= itemCost) // Check if the player has enough money
                {
                    player.HealPlayer(healAmount); // Heal the player for the specified amount
                    player.AumentarDinero(-itemCost); // Deduct the item cost from the player's money
                    Debug.Log("Player healed for " + healAmount); // Log healing action
                    messageText.text = "You bought a healing item for " + itemCost + "!"; // Display purchase message
                }
                else
                {
                    Debug.Log("Not enough money to buy the item."); // Log insufficient funds
                    messageText.text = "Not enough money!"; // Display insufficient funds message
                }
            }
            else
            {
                Debug.LogError("PlayerMovement not found!");
            }

            StartCoroutine(ClearMessage(3f)); // Clear the message after 3 seconds
            CloseStore(); // Close the store after the attempt
        });
    }

    private IEnumerator ClearMessage(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
        messageText.text = ""; // Clear the message text
    }


    public void CloseStore()
    {
        storeUI.SetActive(false); // Deactivate the store UI
        ResumeGame(); // Resume the game
        StartCoroutine(Respawn()); // Optional respawn logic
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Stop the game
    }

    private void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(30f); // Wait 30 seconds
        vida = 5; // Reset health
        isAlive = true; // Mark as alive again
        gameObject.SetActive(true); // Optionally, reactivate the barrel in the scene
    }
}

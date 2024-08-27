using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldenBarrel : MonoBehaviour
{
    public GameObject storeUI;
    public Button healButton;
    public int healAmount = 50;
    public int itemCost = 100;
    public TMP_Text messageText;

    private bool isPurchaseInProgress = false; // Safeguard flag

    void Start()
    {
        // Set the store UI inactive initially
        storeUI.SetActive(false);

        // Add listener for the heal button
        healButton.onClick.AddListener(HealPlayer);
    }

    public void OpenStore()
    {
        storeUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    private void HealPlayer()
    {
        if (isPurchaseInProgress) return; // Prevent multiple calls
        isPurchaseInProgress = true;

        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            // Debug the player's money before checking the condition
            Debug.Log("Player's money before transaction: " + player.dinero);

            // Reset message text before proceeding
            messageText.text = "";

            // Check if the player has enough money before performing the transaction
            if (player.dinero >= itemCost)
            {
                player.AumentarDinero(-itemCost);  // Deduct the money
                player.HealPlayer(healAmount);  // Heal the player

                Debug.Log("Purchase successful! Money after purchase: " + player.dinero);
                messageText.text = "Curación comprada";  // Display success message
            }
            else
            {
                Debug.Log("Not enough money to buy the item. Current money: " + player.dinero);
                messageText.text = "Insuficientes Fondos!";  // Display insufficient funds message
            }
        }
        else
        {
            Debug.LogError("PlayerMovement not found!");
        }

        StartCoroutine(ClearMessageAndCloseStore(3f));
    }

    private IEnumerator ClearMessageAndCloseStore(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Use WaitForSecondsRealtime to respect the time scale
        messageText.text = "";
        CloseStore(); // Close the store after the message is cleared
        isPurchaseInProgress = false; // Reset safeguard flag after the purchase process is complete
    }

    public void CloseStore()
    {
        storeUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}

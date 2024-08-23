using System.Collections; // Add this line to use IEnumerator
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldenBarrel : MonoBehaviour
{
    public GameObject storeUI; // Reference to the store UI to display
    public Button healButton; // Reference to the healing button
    public int healAmount = 50; // Amount of health to heal
    public int itemCost = 100; // Cost of the healing item
    public TMP_Text messageText; // Reference to a TextMeshPro element for messages

    private BarrelHealth barrelHealth;

    void Start()
    {
        barrelHealth = GetComponent<BarrelHealth>(); // Get reference to the BarrelHealth script
    }

    public void OpenStore()
    {
        storeUI.SetActive(true); // Activate the store UI
        // Assign the heal action to the button
        healButton.onClick.AddListener(HealPlayer);
    }

    private void HealPlayer()
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
    }

    private IEnumerator ClearMessage(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        messageText.text = ""; // Clear the message text
    }

    public void CloseStore()
    {
        storeUI.SetActive(false); // Deactivate the store UI
        // Other logic for closing the store can go here
    }
}

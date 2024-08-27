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
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            // Check if the player has enough money before performing the transaction
            if (player.dinero < itemCost)
            {
                // Display "Insuficientes Fondos" message
                messageText.text = "Insuficientes Fondos!";
                Debug.Log("Not enough money to buy the item.");
            }
            else
            {
                // Heal the player and deduct the item cost
                player.HealPlayer(healAmount); // Heal the player
                player.AumentarDinero(-itemCost); // Deduct money
                Debug.Log("Player healed for " + healAmount);
                messageText.text = "Compra exitosa por " + itemCost + "!";

                // Update the displayed money from within the PlayerMovement class
                player.cantidadDinero.GetComponent<TMP_Text>().text = player.dinero.ToString(); // Ensure cantidadDinero is correctly updated
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
    }

    public void CloseStore()
    {
        storeUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}

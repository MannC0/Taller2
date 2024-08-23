using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject efectoExplosion;
    public int dañoBala;

    // Array of target tags
    public string[] objetivos; // You can set this in the Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);

        foreach (string objetivo in objetivos)
        {
            if (collision.gameObject.CompareTag(objetivo))
            {
                // Call the respective method based on the target type
                if (collision.gameObject.CompareTag("Enemigo"))
                {
                    collision.gameObject.GetComponent<Enemigo>().BajarVida(dañoBala);
                }
                else if (collision.gameObject.CompareTag("Player"))
                {
                    collision.gameObject.GetComponent<PlayerMovement>().BajarVida(dañoBala);
                }
                else if (collision.gameObject.CompareTag("GoldenBarrel")) // Change to "Barrel"
                {
                    Debug.Log("Hit Barrel!");
                    collision.gameObject.GetComponent<BarrelHealth>().BajarVida(dañoBala); // Call BajarVida from BarrelHealth
                }
            }
        }

        // Create explosion effect and destroy the bullet
        GameObject efecto = Instantiate(efectoExplosion, transform.position, Quaternion.identity);
        Destroy(efecto, 1f);
        Destroy(gameObject);
    }
}

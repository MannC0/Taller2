using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPerseguido : MonoBehaviour
{
    public GameObject efectoExplosion;
    public string objetivo;
    public float velocidad = 10f;
    public float tiempoVida = 10f;
    public int daño = 1;
    private Rigidbody2D rigidbody;
    private GameObject jugador;

    private void Start()
    {
        // Try to find the player by tag
        jugador = GameObject.FindWithTag("Player");
        if (jugador == null)
        {
            Debug.LogError("Player object not found. Make sure the player GameObject has the 'Player' tag.");
        }

        // Get the Rigidbody2D component
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
        {
            Debug.LogError("Rigidbody2D component not found. Make sure the bullet GameObject has a Rigidbody2D component.");
        }

        Destroy(gameObject, tiempoVida);
    }

    private void FixedUpdate()
    {
        if (jugador != null)
        {
            // Calculate direction to the player
            Vector2 direccion = (jugador.transform.position - transform.position).normalized;
            // Move the bullet towards the player
            rigidbody.MovePosition(rigidbody.position + direccion * velocidad * Time.fixedDeltaTime);
            // Rotate the bullet to face the player
            float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            rigidbody.rotation = angle;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == objetivo)
        {
            if (collision.gameObject.tag == "Enemigo")
            {
                collision.gameObject.GetComponent<Enemigo>().BajarVida(daño);
            }
            else if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerMovement>().BajarVida(daño);
            }

            GameObject efecto = Instantiate(efectoExplosion, transform.position, Quaternion.identity);
            Destroy(efecto, 1f);
            Destroy(gameObject);
        }
        else
        {
            // Optional: Destroy the bullet if it hits something else
            GameObject efecto = Instantiate(efectoExplosion, transform.position, Quaternion.identity);
            Destroy(efecto, 1f);
            Destroy(gameObject);
        }
    }
}

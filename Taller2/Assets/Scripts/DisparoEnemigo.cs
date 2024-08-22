using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public Transform posicionGenerar;
    public GameObject bala;
    private GameObject jugador;

    public int da�oBala;
    public float velocidadBala = 10f;
    public float tiempoEntreDisparos = 1f;
    public int cantidadRafaga = 5;
    public float tiempoRafaga = 0.2f;
    private bool atacando = false;

    private void Start()
    {
        jugador = GameObject.FindWithTag("Player");
        if (jugador == null)
        {
            Debug.LogError("Player object not found. Make sure the player GameObject has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (!atacando && (transform.position - jugador.transform.position).magnitude < 7)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        StartCoroutine(Disparo());
    }

    private IEnumerator Disparo()
    {
        atacando = true;

        for (int i = 0; i < cantidadRafaga; i++)
        {
            if (jugador == null)
            {
                Debug.LogError("jugador es null");
                yield break; // Salir de la coroutine si jugador es null
            }

            if (posicionGenerar == null)
            {
                Debug.LogError("posicionGenerar es null");
                yield break; // Salir de la coroutine si posicionGenerar es null
            }

            Vector2 direccionDisparo = jugador.transform.position - transform.position;

            GameObject bullet = Instantiate(bala, posicionGenerar.position, posicionGenerar.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

            if (rigidbody == null)
            {
                Debug.LogError("Rigidbody2D no encontrado en la bala");
                yield break; // Salir de la coroutine si Rigidbody2D no se encuentra
            }

            bullet.transform.right = direccionDisparo;
            rigidbody.AddForce(bullet.transform.right * velocidadBala, ForceMode2D.Impulse);
            Destroy(bullet, 5f);

            // Check if the bullet is of type DisparoPerseguido and set the damage value
            if (bullet.CompareTag("DisparoPerseguido")) // Ensure your bullet prefab has this tag
            {
                bullet.GetComponent<DisparoPerseguido>().da�oBala = da�oBala; // Set the damage value
            }
            else
            {
                bullet.GetComponent<Bala>().da�oBala = da�oBala; // Set the damage value for regular bullets
            }

            yield return new WaitForSeconds(tiempoRafaga);
        }

        yield return new WaitForSeconds(tiempoEntreDisparos);
        atacando = false;
    }
}

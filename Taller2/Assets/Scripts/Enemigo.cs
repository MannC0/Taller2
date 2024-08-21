using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private GameObject jugador;
    public Rigidbody2D rigidbody;
    public GameObject[] experienciaPrefabs;
    public GameObject[] powerUpPrefabs;
    public GameObject[] dineroPrefabs;
    public float velocidadMovimiento = 5f;
    public int vida = 200;
    public bool jefe;
    public bool useAnimator;
    private Vector2 direccion;
    private Animator animacion;
    private GameManager victoryManager;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        if (useAnimator)
        {
            animacion = GetComponent<Animator>();
        }
        victoryManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (jugador != null)
        {
            int ajuste = 0;
            direccion = (jugador.transform.position - transform.position).normalized;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

            if (jefe)
            {
                ajuste = 90;
            }

            rigidbody.rotation = angulo + ajuste;

            rigidbody.velocity = direccion * velocidadMovimiento;
        }
    }

    public void BajarVida(int dañoRecibido)
    {
        vida = vida - dañoRecibido;
        if (vida <= 0)
        {
            MatarEnemigo();
        }

        if (useAnimator)
        {
            // Set the direction parameters for the blend tree
            animacion.SetFloat("DamageDirectionX", direccion.x);
            animacion.SetFloat("DamageDirectionY", direccion.y);

            // Trigger the damage animation
            animacion.SetTrigger("TakeDamage");
        }
    }

    void MatarEnemigo()
    {
        bool soltarExperiencia = Random.value > 0.5f;

        if (soltarExperiencia)
        {
            int numeroAleatorioExperiencia = Random.Range(0, 100);
            int numeroGenerar = 0;
            if (numeroAleatorioExperiencia < 65)
            {
                numeroGenerar = 0;
            }
            else if (numeroAleatorioExperiencia < 95)
            {
                numeroGenerar = 1;
            }
            else
            {
                numeroGenerar = 2;
            }
            GameObject experiencia = Instantiate(experienciaPrefabs[numeroGenerar], transform.position, Quaternion.identity);
            Destroy(experiencia, 5f);
        }
        else
        {
            int numeroAleatorioDinero = Random.Range(0, 100);
            int numeroGenerar = 0;
            if (numeroAleatorioDinero < 65)
            {
                numeroGenerar = 0;
            }
            else if (numeroAleatorioDinero < 95)
            {
                numeroGenerar = 1;
            }
            else
            {
                numeroGenerar = 2;
            }
            GameObject dinero = Instantiate(dineroPrefabs[numeroGenerar], transform.position, Quaternion.identity);
            Destroy(dinero, 5f);
        }

        // Añadir el sistema de aparición del power-up
        if (Random.value < 0.3f) // 30% de probabilidad
        {
            GameObject powerUp = Instantiate(powerUpPrefabs[0], transform.position, Quaternion.identity); // Asegúrate de que powerUpPrefabs[0] sea tu prefab de poder
            Destroy(powerUp, 5f);
        }

        if (jefe && victoryManager != null)
        {
            victoryManager.EndGame();
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private GameObject jugador;
    public Rigidbody2D rigidbody;
    // El [] es para que sea un array de cosas
    public GameObject[] rupiasPrefabs;
    public GameObject[] powerUpPrefabs;
    public float velocidadMovimiento;
    public int vida = 5;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, jugador.transform.position, velocidadMovimiento * Time.deltaTime);
    }

    public void BajarVida()
    {
        vida = vida - 1;
        if (vida == 0)
        {
            MatarEnemigo();
        }

    }

    void MatarEnemigo()
    {
        // Esto genera otro numero de 0 - 100
        int numeroAleatorioEfecto = Random.Range(0, 100);

        // Esto indica que si el numero que se genera es mayor a 40, se corre el codigo de las rupias.
        if (numeroAleatorioEfecto > 40)
        {
            // Se genera un numero del 0 - 100
            int numeroAleatorioRupia = Random.Range(0, 100);
            int numeroGenerar = 0;

            // Si es menor a 65 sale una rupia verde
            if (numeroAleatorioRupia < 65)
            {
                numeroGenerar = 0;
            }
            else
            {
                // Si es menor a 95 sale Azul
                if (numeroAleatorioRupia < 95)
                {
                    numeroGenerar = 1;
                }
                // Si es mayor a 95 sale gris
                else
                {
                    numeroGenerar = 2;
                }
            }
            // Esto genera el numero que corresponde a cada rupia (1,2,3), estas posiciones corresponden a las que tiene Unity en el inspector mode, estan en el mismo orden que fueron puestas ahi
            GameObject rupia = Instantiate(rupiasPrefabs[numeroGenerar], transform.position, Quaternion.identity);
            Destroy(rupia, 5f);
        }
        else
        {
            // En este caso dice [0] porque solo hay 1 item de PowerUp.
            GameObject PowerUp = Instantiate(powerUpPrefabs[0], transform.position, Quaternion.identity);
            Destroy(PowerUp, 5f);
        }
        Destroy(gameObject);
    }
}

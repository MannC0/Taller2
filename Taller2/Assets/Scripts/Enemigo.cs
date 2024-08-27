using System.Collections;
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
    private bool isDead = false;  // New flag to track if the enemy is dead

    bool isFreeze = false;

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
        if (!isDead && jugador != null && !isFreeze)
        {
            int ajuste = 0;
            direccion = (jugador.transform.position - transform.position).normalized;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

            // Only set animation parameters if useAnimator is true
            if (useAnimator)
            {
                animacion.SetFloat("Horizontal", 1);
            }

            /*if (jefe)
            {
                ajuste = 180;
            }

            rigidbody.rotation = angulo + ajuste; */
            rigidbody.velocity = direccion * velocidadMovimiento;
        }

        if (isDead)
        {
            transform.position = transform.position;
        }
    }


    public void BajarVida(int dañoRecibido)
    {
        if (isDead) return;  // Prevents further actions if the enemy is dead

        isFreeze = true;   
        vida -= dañoRecibido;
        if (vida <= 0)
        {
            if (useAnimator)
            {
                animacion.SetTrigger("TriggerDeath");
                StartCoroutine(WaitForDeathAnimation());
                isDead = true;  // Set the flag to prevent actions during the death animation
            }
            else
            {
                MatarEnemigo();
            }
        }
        else
        {
            if (useAnimator)
            {
                animacion.SetFloat("DamageDirectionX", direccion.x);
                animacion.SetFloat("DamageDirectionY", direccion.y);
                animacion.SetTrigger("TakeDamage");
            }

            Invoke("ReturnToMove", .3f);
        }
    }

    void ReturnToMove()
    {
        isFreeze = false;
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Wait until the current animation state ends
        yield return new WaitForSeconds(animacion.GetCurrentAnimatorStateInfo(0).length);
        MatarEnemigo();
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

        if (Random.value < 0.3f)
        {
            GameObject powerUp = Instantiate(powerUpPrefabs[0], transform.position, Quaternion.identity);
            Destroy(powerUp, 5f);
        }

        if (jefe && victoryManager != null)
        {
            victoryManager.EndGame();
        }

        Destroy(gameObject);
    }
}

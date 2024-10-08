using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public int da�o = 100;
    public float defensa = 1;
    public int regeneracionVida = 0;
    public float velocidadMovimiento = 1f;
    public int vida = 1000;
    private int vidaMax;
    public float area = 1;
    public int nivel = 1;
    public int siguienteNivel = 100;
    public int incrementoNivel = 2;
    private int experienciaActual = 0;
    public Slider experienceBar;
    public Image cartaSeleccionadaImage; // Referencia a la imagen de la carta seleccionada
    public Sprite[] spritesCartas; // Array para almacenar los sprites de las cartas
    private float tiempoDesdeRegeneracion = 0f;

    public GameObject[] cartas;
    public GameObject cantidadDinero;
    public int dinero = 0;
    private float tiempoEfectoPowerUp = 0;
    private float tiempoTranscurrido = 0;
    private float tiempoTranscurridoRegeneracion = 0;
    private bool powerUpActivo = false;
    public Camera camara;
    public Rigidbody2D rigidbody;
    private Animator animator;
    private DeathScreenController deathScreenController;

    public TMP_Text currentHPText;
    public TMP_Text maxHPText;

    Vector2 movimiento;
    Vector2 posicionMouse;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cantidadDinero.GetComponent<TMP_Text>().text = dinero.ToString();
        deathScreenController = FindObjectOfType<DeathScreenController>();

        gameObject.GetComponent<Disparar>().area = area;
        gameObject.GetComponent<Disparar>().da�oBala = da�o;
        vidaMax = vida;
        experienceBar.maxValue = siguienteNivel;
        experienceBar.value = experienciaActual;
        cantidadDinero.GetComponent<TMP_Text>().text = dinero.ToString(); // Initialize money display

        UpdateHealthUI();
    }

    void Update()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movimiento.x);
        animator.SetFloat("Vertical", movimiento.y);
        animator.SetFloat("Velocidad", movimiento.sqrMagnitude);

        posicionMouse = camara.ScreenToWorldPoint(Input.mousePosition);

        // Handle power-up effects timing
        if (powerUpActivo == true)
        {
            tiempoTranscurrido += Time.deltaTime;
            if (tiempoTranscurrido > tiempoEfectoPowerUp)
            {
                powerUpActivo = false;
                tiempoTranscurrido = 0;
                tiempoEfectoPowerUp = 0;
                gameObject.GetComponent<Disparar>().ratioDeDisparo = gameObject.GetComponent<Disparar>().ratioDeDisparo * 2;
            }
        }

        // Handle health regeneration
        if (regeneracionVida > 0)
        {
            if (vida < vidaMax)
            {
                tiempoDesdeRegeneracion += Time.deltaTime;
                if (tiempoDesdeRegeneracion >= 3) // Check if 3 seconds have passed
                {
                    vida += 5 * regeneracionVida; // Heal based on the number of regeneration buffs
                    if (vida > vidaMax) // Ensure health does not exceed maximum
                    {
                        vida = vidaMax;
                    }
                    UpdateHealthUI();
                    tiempoDesdeRegeneracion = 0; // Reset the timer
                }
            }
        }

        UpdateHealthUI();
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movimiento.normalized * velocidadMovimiento * 5 * Time.fixedDeltaTime);

        Vector2 direccion = posicionMouse - rigidbody.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) *Mathf.Rad2Deg;
        rigidbody.rotation = angulo;
    }

    public void BajarVida(float da�oRecibido)
    {
        da�oRecibido = da�oRecibido / defensa;
        vida = vida - (int)da�oRecibido;
        UpdateHealthUI();
        if (vida <= 0)
        {
            Muerte();
        }
    }

    public void AumentarExperiencia(int experienciaRecibida)
    {
        experienciaActual += experienciaRecibida;
        experienceBar.value = experienciaActual;
        if (experienciaActual > siguienteNivel)
        {
            Time.timeScale = 0;
            nivel++;
            experienciaActual = 0;
            siguienteNivel = siguienteNivel * incrementoNivel;
            experienceBar.maxValue = siguienteNivel;
            int randomAnterior = -1;
            int randomTrasAnterior = -1;
            for (int i = 0; i < 3; i++)
            {
                int random = randomAnterior;
                while (random == randomAnterior || random == randomTrasAnterior)
                {
                    random = Random.Range(0, 5); //Esto son las cartas que hay
                }
                if (i == 0)
                {
                    randomAnterior = random;
                }
                else
                {
                    randomTrasAnterior = randomAnterior;
                    randomAnterior = random;
                }
                cartas[i].SetActive(true);
                cartas[i].GetComponent<CargarCarta>().CargarDatos(random);
            }
        }
    }

    public void HealPlayer(int healAmount)
    {
        if (vida < vidaMax) // Check if player can be healed
        {
            vida += healAmount; // Heal the player
            if (vida > vidaMax) // Ensure health does not exceed maximum
            {
                vida = vidaMax;
            }
            UpdateHealthUI(); // Update the UI to reflect the new health
        }
    }
    public void AumentarDinero(int ganancia)
    {
        dinero += ganancia;
        cantidadDinero.GetComponent<TMP_Text>().text = dinero.ToString(); // Update UI text
    }

    public void ElegirEfecto(int numCarta)
    {
        string efecto = cartas[numCarta].GetComponent<CargarCarta>().efecto;
        //cartaSeleccionadaImage.sprite = spritesCartas[numCarta]; // Actualiza la imagen de la carta seleccionada
        Time.timeScale = 1;
        for (int i = 0; i < 3; i++)
        {
            cartas[i].SetActive(false);
        }
        switch (efecto)
        {
            case "Area":
                AumentarArea();
                break;
            case "Vida":
                AumentarVida();
                break;
            case "Ataque":
                Aumentarda�o();
                break;
            case "Velocidad":
                AumentarVelocidad();
                break;
            case "Regeneracion":
                AumentarRegeneracion();
                break;
            case "Defensa":
                AumentarDefensa();
                break;
        }
    }

    public void AumentarArea()
    {
        area = area * 1.5f;
        gameObject.GetComponent<Disparar>().area = area;
        Debug.Log("Area");
    }
    public void AumentarVida()
    {
        int incrementoVida = (int)(vidaMax * 0.5f);
        vidaMax = vidaMax + incrementoVida;
        vida = vida + incrementoVida;
        UpdateHealthUI();
        Debug.Log("Vida");
    }

    private void Aumentarda�o()
    {
        da�o = (int)(da�o * 1.2f);
        gameObject.GetComponent<Disparar>().da�oBala = da�o;
        Debug.Log("Ataque");
    }
    public void AumentarVelocidad()
    {
        velocidadMovimiento = velocidadMovimiento * 1.2f;
        Debug.Log("Velocidad");
    }

    public void AumentarRegeneracion()
    {
        regeneracionVida = regeneracionVida + 1;
        Debug.Log("Regeneracion");
    }
    public void AumentarDefensa()
    {
        defensa = defensa * 2;
        Debug.Log("Defensa");
    }

    void Muerte()
    {
        deathScreenController.ShowDeathScreen();
    }

    public void PowerUp(int numPowerUp)
    {
        if (numPowerUp == 1)
        {
            if (powerUpActivo == false)
            {
                powerUpActivo = true;
                tiempoEfectoPowerUp = 10;
                gameObject.GetComponent<Disparar>().ratioDeDisparo = gameObject.GetComponent<Disparar>().ratioDeDisparo / 2;
            }
            else 
            {
                tiempoTranscurrido = 0;
            }
        }
    }
    private void UpdateHealthUI()
    {
        currentHPText.text = vida.ToString();
        maxHPText.text = vidaMax.ToString();
    }
}

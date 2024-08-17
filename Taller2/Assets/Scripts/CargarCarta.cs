using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargarCarta : MonoBehaviour
{
    public Cartas[] cartas;
    public GameObject textoCarta;
    public Image imagenCarta;
    public string efecto;
    

    public void CargarDatos(int random)
    {

        textoCarta.GetComponent<TMP_Text>().text = cartas[random].propiedades.textoEfecto.ToString();
        imagenCarta.sprite = cartas[random].propiedades.imagenEfecto;
        efecto = cartas[random].propiedades.efecto;
    }
}

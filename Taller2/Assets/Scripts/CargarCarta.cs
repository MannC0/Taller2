using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargarCarta : MonoBehaviour
{
    public Cartas[] cartas;
    public Image imagen;
    public GameObject texto;
    public string efecto;
    
    void Start()
    {
        int random = Random.Range(0, cartas.Length);
        imagen.sprite = cartas[random].propiedades.sprite;
        texto.GetComponent<TMP_Text>().text = cartas[random].propiedades.descripcion.ToString();
        efecto = cartas[random].propiedades.efecto; 
    }
}

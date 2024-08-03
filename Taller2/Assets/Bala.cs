using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject efectoExplosion;
    public string objetivo;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == objetivo)
        {
            if (collision.gameObject.tag == "Enemigo")
            {
                collision.gameObject.GetComponent<Enemigo>().BajarVida();
            }
            else 
            {
                if (collision.gameObject.tag == "Player")
                {
                    collision.gameObject.GetComponent<PlayerMovement>().BajarVida();
                }
            }
        }
        else 
        {
            
        }

        GameObject efecto = Instantiate(efectoExplosion, transform.position, Quaternion.identity);
        Destroy(efecto, 1f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Detecta cuando el player colisiona con la moneda y "lanza" un sonido.
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioClip coinCollected;

    private void OnTriggerEnter(Collider other) //Cuando algo colisione con la moneda
    {
        CoinCollectible coinCollectible = other.GetComponent<CoinCollectible>(); //Chequea que la colision sea con el player.

        if (coinCollectible != null ) //Si no es nulo, entonces esta colisionando con el player.
        {
            coinCollectible.CoinsCollected();
            gameObject.SetActive(false); //Lo desactiva una vez que colisione.
            SFX.clip = coinCollected;
            SFX.Play();
        }
    }
}

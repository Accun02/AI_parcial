using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //detecta cuando el player colisiona con la moneda
    [SerializeField] private AudioSource SFX;
    [SerializeField] private AudioClip coinCollected;

    private void OnTriggerEnter(Collider other) //cuando algo colisione con la moneda
    {
        CoinCollectible coinCollectible = other.GetComponent<CoinCollectible>(); //chequea que la colision sea con el player

        if (coinCollectible != null ) //si no es nulo, entonces esta colisionando con el player
        {
            coinCollectible.CoinsCollected();
            gameObject.SetActive(false); //lo desactiva una vez que colisione
            SFX.clip = coinCollected;
            SFX.Play();
        }
    }
}

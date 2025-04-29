using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CoinCollectible : MonoBehaviour
{
    public int numberOfCoins {  get; private set; } //cualquier script puede leer el valor
                                                    //pero solo este setea el valor
    
    public UnityEvent<CoinCollectible> OnCoinsCollected;
    public void CoinsCollected()
    {
        numberOfCoins++; //aumenta el numero de monedas cuando son agarradas
        OnCoinsCollected.Invoke(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    public int numberOfCoins {  get; private set; } //cualquier script puede leer el valor
                                                     //pero solo este setea el valor

    public void CoinsCollected()
    {
        numberOfCoins++; //aumenta el numero de monedas cuando son agarradas
    }

}

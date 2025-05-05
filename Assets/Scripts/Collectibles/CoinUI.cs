using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    private CoinCollectible coinCollectible;
    private TextMeshProUGUI coinText;
    private int totalCoins = 7;


    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
 
    }

    public void UpdateCoinText(CoinCollectible coinCollectible)
    {
        coinText.text = coinCollectible.numberOfCoins.ToString();
        coinText.text = coinCollectible.numberOfCoins + "/" + totalCoins; //muestra en el canvas monedas agarradas / total
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public GameObject Count;
    public TextMeshProUGUI CoinText;
    private float PlayerCoin;

    private void Update()
    {
        PlayerCoin = Count.transform.position.x;

        CoinText.text = "" + PlayerCoin;
    }
}

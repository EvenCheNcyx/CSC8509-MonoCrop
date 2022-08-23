using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    public GameObject GameEnd;
    public GameObject Count;
    public TextMeshProUGUI ScoreText;

    public void Update()
    {
        if (GameEnd.activeSelf)
        {
            Time.timeScale = 0;
            float Score = Count.transform.position.y;
            ScoreText.text = "" + Score;
        }
    }
}

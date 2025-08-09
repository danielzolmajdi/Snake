using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int scoreNum = 1;

    public TextMeshProUGUI lostText;
    public GameObject pannel;
    public TextMeshProUGUI textForScore;

    public void AddScore()
    {
        scoreNum++;
        scoreText.text = scoreNum.ToString();
    }

    public void LostUI()
    {
        lostText.text = lostText.text + scoreNum.ToString();

        scoreText.gameObject.SetActive(false);
        textForScore.gameObject.SetActive(false);
        lostText.gameObject.SetActive(true);
        pannel.gameObject.SetActive(true);
    }
}

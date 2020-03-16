using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{

    public Text scoreMessage;

    private void Start()
    {
        UpdateText();
    }

    /*
     * Public function that takes score from other game objects and adds it to total  
     */
    public void AddScore(int amt)
    {
        PlayerPrefs.SetInt("CurrentScore", PlayerPrefs.GetInt("CurrentScore") + amt);

        UpdateText();
    }

    /*
     * Public function that takes score from other game objects and subtracts it from the total  
     */
    public void SubtractScore(int amt)
    {
        if (PlayerPrefs.GetInt("CurrentScore") - amt < 0)
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
        }

        else
        {
            PlayerPrefs.SetInt("CurrentScore", PlayerPrefs.GetInt("CurrentScore") - amt);
        }

        UpdateText();
    }

    /*
     * Dynamically updates text
     */
    private void UpdateText()
    {
        scoreMessage.text = "Score: " + PlayerPrefs.GetInt("CurrentScore").ToString();
    }
}
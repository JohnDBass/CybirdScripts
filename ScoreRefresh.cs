using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRefresh : MonoBehaviour {

    private void Start()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
    }
}

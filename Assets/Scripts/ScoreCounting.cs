using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounting : MonoBehaviour
{

    // Script for updating the score every frame.
    public static int Score = 0;
    Text ScoreText;

    private void Start()
    {
        ScoreText = GetComponent<Text>();
        Score = 0;
    }

    private void Update()
    {
        ScoreText.text = "Score: " + Score;
    }
}

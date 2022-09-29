using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text p1Score;
    int p1_score_value;
    public TMP_Text p2Score;
    int p2_score_value;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(p1Score.text);
        Debug.Log(p2Score.text);
        p1_score_value = 0;
        p2_score_value = 0;
        p1Score.text = "Player 1: " + p1_score_value;
        p2Score.text = "Player 2: " + p2_score_value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

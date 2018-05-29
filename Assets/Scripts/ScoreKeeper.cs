using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    Text textScore;

    private void Start() {
        textScore = GetComponent<Text>();
        textScore.text = score.ToString();
    }

    public void Score(int points) {
        score += points;
        textScore.text = score.ToString();
    }

    public static void Reset() {
        score = 0;
    }
}

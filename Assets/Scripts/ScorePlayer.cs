using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePlayer : MonoBehaviour
{
    private float scores;

    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {

        textMesh.text = "Puntaje: " + scores.ToString("0");
    }

    public void IncrementScore(float scoreIn)
    {
        scores += scoreIn;
    }
}

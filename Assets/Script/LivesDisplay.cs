using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    Text livesText;
    GameSession gameSession;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        updateDisplay();
    }

    // Update is called once per frame
    public void updateDisplay()
    {
        livesText = GetComponent<Text>();
        livesText.text = gameSession.getLives().ToString();
    }
}

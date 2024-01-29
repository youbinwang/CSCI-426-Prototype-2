using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 60;
    public TMP_Text timeText;
    bool timerOver;

    // for game end
    public GameObject gameOverContainer;
    public GameOver gameOverScript;
    public bool won;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMP_Text>();
        timerOver = false;
        Debug.Log("GameOver script is " + (gameOverScript != null ? "attached" : "not attached"));
    }
    
    
    void Update()
    {
        if (!timerOver)
        {
            if (won)
            {
                gameOverScript.SetState(true);
                timerOver = true;
                return;
            }

            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
           
            if (timeValue <= 0 && !timerOver)
            {
                Debug.Log("Time's up! Game over.");
                timerOver = true;
                gameOverScript.SetState(false);
            }

            DisplayTime(timeValue);
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0 && !timerOver)
        {
            timeToDisplay = 0;
            timerOver = true;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
    
    public void SetWinCondition(bool hasWon)
    {
        
        Debug.Log("123");
        won = hasWon;
    }
}

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
    private GameOver gameOverScript;
    public bool won;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<TMP_Text>();
        gameOverScript = gameOverContainer.GetComponent<GameOver>();
        timerOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);

        if (won)
        {
            gameOverScript.SetState(true);

        }
        else if(timerOver)
        {
            gameOverScript.SetState(false);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverContainer;
    public GameObject conditionImage;
    public TMP_Text gameOverConditionText;
    // Start is called before the first frame update
    void Start()
    {
        gameOverContainer.SetActive(false);
        conditionImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(bool won)
    {
        if (won)
        {
            conditionImage.SetActive(true);
            gameOverConditionText.SetText("You Win!");
        }
        else
        {
            gameOverConditionText.SetText("You Lose...");
        }

        gameOverContainer.SetActive(true);
    }
}

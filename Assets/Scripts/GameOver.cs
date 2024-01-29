    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class GameOver : MonoBehaviour
    {
        public GameObject gameOverContainer;
        public GameObject conditionImage;
        public TMP_Text gameOverConditionText;

        public Collider2D playerCollider;
        void Start()
        {
            gameOverContainer.SetActive(false);
            conditionImage.SetActive(false);
        }

        public void SetState(bool won)
        {
            if (won)
            {
                gameOverContainer.SetActive(true);
                conditionImage.SetActive(true);
                gameOverConditionText.SetText("You Win!");
            }
            else
            {
                gameOverContainer.SetActive(true);
                conditionImage.SetActive(true);
                gameOverConditionText.SetText("You Lose...");
            }
            
            if (playerCollider != null)
            {
                playerCollider.enabled = false;
            }
        }
    }

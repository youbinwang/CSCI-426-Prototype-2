using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    SpriteRenderer bgRenderer;
    // Start is called before the first frame update
    void Start()
    {
        bgRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBackground(string path)
    {
        bgRenderer.sprite = Resources.Load<Sprite>(path);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    GameObject bgColorToUpdate;
    // Start is called before the first frame update
    void Awake()
    {
        bgColorToUpdate = GameObject.Find("Red");        
        bgColorToUpdate.GetComponent<SpriteRenderer>().enabled = false;

        bgColorToUpdate = GameObject.Find("Orange");
        bgColorToUpdate.GetComponent<SpriteRenderer>().enabled = false;

        bgColorToUpdate = GameObject.Find("Yellow");
        bgColorToUpdate.GetComponent<SpriteRenderer>().enabled = false;

        bgColorToUpdate = GameObject.Find("Green");
        bgColorToUpdate.GetComponent<SpriteRenderer>().enabled = false;

        bgColorToUpdate = GameObject.Find("Blue");
        bgColorToUpdate.GetComponent<SpriteRenderer>().enabled = false;

        bgColorToUpdate = GameObject.Find("Purple");
        bgColorToUpdate.GetComponent<SpriteRenderer>().enabled = false;

        bgColorToUpdate = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBackground(string path)
    {
        bgColorToUpdate = GameObject.Find(path);
        bgColorToUpdate.GetComponent<SpriteRenderer>().enabled = true;
    }
}

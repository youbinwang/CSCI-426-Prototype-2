using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChangerHelper : MonoBehaviour
{
    private GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.FindWithTag("Background");
    }

    // Update is called once per frame
    void Update()
    {
    }

    // attach to whatever determines number of blank color drops have been collected
    public void UpdateBackgroundHelper(string path)
    {
        background.GetComponent<BackgroundChanger>().UpdateBackground(path);
    }
}

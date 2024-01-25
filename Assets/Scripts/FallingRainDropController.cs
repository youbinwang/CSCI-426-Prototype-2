using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRainDropController : MonoBehaviour
{
    float wait = 0.3f;
    public GameObject fallingDrop;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fall", wait, wait);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fall()
    {
        Instantiate(fallingDrop, new Vector3(Random.Range(-9, 9), 10, 0), Quaternion.identity);
    }
}

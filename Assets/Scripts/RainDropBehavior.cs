using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDropBehavior : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SortedDictionary<int, Color> colorChoices;
    SortedDictionary<int, string> clipChoices;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        // choose color (0-6)
        colorChoices = new SortedDictionary<int, Color>
        {
            { 0, Color.gray },
            { 1, Color.red },
            { 2, Color.magenta },
            { 3, Color.yellow },
            { 4, Color.green },
            { 5, Color.cyan },
            { 6, Color.blue }
        };

        // choose sound
        clipChoices = new SortedDictionary<int, string>
        {
            { 0,  "do"},
            { 1,  "Audio/Raindrop/re"},
            { 2,  "Audio/Raindrop/mi"},
            { 3,  "Audio/Raindrop/fa"},
            { 4,  "Audio/Raindrop/so"},
            { 5,  "Audio/Raindrop/la"},
            { 6,  "Audio/Raindrop/ti"}
        };

        int color = Random.Range(0, 6);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = colorChoices[color];
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(clipChoices[color]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(audioSource.clip);
        Destroy(this.gameObject);
    }
}

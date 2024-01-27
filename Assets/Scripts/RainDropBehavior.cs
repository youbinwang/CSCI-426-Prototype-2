using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RainDropBehavior : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;

    SpriteRenderer spriteRenderer;
    SortedDictionary<int, Color> colorChoices;
    SortedDictionary<int, string> clipChoices;

    int index;

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
            { 1,  "re"},
            { 2,  "mi"},
            { 3,  "fa"},
            { 4,  "so"},
            { 5,  "la"},
            { 6,  "ti" }
        };

        index = Random.Range(0, 6);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = colorChoices[index];
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>(clipChoices[index]));
        Destroy(this.gameObject, 1.2f);
    }
}

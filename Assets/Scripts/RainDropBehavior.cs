using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RainDropBehavior : MonoBehaviour
{
    private static int lastSoundIndex = -1;
    private string[] soundClips = new string[7] { "do", "re", "mi", "fa", "so", "la", "ti" };
    private AudioSource audioSource;
    
    private int colorIndex;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.1f;
            audioSource.playOnAwake = false;
        }
        
        SetNextSoundIndex();
    }
    
    private void Start()
    {
        colorIndex = Random.Range(0, RainDropSpawner.colorCounts.Length);
        GetComponent<SpriteRenderer>().color = RainDropSpawner.rainbowColors[colorIndex];
    }
    
    

    private void SetNextSoundIndex()
    {
        lastSoundIndex = (lastSoundIndex + 1) % soundClips.Length;
        if (audioSource != null)
        {
            audioSource.clip = Resources.Load<AudioClip>(soundClips[lastSoundIndex]);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (audioSource != null && audioSource.clip != null)
            {
                if (colorIndex == 6) //If hit on Grey
                {
                    HitOnGrey();
                }
                else
                {
                    RainDropSpawner.CollectColor(colorIndex);
                }
                audioSource.PlayOneShot(audioSource.clip);
                Renderer renderer = GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.enabled = false;
                }
                Destroy(gameObject, audioSource.clip.length);
            }
        }
        
        if (collider.gameObject.tag == "Ground")
        {
            Destroy(gameObject, audioSource.clip.length);
        }
        
        if (collider.gameObject.tag == "Umbrella")
        {
            Destroy(gameObject);
        }
    }
    
    private void HitOnGrey()
    {
        int randomColorIndex;
        do
        {
            randomColorIndex = Random.Range(0, RainDropSpawner.colorCounts.Length - 1);
        } while (randomColorIndex == colorIndex || RainDropSpawner.colorCounts[randomColorIndex] == 0);

        RainDropSpawner.colorCounts[randomColorIndex]--;
        RainDropSpawner.UpdateColorObjectAlpha(randomColorIndex, RainDropSpawner.colorCounts[randomColorIndex] / 3f);
    }
}

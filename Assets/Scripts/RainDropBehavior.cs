using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RainDropBehavior : MonoBehaviour
{
    private static int lastSoundIndex = -1;
    private string[] soundClips = new string[7] { "do", "re", "mi", "fa", "so", "la", "ti" };
    private AudioSource audioSource;

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
                audioSource.PlayOneShot(audioSource.clip);
                Renderer renderer = GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.enabled = false;
                }
                Destroy(gameObject, audioSource.clip.length);
                
            }
            else
            {
                Debug.LogError("AudioSource or AudioClip is null");
            }
        }
        
        if (collider.gameObject.tag == "Ground")
        {
            Destroy(gameObject, audioSource.clip.length);
        }
        
    }
}

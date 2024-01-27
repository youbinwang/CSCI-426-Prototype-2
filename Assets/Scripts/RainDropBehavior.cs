using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RainDropBehavior : MonoBehaviour
{
    private int colorIndex;
    private int soundIndex;
    private string[] soundClips = new string[7] { "do", "re", "mi", "fa", "so", "la", "ti" };
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    public void SetColorAndSoundIndex(int colorIdx, int soundIdx)
    {
        colorIndex = colorIdx;
        soundIndex = soundIdx;
        if (audioSource != null)
        {
            audioSource.clip = Resources.Load<AudioClip>(soundClips[soundIndex]);
            if (audioSource.clip == null)
            {
                Debug.LogError("Failed to load audio clip: " + soundClips[soundIndex]);
            }
            else
            {
                Debug.Log("Loaded audio clip: " + soundClips[soundIndex]);
            }
        }
        else
        {
            Debug.LogError("AudioSource component not found!");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Player hit");

            // 确保 AudioSource 和 AudioClip 都不为 null
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("Playing audio: " + soundClips[soundIndex]);
            }
            else
            {
                Debug.LogError("AudioSource or AudioClip is null");
            }

            // 在此处销毁对象可能会立即停止播放声音
            // 如果您希望声音播放完毕后再销毁对象，您可能需要稍作调整
            Destroy(gameObject, audioSource.clip.length);
        }
        
        if (collider.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        
    }
}

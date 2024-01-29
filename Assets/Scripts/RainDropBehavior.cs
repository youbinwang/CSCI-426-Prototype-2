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
    
    public GameObject particleSystemPrefab;

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
        if (RainDropSpawner.Instance.availableColors.Count > 0)
        {
            int index = Random.Range(0, RainDropSpawner.Instance.availableColors.Count);
            colorIndex = RainDropSpawner.Instance.availableColors[index];
        }
        else
        {
            colorIndex = 6;
        }

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
            SpawnParticleSystem();
            Destroy(gameObject, audioSource.clip.length);
        }

        if (collider.gameObject.tag == "Umbrella")
        {
            SpawnParticleSystem();
            Destroy(gameObject);
        }
    }
    
    private void SpawnParticleSystem()
    {
        if (particleSystemPrefab != null)
        {
            GameObject particleSystemInstance = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            Destroy(particleSystemInstance, 1f);
        }
    }
    
    

    private void HitOnGrey()
    {
        bool hasNonGrayRaindrops = false;
        for (int i = 0; i < RainDropSpawner.colorCounts.Length - 1; i++)
        {
            if (RainDropSpawner.colorCounts[i] > 0)
            {
                hasNonGrayRaindrops = true;
                break;
            }
        }

        if (!hasNonGrayRaindrops)
        {
            return;
        }

        int randomColorIndex;
        do
        {
            randomColorIndex = Random.Range(0, RainDropSpawner.colorCounts.Length - 1);
        } while (randomColorIndex == colorIndex || RainDropSpawner.colorCounts[randomColorIndex] == 0);

        RainDropSpawner.colorCounts[randomColorIndex]--;

        if (RainDropSpawner.colorCounts[randomColorIndex] < 3)
        {
            var colorObj = RainDropSpawner.Instance.rainbowObjects[randomColorIndex];
            var border = colorObj.transform.Find("Border");
            if (border != null)
            {
                border.gameObject.SetActive(false);
            }

            if (!RainDropSpawner.Instance.availableColors.Contains(randomColorIndex) &&
                RainDropSpawner.colorCounts[randomColorIndex] < 3)
            {
                RainDropSpawner.Instance.availableColors.Add(randomColorIndex);
                Debug.Log($"Re-added color index {randomColorIndex} to available colors.");
            }

            RainDropSpawner.UpdateColorObjectAlpha(randomColorIndex,
                RainDropSpawner.colorCounts[randomColorIndex] / 3f);
        }
    }
}

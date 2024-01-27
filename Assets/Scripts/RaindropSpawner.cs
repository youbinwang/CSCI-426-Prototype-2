using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDropSpawner : MonoBehaviour
{
    public GameObject raindropPrefab;
    public float spawnRate = 1f;
    
    private Color[] rainbowColors = new Color[7] {
        new Color(0.9254903f, 0.1098039f, 0.1372549f), // Red
        new Color(0.9960785f, 0.4980392f, 0.145098f),  // Orange
        new Color(0.9960785f, 0.9529412f, 0f),         // Yellow
        new Color(0.7529413f, 0.854902f, 0.6784314f),  // Green
        new Color(0f, 0.6352941f, 0.9058824f),         // Blue
        new Color(0.6352941f, 0.282353f, 0.6470588f),  // Purple
        new Color(0.45f, 0.45f, 0.45f),                // Indigo
    };

    private int soundIndex = 0;
    
    private void Start()
    {
        StartCoroutine(SpawnRaindrops());
    }

    private IEnumerator SpawnRaindrops()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / spawnRate);
            SpawnRaindrop();
        }
    }

    private void SpawnRaindrop()
    {
        GameObject raindrop = Instantiate(raindropPrefab, RandomSpawnPosition(), Quaternion.identity);
        
        int colorIndex = Random.Range(0, rainbowColors.Length);
        raindrop.GetComponent<SpriteRenderer>().color = rainbowColors[colorIndex];
        
        float randomScale = Random.Range(0.15f, 0.4f);
        raindrop.transform.localScale = new Vector3(randomScale, randomScale, 1);

        RainDropBehavior behavior = raindrop.AddComponent<RainDropBehavior>();
        behavior.SetColorAndSoundIndex(colorIndex, soundIndex);

        soundIndex = (soundIndex + 1) % 7;
    }

    private Vector2 RandomSpawnPosition()
    {
        float x = Random.Range(-9f, 9f);
        float y = 6f;
        return new Vector2(x, y);
    }
}

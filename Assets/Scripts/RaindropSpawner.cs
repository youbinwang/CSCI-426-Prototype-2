using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDropSpawner : MonoBehaviour
{
    public GameObject raindropPrefab;
    public float spawnRate = 1f;

    public GameObject[] rainbowObjects;
    
    public static Color[] rainbowColors = new Color[7] {
        new Color(0.9254903f, 0.1098039f, 0.1372549f), // Red
        new Color(0.9960785f, 0.4980392f, 0.145098f),  // Orange
        new Color(0.9960785f, 0.9529412f, 0f),         // Yellow
        new Color(0.7529413f, 0.854902f, 0.6784314f),  // Green
        new Color(0f, 0.6352941f, 0.9058824f),         // Blue
        new Color(0.6352941f, 0.282353f, 0.6470588f),  // Purple
        new Color(0.45f, 0.45f, 0.45f),                // Indigo
    };
    
    public static int[] colorCounts = new int[7];
    
    public static void CollectColor(int colorIndex)
    {
        colorCounts[colorIndex]++;
        if (colorCounts[colorIndex] <= 3)
        {
            UpdateColorObjectAlpha(colorIndex, colorCounts[colorIndex] / 3f);
        }
        if (colorCounts[colorIndex] == 3)
        {
            Debug.Log($"Collected 3 of color index: {colorIndex}");
        }
    }
    
    public static void UpdateColorObjectAlpha(int colorIndex, float alpha)
    {
        if (colorIndex >= 0 && colorIndex < Instance.rainbowObjects.Length)
        {
            var colorObj = Instance.rainbowObjects[colorIndex];
            if (colorObj != null)
            {
                var spriteRenderer = colorObj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    var color = spriteRenderer.color;
                    color.a = alpha;
                    spriteRenderer.color = color;
                }
            }
        }
    }


    public static RainDropSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    private void Start()
    {
        InitializeColorObjectsAlpha();
        
        StartCoroutine(SpawnRaindrops());
    }

    private void InitializeColorObjectsAlpha()
    {
        foreach (var colorObj in rainbowObjects)
        {
            if (colorObj != null)
            {
                var spriteRenderer = colorObj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                    var color = spriteRenderer.color;
                    color.a = 0f;
                    spriteRenderer.color = color;
                }
            }
        }
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
    }

    private Vector2 RandomSpawnPosition()
    {
        float x = Random.Range(-9f, 9f);
        float y = 6f;
        return new Vector2(x, y);
    }
}

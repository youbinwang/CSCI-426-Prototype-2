using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDropSpawner : MonoBehaviour
{
    public GameObject raindropPrefab;
    public float spawnRate = 1f;

    public GameObject[] rainbowObjects;
    
    public SpriteRenderer backgroundRenderer;
    public SpriteRenderer groundRenderer;
    
    public static Color[] rainbowColors = new Color[7] {
        new Color(0.9254903f, 0.1098039f, 0.1372549f), // Red
        new Color(0.9960785f, 0.4980392f, 0.145098f),  // Orange
        new Color(0.9960785f, 0.9529412f, 0f),         // Yellow
        new Color(0.13f, 0.69f, 0.29f),                 // Green
        new Color(0f, 0.6352941f, 0.9058824f),         // Blue
        new Color(0.6352941f, 0.282353f, 0.6470588f),  // Purple
        new Color(0.45f, 0.45f, 0.45f),                // Indigo
    };
    
    public static int[] colorCounts = new int[7];
    public List<int> availableColors = new List<int>();
    
    private Coroutine flashBackgroundCoroutine;
    
    public static void CollectColor(int colorIndex)
    {
        colorCounts[colorIndex]++;
        if (colorCounts[colorIndex] <= 3)
        {
            UpdateColorObjectAlpha(colorIndex, colorCounts[colorIndex] / 3f);
            Color flashColor = rainbowColors[colorIndex];
            if (Instance.flashBackgroundCoroutine != null)
            {
                Instance.StopCoroutine(Instance.flashBackgroundCoroutine);
            }
            Instance.flashBackgroundCoroutine = Instance.StartCoroutine(Instance.FlashBackground(flashColor, 0.2f));
        }
        if (colorCounts[colorIndex] == 3)
        {
            Instance.availableColors.Remove(colorIndex);
            Instance.StartCoroutine(Instance.FlashColor(colorIndex, 0.2f));
            Color flashColor = rainbowColors[colorIndex];
            Instance.StartCoroutine(Instance.FlashGround(flashColor, 0.15f));
            Debug.Log("Current available colors: " + string.Join(", ", Instance.availableColors));
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

    private IEnumerator FlashBackground(Color flashColor, float flashDuration)
    {
        Color originalColor = backgroundRenderer.color;

        for (int i = 0; i < 2; i++)
        {
            backgroundRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            backgroundRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
    
    private IEnumerator FlashGround(Color flashColor, float flashDuration)
    {
        Color originalColor = groundRenderer.color;
        
        for (int i = 0; i < 2; i++)
        {
            groundRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            groundRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
    
    private IEnumerator FlashColor(int colorIndex, float flashDuration)
    {
        if (colorIndex >= 0 && colorIndex < rainbowObjects.Length)
        {
            var colorObj = rainbowObjects[colorIndex];
            if (colorObj != null)
            {
                var spriteRenderer = colorObj.GetComponent<SpriteRenderer>();
                var border = colorObj.transform.Find("Border");
                if (spriteRenderer != null && border != null)
                {
                    Color originalColor = spriteRenderer.color;
                    float originalAlpha = originalColor.a;
                    
                    border.gameObject.SetActive(true);
                    
                    for (int i = 0; i < 3; i++)
                    {
                        for (float a = 0f; a <= 1f; a += Time.deltaTime / flashDuration)
                        {
                            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, a);
                            yield return null;
                        }
                        
                        for (float a = 1f; a >= originalAlpha; a -= Time.deltaTime / flashDuration)
                        {
                            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, a);
                            yield return null;
                        }
                    }

                    spriteRenderer.color = originalColor;
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
        
        for (int i = 0; i < rainbowColors.Length; i++)
        {
            availableColors.Add(i);
        }
        
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
        
        // int colorIndex = Random.Range(0, rainbowColors.Length);
        // raindrop.GetComponent<SpriteRenderer>().color = rainbowColors[colorIndex];
        //
        // float randomScale = Random.Range(0.15f, 0.4f);
        // raindrop.transform.localScale = new Vector3(randomScale, randomScale, 1);
        Debug.Log($"Spawning raindrop. Available colors count: {availableColors.Count}");
        if (availableColors.Count > 0)
        {
            int colorIndex = availableColors[Random.Range(0, availableColors.Count)];
            
            raindrop.GetComponent<SpriteRenderer>().color = rainbowColors[colorIndex];
        }
        else
        {
            Debug.Log("No more colors available. Spawning default color.");
            raindrop.GetComponent<SpriteRenderer>().color = rainbowColors[6];
        }
    
        float randomScale = Random.Range(0.3f, 0.4f);
        raindrop.transform.localScale = new Vector3(randomScale, randomScale, 1);
    }

    private Vector2 RandomSpawnPosition()
    {
        float x = Random.Range(-9f, 9f);
        float y = 6f;
        return new Vector2(x, y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, Mesh }
    public DrawMode drawmode;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public float treeNoiseScale;
    public float treeDensity;

    public bool autoUpdate;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public TerrainType[] regions;

    bool spawnTree;
    public GameObject TreePrefab;
    public int treeSpacing;
    public int currentTreeSpacing;

    private void Start()
    {
        currentTreeSpacing = treeSpacing;
    }

    public void GenerateTree()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        int treeIndex = 0;
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                if(noiseMap[x, y] > 0.4f && noiseMap[x,y] < 0.6f)
                {
                    float d = Random.Range(0f, treeDensity);
                    if (noiseMap[x, y] < d)
                    {
                        treeIndex++;
                        Instantiate(TreePrefab, new Vector2(x, y), Quaternion.identity);
                    }
                }
            }
        }

        Debug.Log($"There are {treeIndex} trees on the map seed {seed}");
    }

    public void generateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapWidth * mapHeight];
        int treeIndex = 0;
        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                //float TreeMap = TreenoiseMap[x, y];
                for(int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }

                    /*if(TreeMap >= 0.6f && currentTreeSpacing <= 0)
                    {
                        treeIndex++;
                        currentTreeSpacing = treeSpacing;
                    }
                    else
                    {
                        currentTreeSpacing--;
                    }*/
                }
            }
        }

        Debug.Log($"There are {treeIndex} spawns for this seed: {seed}");

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawmode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        } else if(drawmode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
        else if(drawmode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrtainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }

        GenerateTree();
    }

    void OnValidate()
    {
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if(mapHeight < 1)
        {
            mapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }

    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;

}

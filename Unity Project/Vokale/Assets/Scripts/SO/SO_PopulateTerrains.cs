using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenerationData", menuName ="Terrain/GenerationData")]
public class SO_PopulateTerrains : ScriptableObject
{
    public TerrainDataGeneration[] terrains;

    public void GenerateObjects(TerrainDataGeneration data)
    {
       data.TerrainSize = (int)data.region.size.x;

        float[,] noiseMap = new float[data.TerrainSize, data.TerrainSize];

        if (data.noiseScale <= 0)
        {
            data.noiseScale = 0.001f;
        }

        for (int y = 0; y < data.TerrainSize; y++)
        {
            for (int x = 0; x < data.TerrainSize; x++)
            {
                float sampleX = x / data.noiseScale;
                float sampleY = y / data.noiseScale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
                Debug.Log(perlinValue);
                if (perlinValue >= data.treeCutOfPoint)
                {
                    SpawnTree(x, y, data.treePrefab);
                }
            }
        }
    }

    public void SpawnTree(int posX, int posZ, GameObject treePrefab)
    {
        if (Physics.Raycast(new Vector3(posX, 100, posZ), new Vector3(0, -1, 0), out RaycastHit hitInfo, 200f))
        {
            if (!HasObstacle(hitInfo))
            {
                Instantiate(treePrefab, new Vector3(posX, hitInfo.point.y, posZ), Quaternion.identity);
            }
            else
            {
                Debug.Log("Spawn Cancled, Hit Obstcale");
            }
        }
    }

    public bool HasObstacle(RaycastHit hitInfo)
    {
        if (hitInfo.transform.gameObject.tag != "World" && hitInfo.transform.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class TerrainDataGeneration
{
    public string terrainName;
    public int TerrainSize;

    public float noiseScale;

    public TerrainData region;
    public GameObject treePrefab;

    [Range(0f, 1f)]
    public float treeCutOfPoint;
}
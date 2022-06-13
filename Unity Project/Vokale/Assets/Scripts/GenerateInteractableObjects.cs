using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateInteractableObjects : MonoBehaviour
{
    public string terrainName;

    [HideInInspector]
    public int TerrainSize;
    public float noiseScale;

    public Terrain region;
    public GameObject treePrefab;

    [Range(0f, 1f)]
    public float treeCutOfPoint;

    private void Start()
    {
        region = gameObject.GetComponent<Terrain>();
        GenerateObjects();
    }

    public void GenerateObjects()
    {
        TerrainSize = (int)region.terrainData.size.x;
        Debug.Log(TerrainSize);

        float[,] noiseMap = new float[TerrainSize, TerrainSize];

        if (noiseScale <= 0)
        {
            noiseScale = 0.001f;
        }

        Debug.Log((int)region.transform.position.z);
        Debug.Log((int)region.transform.position.z + 100);

        int target = (int)region.transform.position.z + 100;
        for (int z = (int)region.transform.position.z; z < target ; z++)
        {
            Debug.Log(z);
            for (int x = 0; x < 100; x++)
            {
                float sampleX = x / noiseScale;
                float sampleY = z / noiseScale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                //noiseMap[x, y] = perlinValue;
                if (perlinValue >= treeCutOfPoint)
                {
                    SpawnTree(x, z, treePrefab);
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
                GameObject tree = Instantiate(treePrefab, new Vector3(posX, hitInfo.point.y, posZ), Quaternion.identity);
                tree.name = "Tree";
                tree.transform.parent = hitInfo.transform;
            }
            else
            {
                Debug.Log("Spawn Cancled, Hit Obstcale");
            }
        }
        else
        {
            Debug.Log($"Hit Nothing X:{posX} Y:0 Z{posZ}");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWater : MonoBehaviour
{
    public List<GameObject> worldWater;
    public GameObject waterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Yo");
        WaterSpawner(transform);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WaterSpawner(Transform parent)
    {
        for (int z = -10000; z < 10000; z += 500)
        {
            for (int x = -10000; x < 10000; x += 500)
            {
                GameObject water = Instantiate(waterPrefab, new Vector3(x, 1, z), Quaternion.identity);
                water.name = $"Water Block [{x},0,{z}]";
                water.transform.parent = parent;
                worldWater.Add(water);

            }
        }
    }

}

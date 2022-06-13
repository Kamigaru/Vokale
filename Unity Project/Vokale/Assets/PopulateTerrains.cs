using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateTerrains : MonoBehaviour
{
    public SO_PopulateTerrains chunks;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < chunks.terrains.Length; i++)
        {
            chunks.GenerateObjects(chunks.terrains[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

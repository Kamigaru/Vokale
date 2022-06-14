using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTerrainChunkInDistance : MonoBehaviour
{
    public float renderDistance;
    private List<GameObject> chunks = new List<GameObject>();

    public static RenderTerrainChunkInDistance instance;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            chunks.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckShouldRender(Transform playerPos)
    {
        foreach(GameObject chunk in chunks)
        {
            if(Vector3.Distance(playerPos.position, chunk.transform.position) <= renderDistance)
            {
                chunk.GetComponent<Terrain>().enabled = true;
            }
            else
            {
                chunk.GetComponent<Terrain>().enabled = false;
            }
        }
    }
}

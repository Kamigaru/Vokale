using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDistance : MonoBehaviour
{
    public Terrain terrain;
    public float viewDistance = 200f;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        terrain = gameObject.GetComponent<Terrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

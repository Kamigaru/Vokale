using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetF3CanvasInfo : MonoBehaviour
{
    public Transform player;
    public TMP_Text coordsText;
    public TMP_Text FPStext;

    public float timer, refresh, avgFrameRate;

    // Start is called before the first frame update
    void Start()
    {
        coordsText.text = $"x{player.transform.position.x}, y:{player.transform.position.y}, z:{player.transform.position.z}";
    }

    // Update is called once per frame
    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFrameRate = (int)(1f / timelapse);

        coordsText.text = $"x{player.transform.position.x.ToString("F0")}, y:{player.transform.position.y.ToString("F0")}, z:{player.transform.position.z.ToString("F0")}";
        FPStext.text = $"FPS: {avgFrameRate.ToString("F0")}";
    }
}

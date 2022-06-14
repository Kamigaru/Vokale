using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public Canvas F3Menu;

    [Header("Day/Night System")]
    public DayNightSettings DayNight;

    [Header("Game Day System")]
    public float minutePerDay;
    [Space(20)]
    public int gameDay;
    public int gameHour;
    public float gameSeconds;

    public float GameSeconds { set => gameSeconds = value; }

    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameSeconds = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        gameSeconds = Time.time;
        if (Input.GetKeyDown(KeyCode.F3))
        {
            TriggerF3();
        }
    }

    private void TriggerF3()
    {
        if (F3Menu.isActiveAndEnabled)
        {
            F3Menu.enabled = false;
        }
        else
        {
            F3Menu.enabled = true;
        }
    }
}

[System.Serializable]
public struct DayNightSettings
{
    public Light worldLightingSettings;
    public float[] worldLightIntensity;
    public Color[] FogColour;
    public Color[] LightSourceColour;
    public Material[] skyboxes;
}

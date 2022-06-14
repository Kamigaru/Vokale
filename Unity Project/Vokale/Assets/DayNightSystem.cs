using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    public GameManager manager;

    private int Index = 0;
    private float timeIteration = 1;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.manager;
        ChangeSky(Index);
    }

    // Update is called once per frame
    void Update()
    {
        if (!UpdateSkyBox()) return;
        Index++;
        if(Index > 3)
        {
            Index = 0;
        }
        ChangeSky(Index);
    }

    public void ChangeSky(int index)
    {
        Debug.Log("Updating");
        manager.DayNight.worldLightingSettings.color = manager.DayNight.LightSourceColour[index];
        manager.DayNight.worldLightingSettings.intensity = manager.DayNight.worldLightIntensity[index];
        RenderSettings.fogColor = manager.DayNight.FogColour[index];
        RenderSettings.skybox = manager.DayNight.skyboxes[index];
    }

    public bool UpdateSkyBox()
    {
        float minutePerDay = manager.minutePerDay;
        float sectionTime = minutePerDay / manager.DayNight.skyboxes.Length;

        if(manager.gameSeconds >= ((60 * sectionTime) * timeIteration))
        {
            timeIteration++;
            return true;
        }

        return false;
    }
}

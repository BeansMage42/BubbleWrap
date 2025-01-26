using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;

public class HeavyMetalStarts : MonoBehaviour
{
    [SerializeField] private Material happy;

    [SerializeField] private Light light;
    [SerializeField] private Color color;

    [SerializeField] private bool update;
    private float blendAmount = 0;

    private void Update()
    {
        if (update && blendAmount < 1)
        {
            blendAmount += Time.deltaTime;
            happy.SetFloat("_Blend", blendAmount);
            print("hjihih");
            RenderSettings.skybox = happy;
            DynamicGI.UpdateEnvironment();
        }
        
    }

    public void ChangeMood()
    {
        update = true;
        light.color = color;
        RenderSettings.skybox = happy;
        DynamicGI.UpdateEnvironment();
    }
}

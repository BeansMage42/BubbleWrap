using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;

public class HeavyMetalStarts : MonoBehaviour
{
    [SerializeField] private Material happy;
    [SerializeField] private Material sad;

    [SerializeField] private Light light;
    [SerializeField] private Color color;

    [SerializeField] private bool update;

    private void Update()
    {
        if (update)
        {
            update = true;
            ChangeMood();
        }
    }

    public void ChangeMood()
    {
        light.color = color;
        RenderSettings.skybox = sad;
        DynamicGI.UpdateEnvironment();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Fog : UdonSharpBehaviour
{
    public bool fog_Default = true;
    void Start()
    {
        RenderSettings.fog = fog_Default;
    }
    public void Interact()
    {
        if (RenderSettings.fog)
        {
            RenderSettings.fog = false;
        }
        else
        {
            RenderSettings.fog = true;
        }
    }
}

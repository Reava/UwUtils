using UnityEngine;
using UdonSharp;

[AddComponentMenu("UwUtils/UnityFogToggle")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class UnityFogToggle : UdonSharpBehaviour
{
    [SerializeField] private bool fog_Default = true;

    void Start()
    {
        RenderSettings.fog = fog_Default;
    }
    public void Interact()
    {
        RenderSettings.fog = !RenderSettings.fog; ;
    }
}
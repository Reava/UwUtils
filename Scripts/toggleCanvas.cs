using UdonSharp;
using Unity.Collections;
using UnityEngine;
using VRC.SDK3.Components;

[AddComponentMenu("UwUtils/ToggleCanvas")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class toggleCanvas : UdonSharpBehaviour
{
    [Header("List of objects")]
    public Canvas[] canvasesToToggle;

    public override void Interact()
    {
        foreach (Canvas toggleObject in canvasesToToggle)
        {
            toggleObject.enabled = !toggleObject.enabled;
        }
    }

    public void _Enable()
    {
        foreach (Canvas toggleObject in canvasesToToggle)
        {
            toggleObject.enabled = true;
        }
    }

    public void _Disable()
    {
        foreach (Canvas toggleObject in canvasesToToggle)
        {
            toggleObject.enabled = false;
        }
    }
}

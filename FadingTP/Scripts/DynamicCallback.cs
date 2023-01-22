using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class DynamicCallback : UdonSharpBehaviour
{
    [HideInInspector] public UdonSharpBehaviour tscript;

    public override void Interact()
    {
        if (tscript != null) tscript.SendCustomEvent("_teleportPlayer");
    }

    public void _sendCallback()
    {
        tscript.SendCustomEvent("_teleportPlayer");
    }

    public void _selectedOutput(GameObject targetScript)
    {
        tscript = targetScript.GetComponent<UdonSharpBehaviour>();
    }
}
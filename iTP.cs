using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
public class iTP : UdonSharpBehaviour
{
    public Transform targetLocation;

    public override void Interact()
    {
        if (Networking.LocalPlayer != null)
        {
            Networking.LocalPlayer.TeleportTo(targetLocation.position, targetLocation.rotation);
        }
    }
}
using UnityEngine;
using VRC.SDKBase;
using UdonSharp;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class _ITP : UdonSharpBehaviour
{
    public Transform targetLocation;

    public override void Interact()
    {
        Networking.LocalPlayer.TeleportTo(targetLocation.position, targetLocation.rotation);
    }
}
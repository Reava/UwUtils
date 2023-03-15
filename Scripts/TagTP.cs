using UnityEngine;
using VRC.SDKBase;
using UdonSharp;

[AddComponentMenu("UwUtils/TagTP")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class TagTP : UdonSharpBehaviour
{
    [Header("Tag name allowed")]
    [SerializeField] private string tagAllowed;
    [Header("TP Target when allowed")]
    [SerializeField] private Transform targetLocation;
    [Header("TP elsewhere when tag doesn't match?")]
    [SerializeField] private bool tpOption;
    [Header("TP Target when disallowed")]
    [SerializeField] private Transform targetLocation2;

    public override void Interact()
    {
        if (Networking.LocalPlayer != null)
        {
            if (Networking.LocalPlayer.GetPlayerTag("rank") == tagAllowed)
            {
                Networking.LocalPlayer.TeleportTo(targetLocation.position, targetLocation.rotation);
            }
            else
            {
                if (tpOption)
                {
                    Networking.LocalPlayer.TeleportTo(targetLocation2.position, targetLocation2.rotation);
                }
            }
        }
    }
}
using UnityEngine;
using VRC.SDKBase;

namespace UdonSharp.Examples.Utilities
{
    public class TagArrayTP : UdonSharpBehaviour
    {
        [Header("Tag name array (Tag 1 TPs to Target 1, 2 to 2...")]
        public string[] tagAllowed;
        [Header("TP Targets (Target1 for Tag1, T2 to T2...")]
        public Transform[] targetLocation;
        [Header("TP to fallback location when no matching tag ?")]
        public bool tpFallbackEnabled;
        [Header("Fallback Location")]
        public Transform targetFallback;

        public override void Interact()
        {
            if (Networking.LocalPlayer != null && Networking.LocalPlayer.GetPlayerTag("rank") != null)
            {
                for (var i = 0; i < tagAllowed.Length; i++)
                {
                    if (tagAllowed[i] != Networking.LocalPlayer.GetPlayerTag("rank")) continue;
                    Networking.LocalPlayer.TeleportTo(targetLocation[i].position, targetLocation[i].rotation);
                }
            }
            else
            {
                if (tpFallbackEnabled)
                {
                    Networking.LocalPlayer.TeleportTo(targetFallback.position, targetFallback.rotation);
                }
            }
         }
    }
}

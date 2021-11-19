using UnityEngine;
using VRC.SDK3.Components;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonSharp.Examples.Utilities
{
    public class InteractToggleTag : UdonSharpBehaviour
    {
        [Tooltip("Tag name allowed")]
        public string tag;
        [Tooltip("TP Target")]
        public Transform targetLocation;

        public override void Interact()
        {
            if (Networking.LocalPlayer.GetPlayerTag("rank") == tag)
            {
                if (Networking.LocalPlayer != null)
                {
                    Networking.LocalPlayer.TeleportTo(targetLocation.position, targetLocation.rotation);
                }
            }
        }
    }
}
using UnityEngine;
using VRC.SDK3.Components;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonSharp.Examples.Utilities
{
    public class TagTP : UdonSharpBehaviour
    {
        [Header("Tag name allowed")]
        public string tagAllowed;
        [Header("TP Target when allowed")]
        public Transform targetLocation;
        [Header("TP elsewhere when tag doesn't match?")]
        public bool tpOption;
        [Header("TP Target when disallowed")]
        public Transform targetLocation2;

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
}

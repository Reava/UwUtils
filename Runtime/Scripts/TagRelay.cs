using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/TagRelay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TagRelay : UdonSharpBehaviour
    {
        [SerializeField] private UdonBehaviour programGranted;
        [SerializeField] private string tagAuthorized = "Visitor";
        public override void Interact()
        {
            if (Networking.LocalPlayer != null && Networking.LocalPlayer.GetPlayerTag("rank") != null)
            {
                if (Networking.LocalPlayer.GetPlayerTag("rank") == tagAuthorized)
                {
                    programGranted.SendCustomEvent("_updateState");
                }
            }
        }
    }
}
using UnityEngine;
using VRC.SDKBase;
using UdonSharp;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/iTP")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class iTP : UdonSharpBehaviour
    {
        [SerializeField] private Transform targetLocation;

        public override void Interact()
        {
            Networking.LocalPlayer.TeleportTo(targetLocation.position, targetLocation.rotation);
        }
    }
}
using UnityEngine;
using VRC.SDKBase;
using UdonSharp;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Player Teleporter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PlayerTeleporter : UdonSharpBehaviour
    {
        [SerializeField] private Transform targetLocation;

        public override void Interact()
        {
            if (!Utilities.IsValid(targetLocation)) return;
            Networking.LocalPlayer.TeleportTo(targetLocation.position, targetLocation.rotation);
        }
    }
}
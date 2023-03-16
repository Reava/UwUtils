using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/TagSetter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TagSetter : UdonSharpBehaviour
    {
        [Tooltip("Name of the tag")]
        [SerializeField] private string playerTag;
        public override void Interact()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            localPlayer.SetPlayerTag("rank", playerTag);
        }
    }
}
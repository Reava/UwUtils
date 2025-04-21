using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/InstanceCreatorRelay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class InstanceCreatorRelay : UdonSharpBehaviour
    {
        [Header("Name of the event")]
        [SerializeField] private string eventName = "_interact";
        [Header("Programs to send a custom event to")]
        [SerializeField] private UdonBehaviour[] programsList;

        void Start()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (Networking.LocalPlayer.isMaster)
            {
                foreach (UdonBehaviour b in programsList)
                {
                    b.SendCustomEvent(eventName);
                }
            }
        }
    }
}
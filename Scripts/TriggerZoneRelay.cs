using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/TriggerZone Relay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TriggerZoneRelay : UdonSharpBehaviour
    {
        [Space]
        [Header("Make your colliders Trigger & on the layer IgnoreRaycast")]
        [SerializeField] private bool LocalPlayerOnly = true;
        [Space]
        [Header("Make sure your colliders are on the same gameObject as this script")]
        [Space]
        [SerializeField] private string eventNameOnExit = "_interact";
        [SerializeField] private string eventNameOnEnter = "_interact";
        [Space]
        [Header("Event settings")]
        [Space]
        [SerializeField] private bool onEnter = true;
        [SerializeField] private bool onExit = false;
        [Space]
        [SerializeField] private UdonBehaviour[] eventTargets;
        [Space]
        [SerializeField] private bool enableLogging = true;
        private bool valid = false;

        void Start()
        {
            if (eventTargets != null)
            {
                valid = true;
            }
            else
            {
                valid = false;
                Debug.LogError("[Reava_/UwUtils/TriggerZoneRelay.cs]: Setup is invalid, check your references for object '" + gameObject.name + "'", gameObject);
            }
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (LocalPlayerOnly && player != Networking.LocalPlayer) return;
            if (enableLogging) Debug.Log("[Reava_/UwUtils/TriggerZoneRelay.cs]: Player exit " + gameObject.name, gameObject);
            if (valid && onExit)
            {
                foreach (UdonBehaviour program in eventTargets)
                {
                    program.SendCustomEvent(eventNameOnExit);
                }
            }
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (LocalPlayerOnly && player != Networking.LocalPlayer) return;
            if (enableLogging) Debug.Log("[Reava_/UwUtils/TriggerZoneRelay.cs]: Player entered " + gameObject.name, gameObject);
            if (valid && onEnter)
            {
                foreach (UdonBehaviour program in eventTargets)
                {
                    program.SendCustomEvent(eventNameOnEnter);
                }
            }
        }
    }
}
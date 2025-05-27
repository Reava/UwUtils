using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using VRC.Udon;
using UnityEngine.UIElements;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Trigger Zone Relay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TriggerZoneRelay : UdonSharpBehaviour
    {
        [Space]
        [Header("Make your colliders Trigger & on the layer MirrorReflection")]
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
        [SerializeField] private UdonSharpBehaviour[] eventTargets;
        [SerializeField] private UdonSharpBehaviour[] eventTargetsEnterOnly;
        [SerializeField] private UdonSharpBehaviour[] eventTargetsExitOnly;
        [Space, Tooltip("Support will only be given if logging is enabled.")]
        [SerializeField] private bool enableLogging = true;
        private bool valid = true;

        void Start()
        {
            if (eventTargets.Length == 0 && eventTargetsEnterOnly.Length == 0 && eventTargetsExitOnly.Length == 0)
            {
                valid = false;
                Debug.LogError("[Reava_/UwUtils/TriggerZoneRelay.cs]: Setup is invalid, check your references for object '" + gameObject.name + "' (No Targets to relay found, did you mean this?)", gameObject);
            }
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if(!valid || !onExit) return;
            if (LocalPlayerOnly && player != Networking.LocalPlayer) return;
            if (enableLogging) Debug.Log("[Reava_/UwUtils/TriggerZoneRelay.cs]: Player exit " + gameObject.name, gameObject);
            foreach (UdonSharpBehaviour program in eventTargets)
            {
                if (program) program.SendCustomEvent(eventNameOnExit);
            }
            foreach (UdonSharpBehaviour program in eventTargetsEnterOnly)
            {
                if (program) program.SendCustomEvent(eventNameOnExit);
            }
        }
        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (!valid || !onEnter) return;
            if (LocalPlayerOnly && player != Networking.LocalPlayer) return;
            if (enableLogging) Debug.Log("[Reava_/UwUtils/TriggerZoneRelay.cs]: Player entered " + gameObject.name, gameObject);
            foreach (UdonSharpBehaviour program in eventTargets)
            {
                if(program) program.SendCustomEvent(eventNameOnEnter);
            }
            foreach (UdonSharpBehaviour program in eventTargetsExitOnly)
            {
                if (program) program.SendCustomEvent(eventNameOnEnter);
            }
        }
    }
}
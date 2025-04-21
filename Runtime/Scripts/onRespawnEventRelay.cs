using UnityEngine;
using VRC.SDKBase;
using UdonSharp;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/on Respawn Event Relay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class onRespawnEventRelay : UdonSharpBehaviour
    {
        [Space]
        [SerializeField] private string eventName = "_interact";
        [SerializeField] private UdonBehaviour[] TargetBehaviours;
        [Space]
        [SerializeField] private bool enableLogging = true;

        public override void OnPlayerRespawn(VRCPlayerApi player)
        {
            if (enableLogging) Debug.Log("[Reava_/UwUtils/onRespawnEventRelay.cs]: Player" + player .displayName + " respawned, triggering "+TargetBehaviours.Length+" target Behaviours from :"+ gameObject.name + "", gameObject);
            foreach(UdonBehaviour t in TargetBehaviours)
            {
                if (t == null) continue;
                t.SendCustomEvent(eventName);
            }
        }
    }
}
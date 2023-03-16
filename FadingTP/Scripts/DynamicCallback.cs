using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Dynamic Callback")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class DynamicCallback : UdonSharpBehaviour
    {
        [HideInInspector] public UdonSharpBehaviour tscript;
        [SerializeField] private string eventName = "_teleportPlayer";

        public override void Interact()
        {
            if (tscript != null) tscript.SendCustomEvent(eventName);
        }

        public void _sendCallback()
        {
            tscript.SendCustomEvent(eventName);
        }

        public void _selectedOutput(GameObject targetScript)
        {
            tscript = targetScript.GetComponent<UdonSharpBehaviour>();
        }
    }
}
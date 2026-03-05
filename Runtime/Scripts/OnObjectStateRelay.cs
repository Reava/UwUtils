using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Object State Relay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class OnObjectStateRelay : UdonSharpBehaviour
    {
        [Tooltip("Use '_interact' for trigger interact on selected behavior")]
        public string[] eventNamesEnabled;
        public UdonBehaviour[] OnEnableRelay;
        [Tooltip("Use '_interact' for trigger interact on selected behavior")]
        public string[] eventNamesDisabled;
        public UdonBehaviour[] OnDisableRelay;

        private void OnDisable()
        {
            if (OnDisableRelay == null || eventNamesDisabled == null) return;
            for (int i = 0; i < OnDisableRelay.Length && i < eventNamesDisabled.Length; i++)
            {
                if (OnDisableRelay[i] != null && eventNamesDisabled[i].Length > 0) OnDisableRelay[i].SendCustomEvent(eventNamesDisabled[i]);
            }
        }

        private void OnEnable()
        {
            if (OnEnableRelay == null || eventNamesEnabled == null) return;
            for (int i = 0; i < OnEnableRelay.Length && i < eventNamesEnabled.Length; i++)
            {
                if (OnEnableRelay[i] != null && eventNamesEnabled[i].Length > 0) OnEnableRelay[i].SendCustomEvent(eventNamesEnabled[i]);
            }
        }
    }
}
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;

namespace UwUtils
{
    namespace UwUtils
    {
        [AddComponentMenu("UwUtils/ToggleHub")]
        [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
        public class ToggleHub : UdonSharpBehaviour
        {
            [Space]
            public bool DefaultToggleValue = true;
            [Space]
            [SerializeField] private Toggle[] TargetToggles;
            [Header("Target behaviors to send an event on value change to")]
            [Space]
            [SerializeField] private UdonBehaviour[] TargetBehaviorUpdate;
            [SerializeField] private string eventName;
            [Space]
            [SerializeField] private bool enableLogging = true;

            void Start()
            {
                if (TargetToggles == null) return;
                foreach (Toggle s in TargetToggles)
                {
                    if (!s) continue;
                    s.SetIsOnWithoutNotify(DefaultToggleValue);
                }
            }

            public override void Interact() => _ToggleChange();

            private void _ToggleChange()
            {
                if (enableLogging) Debug.Log("[Reava_/UwUtils/ToggleHub.cs]: Change detected, updating values from: " + gameObject.name + "", gameObject);
                if (TargetToggles == null) return;
                bool found = false;
                Toggle tempToggle = null;
                foreach (Toggle s in TargetToggles)
                {
                    if (!s) continue;
                    if (found) s.SetIsOnWithoutNotify(DefaultToggleValue);
                    if (s.isOn != DefaultToggleValue)
                    {
                        DefaultToggleValue = s.isOn;
                        tempToggle = s;
                        found = true;
                    }
                }
                foreach (Toggle s in TargetToggles)
                {
                    if (!s) continue;
                    s.SetIsOnWithoutNotify(DefaultToggleValue);
                    if (s == tempToggle) break;
                }
                foreach (UdonBehaviour target in TargetBehaviorUpdate)
                {
                    if (!target) continue;
                    target.SendCustomEvent(eventName);
                }
            }
        }
    }
}
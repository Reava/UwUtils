using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using UwUtils;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Multi UI Slider Manager")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MultiUISliderManager : UdonSharpBehaviour
    {
        [Space]
        public float DefaultSliderValue = 0.6f;
        [Space]
        [SerializeField] private Slider[] TargetSliders;
        [Header("Target behaviors to send an event on value change to")]
        [Space]
        [SerializeField] private UdonBehaviour[] TargetBehaviorUpdate;
        [SerializeField] private string eventName = "_interact";
        [Space]
        [SerializeField] private bool enableLogging = true;

        void Start()
        {
            if (TargetSliders == null) return;
            foreach (Slider s in TargetSliders)
            {
                if (!s) continue;
                s.SetValueWithoutNotify(DefaultSliderValue);
            }
        }

        public override void Interact() => _SliderChange();

        private void _SliderChange()
        {
            if (enableLogging) Debug.Log("[Reava_/UwUtils/SliderHub.cs]: Change detected, updating values from: " + gameObject.name + "", gameObject);
            if (TargetSliders == null) return;
            bool found = false;
            Slider tempSlider = null;
            foreach (Slider s in TargetSliders)
            {
                if (!s) continue;
                if (found) s.SetValueWithoutNotify(DefaultSliderValue);
                if (s.value != DefaultSliderValue)
                {
                    DefaultSliderValue = s.value;
                    tempSlider = s;
                    found = true;
                }
            }
            foreach (Slider s in TargetSliders)
            {
                if (!s) continue;
                s.SetValueWithoutNotify(DefaultSliderValue);
                if (s == tempSlider) break;
            }
            foreach(UdonBehaviour target in TargetBehaviorUpdate)
            {
                if (!target) continue;
                target.SendCustomEvent(eventName);
            }
        }
    }
}
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/SliderArrayDriver")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SliderArrayDriver : UdonSharpBehaviour
    {
        [Space]
        [Header("Trigger logic on target components?")]
        [Space]
        [SerializeField] private bool NotifyTargetComponent = true;
        [SerializeField] private Slider SourceSlider;
        [Space]
        [SerializeField] private Slider[] TargetSliders;
        [Space]
        [SerializeField] private bool enableLogging = true;

        public override void Interact()
        {
            if (!SourceSlider || TargetSliders == null) return;
            if (enableLogging) Debug.Log("<b> | Reava_UwUtils: Interact Event received on: < b > " + gameObject.name + ".</b> (NotifyTargetComponent=" + NotifyTargetComponent + ", Target components found: " + TargetSliders.Length + ")", gameObject);
            if (NotifyTargetComponent)
            {
                foreach (Slider target in TargetSliders)
                {
                    if (target) target.value = SourceSlider.value;
                }
            }
            else
            {
                foreach (Slider target in TargetSliders)
                {
                    if (target) target.SetValueWithoutNotify(SourceSlider.value);
                }
            }
        }
    }
}
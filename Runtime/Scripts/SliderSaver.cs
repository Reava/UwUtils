
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Persistence;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Slider Saver")]
    public class SliderSaver : UdonSharpBehaviour
    {
        [SerializeField] private Slider _Slider;
        [Header("Persistence key must be unique per world.")]
        [SerializeField] private string persistenceKey = "slider_example";
        [Space]
        [Tooltip("Enable logging if encountering issues unrelated to references for debugging or if asked for support.")]
        [SerializeField] private bool enableLogging = false;

        private float defaultValue = 0f;

        void Start()
        {
            if (_Slider) defaultValue = _Slider.value;
            if (enableLogging) Debug.Log("[Reava_/UwUtils/SliderSaver.cs] Default value of " + defaultValue + " saved for slider '" + gameObject.name + "'", gameObject);
        }

        public override void Interact()
        {
            PlayerData.SetFloat(persistenceKey, _Slider.value);
        }

        public void _resetValue()
        {
            PlayerData.SetFloat(persistenceKey, defaultValue);
            _Slider.value = defaultValue;
            if(enableLogging) Debug.Log("[Reava_/UwUtils/SliderSaver.cs] Values reset to " + defaultValue + " for slider '" + gameObject.name + "'", gameObject);
        }

        public override void OnPlayerDataUpdated(VRCPlayerApi player, PlayerData.Info[] infos)
        {
            if (player != Networking.LocalPlayer) return;
            if (!PlayerData.HasKey(player, persistenceKey)) return;
            if (PlayerData.GetType(player, persistenceKey) != typeof(float)) return;

            float restoredValue = PlayerData.GetFloat(player, persistenceKey); // update with notify
            _Slider.value = restoredValue;
            if (enableLogging) Debug.Log("[Reava_/UwUtils/SliderSaver.cs] Value " + restoredValue + " restored for slider '" + gameObject.name + "'", gameObject);
        }
    }
}
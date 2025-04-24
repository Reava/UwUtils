
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

        public override void Interact()
        {
            PlayerData.SetFloat(persistenceKey, _Slider.value);
        }

        public override void OnPlayerDataUpdated(VRCPlayerApi player, PlayerData.Info[] infos)
        {
            if (player != Networking.LocalPlayer) return;
            if (!PlayerData.HasKey(player, persistenceKey)) return;
            if (PlayerData.GetType(player, persistenceKey) != typeof(float)) return;

            _Slider.value = PlayerData.GetFloat(player, persistenceKey); // update with notify
        }
    }
}
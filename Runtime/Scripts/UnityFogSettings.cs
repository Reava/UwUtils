using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Unity Fog Settings")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class UnityFogSettings : UdonSharpBehaviour
    {
        public bool applyDefaultOnStart = false;

        [Header("Default Fog")]
        public bool fogEnabledDefault = true;
        public FogMode fogModeDefault = FogMode.Exponential;
        public Color fogColorDefault = Color.black;
        public float fogDensityDefault = 0.01f;
        public float fogStartDefault = 0f;
        public float fogEndDefault = 300f;

        [Header("Toggled Fog")]
        public bool fogEnabledToggled = true;
        public FogMode fogModeToggled = FogMode.Exponential;
        public Color fogColorToggled = Color.black;
        public float fogDensityToggled = 0.02f;
        public float fogStartToggled = 0f;
        public float fogEndToggled = 200f;

        private bool state = false;
        private int selectionId = 0;

        void Start()
        {
            if (applyDefaultOnStart)
            {
                ApplyDefault();
            }
        }

        public void Interact()
        {
            state = !state;
            _UpdateFogSettings();
        }

        public void _SelectionChanged()
        {
            state = selectionId == 1;
            _UpdateFogSettings();
        }

        public void _UpdateFogSettings()
        {
            if (state)
            {
                ApplyToggled();
            }
            else
            {
                ApplyDefault();
            }
        }

        private void ApplyDefault()
        {
            RenderSettings.fog = fogEnabledDefault;
            RenderSettings.fogMode = fogModeDefault;
            RenderSettings.fogColor = fogColorDefault;
            RenderSettings.fogDensity = fogDensityDefault;
            RenderSettings.fogStartDistance = fogStartDefault;
            RenderSettings.fogEndDistance = fogEndDefault;
        }

        private void ApplyToggled()
        {
            RenderSettings.fog = fogEnabledToggled;
            RenderSettings.fogMode = fogModeToggled;
            RenderSettings.fogColor = fogColorToggled;
            RenderSettings.fogDensity = fogDensityToggled;
            RenderSettings.fogStartDistance = fogStartToggled;
            RenderSettings.fogEndDistance = fogEndToggled;
        }
    }
}
using UdonSharp;
using UnityEngine;
using UnityEngine.Rendering;

namespace UwUtils
{
    public enum AmbientSourceType
    {
        Skybox,
        Gradient,
        Color
    }

    [AddComponentMenu("UwUtils/Unity Ambient Source Setter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class EnvironmentalLightingSwitch : UdonSharpBehaviour
    {
        public bool applyDefaultOnStart = false;

        [Header("Lighting Source Options")]
        public AmbientSourceType sourceOptionDefault = AmbientSourceType.Skybox;
        public AmbientSourceType sourceOptionToggled = AmbientSourceType.Color;

        [Header("Intensity Multipliers")]
        public float intensityOptionDefault = 1f;
        public float intensityOptionToggled = 1f;

        private bool state = false;
        private int selectionId = 0;

        void Start()
        {
            if (applyDefaultOnStart)
            {
                _UpdateAmbientSource();
            }
        }

        public override void Interact()
        {
            state = !state;
            _UpdateAmbientSource();
        }

        public void _SelectionChanged()
        {
            state = selectionId == 1;
            _UpdateAmbientSource();
        }

        public void _UpdateAmbientSource()
        {
            AmbientSourceType selected = state ? sourceOptionToggled : sourceOptionDefault;
            RenderSettings.ambientMode = ConvertAmbientMode(selected);

            float intensity = state ? intensityOptionToggled : intensityOptionDefault;
            RenderSettings.ambientIntensity = intensity;

            DynamicGI.UpdateEnvironment();
        }

        private AmbientMode ConvertAmbientMode(AmbientSourceType source)
        {
            switch (source)
            {
                case AmbientSourceType.Skybox:
                    return AmbientMode.Skybox;

                case AmbientSourceType.Gradient:
                    return AmbientMode.Trilight;

                case AmbientSourceType.Color:
                    return AmbientMode.Flat;
            }

            return AmbientMode.Skybox;
        }
    }
}
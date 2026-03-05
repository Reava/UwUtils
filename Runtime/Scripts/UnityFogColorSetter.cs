using UdonSharp;
using UnityEditor;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Unity Fog Color Setter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class UnityFogColorSetter : UdonSharpBehaviour
    {
        public bool applyDefaultColorOnStart = false;
        public Color colorDefault = new Color(0, 0, 0);
        public Color colorToggled = new Color(0, 0, 0);
        private bool state = false;
        private int selectionId = 0;

        void Start()
        {
            if(applyDefaultColorOnStart) RenderSettings.fogColor = colorDefault;
        }

        public void Interact()
        {
            state = !state;
        }

        public void _UpdateFogColor()
        {
            RenderSettings.fogColor = state ? colorToggled : colorDefault;
        }

        public void _SelectionChanged()
        {
            state = selectionId == 1;
            _UpdateFogColor();
        }
    }
}
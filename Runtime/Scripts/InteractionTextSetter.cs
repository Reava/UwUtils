using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Interaction Text Setter")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class InteractionTextSetter : UdonSharpBehaviour
    {
        [Header("Targets")]
        public UdonBehaviour[] targetBehaviours;

        [Header("Text")]
        public string defaultText = "Click Me!";
        public string setText = "Undo";

        [Space]
        public bool setTextOnStart = true;

        private bool _isSet = false;

        void Start()
        {
            if (!setTextOnStart) return;
            _ApplyText(defaultText);
            _isSet = false;
        }

        public void _SetText()
        {
            _ApplyText(setText);
            _isSet = true;
        }

        public void _ResetText()
        {
            _ApplyText(defaultText);
            _isSet = false;
        }

        public override void Interact()
        {
            _ToggleText();
        }

        public override void OnPickupUseDown()
        {
            Interact();
        }

        public void _ToggleText()
        {
            if (_isSet)
            {
                _ResetText();
            }
            else
            {
                _SetText();
            }
        }

        private void _ApplyText(string text)
        {
            for (int i = 0; i < targetBehaviours.Length; i++)
            {
                UdonBehaviour ub = targetBehaviours[i];
                if (ub != null) ub.InteractionText = text;
            }
        }
    }
}
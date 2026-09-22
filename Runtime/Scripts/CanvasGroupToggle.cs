using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Canvas Group Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CanvasGroupToggle : UdonSharpBehaviour
    {
        public CanvasGroup[] canvasGroups;

        [Header("Settings")]
        public bool AlsoSwitchInteractable = true;
        public bool defaultState = true;

        [Header("Transition")]
        public bool smoothTransition = true;
        public float transitionTime = 0.25f;

        private bool _currentState;
        private float _targetAlpha;

        void Start()
        {
            _currentState = defaultState;
            float alpha = _currentState ? 1f : 0f;

            for (int i = 0; i < canvasGroups.Length; i++)
            {
                if (canvasGroups[i] != null)
                {
                    canvasGroups[i].alpha = alpha;
                }
            }
        }
        public override void Interact()
        {
            _Toggle();
        }

        public void _Toggle()
        {
            _SetState(!_currentState);
        }

        public void _Enable() => _SetOn();
        public void _Disable() => _SetOff();

        public void _SetOn()
        {
            _SetState(true);
        }

        public void _SetOff()
        {
            _SetState(false);
        }

        public void _SetState(bool state)
        {
            _targetAlpha = state ? 1f : 0f;

            if (canvasGroups.Length == 0 || canvasGroups[0] == null) return;

            if (AlsoSwitchInteractable)
            {
                foreach (var group in canvasGroups)
                {
                    group.interactable = state;
                }
            }

            for (int i = 0; i < canvasGroups.Length; i++)
            {
                canvasGroups[i].TweenFade(_targetAlpha, transitionTime, VRCTweenEase.Linear);
            }
        }
    }
}
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Canvas Group Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CanvasGroupToggle : UdonSharpBehaviour
    {
        [Header("Targets")]
        public CanvasGroup[] canvasGroups;

        [Header("State")]
        public bool defaultState = true;

        [Header("Transition")]
        public bool smoothTransition = true;
        public float transitionTime = 0.25f;

        private bool _currentState;
        private float _startAlpha;
        private float _targetAlpha;
        private float _timer;
        private bool _isTransitioning;

        private float LOOP_INTERVAL = 0.02f;

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

        public void _SetState(bool state)
        {
            _currentState = state;
            _targetAlpha = state ? 1f : 0f;

            if (!smoothTransition || transitionTime <= 0f)
            {
                _ApplyAlpha(_targetAlpha);
                return;
            }

            if (canvasGroups.Length == 0 || canvasGroups[0] == null)
                return;

            _startAlpha = canvasGroups[0].alpha;
            _timer = 0f;
            _isTransitioning = true;

            SendCustomEventDelayedSeconds(nameof(_TransitionLoop), LOOP_INTERVAL);
        }

        public void _TransitionLoop()
        {
            if (!_isTransitioning)
                return;

            _timer += LOOP_INTERVAL;

            float t = Mathf.Clamp01(_timer / transitionTime);
            float alpha = Mathf.Lerp(_startAlpha, _targetAlpha, t);

            _ApplyAlpha(alpha);

            if (t >= 1f)
            {
                _isTransitioning = false;
                return;
            }

            SendCustomEventDelayedSeconds(nameof(_TransitionLoop), LOOP_INTERVAL);
        }

        private void _ApplyAlpha(float alpha)
        {
            for (int i = 0; i < canvasGroups.Length; i++)
            {
                CanvasGroup cg = canvasGroups[i];
                if (cg != null)
                {
                    cg.alpha = alpha;
                }
            }
        }
    }
}
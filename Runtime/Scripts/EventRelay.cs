using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Event Relay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class EventRelay : UdonSharpBehaviour
    {
        [Header("Send an event call to any behavior with conditions and/or delay")]
        [SerializeField] private UdonBehaviour[] programRelay;
        [SerializeField] private GameObject stateCheck;
        private bool stateChecked;
        [SerializeField] private string[] eventNames;
        [SerializeField] private bool delayedAction;
        [SerializeField] private float delay;
        [Header("0 = Keep delay, 1 = No delay when object is off, 2 = No delay when object is on")]
        [Range(0, 2)]
        [SerializeField] private int function = 1;
        private bool abort = false;

        public void Start()
        {
            if (programRelay.Length == 0)
            {
                abort = true;
                SendCustomEventDelayedSeconds(nameof(_sendDebugError), 1f);
                return;
            }
        }
        public override void Interact()
        {
            if (abort) return;
            if (!delayedAction)
            {
                _relayAction();
            }
            else
            {
                if (stateCheck == null)
                {
                    Debug.LogWarning("[Reava_/UwUtils/EventRelay.cs] No State check object detected for delayed action, please assign it and refer to doc", this);
                    stateChecked = true;
                }
                else
                {
                    stateChecked = stateCheck.activeSelf;
                }

                if (function == 0)
                {
                    SendCustomEventDelayedSeconds(nameof(_relayAction), delay);
                }
                else if (function == 1)
                {
                    if (stateChecked)
                    {
                        SendCustomEventDelayedSeconds(nameof(_relayAction), delay);
                    }
                    else
                    {
                        _relayAction();
                    }
                }
                else if (function == 2)
                {
                    if (!stateChecked)
                    {
                        SendCustomEventDelayedSeconds(nameof(_relayAction), delay);
                    }
                    else
                    {
                        _relayAction();
                    }
                }
            }
        }

        public void _relayAction()
        {
            if (abort) return;

            for(int i = 0; i < programRelay.Length; i++)
            {
                programRelay[i].SendCustomEvent(eventNames[i]);
            }
        }

        public void _sendDebugError() => Debug.LogError("[Reava_/UwUtils/EventRelay.cs]::<color=red> No Target script found</color>. (" + gameObject + ")", gameObject);
    }
}
using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Event Relay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class EventRelay : UdonSharpBehaviour
    {
        [Header("Send an event call to any behavior with delay")]
        [SerializeField] private UdonBehaviour[] programRelay;
        [SerializeField] private string[] eventNames;
        [SerializeField] private bool delayAction = false;
        [SerializeField] private float delay = 0f;
        private bool abort = false;

        public void Start()
        {
            if (programRelay.Length == 0)
            {
                abort = true;
                SendCustomEventDelayedSeconds(nameof(_sendDebugError), 0.1f);
                return;
            }
        }

        public override void OnPickupUseDown()
        {
            Interact();
        }

        public override void Interact()
        {
            if (abort) return;
            if (!delayAction)
            {
                _relayAction();
            }
            else
            {
                SendCustomEventDelayedSeconds(nameof(_relayAction), delay);
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
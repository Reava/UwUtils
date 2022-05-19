using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class RelayAction : UdonSharpBehaviour
{
    [Header("Please only reference gameObjects with a single Udon Behavior")]
    public UdonBehaviour programRelay;
    public string eventName;
    public bool delayedAction;
    public float delay;

    public override void Interact()
    {
        if (!delayedAction)
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
        programRelay.SendCustomEvent(eventName);
    }
}

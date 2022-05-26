using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ActionRelay : UdonSharpBehaviour
{
    [Header("Send an event call to any behavior with conditions and/or delay")]
    public UdonBehaviour programRelay;
    public GameObject stateCheck;
    private bool stateChecked;
    public string eventName = "_interact";
    public bool delayedAction;
    public float delay;
    [Header("0 = keep delay, 1 = no delay when object is off, 2 = no delay when object is on")]
    [Range(0, 2)]
    public int function = 1;

    public override void Interact()
    {
        stateChecked = stateCheck.activeSelf;
        if (!delayedAction)
        {
            _relayAction();
        }
        else
        {
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
        programRelay.SendCustomEvent(eventName);
    }
}

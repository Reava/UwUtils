using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class UdonKeybinds : UdonSharpBehaviour
{
    public bool keybindsEnabled = true;
    public UdonBehaviour cam1;
    public UdonBehaviour cam2;
    public UdonBehaviour cam3;
    public UdonBehaviour cam4;
    public UdonBehaviour cam5;
    public UdonBehaviour cam6;
    public UdonBehaviour GateCollider;
    public UdonBehaviour GateAnimation;
    public string eventName = "_interact";
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad0))
        {
            // Toggle keybinds
            keybindsEnabled = !keybindsEnabled;
        }
        if (keybindsEnabled)
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
            {
                // Gate controls
                GateCollider.SendCustomEvent(eventName);
                GateAnimation.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad1))
            {
                // Cam 1
                cam1.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad2))
            {
                // Cam 2
                cam2.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad3))
            {
                // Cam 3
                cam3.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad4))
            {
                // Cam 4
                cam4.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad5))
            {
                // Cam 5
                cam5.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad6))
            {
                // Cam 6
                cam6.SendCustomEvent(eventName);
            }
        }
    }
}

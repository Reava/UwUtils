using UdonSharp;
using UnityEngine;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class UdonKeybinds : UdonSharpBehaviour
{
    public bool keybindsEnabled = true;
    public UdonBehaviour Action1;
    public UdonBehaviour Action2;
    public UdonBehaviour Action3;
    public UdonBehaviour Action4;
    public UdonBehaviour Action5;
    public UdonBehaviour Action6;
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
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad1))
            {
                Action1.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad2))
            {
                Action2.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad3))
            {
                Action3.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad4))
            {
                Action4.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad5))
            {
                Action5.SendCustomEvent(eventName);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad6))
            {
                Action6.SendCustomEvent(eventName);
            }
        }
    }
}

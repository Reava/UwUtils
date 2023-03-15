using UdonSharp;
using UnityEngine;
using VRC.Udon;

[AddComponentMenu("UwUtils/UdonKeybinds")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class UdonKeybinds : UdonSharpBehaviour
{
    [SerializeField] private bool keybindsEnabled = true;
    [SerializeField] private UdonBehaviour Action1;
    [SerializeField] private UdonBehaviour Action2;
    [SerializeField] private UdonBehaviour Action3;
    [SerializeField] private UdonBehaviour Action4;
    [SerializeField] private UdonBehaviour Action5;
    [SerializeField] private UdonBehaviour Action6;
    [SerializeField] private string eventName = "_interact";

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

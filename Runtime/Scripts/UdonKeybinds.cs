using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/UdonKeybinds")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class UdonKeybinds : UdonSharpBehaviour
    {
        [Space]
        [Tooltip("Keybind is CTRL + 0 (keypad) to toggle keybind in-game")]
        [SerializeField] private bool keybindEnabled = true;
        [SerializeField] private KeyCode keybind = KeyCode.Keypad1;
        [Space]
        [SerializeField] private string eventName = "_interact";
        [Space]
        [SerializeField] private UdonBehaviour[] TargetBehaviours;

        void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad0))
            {
                keybindEnabled = !keybindEnabled;
            }
            if (keybindEnabled)
            {
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    foreach(UdonBehaviour t in TargetBehaviours)
                    {
                        t.SendCustomEvent(eventName);
                    }
                }
            }
        }
    }
}
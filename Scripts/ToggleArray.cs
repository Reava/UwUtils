using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Toggle Array")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ToggleArray : UdonSharpBehaviour
    {
        [Header("List of objects to invert the state of")]
        public GameObject[] toggleObjects;

        public override void Interact() => _InvertState();

        public void _Enable()
        {
            foreach (GameObject toggleObject in toggleObjects)
            {
                toggleObject.SetActive(true);
            }
        }

        public void _Disable()
        {
            foreach (GameObject toggleObject in toggleObjects)
            {
                toggleObject.SetActive(false);
            }
        }

        public void _InvertState()
        {
            foreach (GameObject toggleObject in toggleObjects)
            {
                toggleObject.SetActive(!toggleObject.activeSelf);
            }
        }

        public void _controlledChange()
        {
            _InvertState();
        }
    }
}
using UnityEngine;
using UdonSharp;
using UnityEngine.InputSystem;
using VRC.SDK3.Persistence;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Collider Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ColliderToggle : UdonSharpBehaviour
    {
        [Header("List of colliders to toggle")]
        [SerializeField] private Collider[] toggleCols;
        public bool setDefaultOnStart = false;
        public bool defaultState = true;

        private bool isDefault = true;

        void Start()
        {
            if (defaultState)
            {
                _SetOn();
            }
            else
            {
                _SetOff();
            }
        }

        public override void Interact()
        {
            _Toggle();
        }
        public override void OnPickupUseDown()
        {
            Interact();
        }

        public void _Toggle()
        {
            foreach (Collider col in toggleCols)
            {
                col.enabled = !col.enabled;
            }
            isDefault = !isDefault;
        }
        public void _SetOn()
        {
            foreach (Collider col in toggleCols)
            {
                col.enabled = true;
            }
            isDefault = !isDefault;
        }
        public void _SetOff()
        {
            foreach (Collider col in toggleCols)
            {
                col.enabled = false;
            }
            isDefault = !isDefault;
        }
    }
}
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Pickupable Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PickupableToggle : UdonSharpBehaviour
    {
        [Header("References")]
        [SerializeField] private VRCPickup pickup;

        [Space, Header("Settings")]
        [SerializeField] private bool setStateOnStart = false;
        [SerializeField] private bool defaultState = true;

        [Header("Debug")]
        [Tooltip("Support will only be given if logging is enabled.")]
        [SerializeField] private bool enableLogging = true;

        private bool isSyncedPickup = false;
        private bool currentState = true;

        private void Start()
        {
            if (!Utilities.IsValid(pickup))
            {
                if (gameObject.GetComponent<VRCPickup>() != null)
                {
                    pickup = gameObject.GetComponent<VRCPickup>();
                }
            }
            else
            {
                Debug.LogError($"[Reava_/UwUtils/PickupableToggle.cs] No Pickup found, disabling self. Check setup for object \"{gameObject.name}\"", gameObject);
                this.enabled = false;
                return;
            }
        }

        public void _Toggle()
        {
            pickup.pickupable = !pickup.pickupable;
        }

        public void _Enable()
        {
            pickup.pickupable = true;
        }

        public void _Disable()
        {
            pickup.pickupable = false;
        }

        public override void Interact() => _Toggle();
    }
}
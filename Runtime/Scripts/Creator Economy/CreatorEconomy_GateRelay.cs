using UdonSharp;
using UnityEngine;
using VRC.Economy;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Creator Economy Gate Relay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CreatorEconomy_GateRelay : UdonSharpBehaviour
    {
        [Header("Settings")]
        [Tooltip("Only enable this for testing")]
        [SerializeField] private UdonProduct product;

        [Space, SerializeField] private bool openStoreIfNotOwned = true;
        [SerializeField] private string listingID = "";

        [Header("Purchased Event Relays")]
        [Tooltip("ProgramsRelays and EventNames MUST match in array sizes!")]
        [SerializeField] private UdonBehaviour[] PurchasedProgramRelays;
        [Tooltip("ProgramsRelays and EventNames MUST match in array sizes!")]
        [SerializeField] private string[] PurchasedEventNames;
        [Header("Not Owned Event Relays")]
        [Tooltip("If enabled, this will send out events on start (Useful for seeing the enabled state in editor but defaulting to off ingame)")]
        [SerializeField] private bool relayNotOwnedEventsOnStart = false;
        [Tooltip("ProgramsRelays and EventNames MUST match in array sizes!")]
        [SerializeField] private UdonBehaviour[] NotOwnedProgramRelays;
        [Tooltip("ProgramsRelays and EventNames MUST match in array sizes!")]
        [SerializeField] private string[] NotOwnedEventNames;

        [Header("Debug")]
        [Tooltip("Only enable this for testing")]
        [SerializeField] public bool isOwned = false;
        [Tooltip("Support will only be given if logging is enabled.")]
        [SerializeField] private bool enableLogging = true;

        private void Start()
        {
            if(!Utilities.IsValid(product))
            {
                this.enabled = false;
                Debug.LogError("[Reava_/UwUtils/CreatorEconomy_GateRelay.cs] No UdonProduct Specified, disabling self", gameObject);
                return;
            }

            if (relayNotOwnedEventsOnStart)
            {
                for (int i = 0; i < NotOwnedProgramRelays.Length; i++)
                {
                    NotOwnedProgramRelays[i].SendCustomEvent(NotOwnedEventNames[i]);
                }
            }
        }

        public void _OpenListing()
        {
            Store.OpenListing(listingID);
        }

        public override void OnPurchaseConfirmed(IProduct eventProduct, VRCPlayerApi player, bool purchased)
        {
            if (!player.isLocal || product == null) return;
            if (eventProduct.ID != product.ID) return;

            isOwned = true;
        }

        public override void OnPurchaseExpired(IProduct eventProduct, VRCPlayerApi player)
        {
            if (!player.isLocal || product == null) return;
            if (eventProduct.ID != product.ID) return;

            isOwned = false;
        }

        public void _Interact()
        {
            if (isOwned)
            {
                for (int i = 0; i < PurchasedProgramRelays.Length; i++)
                {
                    PurchasedProgramRelays[i].SendCustomEvent(PurchasedEventNames[i]);
                }
            }
            else
            {
                if (openStoreIfNotOwned) _OpenListing();
                for (int i = 0; i < NotOwnedProgramRelays.Length; i++)
                {
                    NotOwnedProgramRelays[i].SendCustomEvent(NotOwnedEventNames[i]);
                }
            }
        }
    }
}

using UdonSharp;
using UnityEngine;
using VRC.Economy;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Creator Economy Exclusive Teleport")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CreatorEconomy_ExclusiveTeleport : UdonSharpBehaviour
    {
        [Header("Settings")]
        [SerializeField] private UdonProduct product;

        [Space, SerializeField] private bool openStoreIfNotOwned = true;
        [SerializeField] private string listingID = "";

        [Space, SerializeField] private bool teleportFallbacks = true;

        [Space]
        [Header("Locations")]
        [SerializeField] private Transform TeleportLocation;
        [Tooltip("If user does not own the produt, they get teleported to this location, if none will respawn them")]
        [SerializeField] private Transform FallbackTeleportLocation;

        [Header("Debug")]
        [SerializeField] private bool AllowAllUsers = false;
        [Tooltip("Only enable this for testing")]
        [SerializeField] public bool isOwned = false;
        [Tooltip("Support will only be given if logging is enabled.")]
        [SerializeField] private bool enableLogging = true; 

        public void _OpenListing()
        {
            Store.OpenListing(listingID);
        }

        private void Start()
        {
            if (!Utilities.IsValid(product))
            {
                Debug.LogError("[Reava_/UwUtils/CreatorEconomy_ExclusiveZone.cs] No UdonProduct Specified, disabling self", gameObject);
                this.enabled = false;
                return;
            }
        }

        public override void OnPurchaseConfirmed(IProduct eventProduct, VRCPlayerApi player, bool purchased)
        {
            if (!player.isLocal || product == null) return;
            if (eventProduct.ID != product.ID) return;

            if (enableLogging) Debug.Log("[Reava_/UwUtils/CreatorEconomy_ExclusiveZone.cs]: Purchase confirmed " + gameObject.name, gameObject);

            isOwned = true;
        }

        public override void OnPurchaseExpired(IProduct eventProduct, VRCPlayerApi player)
        {
            if (!player.isLocal || product == null) return;
            if (eventProduct.ID != product.ID) return;

            isOwned = false;
        }

        public override void Interact()
        {
            if (isOwned || AllowAllUsers)
            {
                if (Utilities.IsValid(TeleportLocation))
                {
                    Networking.LocalPlayer.TeleportTo(TeleportLocation.position, TeleportLocation.rotation);
                }
            }
            else
            {
                if (openStoreIfNotOwned) _OpenListing();
                if (!teleportFallbacks) return;

                if (Utilities.IsValid(TeleportLocation))
                {
                    Networking.LocalPlayer.TeleportTo(FallbackTeleportLocation.position, FallbackTeleportLocation.rotation);
                }
                else
                {
                    Networking.LocalPlayer.Respawn();
                }
            }
            
        }

        public override void OnPickupUseDown()
        {
            Interact();
        }
    }
}
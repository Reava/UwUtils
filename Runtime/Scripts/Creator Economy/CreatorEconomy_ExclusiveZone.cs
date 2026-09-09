using UdonSharp;
using UnityEngine;
using VRC.Economy;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Creator Economy Exclusive Zone")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CreatorEconomy_ExclusiveZone : UdonSharpBehaviour
    {
        [Header("Settings")]
        [SerializeField] private UdonProduct product;
        [Space, Tooltip("If user does not own the produt, they get teleported to this location, if none will respawn them")]
        [SerializeField] private Transform ForcedExitLocation;
        [SerializeField] private bool AllowAllUsers = false;

        [Header("Debug")]
        [Tooltip("Only enable this for testing")]
        [SerializeField] public bool isOwned = false;
        [Tooltip("Support will only be given if logging is enabled.")]
        [SerializeField] private bool enableLogging = true;

        private void Start()
        {
            if (!Utilities.IsValid(product))
            {
                this.enabled = false;
                Debug.LogError("[Reava_/UwUtils/CreatorEconomy_ExclusiveZone.cs] No UdonProduct Specified, disabling self", gameObject);
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

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if (player != Networking.LocalPlayer) return;
            if (enableLogging) Debug.Log($"[Reava_/UwUtils/CreatorEconomy_ExclusiveZone.cs]: Player \"{Networking.LocalPlayer.displayName}\" entered " + gameObject.name, gameObject);

            if (!isOwned && !AllowAllUsers)
            {
                if (Utilities.IsValid(ForcedExitLocation))
                {
                    Networking.LocalPlayer.TeleportTo(ForcedExitLocation.position, ForcedExitLocation.rotation);
                }
                else
                {
                    Networking.LocalPlayer.Respawn();
                }
                
            }
        }
    }
}
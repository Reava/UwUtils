using UdonSharp;
using UnityEngine;
using VRC.Economy;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/CE Product Relay")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CreatorEconomyProductRelay : UdonSharpBehaviour
    {
        [Header("Settings")]
        [Tooltip("Only enable this for testing")]
        [SerializeField] public bool isOwned = false;
        [SerializeField] private UdonProduct product;

        [Header("Purchased Product Event Relays")]
        [Tooltip("ProgramsRelays and EventNames MUST match in array sizes!")]
        [SerializeField] private UdonBehaviour[] PurchasedProgramRelays;
        [Tooltip("ProgramsRelays and EventNames MUST match in array sizes!")]
        [SerializeField] private string[] PurchasedEventNames;
        [Header("Expired Product Event Relays")]
        [Tooltip("If enabled, this will send out experied events on start")]
        [SerializeField] private bool relayExpiredOnStart = false;
        [Tooltip("ProgramsRelays and EventNames MUST match in array sizes!")]
        [SerializeField] private UdonBehaviour[] ExpiredProgramRelays;
        [Tooltip("ProgramsRelays and EventNames MUST match in array sizes!")]
        [SerializeField] private string[] ExpiredEventNames;

        private void Start()
        {
            if(!Utilities.IsValid(product))
            {
                this.enabled = false;
                Debug.LogError("[Reava_/UwUtils/CreatorEconomyProductRelay.cs] No UdonProduct Specified, disabling self", gameObject);
                return;
            }

            if (relayExpiredOnStart)
            {
                for (int i = 0; i < ExpiredProgramRelays.Length; i++)
                {
                    ExpiredProgramRelays[i].SendCustomEvent(ExpiredEventNames[i]);
                }
            }
        }

        public override void OnPurchaseConfirmed(IProduct eventProduct, VRCPlayerApi player, bool purchased)
        {
            if (!player.isLocal || product == null) return;
            if (eventProduct.ID != product.ID) return;

            isOwned = true;

            for (int i = 0; i < PurchasedProgramRelays.Length; i++)
            {
                PurchasedProgramRelays[i].SendCustomEvent(PurchasedEventNames[i]);
            }
        }

        public override void OnPurchaseExpired(IProduct eventProduct, VRCPlayerApi player)
        {
            if (!player.isLocal || product == null) return;
            if (eventProduct.ID != product.ID) return;

            isOwned = false;

            for (int i = 0; i < ExpiredProgramRelays.Length; i++)
            {
                ExpiredProgramRelays[i].SendCustomEvent(ExpiredEventNames[i]);
            }
        }
    }
}

using UdonSharp;
using UnityEngine;
using VRC.Economy;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Open Store Listing")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class OpenStoreListing : UdonSharpBehaviour
    {
        public string listingID = "";

        public void _OpenListing()
        {
            Store.OpenListing(listingID);
        }

        public override void Interact()
        {
            Store.OpenListing(listingID);
        }

        public override void OnPickupUseDown()
        {
            Store.OpenListing(listingID);
        }

        public void _OpenStore()
        {
            Store.OpenListing(listingID);
        }
    }
}

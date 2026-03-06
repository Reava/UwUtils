using UdonSharp;
using UnityEngine;
using VRC.Economy;
using VRC.SDKBase;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Open Group Page or Store")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class OpenGroupPage : UdonSharpBehaviour
    {
        [Tooltip("If false, opens the Group Page, if true, opens the Group Store")]
        public bool isStore = false;
        [SerializeField] private string groupId = "Group ID, NOT URL";

        public override void Interact()
        {
            if (isStore) {
                _OpenGroupStore();
            }
            else
            {
                _OpenGroupPage();
            }
        }

        public override void OnPickupUseDown()
        {
            if (isStore)
            {
                _OpenGroupStore();
            }
            else
            {
                _OpenGroupPage();
            }
        }

        public void _OpenGroupStore()
        {
            if(groupId == "" || groupId == "Group ID, NOT URL") return;
            Store.OpenGroupStorePage(groupId);
        }

        public void _OpenGroupPage()
        {
            if (groupId == "" || groupId == "Group ID, NOT URL") return;
            Store.OpenGroupPage(groupId);
        }
    }
}

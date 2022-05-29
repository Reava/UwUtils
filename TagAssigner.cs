using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
namespace UdonSharp.Examples.Utilities
{
    public class TagAssigner : UdonSharpBehaviour
    {
        [Tooltip("Name of the tag")]
        public string playerTag;
        [Tooltip("List of users who will inherit the tag")]
        public string[] userArray;
        [Tooltip("List of objects to toggle ON for VIPs")]
        public GameObject[] toggleObjectsON;
        [Tooltip("List of objects to toggle OFF for VIPs")]
        public GameObject[] toggleObjectsOFF;
        public bool tpPlayerOnJoin = true;
        public Transform tpLocation;
        void Start()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            for (int i = 0; i < userArray.Length; i++)
            {
                if (userArray[i] == localPlayer.displayName)
                {
                    localPlayer.SetPlayerTag("rank", playerTag);
                    if (tpPlayerOnJoin)
                    {
                        Networking.LocalPlayer.TeleportTo(tpLocation.position, tpLocation.rotation);
                    }
                    foreach (GameObject toggleObjectON in toggleObjectsON)
                    {
                        toggleObjectON.SetActive(true);
                    }
                    foreach (GameObject toggleObjectOFF in toggleObjectsOFF)
                    {
                        toggleObjectOFF.SetActive(false);
                    }
                    break;
                }
                else
                {
                    localPlayer.SetPlayerTag("rank", "Visitor");
                }
            }
        }
        public void _updateState()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            localPlayer.SetPlayerTag("rank", playerTag);
            foreach (GameObject toggleObject in toggleObjectsON)
            {
                toggleObject.SetActive(true);
            }
            foreach (GameObject toggleObject in toggleObjectsOFF)
            {
                toggleObject.SetActive(false);
            }
        }
    }
}

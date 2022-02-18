using UnityEngine;
using UdonSharp;
using VRC.SDKBase;

[UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)]
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
    public bool tpPlayerOnJoin = false;
    public Transform tpLocation;
    void Start()
    {
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        for (int i = 0; i < userArray.Length; i++)
        {
            if (userArray[i] == localPlayer.displayName)
            {
                localPlayer.SetPlayerTag("rank", playerTag);
                foreach (GameObject toggleObjectON in toggleObjectsON)
                {
                    toggleObjectON.SetActive(true);
                }
                foreach (GameObject toggleObjectOFF in toggleObjectsOFF)
                {
                    toggleObjectOFF.SetActive(false);
                }
                if (tpPlayerOnJoin) Networking.LocalPlayer.TeleportTo(tpLocation.position, tpLocation.rotation);
                break;
            }
            else
            {
                localPlayer.SetPlayerTag("rank", "Visitor");
            }
        }
    }
}

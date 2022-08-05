using UnityEngine;
using VRC.SDKBase;
using UdonSharp;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class TagAssigner : UdonSharpBehaviour
{
    [Tooltip("Name of the tag")]
    [SerializeField] private string playerTag;
    [Tooltip("List of users who will inherit the tag")]
    [SerializeField] private string[] userArray;
    [Tooltip("List of objects to toggle ON for VIPs")]
    [SerializeField] private GameObject[] toggleObjectsON;
    [Tooltip("List of objects to toggle OFF for VIPs")]
    [SerializeField] private GameObject[] toggleObjectsOFF;
    [SerializeField] private bool tpPlayerOnJoin = true;
    [SerializeField] private Transform tpLocation;
    private float delay = 0.2f;
    private bool abort;
    void Start()
    {
        if (playerTag == null)
        {
            abort = true;
            SendCustomEventDelayedSeconds(nameof(_sendDebugError), 1f);
            return;
        }
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        for (int i = 0; i < userArray.Length; i++)
        {
            if (userArray[i] == localPlayer.displayName)
            {
                localPlayer.SetPlayerTag("rank", playerTag);
                if (tpPlayerOnJoin)
                {
                    SendCustomEventDelayedSeconds(nameof(_initTeleport), delay);
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
                if(Networking.LocalPlayer.GetPlayerTag("rank") == null)
                {
                    localPlayer.SetPlayerTag("rank", "Visitor");
                }
            }
        }
    }
    public void _updateState()
    {
        if (abort) return;
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

    public void _initTeleport()
    {
        if (abort) return;
        Networking.LocalPlayer.TeleportTo(tpLocation.position, tpLocation.rotation);
    }

    public void _sendDebugError() => Debug.LogError("Reava_UwUtils:<color=red> <b>Invalid values</b></color>, no User / Tag found. (" + gameObject + ")", gameObject);
}
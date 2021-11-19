using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDK3.Components;
using VRC.SDKBase;

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
    void Start()
    {
        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        for (int i = 0; i < userArray.Length; i++)
        {
            if (userArray[i] == localPlayer.displayName)
            {
                localPlayer.SetPlayerTag("rank", playerTag);
                foreach (GameObject toggleObject in toggleObjectsON)
                {
                    toggleObject.SetActive(true);
                }
                foreach (GameObject toggleObject in toggleObjectsOFF)
                {
                    toggleObject.SetActive(false);
                }
                break;
            }
            else
            {
                localPlayer.SetPlayerTag("rank", "Visitor");
                break;
            }
        }
    }
}

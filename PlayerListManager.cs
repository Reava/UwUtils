using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UdonSharp;
using VRC.SDK3.Components;
using VRC.SDKBase;
using UnityEngine.UI;
using TMPro;

public class PlayerListManager : UdonSharpBehaviour
{
    private string[] listedPlayers;
    private int[] playerIds;
    public GameObject VisitCountDisplay;
    public GameObject InstanceMasterDisplay;
    public GameObject SelectedUserDisplay;
    public GameObject ActionsParent;
    public GameObject Playerlist_Containers;
    private string InstanceMaster;
    private string SelectedUserName;
    private int visitCount = 0;
    public string default_Tag = "Visitor";
    public string VIP_Tag = "VIP";
    public string Admin_Tag = "Admin";
    public Color Default_color = new Color(1, 1, 1);
    public Color VIP_color = new Color(1, 1, 0);
    public Color Admin_color = new Color(1, 0, 0);
    private int[] playersIDs = new int[82];

    VRCPlayerApi[] players = new VRCPlayerApi[82];

    public void Start()
    {
        UpdateVisitCount();
        /*TextMeshPro TMPcField = VisitCountDisplay.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>();
        TMPcField.SetText("Visits: " + visitCount);*/

        /*for (int i = 0; i < UI_containers.Length; i++)
        {
            UI_containers[i].transform.getChild(0).gameObject.getChild(0).gameObject.GetComponent<Text>().text = "" + visitCount;
        }*/
    }

    public void UpdateVisitCount()
    {
        //VisitCountDisplay.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Visits: " + visitCount;
        VisitCountDisplay.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Visits: " + visitCount;
    }

    public void OnPlayerJoined(VRCPlayerApi player)
    {
        VRCPlayerApi.GetPlayers(players);
        visitCount = visitCount + 1;
        UpdateVisitCount();
        /*foreach (VRCPlayerApi playerD in players)
        {
            if (playerD == null)
            {
                foreach (string listedPlayer in listedPlayers)
                {
                    if (playerD.displayName == listedPlayer)
                    {

                    }
                }
                Debug.Log(player.displayName);
            }
        }*/
    }
}

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
    private string[] listedPlayers = null;
    private int[] playersIDs = null;
    private int selectedUser = 0;
    private string InstanceMaster = "null";
    private string SelectedUserName = "null";
    public GameObject VisitCountDisplay;
    public GameObject CurrentPlayerCount;
    public GameObject InstanceMasterDisplay;
    public GameObject SelectedUserDisplay;
    public GameObject ActionsParent;
    public GameObject PlayerlistContainers;
    [UdonSynced] private int visitCount = 0;
    public string default_Tag = "Visitor";
    public string VIP_Tag = "VIP";
    public string Admin_Tag = "Admin";
    public Color Default_color = new Color(1, 1, 1);
    public Color VIP_color = new Color(1, 1, 0);
    public Color Admin_color = new Color(1, 0, 0);

    //VRCPlayerApi[] currentPlayers;
    private void InitializeIdsIfNull()
    {
        Debug.Log("Init IDs");
        if (playersIDs == null)
        {
            playersIDs = new int[80];
            listedPlayers = new string[80];
            for (int i = 0; i < playersIDs.Length; i++)
            {
                // Assuming that the player ID does not contain -1, leave -1 blank. 
                playersIDs[i] = -1;
                listedPlayers[i] = "";
            }
        }
    }

    public void Start()
    {
        InitializeIdsIfNull();
        Debug.Log("start");
        VisitCountDisplay.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Visits: " + visitCount;
        InstanceMasterDisplay.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Loading..";
        SelectedUserDisplay.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Loading..";
        SelectedUserDisplay.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = VIP_color;

        UpdatePlayerList();
        //currentPlayers = VRCPlayerApi.GetPlayers(currentPlayers);

        //first try method, using playerList from VRCApi player list
        /*for (int i = 0; i < currentPlayers.Length; i++)
        {
            if (currentPlayers != null)
            {
                listedPlayers[i] = currentPlayers[i].displayName;
                playersIDs[i] = i;
                if (currentPlayers[i].isMaster)
                {
                    InstanceMaster = currentPlayers[i].displayName;
                    Debug.Log("master is: " + InstanceMaster);
                }
                PlayerlistContainers.transform.GetChild(0+i).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listedPlayers[i];
            }
            //Debug.Log(player.displayName);
        }*/
    }
    
    public void UpdatePlayerList()
    {
        Debug.Log("Entering UpdatePlayerList Function");
        for (int i = 0; i < playersIDs.Length; i++)
        {
            Debug.Log("entering loop");
            if (playersIDs[i] != -1)
            {
                Debug.Log("player id != -1");
                var player = VRCPlayerApi.GetPlayerById(playersIDs[i]);
                listedPlayers[i] = player.displayName;
                if (player.isMaster)
                {
                    InstanceMaster = player.displayName;
                    Debug.Log("Found instance master");
                }
                PlayerlistContainers.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.displayName;
                string playerRank = player.GetPlayerTag("rank");
                if (playerRank == Admin_Tag)
                {
                    PlayerlistContainers.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Admin_color;
                }
                if (playerRank == VIP_Tag)
                {
                    PlayerlistContainers.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = VIP_color;
                }
                if (playerRank == default_Tag)
                {
                    PlayerlistContainers.transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Default_color;
                }
                Debug.Log(string.Format("id: {0}, name: {1} \r \n ", player.playerId.ToString(), player.displayName));
            }
        }
        Debug.Log("Exited loop");
        VisitCountDisplay.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Visits: " + visitCount;
        InstanceMasterDisplay.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = InstanceMaster;
        SelectedUserDisplay.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = listedPlayers[selectedUser];
    }
    
    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        Debug.Log("Player Join");
        visitCount = visitCount + 1;
        CurrentPlayerCount.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Players: " + VRCPlayerApi.GetPlayerCount();
        InitializeIdsIfNull();

        for (int i = 0; i < playersIDs.Length; i++)
        {
            if (playersIDs[i] == -1)
            {
                playersIDs[i] = player.playerId;
                break;
            }
        }

        UpdatePlayerList();
    }

    public override void OnPlayerLeft(VRCPlayerApi player)
    {
        Debug.Log("Player leave");
        InitializeIdsIfNull();

        for (int i = 0; i < playersIDs.Length; i++)
        {
            if (playersIDs[i] == player.playerId)
            {
                playersIDs[i] = -1;
                break;
            }
        }

        UpdatePlayerList();
    }
}

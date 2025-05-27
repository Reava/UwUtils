using UnityEngine;
using UdonSharp;
using TMPro;
using UnityEngine.UI;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using YamlDotNet.Core.Tokens;
using VRC.Udon.UAssembly.Common;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net.Configuration;
using System;
using UwUtils;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Collection System")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CollectionSystem : UdonSharpBehaviour
    {
        [Space]
        [Tooltip("Default balance. Making this above 0 with persistence on will act as a join reward, adding to the user's balance every time they join the world!")]
        [SerializeField] private int balance = 0;
        [Space]
        [SerializeField] private bool outputToText = true;
        [SerializeField] private string textPrefix = "Balance: ";
        [SerializeField] private GameObject[] textFieldsBalance;
        [SerializeField] private GameObject[] textFieldsTotal;
        [SerializeField] private GameObject[] textFieldsRemaining;
        [Space]
        [Header("Unlockables Section. ARRAYS LENGTHS MUST MATCH")]
        [SerializeField] private bool enableRewardTiers = false;
        [SerializeField] private int[] tierValues;
        [Tooltip("Upon Unlocking a tier, toggles objects matching of the tier.")]
        [SerializeField] private GameObject[] unlockableTierObjects;
        [Tooltip("Upon Recovered, will only trigger the highest tier unlocked!")]
        [SerializeField] private bool relayOnRecovered = true;
        [Tooltip("Upon Unlocking a tier, sends an event to those behaviors. Reference a relay to send a single tier event to multiple scripts!")]
        [SerializeField] private UdonBehaviour[] relayPerTier;
        [SerializeField] private string eventName = "_interact";
        [Space]
        [Header("Keep balance after rejoin ?")]
        [SerializeField] private bool persistent = false;
        [Header("Unique persistence IDs"), Tooltip("Make sure you use a unique parameter.")]
        [SerializeField] private string persistenceParameter = "UwUtils_CollectibleSystem_Balance";
        [Space]
        [Tooltip("If logging is disabled, no support will be given.")]
        [SerializeField] private bool enableLogging = true;

        private int debugTotalValue = 0; //Keep track of total collectible value in the world

        void Start()
        {
            if (outputToText && textFieldsBalance == null) { outputToText = false; Debug.LogError("[Reava_/UwUtils/CollectionSystem.cs]: Ouput to text enabled but no text fields found, disabling feature on " + gameObject.name, gameObject); }
            debugTotalValue = 0;
            if(tierValues.Length > 0)
            {
                // Array.Sort(tierValues); // I would love to use it if VRChat allowed us to so users don't use random order :))
                // Array.Reverse(tierValues);
            }
            if (enableLogging) SendCustomEventDelayedSeconds(nameof(_maxValueDebug), 5f);
        }

        public void _totalValueDebug(int valuePerCollectible)
        {
            debugTotalValue += valuePerCollectible;
        }

        public void _collectValue(int CollectibleValue)
        {
            if (enableLogging) _debugLog("Received new collectible worth " + CollectibleValue + "! New balance is: " + balance);
            balance += CollectibleValue;
            
        }

        public void _updateBalance()
        {
            if (outputToText) _printValue();
            if (persistent) PlayerData.SetInt(persistenceParameter, balance);
            if (!enableRewardTiers) return;
            for (int i = 0; i < tierValues.Length; i++)
            {
                if(balance >= tierValues[i])
                {
                    if (unlockableTierObjects[i]) unlockableTierObjects[i].SetActive(!unlockableTierObjects[i].activeSelf);
                    if (relayPerTier[i]) relayPerTier[i].SendCustomEvent(eventName);
                }
            }
        }

        public void _printValue()
        {
            foreach (GameObject field in textFieldsBalance)
            {
                if (!field) continue;
                if (field.GetComponent<TextMeshProUGUI>() != null) { field.GetComponent<TextMeshProUGUI>().text = textPrefix + balance; continue; }
                if (field.GetComponent<TextMeshPro>() != null) { field.GetComponent<TextMeshPro>().text = textPrefix + balance; continue; }
                if (field.GetComponent<Text>() != null) { field.GetComponent<Text>().text = textPrefix + balance; continue; }
            }
            foreach (GameObject field in textFieldsTotal)
            {
                if (!field) continue;
                if (field.GetComponent<TextMeshProUGUI>() != null) { field.GetComponent<TextMeshProUGUI>().text = "" + debugTotalValue; continue; }
                if (field.GetComponent<TextMeshPro>() != null) { field.GetComponent<TextMeshPro>().text = "" + debugTotalValue; continue; }
                if (field.GetComponent<Text>() != null) { field.GetComponent<Text>().text = "" + debugTotalValue; continue; }
            }
            foreach (GameObject field in textFieldsRemaining)
            {
                if (!field) continue;
                if (field.GetComponent<TextMeshProUGUI>() != null) { field.GetComponent<TextMeshProUGUI>().text = ""+(debugTotalValue - balance); continue; }
                if (field.GetComponent<TextMeshPro>() != null) { field.GetComponent<TextMeshPro>().text = "" + (debugTotalValue - balance); continue; }
                if (field.GetComponent<Text>() != null) { field.GetComponent<Text>().text = "" + (debugTotalValue - balance); continue; }
            }
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (persistent)
            {
                if (Networking.LocalPlayer != player) return;
                if (!PlayerData.HasKey(player, persistenceParameter)) return;
                if (PlayerData.GetType(player, persistenceParameter) != typeof(int)) return;
                int recoveredBalance = PlayerData.GetInt(player, persistenceParameter);
                if (enableLogging) _debugLog("Recovered balance of : " + recoveredBalance + "");
                if (recoveredBalance > 0) balance = recoveredBalance;
                if (outputToText) _printValue();
            }
        }


        public void _debugLog(string reason)
        {
            if (enableLogging) Debug.Log("[Reava_/UwUtils/CollectionSystem.cs]: " + reason + " | Logged by: " + gameObject.name, gameObject);
        }

        private void _maxValueDebug() => Debug.Log("[Reava_/UwUtils/CollectionSystem.cs]: " + gameObject.name + " received total points available: " + debugTotalValue, gameObject);
    }
}
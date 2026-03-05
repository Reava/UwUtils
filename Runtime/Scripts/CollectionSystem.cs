using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Collection System")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CollectionSystem : UdonSharpBehaviour
    {
        [Space]
        [Tooltip("Default balance")]
        [SerializeField] private int balance = 0;
        [Space]
        [SerializeField] private bool outputToText = true;
        [SerializeField] private bool ShowMax = true;
        [SerializeField] private string textPrefix = "Balance: ";
        [SerializeField] private Text[] textFieldsBalanceUnity;
        [SerializeField] private TextMeshPro[] textFieldsBalanceTMPMesh;
        [SerializeField] private TextMeshProUGUI[] textFieldsBalanceTMPUI;
        [Space]
        [Header("Unlockables Section. ARRAYS LENGTHS MUST MATCH")]
        [SerializeField] private bool enableRewardTiers = false;
        [Tooltip("Please use incremental values. (Example: 5,10,25)")]
        [SerializeField] private int[] tierValues;
        [Tooltip("Upon Unlocking a tier, toggles objects matching of the tier.")]
        [SerializeField] private GameObject[] unlockableTierObjects;
        [Tooltip("Upon Unlocking a tier, sends an event to those behaviors. Reference a relay to send a single tier event to multiple scripts!")]
        [SerializeField] private UdonBehaviour[] relayPerTier;
        [SerializeField] private string[] eventNames;
        [Header("Completion Rewards")]
        [SerializeField] private UdonBehaviour[] CompletionRelays;
        [SerializeField] private string[] CompletionEventNames;
        [SerializeField] private GameObject[] CompletionRewardsOn;
        [SerializeField] private GameObject[] CompletionRewardsOff;
        [Space]
        [Tooltip("If logging is disabled, no support will be given.")]
        [SerializeField] private bool enableLogging = true;

        private int debugTotalValue = 0; //Keep track of total collectible value in the world

        void Start()
        {
            if (outputToText && (textFieldsBalanceUnity.Length == 0 && textFieldsBalanceTMPUI.Length == 0 && textFieldsBalanceTMPMesh.Length == 0)) {
                outputToText = false; Debug.LogError("[Reava_/UwUtils/CollectionSystem.cs]: Ouput to text enabled but no text fields found, disabling feature on " + gameObject.name, gameObject);
            }

            debugTotalValue = 0 + balance;

            if (enableLogging) SendCustomEventDelayedSeconds(nameof(_maxValueDebug), 5f);
        }

        public void _totalValueDebug(int valuePerCollectible)
        {
            debugTotalValue += valuePerCollectible;
            if (enableLogging) _debugLog("Received new collectible worth " + valuePerCollectible + "! New max is: " + debugTotalValue);
            _printValue();
        }

        public void _collectValue(int CollectibleValue)
        {
            if (balance >= debugTotalValue)
            {
                if (enableLogging) _debugLog("Balance is already " + balance + "! Maximum is: " + debugTotalValue + ". Please make sure collectible disables itself on collection.");
            }

            if (enableLogging) _debugLog("Received new collectible worth " + CollectibleValue + "! New balance is: " + balance);
            balance += CollectibleValue;
            _updateBalance();
        }

        public void _updateBalance()
        {
            if (outputToText) _printValue();

            _CompletionConfirmation(balance);

            if (!enableRewardTiers) return;
            for (int i = 0; i < tierValues.Length; i++)
            {
                if (balance >= tierValues[i])
                {
                    if (unlockableTierObjects[i]) unlockableTierObjects[i].SetActive(!unlockableTierObjects[i].activeSelf);
                    if (relayPerTier[i])
                    {
                        relayPerTier[i].SendCustomEvent(eventNames[i]);
                    }
                }
            }
        }

        public void _CompletionConfirmation(int balance)
        {
            if (balance < debugTotalValue) return;

            foreach (GameObject o in CompletionRewardsOn)
            {
                if (o) o.SetActive(true);
            }
            foreach (GameObject o in CompletionRewardsOff)
            {
                if (o) o.SetActive(false);
            }
            for (int i = 0; i < CompletionRelays.Length && i < CompletionEventNames.Length; i++)
            {
                if (CompletionRelays[i] != null) CompletionRelays[i].SendCustomEvent(CompletionEventNames[i]);
            }

            _debugLog("Completed collection confirmed!");
        }

        public void _printValue()
        {

            foreach (Text field in textFieldsBalanceUnity)
            {
                if (field != null) { field.text = ShowMax ? textPrefix + balance + "/" + debugTotalValue : textPrefix + balance; continue; }
            }
            foreach (TextMeshPro field in textFieldsBalanceTMPMesh)
            {
                if (field != null) { field.text = ShowMax ? textPrefix + balance + "/" + debugTotalValue : textPrefix + balance; continue; }
            }
            foreach (TextMeshProUGUI field in textFieldsBalanceTMPUI)
            {
                if (field != null) { field.text = ShowMax ? textPrefix + balance + "/" + debugTotalValue : textPrefix + balance; continue; }
            }
        }


        public void _debugLog(string reason)
        {
            if (enableLogging) Debug.Log("[Reava_/UwUtils/CollectionSystem.cs]: " + reason + " | Logged by: " + gameObject.name, gameObject);
        }

        private void _maxValueDebug() => Debug.Log("[Reava_/UwUtils/CollectionSystem.cs]: " + gameObject.name + " received total points available: " + debugTotalValue, gameObject);
    }
}
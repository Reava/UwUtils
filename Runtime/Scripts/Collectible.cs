using UnityEngine;
using UdonSharp;
using UwUtils;
using VRC.Udon;
using VRC.SDK3.Persistence;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Collectible")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Collectible : UdonSharpBehaviour
    {
        [Space]
        [SerializeField] private bool disableSelfOnCollect = true;
        [SerializeField] private CollectionSystem CollectionSystemRef;
        [Header("Objects to enable/disable on collectible collection")]
        [SerializeField] private GameObject[] EnableOnCollect;
        [SerializeField] private GameObject[] DisableOnCollect;
        [Header("Behaviors to relay events to when an object is collected")]
        [SerializeField] string EventName = "_interact";
        [SerializeField] private UdonBehaviour[] EventRelays;
        [SerializeField] private int Value = 10;
        /*
        [Header("Keep collectible state after rejoin ?"), Tooltip("Warning, if you don't enable it on the System manager it will NOT save the points collected.")]
        [SerializeField] private bool persistent = false;
        [Tooltip("Relay events to listed Udon Behaviors when the recovered state from persistence is collected, this helps avoid particle systems and other similar 'on collect' events from happening on join.")]
        [SerializeField] private bool relayOnRecovered = true;
        [Header("Unique persistence ID"),Tooltip("Make sure you use a unique parameter.")]
        [SerializeField] private string persistenceParameter = "Collectible_UNIQUEID";
        */
        [Space]
        [Header("Per collectible debug settings"),Tooltip("If logging is disable, no support will be given.")]
        [SerializeField] private bool enableLogging = true;
        [SerializeField] private bool StartupLogging = false;
        [HideInInspector] public bool collected = false;

        void Start()
        {
            /*if(persistenceParameter == "Collectible_UNIQUEID")
            {
                // do something about the user not following instructions to avoid persistence conflicts
                // >> EDITOR SCRIPT https://docs.unity3d.com/ScriptReference/GlobalObjectId.html 
            }*/
            CollectionSystemRef._totalValueDebug(Value);
            if (enableLogging && StartupLogging) _debugLog("initialized");
        }

        public override void Interact()
        {
            _Collect();
        }

        public void _Collect()
        {
            CollectionSystemRef._collectValue(Value);
            if (enableLogging) _debugLog("claimed");
            collected = true;
            //if(persistent) PlayerData.SetBool(persistenceParameter, true);
            _PostCollection(false);
        }

        public void _PostCollection(bool recovered)
        {
            if (disableSelfOnCollect) this.gameObject.SetActive(false);
            foreach (GameObject o in EnableOnCollect)
            {
                if (o) o.SetActive(true);
            }
            foreach (GameObject o in DisableOnCollect)
            {
                if (o) o.SetActive(false);
            }
            //if (!relayOnRecovered && recovered) return;
            foreach (UdonBehaviour o in EventRelays)
            {
                if (o) o.SendCustomEvent(EventName);
            }
        }

/*        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (persistent)
            {
                if (Networking.LocalPlayer != player) return;
                if (!PlayerData.HasKey(player, persistenceParameter)) return;
                if (PlayerData.GetType(player, persistenceParameter) != typeof(bool)) return;

                bool recoveredState = PlayerData.GetBool(player, persistenceParameter);
                if (enableLogging) _debugLog("recovered persistence state of : " + recoveredState + "");
                if (recoveredState) _PostCollection(true);
            }
        }*/

        public void _debugLog(string reason)
        {
            if (enableLogging) Debug.Log("[Reava_/UwUtils/Collectible.cs]: Collectible " + reason + ", Value: " + Value /*+ ", Peristent: " + persistent*/ + " | Sent to " + CollectionSystemRef + " from:" + gameObject.name, gameObject);
        }
    }
}
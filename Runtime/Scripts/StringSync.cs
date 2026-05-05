using UnityEngine;
using UdonSharp;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using TMPro;
using UnityEngine.UI;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Synced String")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class StringSync : UdonSharpBehaviour
    {
        [Header("References")]
        public TextMeshProUGUI[] displayTextFields;
        public TMP_InputField stringInputField;

        [Header("Settings")]
        public bool allowAutoOwnershipTransfer = true;

        [Header("Persistence Settings")]
        public bool isPersistent = false;
        public string persistenceKey = "UwUtils_SyncedString_";

        [UdonSynced] private string syncedString = "";

        private string persistentString = "";
        private bool _dataLoaded = false;

        void Start()
        {
            _UpdateDisplays();
        }

        public void _SavePersistence()
        {
            persistentString = stringInputField.text;
            PlayerData.SetString(persistenceKey, persistentString);

            Debug.Log($"[Reava_/UwUtils/StringSync.cs] Saving string {persistentString}", this);
        }

        public void _ApplyString()
        {
            if (!Networking.IsOwner(gameObject))
            {
                if (allowAutoOwnershipTransfer)
                {
                    _takeOwnership();
                }
                else
                {
                    return;
                }
            }

            syncedString = stringInputField.text;

            _UpdateDisplays();
            RequestSerialization();

            Debug.Log($"[Reava_/UwUtils/StringSync.cs] Applying new string of {syncedString}", this);
        }

        public void _takeOwnership()
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            Debug.LogWarning($"[Reava_/UwUtils/StringSync.cs] Ownership transfer! New owner: {Networking.LocalPlayer.displayName}", this);
        }

        public void _UpdateDisplays()
        {
            foreach (TextMeshProUGUI textField in displayTextFields)
            {
                if (Utilities.IsValid(textField)) textField.text = syncedString;
            }

            Debug.Log($"[Reava_/UwUtils/StringSync.cs] Updating displays to ({syncedString})", this);
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (!player.isLocal) return;

            if (PlayerData.HasKey(player, persistenceKey))
            {
                persistentString = PlayerData.GetString(player, persistenceKey);
            }

            stringInputField.text = persistentString;

            _dataLoaded = true;

            if (Networking.IsOwner(gameObject))
            {
                syncedString = persistentString;
                RequestSerialization();
            }

            Debug.Log($"[Reava_/UwUtils/StringSync.cs] Persistence value of ({syncedString}) recovered, Owner is: {Networking.GetOwner(gameObject).displayName}", this);
        }

        public override void OnDeserialization()
        {
            _UpdateDisplays();
            Debug.Log($"[Reava_/UwUtils/StringSync.cs] New string ({syncedString}) Owner: {Networking.GetOwner(gameObject).displayName}", this);
        }
    }
}
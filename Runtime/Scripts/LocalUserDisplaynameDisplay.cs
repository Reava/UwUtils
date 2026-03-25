using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Collection System")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LocalUserDisplaynameDisplay : UdonSharpBehaviour
    {
        public TextMeshProUGUI textMeshProUGUI;
        void Start()
        {
            string playerUsername = Networking.LocalPlayer.displayName;
            textMeshProUGUI.text = playerUsername;
        }
    }
}
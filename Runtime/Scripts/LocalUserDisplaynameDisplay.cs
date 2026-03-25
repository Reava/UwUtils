using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Local User Name Display")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class LocalUserDisplaynameDisplay : UdonSharpBehaviour
    {
        public string prefix = "";
        public string suffix = "";
        public TextMeshProUGUI textMeshProUGUI;
        void Start()
        {
            string playerUsername = Networking.LocalPlayer.displayName;
            textMeshProUGUI.text = prefix + playerUsername + suffix;
        }
    }
}
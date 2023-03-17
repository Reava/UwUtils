using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using TMPro;
using UnityEngine.UI;
using VRC.SDK3.StringLoading;
using VRC.Udon.Common.Interfaces;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/RemoteStringToText")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class RemoteStringToText : UdonSharpBehaviour
    {
        [Space]
        [SerializeField] public VRCUrl linkToString;
        [Space]
        [Header("Output to text?")]
        [SerializeField] private bool textOutput = false;
        [SerializeField] private bool usingTMP = false;
        [Header("Is the TMP text GUI or classic ?")]
        [SerializeField] private bool GUI_Text = false;
        [SerializeField] private GameObject Text_Display;
        [Space]
        [SerializeField] private bool SplitIntoArray = false;
        [Tooltip("Character to use to split the string with")]
        [SerializeField] private char SplitStringWithCharacter = ',';
        [HideInInspector] public string[] strArr;
        [Space]
        [Header("Output to logs?")]
        [SerializeField] private bool debugEnabled = true;
        private string loadedString;
        private void Start()
        {
            _LoadUrl();
        }

        public void _LoadUrl()
        {
            VRCStringDownloader.LoadUrl(linkToString, (IUdonEventReceiver)this);
        }

        public override void Interact()
        {
            _LoadUrl();
        }

        public override void OnStringLoadSuccess(IVRCStringDownload result)
        {
            loadedString += result.Result;
            if (SplitIntoArray)
            {
                strArr = loadedString.Split(SplitStringWithCharacter);
            }
            if (textOutput) _printToText();
            if (debugEnabled) Debug.Log("[Reava_/UwUtils/RemoteStringToText.cs]: String successfully loaded: " + loadedString + "On: " + gameObject.name, gameObject);
        }
        public override void OnStringLoadError(IVRCStringDownload result)
        {
            Debug.LogError("[Reava_/UwUtils/RemoteStringToText.cs]: String loading failed: " + result.Error + "| Error Code: " + result.ErrorCode + "On: " + gameObject.name, gameObject);
        }

        public void _printToText()
        {
            if (usingTMP)
            {
                if (GUI_Text)
                {
                    Text_Display.GetComponent<TextMeshProUGUI>().text = loadedString;
                }
                else
                {
                    Text_Display.GetComponent<TextMeshPro>().text = loadedString;
                }
            }
            else
            {
                Text_Display.GetComponent<Text>().text = loadedString;
            }
        }
    }
}
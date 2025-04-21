using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
using TMPro;
using UnityEngine.UI;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/TagDebugger")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class TagDebugger : UdonSharpBehaviour
    {
        private string playerTag;
        [Header("interacting with this behavior will update the display & log.")]
        [SerializeField] private GameObject Text_Display;
        [Header("Output to text?")]
        [SerializeField] private bool textOutput = false;
        [SerializeField] private bool usingTMP = false;
        [Header("Is the TMP text GUI or classic ?")]
        [SerializeField] private bool GUI_Text = false;
        [Header("Output debug logs?")]
        [SerializeField] private bool debugEnabled = true;
        void Start()
        {
            _updateDebugDisplay();
        }

        public override void Interact()
        {
            _updateDebugDisplay();
        }

        public void _updateDebugDisplay()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (Networking.LocalPlayer.GetPlayerTag("rank") != null)
            {
                var playerTag = Networking.LocalPlayer.GetPlayerTag("rank");
                if (debugEnabled)
                {
                    Debug.Log(playerTag);
                }
                if (textOutput)
                {
                    if (usingTMP)
                    {
                        if (GUI_Text)
                        {
                            Text_Display.GetComponent<TextMeshProUGUI>().text = playerTag;
                        }
                        else
                        {
                            Text_Display.GetComponent<TextMeshPro>().text = playerTag;
                        }
                    }
                    else
                    {
                        Text_Display.GetComponent<Text>().text = playerTag;
                    }
                }
            }
            else
            {
                if (debugEnabled)
                {
                    Debug.Log("No tag detected.");
                }
                if (textOutput)
                {
                    if (usingTMP)
                    {
                        if (GUI_Text)
                        {
                            Text_Display.GetComponent<TextMeshProUGUI>().text = "No tag detected";
                        }
                        else
                        {
                            Text_Display.GetComponent<TextMeshPro>().text = "No tag detected";
                        }
                    }
                    else
                    {
                        Text_Display.GetComponent<Text>().text = "No tag detected";
                    }
                }
            }
        }
    }
}
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;
using TMPro;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Advanced UI Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AdvancedUIToggle : UdonSharpBehaviour
    {
        [Header("Advanced UI Toggle")]
        [SerializeField] Toggle toggleSource;
        [Space]
        [Header("Colors to use for Sprite / Text")]
        [SerializeField] Color colorOn = Color.green;
        [SerializeField] Color colorOff = Color.red;
        [Space]
        [SerializeField] bool SwapSprites = false;
        [SerializeField] bool SwapSpritesColor = false;
        [SerializeField] Sprite spriteOn;
        [SerializeField] Sprite spriteOff;
        [SerializeField] Image spriteTarget;
        [Space]
        [SerializeField] bool SetText = false;
        [SerializeField] bool SetTextColor = false;
        [SerializeField] string textOn = "On";
        [SerializeField] string textOff = "Off";
        [Tooltip("Compatible with Unity Text, TMP text and GUI TMP text components.")]
        [SerializeField] GameObject textTarget;
        [Space]
        [SerializeField] bool UseSoundFeedback = false;
        [SerializeField] AudioSource audioFeedbackSource;
        [SerializeField] AudioClip audioFeedbackClipOn;
        [SerializeField] AudioClip audioFeedbackClipOff;
        [Space]
        [SerializeField] bool enableLogging = true;
        private bool state;
        private bool fallbackBoolean = true;
        private Text textCompUnity = null;
        private TextMeshPro textCompTMP = null;
        private TextMeshProUGUI textCompTMPUI = null;

        void Start()
        {
            if (SwapSprites || SwapSpritesColor && !spriteTarget) { _sendDebugError("No Sprite target found"); SwapSprites = SwapSpritesColor = false; }
            if (SetText || SetTextColor && !textTarget) { _sendDebugError("No text target found"); SetText = SetTextColor = false; }
            if (UseSoundFeedback && !audioFeedbackSource) { _sendDebugError("No Audio Source found"); UseSoundFeedback = false; }
            if (!SwapSprites && !SwapSpritesColor && !SetText && !SetTextColor && !UseSoundFeedback && !toggleSource) { enableLogging = false; _disableSelf(); }
            if (!toggleSource) { _sendDebugError("No Toggle source found, using Udon Events only now"); }
            if (textTarget)
            {
                if (textTarget.GetComponent<Text>()) textCompUnity = textTarget.GetComponent<Text>();
                if (textTarget.GetComponent<TextMeshProUGUI>()) textCompTMPUI = textTarget.GetComponent<TextMeshProUGUI>();
                if (textTarget.GetComponent<TextMeshPro>()) textCompTMP = textTarget.GetComponent<TextMeshPro>();
            }
        }

        public override void Interact()
        {
            if (toggleSource) { state = toggleSource.isOn; } else { state = !state; }
            if (SwapSprites)
            {
                if (state) { spriteTarget.sprite = spriteOn; } else { spriteTarget.sprite = spriteOff; }
            }
            if (SwapSpritesColor)
            {
                if (state) { spriteTarget.color = colorOn; } else { spriteTarget.color = colorOff; }
            }
            if (SetText)
            {
                if (state && textCompUnity) { textCompUnity.text = textOn; } // Unity Text
                if (!state && textCompUnity) { textCompUnity.text = textOff; }

                if (state && textCompTMPUI) { textCompTMPUI.text = textOn; } // TMP GUI
                if (!state && textCompTMPUI) { textCompTMPUI.text = textOff; }

                if (state && textCompTMP) { textCompTMP.text = textOn; } // TMP Text
                if (!state && textCompTMP) { textCompTMP.text = textOff; }
            }
            if (SetTextColor)
            {
                if (state && textCompUnity) { textCompUnity.color = colorOn; } // Unity Text
                if (!state && textCompUnity) { textCompUnity.color = colorOff; }

                if (state && textCompTMPUI) { textCompTMPUI.color = colorOn; } // TMP GUI
                if (!state && textCompTMPUI) { textCompTMPUI.color = colorOff; }

                if (state && textCompTMP) { textCompTMP.color = colorOn; } // TMP Text
                if (!state && textCompTMP) { textCompTMP.color = colorOff; }
            }
            if (UseSoundFeedback && audioFeedbackClipOn && audioFeedbackClipOff)
            {
                if(state) { audioFeedbackSource.PlayOneShot(audioFeedbackClipOn); } else { audioFeedbackSource.PlayOneShot(audioFeedbackClipOff); }
            }
        }

        private void _sendDebugError(string errorReported) => Debug.LogError("<color=white><b> | Reava_UwUtils:<color=red> " + errorReported + "</b></color>, please review <color=orange>References <color=white>/</color> Settings</color> on: " + gameObject.name + ".</color>", gameObject);
        private void _disableSelf() 
        {
            Debug.LogError("<color=white><b> | Reava_UwUtils:<color=red> Disabling behaviour</color> on: " + gameObject.name + ". Check references/Setup.</color>", gameObject);
            UdonBehaviour self = this.gameObject.GetComponent<UdonBehaviour>();
            self.enabled = false;
        }
    }
}
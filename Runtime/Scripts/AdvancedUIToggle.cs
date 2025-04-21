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
        [SerializeField] private GameObject[] toggleObjects;
        [Space]
        [Header("Colors to use for Sprite / Text")]
        [SerializeField] Color colorOn = Color.green;
        [SerializeField] Color colorOff = Color.red;
        [Space]
        [SerializeField] bool SwapSprites = false;
        [SerializeField] bool SwapSpritesColor = false;
        [SerializeField] Sprite spriteOn;
        [SerializeField] Sprite spriteOff;
        [Tooltip("Sprite target to swap the sprite of if enabled")]
        [SerializeField] Image spriteTarget;
        [Tooltip("Sprite target to swap the color of if enabled.")]
        [SerializeField] Image spriteTargetColor;
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
            if (SwapSprites && !spriteTarget) { _sendDebugError("No Sprite target found"); SwapSprites = false; }
            if (SwapSpritesColor && !spriteTargetColor) { _sendDebugError("No Sprite color target found"); SwapSpritesColor = false; }
            if (SetText && textTarget == null) { _sendDebugError("No text target found, setting SetText off"); SetText = SetText = false; }
            if (SetTextColor && textTarget == null) { _sendDebugError("No text target found, setting SetTextColor off"); SetText = SetTextColor = false; }
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
            foreach(GameObject o in toggleObjects)
            {
                if (o == null) continue;
                o.SetActive(!o.activeSelf);
            }
            if (toggleSource) { state = toggleSource.isOn; } else { state = !state; }
            if (SwapSprites)
            {
                if (state) { spriteTarget.sprite = spriteOn; } else { spriteTarget.sprite = spriteOff; }
            }
            if (SwapSpritesColor)
            {
                if (state) { spriteTargetColor.color = colorOn; } else { spriteTargetColor.color = colorOff; }
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

        private void _sendDebugError(string errorReported) => Debug.LogError("[Reava_/UwUtils/AdvancedUIToggle.cs]: " + errorReported + ", please review References / Settings on: " + gameObject.name + ".", gameObject);
        private void _disableSelf() 
        {
            Debug.LogError("[Reava_/UwUtils/AdvancedUIToggle.cs]: Disabling behaviour on: " + gameObject.name + ". Check references/Setup.", gameObject);
            UdonBehaviour self = this.gameObject.GetComponent<UdonBehaviour>();
            self.enabled = false;
        }
    }
}
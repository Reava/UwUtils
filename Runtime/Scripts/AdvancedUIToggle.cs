using UdonSharp;
using UnityEngine;
using VRC.Udon;
using TMPro;
using Codice.Client.BaseCommands.Changelist;
using VRC.SDK3.Persistence;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Advanced UI Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AdvancedUIToggle : UdonSharpBehaviour
    {
        [Header("Advanced UI Toggle")]
        [SerializeField] UnityEngine.UI.Toggle toggleSource;
        [SerializeField] private GameObject[] toggleObjects;
        [Space]
        [Header("Colors to use for Sprite / Text if Lerp is off")]
        [SerializeField] Color colorOn = Color.green;
        [SerializeField] Color colorOff = Color.red;
        [Space]
        [SerializeField] bool SwapSprites = false;
        [SerializeField] bool SwapSpritesColor = false;
        [SerializeField] bool lerpSpriteColorChange = false; //TODO
        [SerializeField] float lerpSpriteColorDuration = 0.5f;
        [SerializeField] Color spriteLerpColorON = Color.green;
        [SerializeField] Color spriteLerpColorOFF = Color.red;
        [SerializeField] Sprite spriteOn;
        [SerializeField] Sprite spriteOff;
        [Tooltip("Sprite target to swap the sprite of if enabled")]
        [SerializeField] UnityEngine.UI.Image spriteTarget;
        [Tooltip("Sprite target to swap the color of if enabled.")]
        [SerializeField] UnityEngine.UI.Image spriteTargetColor;
        [Space]
        [SerializeField] bool SetText = false;
        [SerializeField] bool SetTextColor = false;
        [SerializeField] bool lerpTextColorChange = false; //TODO
        [SerializeField] float lerpTextColorDuration = 0.5f;
        [SerializeField] Color textLerpColorON = Color.green;
        [SerializeField] Color textLerpColorOFF = Color.red;
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
        [SerializeField] bool persistent;
        [SerializeField] string persistenceKey = "UwUtils_AdvUI_Toggle_";
        [Space]
        [SerializeField] bool enableLogging = true;
        
        private bool state;
        private bool fallbackBoolean = true;

        private UnityEngine.UI.Text textCompUnity = null;
        private TextMeshPro textCompTMP = null;
        private TextMeshProUGUI textCompTMPUI = null;
        private int textType = 0;

        private float lerpProgressSprite = 0f;
        private float lerpProgressText = 0f;

        // Maybe I should deal with the triple text component shenanigans soon... otherwise I'm not lerping that text color lol

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
                if (textTarget.GetComponent<UnityEngine.UI.Text>()) textCompUnity = textTarget.GetComponent<UnityEngine.UI.Text>(); textType = 0;
                if (textTarget.GetComponent<TextMeshProUGUI>()) textCompTMPUI = textTarget.GetComponent<TextMeshProUGUI>(); textType = 1;
                if (textTarget.GetComponent<TextMeshPro>()) textCompTMP = textTarget.GetComponent<TextMeshPro>(); textType = 2;
            }
        }

        public void _lerpTextColor()
        {
            lerpProgressText += Time.deltaTime;

            if (lerpProgressText > lerpTextColorDuration) return;
            switch (textType)
            {
                case 0:
                    textCompUnity.color = Color.Lerp(textLerpColorON, textLerpColorOFF, lerpProgressText);
                    break;
                case 1:
                    textCompTMPUI.color = Color.Lerp(textLerpColorON, textLerpColorOFF, lerpProgressText);
                    break;
                case 2:
                    textCompTMP.color = Color.Lerp(textLerpColorON, textLerpColorOFF, lerpProgressText);
                    break;
            }
            SendCustomEventDelayedFrames(nameof(_lerpTextColor), 1);
        }

        public void _lerpSpriteColor()
        {
            lerpProgressSprite += Time.deltaTime;

            if (lerpProgressSprite > lerpSpriteColorDuration) return;
            spriteTargetColor.transform.GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(textLerpColorON, textLerpColorOFF, lerpSpriteColorDuration);
            SendCustomEventDelayedFrames(nameof(_lerpSpriteColor), 1);
        }

        public void _toggleState()
        {
            foreach (GameObject o in toggleObjects)
            {
                if (o == null) continue;
                o.SetActive(!o.activeSelf);
            }
            if (toggleSource) { state = toggleSource.isOn; } else { state = !state; }
            if (SwapSprites)
            {
                spriteTarget.sprite = state ? spriteOn : spriteOff;
            }
            if (SwapSpritesColor)
            {
                if (lerpSpriteColorChange)
                {
                    _lerpSpriteColor();
                }
                else
                {
                    if (state) { spriteTargetColor.color = colorOn; } else { spriteTargetColor.color = colorOff; }
                }
            }
            if (SetText)
            {
                switch (textType)
                {
                    case 0:
                        textCompUnity.text = state ? textOn : textOff;
                        break;
                    case 1:
                        textCompTMPUI.text = state ? textOn : textOff;
                        break;
                    case 2:
                        textCompTMP.text = state ? textOn : textOff;
                        break;
                }
            }
            if (SetTextColor)
            {
                if (lerpTextColorChange)
                {
                    _lerpTextColor();
                }
                else
                {
                    if (state && textCompUnity) { textCompUnity.color = colorOn; }
                    if (!state && textCompUnity) { textCompUnity.color = colorOff; }
                    switch (textType)
                    {
                        case 0:
                            textCompUnity.color = state ? colorOn : colorOff;
                            break;
                        case 1:
                            textCompTMPUI.color = state ? colorOn : colorOff; ;
                            break;
                        case 2:
                            textCompTMP.color = state ? colorOn : colorOff; ;
                            break;
                    }
                }
            }
            if (UseSoundFeedback && audioFeedbackClipOn && audioFeedbackClipOff)
            {
                if (state) { audioFeedbackSource.PlayOneShot(audioFeedbackClipOn); } else { audioFeedbackSource.PlayOneShot(audioFeedbackClipOff); }
            }
        }

        public override void Interact()
        {
            _toggleState();
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (persistent)
            {
                if (Networking.LocalPlayer != player) return;
                if (!PlayerData.HasKey(player, persistenceKey)) return;
                if (PlayerData.GetType(player, persistenceKey) != typeof(bool)) return;
                bool recoveredState = PlayerData.GetBool(player, persistenceKey);
                if (enableLogging) Debug.Log("[Reava_/UwUtils/AdvancedUIToggle.cs] Recovered state of : " + recoveredState + "", this.gameObject);

                // TODO
            }
        }

        private void _sendDebugError(string errorReported) => Debug.LogError("[Reava_/UwUtils/AdvancedUIToggle.cs]: " + errorReported + ", please review References / Settings on: " + gameObject.name + ".", this.gameObject);
        private void _disableSelf() 
        {
            Debug.LogError("[Reava_/UwUtils/AdvancedUIToggle.cs]: Disabling behaviour on: " + gameObject.name + ". Check references/Setup.", this.gameObject);
            UdonBehaviour self = this.gameObject.GetComponent<UdonBehaviour>();
            self.enabled = false;
        }
    }
}
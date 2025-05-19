using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Persistence;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/ToggleCanvas")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class toggleCanvas : UdonSharpBehaviour
    {
        [SerializeField] private bool defaultState = true;
        [Header("List of objects")]
        public Canvas[] canvasesToToggle;
        public Canvas[] canvasesToToggleinverted;

        [SerializeField] private bool usePersistence = false;
        [SerializeField] private string PersistenceKey = "Reava_UwUtils_Toggle_Canvas_";

        private bool currentState;

        public void Start()
        {
            foreach (Canvas toggleObject in canvasesToToggle)
            {
                toggleObject.enabled = defaultState;
            }
            foreach (Canvas toggleObject in canvasesToToggleinverted)
            {
                toggleObject.enabled = !defaultState;
            }
            currentState = defaultState;
        }

        public override void Interact()
        {
            foreach (Canvas toggleObject in canvasesToToggle)
            {
                toggleObject.enabled = !toggleObject.enabled;
            }
            foreach (Canvas toggleObject in canvasesToToggleinverted)
            {
                toggleObject.enabled = !toggleObject.enabled;
            }
            currentState = !currentState;
            if (usePersistence) PlayerData.SetBool(PersistenceKey, currentState);
        }

        public void _Enable()
        {
            foreach (Canvas toggleObject in canvasesToToggle)
            {
                toggleObject.enabled = true;
            }
            foreach (Canvas toggleObject in canvasesToToggleinverted)
            {
                toggleObject.enabled = false;
            }
            currentState = true;
            if (usePersistence) PlayerData.SetBool(PersistenceKey, currentState);
        }

        public void _Disable()
        {
            foreach (Canvas toggleObject in canvasesToToggle)
            {
                toggleObject.enabled = false;
            }
            foreach (Canvas toggleObject in canvasesToToggleinverted)
            {
                toggleObject.enabled = true;
            }
            currentState = false;
            if (usePersistence) PlayerData.SetBool(PersistenceKey, currentState);
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (usePersistence)
            {
                if (Networking.LocalPlayer != player) return;
                if (!PlayerData.HasKey(player, PersistenceKey)) return;
                if (PlayerData.GetType(player, PersistenceKey) != typeof(bool)) return;

                bool recoveredState = PlayerData.GetBool(player, PersistenceKey);
                if (recoveredState)
                {
                    _Enable();
                }
                else
                {
                    _Disable();
                }
            }
        }
    }
}
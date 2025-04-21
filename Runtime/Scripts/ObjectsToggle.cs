using UnityEngine;
using UdonSharp;
using UnityEngine.InputSystem;
using VRC.SDK3.Persistence;
using VRC.SDKBase;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Objects Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class ObjectsToggle : UdonSharpBehaviour
    {
        [Header("List of objects to toggle")]
        [SerializeField] private GameObject[] toggleObjects;
        [Header("Save state with persistence? Warning, this will NOT save exact states, read more by hovering.")]
        [Tooltip("If you have another toggle than this one setting the state of one of the objects differently, this will NOT know it and won't restore it properly when rejoining. For accuracy, only use one toggle per object, and separate anything that needs to be saved differently from the rest of the group.")]
        [SerializeField] private bool saveState = false;
        [Header("Use a unique Parameter per toggle!")]
        [SerializeField] private string persistenceParameter = "toggle_example";
        private bool isDefault = true;
        private bool valid = true;
        void Start()
        {
            if (toggleObjects.Length == 0 || toggleObjects == null)
            {
                valid = false;
                Debug.LogError("[UwUtils/iState.cs] Setup is invalid, check your references for object '" + gameObject.name + "'");
            }
            else
            {
                return;
            }
        }

        public override void Interact()
        {
            if (valid)
            {
                foreach (GameObject toggleObject in toggleObjects)
                {
                    if (toggleObject) toggleObject.SetActive(!toggleObject.activeSelf);
                }
                isDefault = !isDefault;
                if (saveState) PlayerData.SetBool(persistenceParameter, isDefault);
            }
        }

        public void _Toggle()
        {
            if (valid)
            {
                foreach (GameObject toggleObject in toggleObjects)
                {
                    if (toggleObject) toggleObject.SetActive(!toggleObject.activeSelf);
                }
                isDefault = !isDefault;
                if (saveState) PlayerData.SetBool(persistenceParameter, isDefault);
            }
        }

        public override void OnPlayerRestored(VRCPlayerApi player)
        {
            if (valid && saveState)
            {
                if (Networking.LocalPlayer != player) return;
                if (!PlayerData.HasKey(player, persistenceParameter)) return;
                if (PlayerData.GetType(player, persistenceParameter) != typeof(bool)) return;

                bool recoveredState = PlayerData.GetBool(player, persistenceParameter);
                if (!recoveredState) _Toggle();
            }
        }
    }
}
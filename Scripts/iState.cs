using UnityEngine;
using UdonSharp;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/iState")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class iState : UdonSharpBehaviour
    {
        [Header("List of objects to toggle")]
        [SerializeField] private GameObject[] toggleObjects;
        private bool valid = true;
        void Start()
        {
            if (toggleObjects.Length == 0)
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
            foreach (GameObject toggleObject in toggleObjects)
            {
                toggleObject.SetActive(!toggleObject.activeSelf);
            }
        }

        public void _EnableAll()
        {
            foreach (GameObject toggleObject in toggleObjects)
            {
                toggleObject.SetActive(true);
            }
        }
        public void _DisableAll()
        {
            foreach (GameObject toggleObject in toggleObjects)
            {
                toggleObject.SetActive(false);
            }
        }

        public void _InvertState()
        {
            foreach (GameObject toggleObject in toggleObjects)
            {
                toggleObject.SetActive(!toggleObject.activeSelf);
            }
        }
    }
}
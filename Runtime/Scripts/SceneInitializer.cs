using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/SceneInitializer")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SceneInitializer : UdonSharpBehaviour
    {
        [Header("Toggle state of objects X seconds after Start()")]
        public int updateDelay = 1;
        [Tooltip("List of objects to toggle ON")]
        public GameObject[] toggleObjectsON;
        [Tooltip("List of objects to toggle OFF")]
        public GameObject[] toggleObjectsOFF;

        void Start()
        {
            SendCustomEventDelayedSeconds(nameof(_updateState), updateDelay); //Call function X seconds after Start() is called
        }

        public void _updateState()
        {
            foreach (GameObject toggleObject in toggleObjectsON)
            {
                toggleObject.SetActive(true);
            }
            foreach (GameObject toggleObject in toggleObjectsOFF)
            {
                toggleObject.SetActive(false);
            }
        }
    }
}
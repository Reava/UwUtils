using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.Udon;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Sequencial Toggle")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SequencialToggle : UdonSharpBehaviour
    {
        [Space]
        [Header("Sequencial Toggle")]
        [SerializeField] GameObject[] Targets;
        [Space]
        [SerializeField] bool enableLogging = true;

        void Start()
        {
            //do the checks
        }

        public override void Interact()
        {
            //do the thing
        }
    }
}
using UnityEngine;
using UdonSharp;
using UwUtils;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Collectible")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Collectible : UdonSharpBehaviour
    {
        [Space]
        [SerializeField] private bool disableSelfOnCollect = true;
        [SerializeField] private CollectionSystem CollectionSystemRef;
        [SerializeField] private int Value = 10;
        [Space]
        [SerializeField] private bool enableLogging = true;

        void Start()
        {
            CollectionSystemRef._totalValueDebug(Value);
            if(enableLogging) Debug.Log("[Reava_/UwUtils/Collectible.cs]: Collectible initialized for " + Value + " points, ref to " + CollectionSystemRef + ". Collectible is :" + gameObject.name, gameObject);
        }

        public void Interact()
        {
            CollectionSystemRef._collectValue(Value);
            if (enableLogging) Debug.Log("[Reava_/UwUtils/Collectible.cs]: Collectible claimed for " + Value + " points sent to "+ CollectionSystemRef+" from:" + gameObject.name, gameObject);
            if (disableSelfOnCollect) this.gameObject.SetActive(false);
        }
    }
}
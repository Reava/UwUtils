using UdonSharp;
using UnityEngine;
using VRC.Economy;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Open World Store")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class OpenWorldStore : UdonSharpBehaviour
    {

        public override void Interact()
        {
            Store.OpenWorldStorePage();
        }

        public override void OnPickupUseDown()
        {
            Store.OpenWorldStorePage();
        }

        public void _OpenStore()
        {
            Store.OpenWorldStorePage();
        }
    }
}

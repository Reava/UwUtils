using UdonSharp;
using UnityEngine;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/MeshRendererSwapper")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class MeshRendererSwapper : UdonSharpBehaviour
    {
        [Header("Mesh Renderers Group 1 & 2")]
        [SerializeField] private MeshRenderer[] RendererGroupOne;
        [SerializeField] private MeshRenderer[] RendererGroupTwo;
        [Header("Defaults")]
        [SerializeField] private bool defaultsGroupOneOnPCVR = true;
        [SerializeField] private bool defaultsGroupTwoOnQuest = true;
        private bool isQuest = false;
        private bool currentGroup = true;

        void Start()
        {
#if UNITY_ANDROID
isQuest = true;
#endif
            if (!isQuest && currentGroup)
            {
                return;
            }
            if (defaultsGroupTwoOnQuest || currentGroup == false)
            {
                currentGroup = false;
                _enableOne();
            }
            else return;
        }

        public void _switchGroup()
        {
            currentGroup = !currentGroup;
            for (int i = 0; i < RendererGroupOne.Length; i++)
            {
                RendererGroupOne[i].enabled = !RendererGroupOne[i].enabled;
            }
            for (int i = 0; i < RendererGroupTwo.Length; i++)
            {
                RendererGroupTwo[i].enabled = !RendererGroupTwo[i].enabled;
            }
        }

        public void _enableTwo()
        {
            for (int i = 0; i < RendererGroupOne.Length; i++)
            {
                RendererGroupOne[i].enabled = false;
            }
            for (int i = 0; i < RendererGroupTwo.Length; i++)
            {
                RendererGroupTwo[i].enabled = true;
            }
        }

        public void _enableOne()
        {
            for (int i = 0; i < RendererGroupOne.Length; i++)
            {
                RendererGroupOne[i].enabled = true;
            }
            for (int i = 0; i < RendererGroupTwo.Length; i++)
            {
                RendererGroupTwo[i].enabled = false;
            }
        }
    }
}
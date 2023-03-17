﻿using UnityEngine;
using UdonSharp;
using TMPro;
using UnityEngine.UI;

namespace UwUtils
{
    [AddComponentMenu("UwUtils/Collection System")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class CollectionSystem : UdonSharpBehaviour
    {
        [Space]
        [SerializeField] private int balance;
        [Space]
        [SerializeField] private bool outputToText = false;
        [Tooltip("Don't forget to add a space before the end !")]
        [SerializeField] private string textPrefix = "Balance: ";
        [Space]
        [SerializeField] private GameObject[] textFields;
        private int debugTotalValue = 0;
        [Space]
        [SerializeField] private bool enableLogging = true;

        void Start()
        {
            if (outputToText && textFields == null) outputToText = false; Debug.LogError("<b> | Reava_UwUtils: Ouput to text enabled but no text fields found, disabling feature on " + gameObject.name, gameObject);
            debugTotalValue = 0;
            if (enableLogging) SendCustomEventDelayedSeconds(nameof(_maxValueDebug), 5f);
        }

        public void _totalValueDebug(int valuePerCollectible)
        {
            debugTotalValue += valuePerCollectible;
        }

        public void _collectValue(int CollectibleValue)
        {
            balance += CollectibleValue;
            if (outputToText)
            {
                foreach(GameObject field in textFields)
                {
                    if (!field) continue;
                    if (field.GetComponents(typeof(Text)) != null) field.GetComponent<Text>().text = textPrefix+balance;
                    if (field.GetComponents(typeof(TextMeshPro)) != null) field.GetComponent<TextMeshPro>().text = textPrefix + balance;
                    if (field.GetComponents(typeof(TextMeshProUGUI)) != null) field.GetComponent<TextMeshProUGUI>().text = textPrefix + balance;
                }
            }
            if (enableLogging) Debug.Log("Reava_UwUtils: " + gameObject.name + " received new collectible worth " + CollectibleValue + "! New balance is: " + balance, gameObject);
        }

        private void _maxValueDebug() => Debug.Log("Reava_UwUtils: " + gameObject.name + " received total points available: " + debugTotalValue, gameObject);
    }
}
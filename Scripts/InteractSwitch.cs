using UnityEngine;
using UdonSharp;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class InteractSwitch : UdonSharpBehaviour
{
    [Header("List of objects to toggle ON")]
    [SerializeField] private GameObject[] toggleObjectsON;
    [Header("List of objects to toggle OFF")]
    [SerializeField] private GameObject[] toggleObjectsOFF;

    public override void Interact()
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
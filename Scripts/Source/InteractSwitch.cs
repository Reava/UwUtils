using UnityEngine;
using UdonSharp;

public class InteractSwitch : UdonSharpBehaviour
{
    [Tooltip("List of objects to toggle ON")]
    public GameObject[] toggleObjectsON;
    [Tooltip("List of objects to toggle OFF")]
    public GameObject[] toggleObjectsOFF;

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
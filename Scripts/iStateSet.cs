using UnityEngine;
using UdonSharp;

[AddComponentMenu("UwUtils/iStateSet")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class iStateSet : UdonSharpBehaviour
{
    [Header("List of objects to toggle ON")]
    [SerializeField] private GameObject[] toggleObjectsON;
    [Header("List of objects to toggle OFF")]
    [SerializeField] private GameObject[] toggleObjectsOFF;
    private bool valid = true;
    void Start()
    {
        if (toggleObjectsON.Length == 0 & toggleObjectsOFF.Length == 0)
        {
            valid = false;
            Debug.LogError("[UwUtils/iStateSet.cs] Setup is invalid, check your references for object '" + gameObject.name + "'");
        }
        else
        {
            return;
        }
    }

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

    public void _Invert()
    {
        foreach (GameObject toggleObject in toggleObjectsON)
        {
            toggleObject.SetActive(false);
        }
        foreach (GameObject toggleObject in toggleObjectsOFF)
        {
            toggleObject.SetActive(true);
        }
    }
    public void _Switch()
    {
        foreach (GameObject toggleObject in toggleObjectsON)
        {
            toggleObject.SetActive(toggleObject.activeSelf);
        }
        foreach (GameObject toggleObject in toggleObjectsOFF)
        {
            toggleObject.SetActive(toggleObject.activeSelf);
        }
    }
}
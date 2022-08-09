using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;
using UnityEngine.UI;

public class UwUtils_Keypad : UdonSharpBehaviour
{
    [SerializeField] private GameObject displayTextField;
    void Start()
    {
        displayTextField.GetComponent<TextMeshProUGUI>().text = "Waiting..";
    }

    public void _Key0()
    {
        return;
    }
    public void _Key1()
    {
        return;
    }
    public void _Key2()
    {
        return;
    }
    public void _Key3()
    {
        return;
    }
    public void _Key4()
    {
        return;
    }
    public void _Key5()
    {
        return;
    }
    public void _Key6()
    {
        return;
    }
    public void _Key7()
    {
        return;
    }
    public void _Key8()
    {
        return;
    }
    public void _Key9()
    {
        return;
    }
    public void _Clear()
    {
        return;
    }
    public void _Confirm()
    {
        return;
    }
}

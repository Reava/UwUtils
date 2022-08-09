using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class LanguageSwitcher : UdonSharpBehaviour
{
    [Header("Language codes: FR, EN, GER, SP, JP")]
    [SerializeField] private string currentLang = "EN";
    [SerializeField] private GameObject[] EnTextFields;
    [SerializeField] private GameObject[] FrTextFields;
    [SerializeField] private GameObject[] JpTextFields;
    [SerializeField] private GameObject[] GerTextFields;
    [SerializeField] private GameObject[] SpTextFields;
    private string prevLang = "null";
    void Start()
    {
        _updateLanguage();
    }

    public void _updateLanguage()
    {
        switch (prevLang)
        {
            case "EN":
                foreach (GameObject textField in EnTextFields)
                {
                    textField.SetActive(false);
                }
                break;
            case "JP":
                foreach (GameObject textField in JpTextFields)
                {
                    textField.SetActive(false);
                }
                break;
            case "FR":
                foreach (GameObject textField in FrTextFields)
                {
                    textField.SetActive(false);
                }
                break;
            case "GER":
                foreach (GameObject textField in GerTextFields)
                {
                    textField.SetActive(false);
                }
                break;
            case "SP":
                foreach (GameObject textField in SpTextFields)
                {
                    textField.SetActive(false);
                }
                break;
            case "null":
                // We do nothing ! :)
                break;
            default:
                Debug.Log("Couldn't find previous Language, continuing");
                break;
        }
        switch (currentLang)
        {
            case "EN":
                foreach (GameObject textField in EnTextFields)
                {
                    textField.SetActive(true);
                    prevLang = currentLang;
                }
                return;
            case "JP":
                foreach (GameObject textField in JpTextFields)
                {
                    textField.SetActive(true);
                    prevLang = currentLang;
                }
                return;
            case "FR":
                foreach (GameObject textField in FrTextFields)
                {
                    textField.SetActive(true);
                    prevLang = currentLang;
                }
                return;
            case "GER":
                foreach (GameObject textField in GerTextFields)
                {
                    textField.SetActive(true);
                    prevLang = currentLang;
                }
                return;
            case "SP":
                foreach (GameObject textField in SpTextFields)
                {
                    textField.SetActive(true);
                    prevLang = currentLang;
                }
                return;
            default:
                foreach (GameObject textField in EnTextFields)
                {
                    textField.SetActive(true);
                    prevLang = currentLang;
                }
                return;
        }
    }

    public void _setFrench()
    {
        currentLang = "FR";
        _updateLanguage();
    }
    public void _setEnglish()
    {
        currentLang = "EN";
        _updateLanguage();
    }
    public void _setJapanese()
    {
        currentLang = "JP";
        _updateLanguage();
    }
    public void _setGerman()
    {
        currentLang = "GER";
        _updateLanguage();
    }
    public void _setSpanish()
    {
        currentLang = "SP";
        _updateLanguage();
    }
}

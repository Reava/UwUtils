using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using TMPro;
using UnityEngine.UI;
using VRC.SDK3.StringLoading;
using VRC.Udon.Common.Interfaces;

[AddComponentMenu("UwUtils/RemoteStringToText")]
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class RemoteStringToText : UdonSharpBehaviour
{
    [SerializeField] public VRCUrl linkToString;
    [SerializeField] private GameObject Text_Display;
    [Header("Output to text?")]
    [SerializeField] private bool textOutput = false;
    [SerializeField] private bool usingTMP = false;
    [Header("Is the TMP text GUI or classic ?")]
    [SerializeField] private bool GUI_Text = false;
    [Header("Output debug logs?")]
    [SerializeField] private bool debugEnabled = true;
    public bool SplitIntoArray = false;
    public char t = ',';
    public string[] strArr;
    private string loadedString;
    private void Start()
    {
        _LoadUrl();
    }

    public void _LoadUrl()
    {
        VRCStringDownloader.LoadUrl(linkToString, (IUdonEventReceiver)this);
    }

    public override void Interact()
    {
        _LoadUrl();
    }

    public override void OnStringLoadSuccess(IVRCStringDownload result)
    {
        loadedString += result.Result;
        if (SplitIntoArray)
        {
            strArr = loadedString.Split(t);
        }
        if (textOutput) _printToText();
        if (debugEnabled) Debug.Log("Reava_UwUtils: String successfully loaded: "+ loadedString + "On: " + gameObject.name, gameObject);
    }
    public override void OnStringLoadError(IVRCStringDownload result)
    {
        Debug.LogError("Reava_UwUtils: String loading failed: "+result.Error+ "| Error Code: " + result.ErrorCode + "On: " + gameObject.name, gameObject);
    }

    public void _printToText()
    {
        if (usingTMP)
        {
            if (GUI_Text)
            {
                Text_Display.GetComponent<TextMeshProUGUI>().text = loadedString;
            }
            else
            {
                Text_Display.GetComponent<TextMeshPro>().text = loadedString;
            }
        }
        else
        {
            Text_Display.GetComponent<Text>().text = loadedString;
        }
    }
}
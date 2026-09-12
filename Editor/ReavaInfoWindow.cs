#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class ReavaInfoWindow : EditorWindow
{

    private const string GitHubURL = "https://github.com/Reava/UwUtils";
    private const string LinktreeURL = "https://linktr.ee/Reava_";
    private const string TwitterURL = "https://x.com/Reava_VR";
    private const string VRChatGroupURL = "https://vrc.group/REAVA.6955";

    [SerializeField]
    private Texture2D bannerImage;

    [MenuItem("Tools/Reava_/Info")]
    public static void ShowWindow()
    {
        GetWindow<ReavaInfoWindow>("Info");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(8);

        DrawBanner();

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(
            "UwUtils",
            EditorStyles.boldLabel
        );

        EditorGUILayout.LabelField(
            "Created by Reava_",
            EditorStyles.miniLabel
        );

        EditorGUILayout.Space(10);

        EditorGUILayout.HelpBox(
            "Thank you for using UwUtils!\n\n" +
            "You can find more of my work, projects and updates " +
            "through the links below.",
            MessageType.Info
        );

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(
            "Links",
            EditorStyles.boldLabel
        );

        if (GUILayout.Button("GitHub", GUILayout.Height(30)))
        {
            Application.OpenURL(GitHubURL);
        }

        if (GUILayout.Button("Linktree", GUILayout.Height(30)))
        {
            Application.OpenURL(LinktreeURL);
        }

        if (GUILayout.Button("Twitter / X", GUILayout.Height(30)))
        {
            Application.OpenURL(TwitterURL);
        }

        if (GUILayout.Button("VRChat Group", GUILayout.Height(30)))
        {
            Application.OpenURL(VRChatGroupURL);
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField(
            "Other Tools",
            EditorStyles.boldLabel
        );

        EditorGUILayout.LabelField(
            "Additional information and other tools I made can be " +
            "accessed through the Tools/Reava_ menu when downloaded.",
            EditorStyles.wordWrappedLabel
        );

        EditorGUILayout.Space(8);
    }

    private void DrawBanner()
    {
        if (bannerImage == null)
        {
            EditorGUILayout.HelpBox(
                "No banner image assigned.",
                MessageType.Warning
            );

            return;
        }

        float availableWidth = EditorGUIUtility.currentViewWidth - 20f;

        float aspectRatio =
            (float)bannerImage.width /
            bannerImage.height;

        float height = availableWidth / aspectRatio;

        Rect bannerRect = GUILayoutUtility.GetRect(
            availableWidth,
            height
        );

        GUI.DrawTexture(
            bannerRect,
            bannerImage,
            ScaleMode.ScaleAndCrop
        );
    }
}

#endif
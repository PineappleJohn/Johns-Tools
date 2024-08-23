using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;

public class InfoPage : EditorWindow
{

    public static Vector2 scrollPos;
    private void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos, true, true);
        GUILayout.Label("Info", EditorStyles.largeLabel);
        GUILayout.Space(5);
        GUILayout.TextArea("Make Hitsounds requires the BetterHitSounds script. " +
            "Any quick scripts require you to have the object selected. " +
            "Add MonkeButton requires Johns MonkeButton script. " +
            "Making an object grabable requires a unity package attached below."); //readonly
        GUILayout.Space(15);
        GUILayout.Label("Make sure to setup the script as well");
        GUILayout.Space(5);
        if (GUILayout.Button("Get the MonkeButton Script"))
            Application.OpenURL("https://drive.google.com/file/d/1K9NqgC_qgKDEGIBfDrbIyuGAf8ytsgYN/view?usp=sharing");
        GUILayout.Space(5);
        if (GUILayout.Button("Get the modified BetterHitSounds script!"))
            Application.OpenURL("https://drive.google.com/file/d/1YxIxJaIlMy-5ZcjNiZve1X0jcKGQb61l/view?usp=sharing");
        GUILayout.Space(5);
        GUILayout.Label("Credits to: Kevinboy160");
        if (GUILayout.Button("Get the grabable unity package!"))
            Application.OpenURL("https://drive.google.com/file/d/1Pw8SRfJPXNDSq2EE0GLQ67uWU36pMkTH/view?usp=sharing");
        GUILayout.Space(20);
        if (GUILayout.Button("Got it!"))
        {
            this.Close();
        }
        GUILayout.EndScrollView();
    }
}

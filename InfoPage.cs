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
        GUILayout.TextArea("Please check the github or click the button below. I keep my code open sourced to help people learn, please don't use my code," +
            " I spend a lot of time on it and stealing it without crediting me makes me sad :("); //readonly
        GUILayout.Space(15);
        GUILayout.Label("Make sure to setup the script as well");
        GUILayout.Space(5);
        if (GUILayout.Button("Get all the scripts!"))
            Application.OpenURL("https://github.com/PineappleJohn/JohnsToolsDependencies");
        GUILayout.Space(5);
        GUILayout.Label("Credits to: Kevinboy160");
        if (GUILayout.Button("Get the grabable unity package!"))
            Application.OpenURL("https://drive.google.com/file/d/1Pw8SRfJPXNDSq2EE0GLQ67uWU36pMkTH/view?usp=sharing"); //gonna start moving scripts to git hub
        GUILayout.Space(20);
        if (GUILayout.Button("Got it!"))
        {
            this.Close();
        }
        GUILayout.EndScrollView();
    }
}

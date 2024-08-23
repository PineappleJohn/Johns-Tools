using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Linq;
using static HitSoundsv2;

public class AdvancedHitsound : EditorWindow
{
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<AdvancedHitsound>("Advanced Hitsounds");
    }
    string soundTag;
    AudioClip sound;
    private GameObject handR;
    private GameObject handL;

    public static Vector2 scrollPos;
    
    private void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos, true, true);
        GUILayout.Label("Make a Soft-Chimp Loco hitsound!", EditorStyles.largeLabel);
        GUILayout.Space(15);
        GUILayout.Label("Tag for sound");
        soundTag = EditorGUILayout.TagField(soundTag);
        GUILayout.Space(5);
        sound = (AudioClip)EditorGUILayout.ObjectField(sound, typeof(AudioClip), false);
        GUILayout.Space(5);
        GUILayout.Label("Right Hand");
        handR = (GameObject)EditorGUILayout.ObjectField(handR, typeof(GameObject), true);
        GUILayout.Label("Left Hand");
        handL = (GameObject)EditorGUILayout.ObjectField(handL, typeof(GameObject), true);
        GUILayout.Space(5);
        if (GUILayout.Button("Make Hitsound"))
        {
            makeSound();
        }

        if (GUILayout.Button("Get the modified script"))
            Application.OpenURL("https://drive.google.com/file/d/14W376EBB6Jq1RsE2i7PDP7s31aXBTZPj/view?usp=sharing");

        GUILayout.EndScrollView();
    }

    private void makeSound()
    {
        HitSoundsv2 offlineR = handR.GetComponentInChildren<HitSoundsv2>();
        HitSoundsv2 offlineL = handL.GetComponentInChildren<HitSoundsv2>();
        offlineR.audioData.Add(new AudioData(sound, soundTag));
        offlineL.audioData = offlineR.audioData;
        Selection.objects[0] = offlineR;
        Selection.objects[1] = offlineL;
    }
}

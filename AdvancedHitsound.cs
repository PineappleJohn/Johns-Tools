using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Linq;
using static HitSoundsv2;
using Photon.Pun;

public class AdvancedHitsound : EditorWindow
{
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<AdvancedHitsound>("Advanced Hitsounds");
    }
    string soundTag;
    AudioClip sound;
    bool networked;
    private GameObject handR;
    private GameObject handL;

    public static Vector2 scrollPos;
    
    private void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos, true, true);
        GUILayout.Label("Make a Soft-Chimp Loco hitsound!", EditorStyles.largeLabel);
        GUILayout.Space(15);
        soundTag = EditorGUILayout.TagField("Tag for sound", soundTag);
        GUILayout.Space(5);
        networked = EditorGUILayout.Toggle("Networked (BETA, UNTESTED)", networked);
        GUILayout.Space(5);
        sound = (AudioClip)EditorGUILayout.ObjectField(sound, typeof(AudioClip), false);
        GUILayout.Space(5);
        handR = (GameObject)EditorGUILayout.ObjectField("Right Hand", handR, typeof(GameObject), true);
        handL = (GameObject)EditorGUILayout.ObjectField("Left Hand", handL, typeof(GameObject), true);
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
        if (networked)
        {
            offlineR.networked = true;
            offlineR.pt = handR.AddComponent<PhotonView>();
            offlineL.networked = true;
            offlineL.pt = handL.AddComponent<PhotonView>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Photon.Pun;

public class NetworkObj : EditorWindow
{
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow<NetworkObj>("Network Object's");
        window.minSize = new Vector2(400f, 400f);
        window.maxSize = new Vector2(400f, 400f);
    }
    private void OnGUI()
    {
        GUILayout.Label("Network Selected Object", EditorStyles.whiteLargeLabel);
        GUILayout.Label("Requires V3 dependencies", EditorStyles.boldLabel);
        GUILayout.Space(10);
        if (GUILayout.Button("Networking Method 1"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.AddComponent<PhotonView>();
                if (!obj.GetComponent<Rigidbody>())
                    obj.AddComponent<PhotonTransformView>();
                else
                    obj.AddComponent<PhotonRigidbodyView>();
            }
        }
        GUILayout.Label("Requires Photon only", EditorStyles.miniLabel);
        GUILayout.Space(5);
        if (GUILayout.Button("Networking Method 2"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.AddComponent<NetworkMethod>();
            }
        }
        GUILayout.Label("Requires Photon and NetworkMethod", EditorStyles.miniLabel);
        GUILayout.Space(10);
        if (GUILayout.Button("Remove Networking"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.GetComponent<PhotonView>() != null && obj.GetComponent<PhotonTransformView>() != null)
                {
                    if (obj.GetComponent<NetworkMethod>())
                        DestroyImmediate(obj.GetComponent<NetworkMethod>());
                    DestroyImmediate(obj.GetComponent<PhotonView>());
                    if (obj.GetComponent<PhotonRigidbodyView>())
                        DestroyImmediate(obj.GetComponent<PhotonRigidbodyView>());
                    else
                        DestroyImmediate(obj.GetComponent<PhotonTransformView>());
                }
            }
        }
    }
}
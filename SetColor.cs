using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SetColor : EditorWindow
{
    Color color;
    Object mat;
    bool useMat;
    private static Vector2 scrollPos;
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow<SetColor>("Set color");
        window.minSize = new Vector2(200f, 350f);
    }
    void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
        GUILayout.Label("Color selected object", EditorStyles.largeLabel);
        GUILayout.Label("Suggested by Shadow (bigpapi1224225)");
        GUILayout.Space(10);
        color = EditorGUILayout.ColorField(color);
        useMat = EditorGUILayout.Toggle("Copy color from material", useMat);
        mat = (GameObject)EditorGUILayout.ObjectField(mat, typeof(GameObject), true);
        GUILayout.Space(5);
        if (GUILayout.Button("Set Color"))
            colorTheObject();
        GUILayout.EndScrollView();
    }

    private void colorTheObject()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (obj.GetComponent<Renderer>())
            {
                if (useMat && mat.GetComponent<Renderer>())
                {
                    color = mat.GetComponent<Renderer>().sharedMaterial.color;
                }
                else if (useMat && !mat.GetComponent<Renderer>())
                    Debug.LogError("Object: " + mat + " does not have a Renderer!");
                obj.GetComponent<Renderer>().sharedMaterial.color = color;
            }
            else
                Debug.LogError("Selected object: " + obj + " does not have a Renderer!");
        }
    }
}
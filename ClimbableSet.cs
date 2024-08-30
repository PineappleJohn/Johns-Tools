using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Search;

public class ClimbableSet : EditorWindow
{
    GameObject _lh;
    GameObject _rh;
    GameObject _cPrefab;
    LayerMask _lm;

    private void OnGUI()
    {
        GUILayout.Label("Make selected object climbable", EditorStyles.largeLabel);
        GUILayout.Label("Suggested by treehouse.vr");
        GUILayout.Space(10);
        _lh = (GameObject)EditorGUILayout.ObjectField("Left hand", _lh, typeof(GameObject), true);
        _rh = (GameObject)EditorGUILayout.ObjectField("Right hand", _rh, typeof(GameObject), true);
        _cPrefab = (GameObject)EditorGUILayout.ObjectField("ClimbPrefab", _cPrefab, typeof(GameObject), false);
        _lm = EditorGUILayout.LayerField("Climbable Layer", _lm);
        GUILayout.Space(5);
        if (GUILayout.Button("Setup the grab script"))
        {
            Climb lhc = _lh.AddComponent<Climb>(); 
            Climb rhc = _rh.AddComponent<Climb>();
            lhc.climbableLayer = _lm;
            lhc.hand = easyInputs.EasyHand.LeftHand;
            if (_cPrefab)
                lhc.prefabToSpawn = _cPrefab;
            else
                EasyLogError("Prefab not set!");

            rhc.climbableLayer = _lm;
            rhc.hand = easyInputs.EasyHand.RightHand;
            if (_cPrefab)
                lhc.prefabToSpawn = _cPrefab;
            else
                EasyLogError("Prefab not set!");
            EasyLogError("Issue with the layer setting, you may have to do it manually");
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Make Obj Climbable"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (_lm != 0)
                    obj.layer = _lm;
                else
                    Debug.LogError("Layer not set!");
            }
        }
    }

    void EasyLogError(string _s)
    {
        Debug.LogError(_s);
    }
}

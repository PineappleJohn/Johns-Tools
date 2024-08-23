using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;
using Unity.VisualScripting;
using static BetterHitSounds;
using System.Linq;
using Photon.Pun;

public class JohnsWindow : EditorWindow {

    string audioTag;
    string slipTag;
    Object handR;
    Object handL;
    AudioClip sound;
    private TagSound hitSound;
    bool hadMesh;
    Material material;
    public static Vector2 scrollPos;
    private GameObject prefab1;
    LayerMask lm;

    [MenuItem("Window/John's Tools")]
    [MenuItem("Tools/John's Tools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<JohnsWindow>("John's Tools");
    }
    //ty brackeys :)
    void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true);
        GUILayout.Label("John's tools, V1.5", EditorStyles.largeLabel); //scrollbar broken :(
        GUILayout.Label("Made by john; Don't skid please");
        if (GUILayout.Button("Info"))
        {
            var window = ScriptableObject.CreateInstance<InfoPage>();
            window.Show();
        }
        GUILayout.Label("Non-walkable");
        lm = EditorGUILayout.LayerField(lm);
        GUILayout.Space(10); //hitsounds
        GUILayout.Label("Easy Hitsounds (DEFAULT RIG)", EditorStyles.largeLabel);
        GUILayout.Space(5);
        GUILayout.Label("Tag");
        audioTag = EditorGUILayout.TagField(audioTag);
        GUILayout.Space(5);
        GUILayout.Label("Right Hand Controller");
        handR = EditorGUILayout.ObjectField(handR, typeof(Object), true);
        GUILayout.Label("Left Hand Controller");
        handL = EditorGUILayout.ObjectField(handL, typeof(Object), true);
        GUILayout.Space(5);
        GUILayout.Label("Sound");
        sound = (AudioClip)EditorGUILayout.ObjectField(sound, typeof(AudioClip), false); //thanks SisusCo on unity discussions
        if (GUILayout.Button("Make Hitsound"))
            MakeHitsound();
        GUILayout.Space(5);
        if (GUILayout.Button("Add Hitsound Script"))
        {
            if (!handR.GetComponent<BetterHitSounds>() && !handL.GetComponent<BetterHitSounds>())
            {
                handR.AddComponent<BetterHitSounds>();
                handL.AddComponent<BetterHitSounds>();
                handL.GetComponent<BetterHitSounds>().LeftController = true;
            }
            else
            {
                Debug.Log("Your controllers already have the hitsound script!");
            }
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Soft Chimp Loco Hitsound"))
        {
            var window = ScriptableObject.CreateInstance<AdvancedHitsound>();
            window.Show();
        }
        GUILayout.Space(10); //hitsounds
        GUILayout.Label("Quick Scripts (Applies to selected objects)", EditorStyles.largeLabel);
        GUILayout.Space(5);
        if (GUILayout.Button("Add MonkeButton"))
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.GetComponent<MeshRenderer>())
                    hadMesh = true;
                else
                    hadMesh = false;
                if (obj)
                {
                    obj.AddComponent<MonkeButton>();
                    obj.GetComponent<Collider>().isTrigger = true;
                }
                
            }
        GUILayout.Space(5);
        if (GUILayout.Button("Remove MonkeButton"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.GetComponent<MonkeButton>())
                {
                    DestroyImmediate(obj.GetComponent<MonkeButton>());
                    DestroyImmediate(obj.GetComponent<AudioSource>());
                    if (!hadMesh)
                    {
                        DestroyImmediate(obj.GetComponent<MeshRenderer>());
                    }
                    hadMesh = false;
                }
            }
        }
        GUILayout.Space(10);
        slipTag = EditorGUILayout.TagField(slipTag);
        if (GUILayout.Button("Make Object Slippery"))
        {
            PhysicMaterial physicMaterial = new PhysicMaterial("Slippery");
            physicMaterial.dynamicFriction = 0;
            physicMaterial.staticFriction = 0;
            physicMaterial.frictionCombine = 0;
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.GetComponent<Collider>())
                    obj.GetComponent<Collider>().material = physicMaterial;
                else
                    Debug.LogError("This object is missing a collider!");
                obj.tag = slipTag;
            }
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Return Objects friction"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Collider collider = obj.GetComponent<Collider>();
                if (collider && obj)
                {
                    obj.GetComponent<Collider>().material = null;
                    obj.tag = "Untagged";
                }
                else if (!collider)
                    Debug.Log("This object doesn't have a collider!");
            }
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Make Objects Grabable"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.GetComponent<Collider>())
                {
                    obj.GetComponent<Collider>().material = null;
                    obj.AddComponent<Rigidbody>();
                    obj.AddComponent<XRGrabNetworkInteractable>();
                    obj.AddComponent<DisableColliderOnGrab>();
                    obj.GetComponent<DisableColliderOnGrab>().grabInteractable = obj.GetComponent<XRGrabNetworkInteractable>();
                    obj.GetComponent<DisableColliderOnGrab>().colliderToDisable = obj.GetComponent<Collider>();
                    obj.AddComponent<PhotonView>();
                    obj.AddComponent<PhotonRigidbodyView>();
                    obj.GetComponent<XRGrabNetworkInteractable>().useDynamicAttach = true;
                    obj.GetComponent<PhotonRigidbodyView>().m_TeleportIfDistanceGreaterThan = 3f;
                    obj.GetComponent<PhotonRigidbodyView>().m_TeleportEnabled = true;
                    obj.GetComponent<PhotonRigidbodyView>().m_SynchronizeVelocity = true;
                    obj.GetComponent<PhotonRigidbodyView>().m_SynchronizeAngularVelocity = true;
                }
            }
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Remove Grabable Components"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                DestroyImmediate(obj.GetComponent<XRGrabNetworkInteractable>());
                DestroyImmediate(obj.GetComponent<DisableColliderOnGrab>());
                DestroyImmediate(obj.GetComponent<PhotonView>());
                DestroyImmediate(obj.GetComponent<PhotonRigidbodyView>());
                DestroyImmediate(obj.GetComponent<Rigidbody>());
            }
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Make objects disconnect you"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (lm != 0)
                {
                    obj.AddComponent<Disconnect>();
                    obj.GetComponent<Collider>().isTrigger = true;
                    obj.layer = lm;
                }
                else
                    Debug.LogError("Non-Walkable is not set!");
            }
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Remove disconnect components"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                if (obj.GetComponent<Disconnect>())
                {
                    DestroyImmediate(obj.GetComponent<Disconnect>());
                    obj.GetComponent<Collider>().isTrigger = false;
                    obj.layer = 0;
                }
                else
                    Debug.LogError("Object " + Selection.activeGameObject + " does not have a Disconnect component!");
            }
        }
        GUILayout.Space(10); //unrelated
        GUILayout.Label("Objects", EditorStyles.largeLabel);
        GUILayout.TextArea("Find prefab at: " + Application.dataPath + "/Editor/JohnsTools/Prefabs");
        prefab1 = (GameObject)EditorGUILayout.ObjectField(prefab1, typeof(Object), true);
        if (GUILayout.Button("Create Cosmetic Stand"))
            MakeCosmeticStand(prefab1);
        GUILayout.Space(5);
        GUILayout.Label("Material");
        material = (Material)EditorGUILayout.ObjectField(material, typeof(Material), true);
        if (GUILayout.Button("Give Selected Objects a material"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                obj.GetComponent<Renderer>().material = material;
            }
        }
        GUILayout.Space(10);
        GUILayout.Label("Support me!", EditorStyles.largeLabel);
        GUILayout.Space(5);
        if (GUILayout.Button("Subscribe to my YouTube"))
        {
            Application.OpenURL("https://www.youtube.com/@John-The-Dev");
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Visit the GitHub"))
        {
            Application.OpenURL("https://github.com/PineappleJohn/Johns-Tools");
        }
        GUILayout.Space(5);
        if (GUILayout.Button("Follow me on TikTok"))
        {
            Application.OpenURL("https://www.tiktok.com/@pineapple.john?is_from_webapp=1&sender_device=pc");
        }

        GUILayout.EndScrollView();
    }
    public void MakeHitsound()
    {
        if (handL != null && handR != null && audioTag != null && sound != null)
        if (handL.name == "LeftHand Controller" && handR.name == "RightHand Controller")
        {
            BetterHitSounds bhsR;
            BetterHitSounds bhsL;
            bhsR = handR.GetComponent<BetterHitSounds>();
            bhsL = handL.GetComponent<BetterHitSounds>();

            //bhsR.tagSounds.AddRange(Haha.tag = audioTag, Haha.sounds[Haha.sounds.Length + 1]); og code lol
            bhsR.tagSounds.Add(hitSound = new TagSound(audioTag, sound));
            hitSound.tag = audioTag;
            hitSound.sounds[0] = sound; //THISD TOOK SO LONG WHY CANT I ADD TO AN ARRAY
            bhsL.tagSounds = bhsR.tagSounds;
        }
        else if (handL.name != "LeftHand Controller" || handR.name != "RightHand Controller")
        {
            Debug.LogError("Names are not default, therefore not detected. To fix, set the names to: LeftHand Controller, and RightHand Controller");
        }
    }

    public void MakeCosmeticStand(GameObject prefab)
    {
        if (Selection.activeTransform)
            PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);
        else
            PrefabUtility.InstantiatePrefab(prefab);
    }
}
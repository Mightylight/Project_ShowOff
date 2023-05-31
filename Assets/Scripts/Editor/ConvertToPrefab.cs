using UnityEngine;
using UnityEditor;

public class ConvertToPrefab : EditorWindow
{
    public GameObject prefab;
    public bool preserveTransform = true;

    [MenuItem("Tools/Convert Selected Objects to Prefab")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<ConvertToPrefab>();
    }

    void OnGUI()
    {
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        preserveTransform = EditorGUILayout.Toggle("Preserve Transform", preserveTransform);

        if (GUILayout.Button("Convert Selected Objects to Prefab"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                GameObject newObject;
                if (preserveTransform)
                {
                    newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, obj.transform.parent);
                    newObject.transform.position = obj.transform.position;
                    newObject.transform.rotation = obj.transform.rotation;
                    newObject.transform.localScale = obj.transform.localScale;
                }
                else
                {
                    newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab, obj.transform.parent);
                    newObject.transform.SetSiblingIndex(obj.transform.GetSiblingIndex());
                }
                newObject.name = prefab.name;
                Undo.RegisterCreatedObjectUndo(newObject, "Convert to Prefab");
                Undo.DestroyObjectImmediate(obj);
            }
        }
    }
}
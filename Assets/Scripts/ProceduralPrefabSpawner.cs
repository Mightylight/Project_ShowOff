using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[AddComponentMenu("Custom/Procedural Prefab Spawner")]
public class ProceduralPrefabSpawner : MonoBehaviour
{
    public int maxPrefabs = 100;

    // Bounds of the spawn area
    public Vector3 bounds = new Vector3(10f, 0f, 10f);

    // List of prefabs to spawn
    public GameObject[] prefabs;

    // Height of the spawn area
    public float height = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveAllChildren()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public void SpawnProceduralPrefabs()
    {
        // Spawn prefabs at the given height all within the bounds of the spawn area
        for (int i = 0; i < maxPrefabs; i++)
        {
            // Get a random prefab from the list
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

            // Get a random position within the custom bounds
            Vector3 position = new Vector3(
                Random.Range(-bounds.x, bounds.x),
                height,
                Random.Range(-bounds.z, bounds.z)
            );

            // Check for overlap before spawning
            bool overlapDetected = CheckOverlap(position);
            if (overlapDetected)
            {
                // If overlap detected, skip spawning this prefab
                continue;
            }

            // Instantiate the prefab
            GameObject newObject = Instantiate(prefab, transform);

            // Set the position relative to the object's local space
            newObject.transform.localPosition = position;
            newObject.name = prefab.name;

            // Random size
            // float size = Random.Range(0.25f, 1f);
            // newObject.transform.localScale = new Vector3(size, size, size);

            // Random yaw rotation
            newObject.transform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // New position with custom height
        Gizmos.DrawWireCube(transform.position + new Vector3(0f, height, 0f), bounds);
    }

    private bool CheckOverlap(Vector3 position)
    {
        // Get all the colliders within a certain range of the position
        Collider[] colliders = Physics.OverlapSphere(position, 2.5f);

        // Check if any colliders are detected
        if (colliders.Length > 0)
        {
            // Check if any of the colliders are part of the spawned prefabs
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("SpawnedPrefab"))
                {
                    // Overlap detected with existing prefab, return true
                    return true;
                }
            }
        }

        // No overlap detected, return false
        return false;
    }
}

#region newButton
#if UNITY_EDITOR
[CustomEditor(typeof(ProceduralPrefabSpawner))]
public class ProceduralPrefabSpawnerEditor : Editor
{
    SerializedProperty maxPrefabsProperty;
    SerializedProperty boundsProperty;
    SerializedProperty prefabsProperty;
    SerializedProperty heightProperty;

    public void OnEnable()
    {
        maxPrefabsProperty = serializedObject.FindProperty("maxPrefabs");
        boundsProperty = serializedObject.FindProperty("bounds");
        prefabsProperty = serializedObject.FindProperty("prefabs");
        heightProperty = serializedObject.FindProperty("height");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(maxPrefabsProperty);
        EditorGUILayout.PropertyField(boundsProperty);
        EditorGUILayout.PropertyField(prefabsProperty);
        EditorGUILayout.PropertyField(heightProperty);

        if (GUILayout.Button("Spawn Procedural Prefabs"))
        {
            ((ProceduralPrefabSpawner)target).SpawnProceduralPrefabs();
        }

        if (GUILayout.Button("Remove All Children"))
        {
            ((ProceduralPrefabSpawner)target).RemoveAllChildren();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
#endregion
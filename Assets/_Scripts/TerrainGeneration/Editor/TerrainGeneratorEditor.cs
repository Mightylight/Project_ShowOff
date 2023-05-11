using UnityEditor;
using UnityEngine;

namespace _Scripts.TerrainGeneration.Editor
{
    [CustomEditor(typeof(TerrainGenerator))]
    public class TerrainGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Space(20);
            var terrainGenerator = (TerrainGenerator)target;
            if (GUILayout.Button("Generate Terrain"))
            {
                terrainGenerator.GenerateStartingTerrain();
            }

            if (GUILayout.Button("Clear Terrain"))
            {
                terrainGenerator.ClearChildren();
            }
            GUILayout.Space(20);
            base.OnInspectorGUI();
        }
    }
}

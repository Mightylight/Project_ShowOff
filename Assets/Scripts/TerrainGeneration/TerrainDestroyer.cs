using UnityEngine;

namespace TerrainGeneration
{
    [RequireComponent(typeof(Collider))]
    public class TerrainDestroyer : MonoBehaviour
    {
        [SerializeField] private TerrainGenerator _terrainGenerator;

        private void OnTriggerEnter(Collider pOther)
        {
            //TODO:Reuse pieces for optimization instead of destroying them
            //Debug.Log("Hey?");
            if (!pOther.GetComponent<TerrainPiece>()) return;
            
            Destroy(pOther.gameObject);
            //Debug.Log("Destroyed");
            _terrainGenerator.RemoveTerrainSegment(pOther.GetComponent<TerrainPiece>());
            _terrainGenerator.GenerateNextSegment();
        }
    }
}

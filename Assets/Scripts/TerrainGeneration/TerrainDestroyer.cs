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
            if (!pOther.GetComponent<TerrainPiece>()) return;
            
            Destroy(pOther.gameObject);
        }
    }
}

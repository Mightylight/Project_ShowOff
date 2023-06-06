using UnityEngine;

namespace TerrainGeneration
{
    public class TerrainTrigger : MonoBehaviour
    {
        [SerializeField] private TerrainGenerator _terrainGenerator;

        private void Awake()
        {
            //Necessary to find the TerrainGenerator in the scene, since it is not a singleton,
            //maybe i should make a singleton
        
            _terrainGenerator = FindObjectOfType<TerrainGenerator>();
        }

        private void OnTriggerEnter(Collider pOther)
        {
            _terrainGenerator.EnableNextSegment();
        }
    
    }
}

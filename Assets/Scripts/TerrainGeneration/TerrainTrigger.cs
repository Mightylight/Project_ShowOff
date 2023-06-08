using UnityEngine;

namespace TerrainGeneration
{
    public class TerrainTrigger : MonoBehaviour
    {
        [SerializeField] private TerrainGenerator _terrainGenerator;
        public bool _hasBeenActivated = false;
        public bool _isLastOfSegment = false;

        private void Awake()
        {
            //Necessary to find the TerrainGenerator in the scene, since it is not a singleton,
            //maybe i should make a singleton out of it
        
            _terrainGenerator = FindObjectOfType<TerrainGenerator>();
        }

        private void OnTriggerEnter(Collider pOther)
        {
            if(pOther.CompareTag("Canoe") || pOther.CompareTag("alligator") && !_hasBeenActivated)
            {
                _terrainGenerator.EnableNextSegment();
                _hasBeenActivated = true;
                if (_isLastOfSegment)
                {
                    _terrainGenerator.OnSegmentChange?.Invoke();
                }
            }
        }
        
    
    }
}

using UnityEngine;

namespace TerrainGeneration
{
    public class TerrainTrigger : MonoBehaviour
    {
        [SerializeField] private TerrainGenerator _terrainGenerator;
        public bool _hasBeenActivated = false;
        public bool _isLastOfSegment = false;
        public int _segmentIndex = -1;

        private void Awake()
        {
            //Necessary to find the TerrainGenerator in the scene, since it is not a singleton,
            //maybe i should make a singleton out of it
        
            _terrainGenerator = FindObjectOfType<TerrainGenerator>();
        }

        private void OnTriggerEnter(Collider pOther)
        {
            Debug.Log("i beg u pls register stuff");
            if(pOther.CompareTag("Canoe") || pOther.CompareTag("alligator") && !_hasBeenActivated)
            {                
                    _hasBeenActivated = true;
                    if (_isLastOfSegment)
                    {
                        if (_segmentIndex == 1)
                        {
                            Debug.Log("screeaammm2");
                            _terrainGenerator.OnSegmentLastChange?.Invoke();
                        }
                        if (_segmentIndex == 0)
                        {
                            Debug.Log("screeaammm");
                            _terrainGenerator.OnSegmentMidChange?.Invoke();
                        }

                    }

                    else
                    {
                        _terrainGenerator.EnableNextSegment();
                    }
                
            }
        }
        
    
    }
}

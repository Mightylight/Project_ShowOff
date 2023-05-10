using UnityEngine;

namespace _Scripts
{
    public class TerrainGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] _terrainTiles;
        [SerializeField] private GameObject _player;
        [SerializeField] private float _tileSize;

        
        
        
        private void Start()
        {
            for (int i = -1; i < 2; i++)
            {
                int index = Random.Range(0, _terrainTiles.Length);
                Instantiate(_terrainTiles[index], new Vector3(0, 0, i * _tileSize), Quaternion.identity);
            }
        }
        
        

    }
}

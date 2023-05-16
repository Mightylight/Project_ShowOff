using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneration
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private List<Transform> _obstacleSpawnpoints = new List<Transform>();
        [SerializeField] private List<GameObject> _obstaclesToSpawn = new List<GameObject>();

        
        [SerializeField] private float spawnTime = 5f;
        private float timer;



        void Update()
        {
            
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                SpawnObstacle();
                timer = 0;
            }
        }

        private void SpawnObstacle()
        {
            int randomSpawnPoint = Random.Range(0, _obstacleSpawnpoints.Count);
            int randomObstacle = Random.Range(0, _obstaclesToSpawn.Count);
            Instantiate(_obstaclesToSpawn[randomObstacle], _obstacleSpawnpoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }
}

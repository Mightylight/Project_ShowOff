using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TerrainGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        [Header("Generation Settings")]

        [Range(1, 200)] 
        [SerializeField] private int _terrainLength = 1;

        [SerializeField] private int _initialStartingTerrainLength = 1;
        
        
        [SerializeField] private float _tileSize;
        [SerializeField] private float _speed;
        
        
        
        [FormerlySerializedAs("_riverTiles")]
        [Header("Generation Objects")]
        [SerializeField] private TerrainTiles[] _tiles;
        [SerializeField] private Transform _tileParent;


        private readonly List<TerrainPiece> _terrain = new List<TerrainPiece>();
        private readonly List<TerrainPiece> _inactiveTerrainPieces = new List<TerrainPiece>();



        private void Start()
        {
            GenerateStartingTerrain();
        }

        private void MoveTerrain()
        {
            int children = _tileParent.childCount;
            for (var i = children - 1; i >= 0; i--)
            {
                GameObject child = _tileParent.GetChild(i).gameObject;
                child.transform.position += Vector3.back * _speed * Time.deltaTime;
            }
        }

        public void GenerateStartingTerrain()
        {
            ClearChildren();
            //Generate the middle river row
            for (int j = -1; j < _terrainLength; j++)
            {
                Vector3 pos = new Vector3(0, 0, j * _tileSize) + _tileParent.position;
                GenerateNewTerrainPiece(pos);
            }

            for (int i = 0; i < _initialStartingTerrainLength; i++)
            {
                EnableNextSegment();
            }
        }
        
        public void GenerateNextSegment()
        {
            GameObject lastTile = _terrain.Last().gameObject;
            Vector3 newPos = lastTile.transform.position + Vector3.forward * _tileSize;
            GenerateNewTerrainPiece(newPos);
        }

        public void EnableNextSegment()
        {
            GameObject tileToBeActivated = _inactiveTerrainPieces.First().gameObject;
            tileToBeActivated.SetActive(true);
            _inactiveTerrainPieces.Remove(tileToBeActivated.GetComponent<TerrainPiece>());
            GameObject tileToBeDeactivated = _terrain.First().gameObject;
            _terrain.Remove(tileToBeDeactivated.GetComponent<TerrainPiece>());
            tileToBeDeactivated.SetActive(false);
        }

        public void ClearChildren()
        {
            int children = _tileParent.childCount;
            for (var i = children - 1; i >= 0; i--)
            {
                DestroyImmediate(_tileParent.GetChild(i).gameObject);
            }
            _terrain.Clear();
        }

        public void RemoveTerrainSegment(TerrainPiece pTileToRemove)
        {
            // _terrain.Remove(pTileToRemove);
            // pTileToRemove.gameObject.SetActive(false);
            // _recycledTerrainTiles.Add(pTileToRemove);
        }

        private void GenerateNewTerrainPiece(Vector3 pNewPos, bool isActive = false)
        {
            TerrainPiece newTile =
                Instantiate(GetRandomWeightedTile(), pNewPos, Quaternion.identity, _tileParent);
            if (isActive)
            {
                newTile.gameObject.SetActive(true);
                _terrain.Add(newTile);
            }
            else
            {
                _inactiveTerrainPieces.Add(newTile);
                newTile.gameObject.SetActive(false);
            }
        }

        private TerrainPiece GetRandomWeightedTile()
        {
            int totalWeight = _tiles.Sum(x => x.weight);
            int randomWeight = Random.Range(0, totalWeight);
            int currentWeight = 0;
            foreach (TerrainTiles tile in _tiles)
            {
                currentWeight += tile.weight;
                if (currentWeight > randomWeight)
                {
                    return tile.tile;
                }
            }

            return null;
        }


    }
    [Serializable]
    public class TerrainTiles
    {
        public TerrainPiece tile;
        public int weight;
    }
}

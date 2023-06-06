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
        
        [Range(1,50)]
        [SerializeField] private int _riverWidth = 1;
        
        [Range(1,50)]
        [SerializeField] private int _terrainWidth = 1;
        
        [Range(1, 50)] 
        [SerializeField] private int _terrainLength = 1;
        
        [SerializeField] private float _tileSize;
        [SerializeField] private float _speed;
        
        
        
        [FormerlySerializedAs("_riverTiles")]
        [Header("Generation Objects")]
        //TODO: add weights to the tiles
        //[SerializeField] private TerrainPiece[] _terrainTiles;
        [SerializeField] private TerrainTiles[] _tiles;
        [SerializeField] private Transform _tileParent;


        private readonly List<TerrainPiece> _terrain = new List<TerrainPiece>();
        private readonly List<TerrainPiece> _recycledTerrainTiles = new List<TerrainPiece>();



        private void Start()
        {
            GenerateStartingTerrain();
        }

        private void Update()
        {
            MoveTerrain();
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
                // int index = Random.Range(0, _tiles.Length);
                // Instantiate(_tiles[index], new Vector3(0, 0, j * _tileSize) + _tileParent.position, Quaternion.identity,_tileParent);
                Vector3 pos = new Vector3(0, 0, j * _tileSize) + _tileParent.position;
                GenerateNewTerrainPiece(pos);
            }

            // //Generate the rest of the river
            // for (int i = 1; i < _riverWidth; i++)
            // {
            //     for (int j = -1; j < _terrainLength; j++)
            //     {
            //         int index = Random.Range(0, _tiles.Length);
            //         TerrainPiece riverTile = Instantiate(_tiles[index], new Vector3(i * _tileSize, 0, j * _tileSize)+ _tileParent.position,
            //             Quaternion.identity, _tileParent);
            //         _terrain.Add(riverTile);
            //         index = Random.Range(0, _tiles.Length);
            //         TerrainPiece riverTile2 = Instantiate(_tiles[index], new Vector3(-i * _tileSize, 0, j * _tileSize)+ _tileParent.position,
            //             Quaternion.identity, _tileParent);
            //         _terrain.Add(riverTile2);
            //     }
            // }


            //Generate the terrain around the river
            // for (int i = _riverWidth; i < _riverWidth + _terrainWidth; i++)
            // {
            //     for (int j = -1; j < _terrainLength; j++)
            //     {
            //         int index = Random.Range(0, _terrainTiles.Length);
            //         TerrainPiece terrainTile = Instantiate(_terrainTiles[index], 
            //             new Vector3(i * _tileSize, 0, j * _tileSize) + _tileParent.position, Quaternion.identity,_tileParent);
            //         _terrain.Add(terrainTile); 
            //         index = Random.Range(0, _terrainTiles.Length);
            //         TerrainPiece terrainTile2 = Instantiate(_terrainTiles[index], 
            //             new Vector3(-i * _tileSize, 0, j * _tileSize) + _tileParent.position, Quaternion.identity,_tileParent);
            //         _terrain.Add(terrainTile2);
            //     }
            // }
        }
        
        public void GenerateNextSegment()
        {
            GameObject lastTile = _terrain.Last().gameObject;
            Vector3 newPos = lastTile.transform.position + Vector3.forward * _tileSize;
            GenerateNewTerrainPiece(newPos);
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
            _terrain.Remove(pTileToRemove);
            pTileToRemove.gameObject.SetActive(false);
            _recycledTerrainTiles.Add(pTileToRemove);
        }

        private void GenerateNewTerrainPiece(Vector3 pNewPos)
        {
            TerrainPiece newTile =
                Instantiate(GetRandomWeightedTile(), pNewPos, Quaternion.identity, _tileParent);
            _terrain.Add(newTile);
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

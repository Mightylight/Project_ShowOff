using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.TerrainGeneration
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
        
        
        
        [Header("Generation Objects")]
        //TODO: add weights to the tiles
        [SerializeField] private TerrainPiece[] _terrainTiles;
        [SerializeField] private TerrainPiece[] _riverTiles;
        [SerializeField] private Transform _tileParent;

        //TODO: dynamically calculate the tile size
        [SerializeField] private float _tileSize;
        [SerializeField] private float _speed;
        

        [NonSerialized] public bool hasHitDestructor;
        
        private readonly List<TerrainPiece> _terrain = new List<TerrainPiece>();



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
                int index = Random.Range(0, _riverTiles.Length);
                Instantiate(_riverTiles[index], new Vector3(0, 0, j * _tileSize), Quaternion.identity,_tileParent);
            }
            
            //Generate the rest of the river
            for (int i = 1; i < _riverWidth; i++)
            {
                for (int j = -1; j < _terrainLength; j++)
                {
                    int index = Random.Range(0, _riverTiles.Length);
                    TerrainPiece riverTile = Instantiate(_riverTiles[index], new Vector3(i * _tileSize, 0, j * _tileSize),
                        Quaternion.identity, _tileParent);
                    _terrain.Add(riverTile);
                    index = Random.Range(0, _riverTiles.Length);
                    TerrainPiece riverTile2 = Instantiate(_riverTiles[index], new Vector3(-i * _tileSize, 0, j * _tileSize),
                        Quaternion.identity, _tileParent);
                    _terrain.Add(riverTile2);
                }
            }


            //Generate the terrain around the river
            for (int i = _riverWidth; i < _riverWidth + _terrainWidth; i++)
            {
                for (int j = -1; j < _terrainLength; j++)
                {
                    int index = Random.Range(0, _terrainTiles.Length);
                    TerrainPiece terrainTile = Instantiate(_terrainTiles[index], 
                        new Vector3(i * _tileSize, 0, j * _tileSize), Quaternion.identity,_tileParent);
                    _terrain.Add(terrainTile); 
                    index = Random.Range(0, _terrainTiles.Length);
                    TerrainPiece terrainTile2 = Instantiate(_terrainTiles[index], 
                        new Vector3(-i * _tileSize, 0, j * _tileSize), Quaternion.identity,_tileParent);
                    _terrain.Add(terrainTile2);
                }
            }
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
    }
}

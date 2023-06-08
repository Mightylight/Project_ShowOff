using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TerrainGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        [Header("Generation Settings")]

        [Range(1, 200)] 
        [SerializeField] private int _terrainLength = 1;
        [Tooltip("If true, the length of each segment will be used instead of the terrain length.")]
        [SerializeField] private bool _useSegmentLengths = false;


        [SerializeField] private int _initialStartingTerrainLength = 1;
        [SerializeField] private int _terrainPiecesBehindPlayer = 1;
        


        [SerializeField] private float _tileSize;
        [SerializeField] private float _speed;
        
        
        
        [FormerlySerializedAs("_riverTiles")]
        [Header("Generation Objects")]
        [SerializeField] private Segment[] _segments;
        //[SerializeField] private TerrainTiles[] _tiles;
        [SerializeField] private Transform _tileParent;

        [SerializeField] public UnityEvent OnSegmentChange;
        
        
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

            int amountOfSegments = _segments.Length;
            int posIndex = -_terrainPiecesBehindPlayer;

            foreach (Segment segment in _segments)
            {
                int segmentLength = Mathf.FloorToInt(_terrainLength / amountOfSegments);
                if (_useSegmentLengths)
                {
                    segmentLength = segment.length;
                }
                for (int i = 0; i < segmentLength ; i++)
                {
                    Vector3 pos = new Vector3(0, 0, (i + posIndex) * _tileSize) + _tileParent.position;
                    GenerateNewTerrainPiece(pos,segment);
                }

                if (_inactiveTerrainPieces.Last() != null)
                {
                    TerrainPiece lastOfSegment = _inactiveTerrainPieces.Last();
                    lastOfSegment.GetComponent<TerrainTrigger>()._isLastOfSegment = true;
                    Debug.Log("Last of segment: " + lastOfSegment.name);
                }

                posIndex += segmentLength;
            }
            

            for (int i = 0; i < _initialStartingTerrainLength; i++)
            {
                EnableNextSegment(false);
            }
            
            TerrainPiece firstTile = _terrain.First();
            if(firstTile.GetComponent<TerrainTrigger>() == null) return;
            firstTile.GetComponent<TerrainTrigger>()._hasBeenActivated = true;
        }
        
        public void EnableNextSegment(bool pDeleteSegments = true)
        {
            GameObject tileToBeActivated = _inactiveTerrainPieces.First().gameObject;
            tileToBeActivated.SetActive(true);
            _terrain.Add(tileToBeActivated.GetComponent<TerrainPiece>());
            _inactiveTerrainPieces.Remove(tileToBeActivated.GetComponent<TerrainPiece>());
            if (_terrain.Count <= 1) return;
            if (!pDeleteSegments) return;
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
            _inactiveTerrainPieces.Clear();
        }

        private void GenerateNewTerrainPiece(Vector3 pNewPos,Segment pSegment, bool pIsActive = false)
        {
            TerrainPiece newTile =
                Instantiate(GetRandomWeightedTile(pSegment.terrainTiles), pNewPos, Quaternion.identity, _tileParent);
            if (pIsActive)
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

        private TerrainPiece GetRandomWeightedTile(TerrainTiles[] pTiles)
        {
            int totalWeight = pTiles.Sum(x => x.weight);
            int randomWeight = Random.Range(0, totalWeight);
            int currentWeight = 0;
            foreach (TerrainTiles tile in pTiles)
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
    
    [Serializable]
    public class Segment
    {
        public string name;
        public int length;
        public TerrainTiles[] terrainTiles;
    }
}

using System;
using UnityEngine;

namespace _Scripts.TerrainGeneration
{
    [RequireComponent(typeof(Collider))]
    public class TerrainDestroyer : MonoBehaviour
    {
        [SerializeField] TerrainGenerator _terrainGenerator;

        private void OnTriggerEnter(Collider pOther)
        {
            //TODO:Reuse pieces for optimization instead of destroying them
            Debug.Log("Hey?");
            if (pOther.GetComponent<TerrainPiece>())
            {
                Destroy(pOther.gameObject);
                Debug.Log("Destroyed");
                _terrainGenerator.hasHitDestructor = true;
            }
        }
    }
}

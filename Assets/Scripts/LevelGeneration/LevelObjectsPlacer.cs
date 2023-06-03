using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.LevelGeneration
{
    public class LevelObjectsPlacer : MonoBehaviour
    {
        private Transform startPoint;
        [SerializeField] ObjectPlacementConstrains endPoint;

        [SerializeField] uint numberOfGodTurrets = 3;
        [SerializeField] ObjectPlacementConstrains turretPrefab;

        [SerializeField] uint numberOfSheildPickUps = 2;
        [SerializeField] ObjectPlacementConstrains sheildPickupPrefab;

        private List<int> openSlots;
        private List<GameObject> floorTiles;

        private void Start()
        {
            startPoint = new GameObject("Start Point").transform;
            floorTiles = GetComponent<MazeRenderer>().GetFloorTiles();
            openSlots.Capacity = floorTiles.Count;
            for (int i = 0; i < floorTiles.Count; i++)
                openSlots.Add(i);

            PlaceStartAndEnd();
            PlaceItems(turretPrefab, numberOfGodTurrets);
            PlaceItems(sheildPickupPrefab, numberOfSheildPickUps);
        }

        private void PlaceStartAndEnd()
        {

        }

        private void PlaceItems(ObjectPlacementConstrains item, uint amount)
        {

        }
    }
}
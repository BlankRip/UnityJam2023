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

        private int startPointIndex;
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

            List<ObjectPlacementConstrains> tempList = new List<ObjectPlacementConstrains>();
            tempList.Add(turretPrefab);
            PlaceItems(tempList, numberOfGodTurrets);

            tempList.Clear();
            tempList.Add(sheildPickupPrefab);
            PlaceItems(tempList, numberOfSheildPickUps);
        }

        private void PlaceStartAndEnd()
        {
            int tileDeviationRange = 15;
            int pickedIndex = MazeGenerator.GetRandomFromRange(0, tileDeviationRange);
            startPoint.position = floorTiles[pickedIndex].transform.position;
            startPoint.position += new Vector3(0, 1.5f, 0);
            startPointIndex = pickedIndex;
            openSlots.Remove(pickedIndex);
            
            pickedIndex = MazeGenerator.GetRandomFromRange(floorTiles.Count - tileDeviationRange, floorTiles.Count);
            ObjectPlacementConstrains spawnedEnd = Instantiate(endPoint);
            spawnedEnd.transform.position = floorTiles[pickedIndex].transform.position;
            spawnedEnd.transform.position += spawnedEnd.placementOffset;
            openSlots.Remove(pickedIndex);
        }

        private void PlaceItems(List<ObjectPlacementConstrains> items, uint amount, bool oneOfAKind = false, bool canPlaceNearStart = false)
        {
            List<int> placedIndices = new List<int>();
            if(!canPlaceNearStart)
                placedIndices.Add(startPointIndex);
            int pickedIndex = 0;
            ObjectPlacementConstrains spawnedItem;
            for (int i = 0; i < amount; i++)
            {
                int pickedItem = MazeGenerator.GetRandomFromRange(0, items.Count);
                spawnedItem = Instantiate(items[pickedItem]);
                if(TryPlaceItem(ref placedIndices, ref spawnedItem))
                {
                    if(oneOfAKind)
                        items.RemoveAt(pickedIndex);
                }
                else
                {
                    Debug.Log($"Failed to place {spawnedItem.gameObject.name}, after placing {i} number");
                    break;
                }
            }
        }

        private bool TryPlaceItem(ref List<int> placedIndices, ref ObjectPlacementConstrains spawnedItem)
        {
            int iterations = 0;
            int maxPlacementIterations = 5;
            int pickedIndex = 0;
            while (iterations < maxPlacementIterations)
            {
                pickedIndex = MazeGenerator.GetRandomFromRange(0, openSlots.Count);
                if(CanPlace(ref placedIndices, ref spawnedItem, pickedIndex))
                {
                    spawnedItem.transform.position = floorTiles[pickedIndex].transform.position;
                    spawnedItem.transform.position += spawnedItem.placementOffset;
                    openSlots.RemoveAt(pickedIndex);
                    return true;
                }
                else
                {
                    iterations++;
                }
            }
            return false;
        }

        private bool CanPlace(ref List<int> placedIndices, ref ObjectPlacementConstrains constrains, int pickedIndex)
        {
            float distance;
            float gapTocheck = constrains.minDistanceBtwObjs * constrains.minDistanceBtwObjs;
            for (int i = 0; i < placedIndices.Count; i++)
            {
                distance = (floorTiles[pickedIndex].transform.position - floorTiles[placedIndices[i]].transform.position).sqrMagnitude;
                if(distance < gapTocheck)
                    return false;
            }

            return true;
        }
    }
}
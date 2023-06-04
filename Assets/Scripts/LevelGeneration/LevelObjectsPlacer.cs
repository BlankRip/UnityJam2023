using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.UI;
using Gameplay.AI;

namespace Gameplay.LevelGeneration
{
    public class LevelObjectsPlacer : MonoBehaviour
    {
        private Transform startPoint;
        [SerializeField] ObjectPlacementConstrains endPoint;
        [Space]
        [SerializeField] uint numberOfGodTurrets = 3;
        [SerializeField] ObjectPlacementConstrains turretPrefab;
        [SerializeField] TurretMaterialObject materialObject;
        [SerializeField] uint numberOfMortalTurrets = 3;
        [SerializeField] ObjectPlacementConstrains turretSwitchPrefab;
        [Space]
        [SerializeField] uint numberOfSheildPickUps = 2;
        [SerializeField] ObjectPlacementConstrains sheildPickupPrefab;
        [Space]
        [Header("Unlocking Player for Play")]
        [SerializeField] GameObject blackScreen;
        [SerializeField] Pause pauseObject;
        [SerializeField] PlayerController playerObject;      

        private int startPointIndex;
        private List<int> openSlots;
        private List<GameObject> floorTiles;
        private Transform startEndParent, godTurretParent, sheildPickUpParent, moralTurretParent;

        public void PlaceObjects(MazeRenderer caller)
        {
            CreateParentObjects();
            materialObject.ResetMaterialsList();
            startPoint = new GameObject("Start Point").transform;
            floorTiles = caller.GetFloorTiles();
            openSlots = new List<int>();
            openSlots.Capacity = floorTiles.Count;
            for (int i = 0; i < floorTiles.Count; i++)
                openSlots.Add(i);

            PlaceStartAndEnd();

            List<ObjectPlacementConstrains> tempList = new List<ObjectPlacementConstrains>();
            tempList.Add(turretPrefab);
            PlaceItems(tempList, ref godTurretParent, numberOfGodTurrets);

            tempList.Clear();
            tempList.Add(sheildPickupPrefab);
            PlaceItems(tempList, ref sheildPickUpParent, numberOfSheildPickUps);

            MakeGameReadyToPlay();
        }

        private void PlaceStartAndEnd()
        {
            int tileDeviationRange = 15;
            int pickedIndex = MazeGenerator.GetRandomFromRange(0, tileDeviationRange);
            startPoint.position = floorTiles[pickedIndex].transform.position;
            startPoint.position += new Vector3(0, 1.5f, 0);
            startPoint.parent = startEndParent;
            startPointIndex = pickedIndex;
            openSlots.Remove(pickedIndex);
            
            pickedIndex = MazeGenerator.GetRandomFromRange(floorTiles.Count - tileDeviationRange, floorTiles.Count);
            ObjectPlacementConstrains spawnedEnd = Instantiate(endPoint);
            spawnedEnd.transform.position = floorTiles[pickedIndex].transform.position;
            spawnedEnd.transform.position += spawnedEnd.placementOffset;
            spawnedEnd.transform.parent = startEndParent;
            openSlots.Remove(pickedIndex);
        }

        private void PlaceItems(List<ObjectPlacementConstrains> items, ref Transform parent, uint amount, bool oneOfAKind = false, bool canPlaceNearStart = false)
        {
            List<int> placedIndices = new List<int>();
            if(!canPlaceNearStart)
                placedIndices.Add(startPointIndex);
            ObjectPlacementConstrains spawnedItem;
            for (int i = 0; i < amount; i++)
            {
                int pickedItem = MazeGenerator.GetRandomFromRange(0, items.Count);
                spawnedItem = Instantiate(items[pickedItem]);
                if(TryPlaceItem(ref placedIndices, ref spawnedItem, ref parent))
                {
                    if(oneOfAKind)
                        items.RemoveAt(pickedItem);
                }
                else
                {
                    Debug.Log($"Failed to place {spawnedItem.gameObject.name}, after placing {i} number");
                    Destroy(spawnedItem.gameObject);
                    break;
                }
            }
        }

        private bool TryPlaceItem(ref List<int> placedIndices, ref ObjectPlacementConstrains spawnedItem, ref Transform parent)
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
                    spawnedItem.transform.parent = parent;
                    placedIndices.Add(pickedIndex);
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

        private void MakeGameReadyToPlay()
        {
            playerObject.TeleportPlayerToPosition(startPoint.position);
            pauseObject.gameObject.SetActive(true);
            blackScreen.SetActive(false);
            Destroy(GetComponent<MazeRenderer>());
            Destroy(this);
        }

        private void CreateParentObjects()
        {
            startEndParent = new GameObject("StartEndParent").transform;
            startEndParent.parent = this.transform;
            startEndParent.localPosition = Vector3.zero;
            godTurretParent = new GameObject("GodTurretsParent").transform;
            godTurretParent.parent = this.transform;
            godTurretParent.localPosition = Vector3.zero;
            sheildPickUpParent = new GameObject("SheildPickUpsParent").transform;
            sheildPickUpParent.parent = this.transform;
            sheildPickUpParent.localPosition = Vector3.zero;
            moralTurretParent = new GameObject("MortalTurretParent").transform;
            moralTurretParent.parent = this.transform;
            moralTurretParent.localPosition = Vector3.zero;
        }
    }
}
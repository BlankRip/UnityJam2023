using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.LevelGeneration
{
    // This class uses the MazeGenerator class to generate maze input values, and using these values
    // the maze is generated using the wall, floor, celing prefbas
    public class MazeRenderer : MonoBehaviour
    {
        [SerializeField] uint width = 10;
        [SerializeField] uint hight = 10;
        [Tooltip("Up Walls is used as the prefab")]
        [SerializeField] float cellSize = 1;
        [SerializeField] GameObject floor;
        [SerializeField] GameObject celing;
        [SerializeField] float wallHight = 1;
        [SerializeField] GameObject wall;

        // This section is added for the code demo purpous so that the scripts can be put in unity and tested right away
        [Space]
        [Header("Useing Seed")]
        [SerializeField] bool useSeed = false;
        [Tooltip("Seed value is only considered if useSeed bool is ticked to true")]
        [SerializeField] int seedValue;

        private WallState[,] maze;
        private float halfCellSize;
        private List<GameObject> floorTiles;
        Transform floorParent, celingParent, wallParent;

        private void Start()
        {
            if (useSeed)
                CreateMazeAndGetFloorTiles(seedValue);
            else
                CreateMazeAndGetFloorTiles(Random.Range(int.MinValue, int.MaxValue));
        }

        public List<GameObject> CreateMazeAndGetFloorTiles(int seed)
        {
            maze = MazeGenerator.Generate(width, hight, seed);
            halfCellSize = cellSize / 2;
            wallHight = wallHight / 2;

            floorTiles = new List<GameObject>();
            RenderMaze();

            return floorTiles;
        }

        private void RenderMaze()
        {
            CreateParentObjects();
            Vector3 euler0 = new Vector3(0, 0, 0);
            Vector3 eluler90 = new Vector3(0, 90, 0);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    WallState cell = maze[i, j];
                    Vector3 pos = transform.position + new Vector3(-width / 2 + (i * cellSize), 0, -hight / 2 + (j * cellSize)); //Finding center point of the cell

                    //Placing the floor and celing for the cell
                    Transform floorPiece = GameObject.Instantiate(floor, transform).transform;
                    PlacePiece(ref floorPiece, ref floorParent, pos + new Vector3(0, -wallHight, 0), euler0);
                    floorTiles.Add(floorPiece.gameObject);
                    //This extra check as it is easier to see the demo with out the celing placed
                    if (celing != null)
                    {
                        Transform celingPiece = GameObject.Instantiate(celing, transform).transform;
                        PlacePiece(ref celingPiece, ref celingParent, pos + new Vector3(0, wallHight, 0), euler0);
                    }

                    //If certain flag is raised then placing a wall at it's respective location
                    if (cell.HasFlag(WallState.Up))
                    {
                        Transform topWall = GameObject.Instantiate(wall, transform).transform;
                        PlacePiece(ref topWall, ref wallParent, pos + new Vector3(0, 0, halfCellSize), euler0);
                    }
                    if (cell.HasFlag(WallState.Left))
                    {
                        Transform leftWall = GameObject.Instantiate(wall, transform).transform;
                        PlacePiece(ref leftWall, ref wallParent, pos + new Vector3(-halfCellSize, 0, 0), eluler90);
                    }

                    // Extra checks so that the edge walls are always placed
                    if (j == 0)
                    {
                        if (cell.HasFlag(WallState.Down))
                        {
                            Transform downWall = GameObject.Instantiate(wall, transform).transform;
                            PlacePiece(ref downWall, ref wallParent, pos + new Vector3(0, 0, -halfCellSize), euler0);
                        }
                    }
                    if (i == width - 1)
                    {
                        if (cell.HasFlag(WallState.Right))
                        {
                            Transform rightWall = GameObject.Instantiate(wall, transform).transform;
                            PlacePiece(ref rightWall, ref wallParent, pos + new Vector3(halfCellSize, 0, 0), eluler90);
                        }
                    }
                }
            }
        }

        private void PlacePiece(ref Transform piece, ref Transform parent, Vector3 position, Vector3 elulerAngles)
        {
            piece.position = position;
            piece.eulerAngles = elulerAngles;
            piece.parent = parent;
        }

        private void CreateParentObjects()
        {
            floorParent = new GameObject("FloorParent").transform;
            floorParent.parent = this.transform;
            floorParent.localPosition = Vector3.zero;
            wallParent = new GameObject("WallParent").transform;
            wallParent.parent = this.transform;
            wallParent.localPosition = Vector3.zero;
            if (celing != null)
            {
                celingParent = new GameObject("CelingParent").transform;
                celingParent.parent = this.transform;
                celingParent.localPosition = Vector3.zero;
            }
        }
    }
}
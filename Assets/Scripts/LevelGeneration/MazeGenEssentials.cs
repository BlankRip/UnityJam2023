using System.Collections;
using System.Collections.Generic;

namespace Gameplay.LevelGeneration
{
    // This File has all the class/enums/structs that are needed by the maze generator

    [System.Flags]
    public enum WallState
    {
        Left = 1, /*0001*/ Right = 2, /*0010*/
        Up = 4, /*0100*/ Down = 8, /*1000*/
        Visited = 32 // 10 0000
    }

    public class Neighbour
    {
        public Vector2Int position;
        public WallState sharedWall;

        public Neighbour(Vector2Int pos, WallState state)
        {
            position = pos;
            sharedWall = state;
        }
    }

    // Created own Vecotr2Int so that the maze generator will not be dependent on Unty for this class
    public class Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

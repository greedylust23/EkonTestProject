using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PathFinder
    {
        public Vector2Int[] FindPath(Vector2Int S, Vector2Int E)
        {
            int topLimit = S.y + 1;
            int rightLimit = S.x + 1;
            int bottomLimit = S.y - 1;
            int leftLimit = S.x - 1;
            Vector2Int currentPosition = S;
            Vector2Int currentDirection = Vector2Int.up;
            List<Vector2Int> pathCoordinates = new List<Vector2Int>() { currentPosition };

            while (currentPosition != E)
            {
                currentPosition += currentDirection;

                if (currentDirection == Vector2Int.up && currentPosition.y == topLimit)
                {
                    currentDirection = Vector2Int.right;
                    topLimit++;
                }
                else if (currentDirection == Vector2Int.right && currentPosition.x == rightLimit)
                {
                    currentDirection = Vector2Int.down;
                    rightLimit++;
                }
                else if (currentDirection == Vector2Int.down && currentPosition.y == bottomLimit)
                {
                    currentDirection = Vector2Int.left;
                    bottomLimit--;
                }
                else if (currentDirection == Vector2Int.left && currentPosition.x == leftLimit)
                {
                    currentDirection = Vector2Int.up;
                    leftLimit--;
                }

                pathCoordinates.Add(currentPosition);
            }

            return pathCoordinates.ToArray();
        }

        //TracePath is a coroutine that moves a GameObject along a given path
        //unit: the visual GameObject to be moved along the path
        //path: the array of vectors that the path consists of
        //speed: the number of points traversed per second

        public IEnumerator TracePath(GameObject unit, Vector2Int[] path, float speed)
        {
            //we cannot navigate along a path without at least two points
            if (path.Length < 2)
                yield break;

            for (int i = 0; i < path.Length - 1; i++)
            {
                float t = 0;
                Vector2Int from = path[i];
                Vector2Int to = path[i + 1];

                while (t <= 1f / speed)
                {
                    t += Time.deltaTime;
                    unit.transform.position =
                           Vector3.Lerp(new Vector3(from.x, from.y), new Vector3(to.x, to.y), t * speed);
                    yield return null;
                }
                unit.transform.position = new Vector3(to.x, to.y);
            }
        }
    }
}


using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public float triangleSize = 2f;
    public int rows = 5;
    public int cols = 5;

    private List<Vector2> visitedCells = new List<Vector2>();
    private Stack<Vector2> stack = new Stack<Vector2>();

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        Vector2 startCell = new Vector2(0, 0); // Start at the center
        stack.Push(startCell);
        visitedCells.Add(startCell);

        while (stack.Count > 0)
        {
            Vector2 currentCell = stack.Pop();
            List<Vector2> neighbors = GetUnvisitedNeighbors(currentCell);

            if (neighbors.Count > 0)
            {
                stack.Push(currentCell); // Push current cell back onto the stack

                Vector2 nextCell = neighbors[Random.Range(0, neighbors.Count)];
                RemoveWallBetween(currentCell, nextCell);
                visitedCells.Add(nextCell);
                stack.Push(nextCell);
            }
        }
    }

    List<Vector2> GetUnvisitedNeighbors(Vector2 cell)
    {
        List<Vector2> neighbors = new List<Vector2>();

        float xOffset = triangleSize * 0.5f;
        float yOffset = triangleSize * Mathf.Sqrt(3) / 2;

        Vector2 topNeighbor = cell + new Vector2(0, yOffset);
        Vector2 leftNeighbor = cell + new Vector2(-xOffset, -yOffset);
        Vector2 rightNeighbor = cell + new Vector2(xOffset, -yOffset);

        if (!visitedCells.Contains(topNeighbor))
            neighbors.Add(topNeighbor);

        if (!visitedCells.Contains(leftNeighbor))
            neighbors.Add(leftNeighbor);

        if (!visitedCells.Contains(rightNeighbor))
            neighbors.Add(rightNeighbor);

        return neighbors;
    }

    void RemoveWallBetween(Vector2 cellA, Vector2 cellB)
    {
        Vector2 midPoint = (cellA + cellB) / 2;
        GameObject wall = Instantiate(wallPrefab, new Vector3(midPoint.x, midPoint.y, 0), Quaternion.identity);
        wall.transform.localScale = new Vector3(Vector2.Distance(cellA, cellB), 0.1f, 1f);
        wall.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(cellB.y - cellA.y, cellB.x - cellA.x) * Mathf.Rad2Deg);
    }
}
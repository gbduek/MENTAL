using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private MazeCell mazeCellPrefab;
    private int mazeWidth;
    private int mazeDepth;
    private GameObject[] Collectables;

    private float cellSize;
    private MazeCell[,] mazeGrid;

    private MazeCell mazeCellExit;

    //IEnumerator
    public ref MazeCell Generate(ref MazeCell mzCellPrefab, int mzWidth, int mzDepth, ref GameObject[] Collecs)
    {
        mazeCellPrefab = mzCellPrefab;
        mazeWidth = mzWidth;
        mazeDepth = mzDepth;
        Collectables = Collecs;

        mazeGrid = new MazeCell[mazeWidth, mazeDepth];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeDepth; y++)
            {
                mazeGrid[x, y] = Instantiate(mazeCellPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }

        //yield return 
        GenerateMaze(null, mazeGrid[0, 0]);
        PlaceCollectables();
        PlaceExit();
        return ref mazeCellExit;
    }

    //private IEnumerator
    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);
        //yield return new WaitForSeconds(0.1f);
        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                //yield return
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int y = (int)currentCell.transform.position.y;

        if (x + 1 < mazeWidth)
        {
            var cellToRight = mazeGrid[x + 1, y];

            if (cellToRight.isVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = mazeGrid[x - 1, y];

            if (cellToLeft.isVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (y + 1 < mazeDepth)
        {
            var cellToFront = mazeGrid[x, y + 1];

            if (cellToFront.isVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (y - 1 >= 0)
        {
            var cellToBack = mazeGrid[x, y - 1];

            if (cellToBack.isVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.y < currentCell.transform.position.y)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }

        if (previousCell.transform.position.y > currentCell.transform.position.y)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
    }

    private void PlaceCollectables()
    {
        Debug.Log("Placing collectables... (" + Collectables.Length + ")");
        for (int i = 0; i < Collectables.Length; i++)
        {
            // Divide the maze into sqrt(Collectables.Length) regions along each axis
            int regionsPerAxis = Mathf.CeilToInt(Mathf.Sqrt(Collectables.Length));
            int regionX = i % regionsPerAxis;
            int regionY = i / regionsPerAxis;

            int minWidth = Mathf.FloorToInt((float)mazeWidth * regionX / regionsPerAxis);
            int maxWidth = Mathf.FloorToInt((float)mazeWidth * (regionX + 1) / regionsPerAxis);
            int minDepth = Mathf.FloorToInt((float)mazeDepth * regionY / regionsPerAxis);
            int maxDepth = Mathf.FloorToInt((float)mazeDepth * (regionY + 1) / regionsPerAxis);

            Debug.Log("Collectable " + i + " min: " + minWidth + ", " + minDepth);
            Debug.Log("Collectable " + i + " max: " + maxWidth + ", " + maxDepth);

            int collectableX = Random.Range(minWidth, maxWidth);
            int collectableY = Random.Range(minDepth, maxDepth);
            Debug.Log("Collectable " + i + " position: " + collectableX + ", " + collectableY);
            Collectables[i].transform.position = new Vector3(collectableX, collectableY, 0);
        }

    }

    private void PlaceExit()
    {
        int exitDirection = Random.Range(0, 4);
        Debug.Log("Exit direction: " + exitDirection);
        if (exitDirection == 0)
        {
            int exitX = Random.Range(0, mazeWidth);
            mazeCellExit = mazeGrid[exitX, 0];
            mazeCellExit.setBackExit();
        }
        else if (exitDirection == 1)
        {
            int exitX = Random.Range(0, mazeWidth);
            mazeCellExit = mazeGrid[exitX, mazeDepth - 1]; 
            mazeCellExit.setFrontExit();
        }
        else if (exitDirection == 2)
        {
            int exitY = Random.Range(0, mazeDepth);
            mazeCellExit = mazeGrid[0, exitY]; 
            mazeCellExit.setLeftExit();
        }
        else
        {
            int exitY = Random.Range(0, mazeDepth);
            mazeCellExit = mazeGrid[mazeWidth - 1, exitY]; 
            mazeCellExit.setRightExit();
        }
    }
}

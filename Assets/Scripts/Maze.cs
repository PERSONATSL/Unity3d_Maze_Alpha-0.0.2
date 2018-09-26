using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maze : MonoBehaviour {

    private int xLong;
    private int yLong;

    public GameObject wallPrefab;
    private GameObject wallHoder;

    public Cell[] cells;

    private int currentCell;
    private int totalCell;
    private int visitedCells;
    private bool startBuilding;

    private int currentNeighbor;
    private List<int> lastCells;
    private int backingUp;
    private int theBreakWall;
    private float speed;

    public GameObject scan;
    public InputField ip1;
    public InputField ip2;
    public InputField sp1;

    public Text jindu;

    private void Start()
    {
        jindu.text = "0%";
    }
    public void CerateGrid()
    {
        xLong = int.Parse(ip1.text);
        yLong = int.Parse(ip2.text);
        speed = float.Parse(sp1.text);

        wallHoder = new GameObject();
        wallHoder.name = "WallHolder";
        for(int i = 0; i <= xLong; i++)
            for(int j = 0; j < yLong; j++)
            {
                Vector3 pos = new Vector3(i - xLong / 2f, 0f, (yLong - 1f) / 2f - j);
                GameObject t = Instantiate(wallPrefab, pos, Quaternion.identity);
                t.transform.SetParent(wallHoder.transform);
            }

        for (int i = 0; i < xLong; i++)
            for (int j = 0; j <= yLong; j++)
            {
                Vector3 pos = new Vector3(i - (xLong - 1f) / 2f, 0f, yLong  / 2f - j);
                GameObject t = Instantiate(wallPrefab, pos, Quaternion.Euler(0f, 90f, 0f));
                t.transform.SetParent(wallHoder.transform);
            }
        CreateCell();
    }

    void CreateCell()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        totalCell = xLong * yLong;
        int westEastNumber = 0;
        int northSouthNumber = 0;
        cells = new Cell[xLong * yLong];
        List<GameObject> allWalls = new List<GameObject>();

        for(int i = 0; i < wallHoder.transform.childCount; i++)
        {
            allWalls.Add(wallHoder.transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < cells.Length; i++)
        {
            cells[i] = new Cell();
            cells[i].ID = i+1;
            cells[i].West = allWalls[westEastNumber];
            cells[i].East = allWalls[westEastNumber + yLong];

            cells[i].North = allWalls[northSouthNumber + xLong * yLong + yLong];
            cells[i].South = allWalls[northSouthNumber + xLong * yLong + yLong + 1];

            if((i+1)%yLong == 0)
            {
                northSouthNumber += 2;
            }
            else
            {
                northSouthNumber++;
            }
            cells[i].CellPos = new Vector3(cells[i].East.transform.position.x - .5f, 0f, cells[i].East.transform.position.z);

            westEastNumber++;
           

        }
        CreateMaze();
    }

    void CreateMaze()
    {
        scan.SetActive(true);
        if(visitedCells < totalCell)
        {
            if (startBuilding)
            {
                GiveMeNeighbor();
                if(cells[currentNeighbor].Visited == false && cells[currentCell].Visited == true)
                {
                    BreakWall();
                    cells[currentNeighbor].Visited = true;
                    visitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbor;
                    scan.transform.position = cells[currentCell].CellPos;
                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }

                }

            }
            else
            {
                currentCell = Random.Range(-1, totalCell-1);
                cells[currentCell].Visited = true;
                visitedCells++;
                startBuilding = true;
                scan.transform.position = cells[currentCell].CellPos;
            }

        }

        Invoke("CreateMaze", speed);
    }

    void BreakWall()
    {
        switch (theBreakWall)
        {
            
            case 1: Destroy(cells[currentCell].North);break;
            case 2: Destroy(cells[currentCell].South);break;
            case 3: Destroy(cells[currentCell].West);break;
            case 4: Destroy(cells[currentCell].East);break;
        }

    }

    void GiveMeNeighbor()
    {
        float x = visitedCells *100f / totalCell;
        jindu.text = x .ToString("F0") + "%";
        int[] neighbors = new int[4];
        int[] connectionWall = new int[4];
        int lenth = 0;

        if(currentCell % yLong !=0)
        {
            if (cells[currentCell - 1].Visited == false)
            {
                neighbors[lenth] = currentCell - 1;
                connectionWall[lenth] = 1;
                lenth++;
            }
        }
        if(currentCell%yLong != yLong-1)
        {
            if (cells[currentCell + 1].Visited == false)
            {
                neighbors[lenth] = currentCell + 1;
                connectionWall[lenth] = 2;
                lenth++;
            }
        }
        if (currentCell >= yLong)
        {
            if (cells[currentCell - yLong].Visited == false)
            {
                neighbors[lenth] = currentCell - yLong;
                connectionWall[lenth] = 3;
                lenth++;
            }
        }
        if (currentCell < totalCell-yLong)
        {
            if (cells[currentCell + yLong].Visited == false)
            {
                neighbors[lenth] = currentCell + yLong;
                connectionWall[lenth] = 4;
                lenth++;
            }
        }

        if(lenth != 0)
        {
            int theChoosenOne = Random.Range(0, lenth);
            currentNeighbor = neighbors[theChoosenOne];

            theBreakWall = connectionWall[theChoosenOne];
        }
        else
        {
           
            ///出栈
            if (backingUp > 0)
            {
                lastCells.Remove(currentCell);
                currentCell = lastCells[backingUp];
                backingUp--;
                scan.transform.position = cells[currentCell].CellPos;
            }
        }

    }

}

[System.Serializable]
public class Cell
{

    private GameObject east;
    private GameObject west;
    private GameObject south;
    private GameObject north;
    private int id;
    private bool visited;

    private Vector3 cellPos;

    public GameObject East
    {
        get { return east; }
        set { east = value; }
    }
    public GameObject West
    {
        get { return west; }
        set { west = value; }
    }
    public GameObject South
    {
        get { return south; }
        set { south = value; }
    }
    public GameObject North
    {
        get { return north; }
        set { north = value; }
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    public bool Visited
    {
        get { return visited; }
        set { visited = value; }
    }

    public Vector3 CellPos
    {
        get { return cellPos; }
        set { cellPos = value; }
    }


}

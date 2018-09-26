# Unity3d_Maze

* Depth-First-Search

## Mian code

1.Create Grid

```
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
```

2.CreateCell

```
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
```

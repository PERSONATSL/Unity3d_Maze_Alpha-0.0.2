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

2.

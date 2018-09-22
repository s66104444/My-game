using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{

    private class wall
    {
        public int x1, y1, x2, y2, x, y;
        public wall(int x1, int y1, int x2, int y2, int x, int y)
        {
            this.x1 = x1; this.y1 = y1;
            this.x2 = x2; this.y2 = y2;
            this.x = x; this.y = y;
        }
    }
    private bool[,] map = new bool[31, 31];
    private List<wall> walls = new List<wall>();
    private Vector2Int[,] fa = new Vector2Int[31, 31];
    public GameObject zhu;
    public GameObject Empty;

    void Start()
    {
        makemaze();
    }

    Vector2Int find(int x, int y)
    {
        if (fa[x, y].x == x && fa[x, y].y == y) return fa[x, y];
        else return fa[x, y] = find(fa[x, y].x, fa[x, y].y);
    }

    void makemaze()
    {
        for (int x = 0; x < 31; x++)
            for (int y = 0; y < 31; y++)
            {
                if (y % 2 == 1)
                {
                    map[x, y] = false;
                    if (x % 2 == 0) walls.Add(new wall(x, y - 1, x, y + 1, x, y));
                }
                else
                {
                    if (x % 2 == 0) map[x, y] = true;
                    else
                    {
                        map[x, y] = false;
                        walls.Add(new wall(x - 1, y, x + 1, y, x, y));
                    }
                }
                fa[x, y] = new Vector2Int(x, y);
            }
        System.Random rnd = new System.Random();
        for (int i = 0; i < walls.Count; i++)
        {
            int a = rnd.Next(walls.Count - 1), b = rnd.Next(a + 1, walls.Count);
            wall c = walls[a]; walls[a] = walls[b]; walls[b] = c;
        }
        for (int i = 0; i < walls.Count; i++)
        {
            Vector2Int a = find(walls[i].x1, walls[i].y1);
            Vector2Int b = find(walls[i].x2, walls[i].y2);
            if (!a.Equals(b))
            {
                map[walls[i].x, walls[i].y] = true;
                fa[a.x, a.y] = b;
            }
            else continue;
            if (find(0, 0).Equals(find(30, 30))) break;
        }
        for (int x = 0; x < 31; x++)
            for (int y = 0; y < 31; y++)
                if (!map[x, y])
                {
                    GameObject go = Instantiate(zhu);
                    go.transform.position = new Vector3(x, 0.5f, y);
                }
        while(true)
        {
            int a = rnd.Next(30);
            int b = rnd.Next(30);
            if(find(0,0).Equals(find(0,0)))
            {
                GameObject go=Instantiate(Empty);
                go.transform.position = new Vector3(a, 0, b);
                break;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {

    }


}

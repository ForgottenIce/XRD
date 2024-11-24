using UnityEngine;
using System.Linq;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private Grid Grid;
    [SerializeField] private GameObject Spawner;
    [SerializeField] private MazeCellDic cellDic;
    [SerializeField] private GameObject mazePlane;
    [SerializeField] private GameObject endArea;
    //public int[][] maze;
    [SerializeField] private Vector3 testpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //var maze = WilsonsAlgorithm.WilsonsMaze(20, 20);
        //TypedSpawnMaze(maze);
    }

    public void TypedSpawnMaze(int[][] maze) {
        var typedMaze = WilsonsAlgorithm.MetaStructMaze(maze);
        for (int i = 0; i < maze.Length; i++) {
            for (int j = 0; j < maze[i].Length; j++) {
                if (typedMaze[i][j].type != MazeCellTypeEnum.XPath) {
                    var wallobj = Instantiate(cellDic.GetCell(typedMaze[i][j].type), Grid.gameObject.transform, false);
                    wallobj.transform.LookAt(typedMaze[i][j].direction);
                    wallobj.transform.localPosition = Grid.CellToLocal(new Vector3Int(i, 0, j));
                }
            }
        }
        var mazePlaneScaleFactor = 0.3f * maze.Length - 0.2f;
        
        var mazePlaneFloor = Instantiate(mazePlane, Grid.gameObject.transform, false);
        mazePlaneFloor.transform.localScale = new Vector3(mazePlaneScaleFactor, 1, mazePlaneScaleFactor);
        mazePlaneFloor.transform.localPosition = Grid.CellToLocal(new Vector3Int(maze.Length/2, 0, maze[0].Length/2));
        
        var mazePlaneRoof = Instantiate(mazePlane, Grid.gameObject.transform, false);
        mazePlaneRoof.transform.localScale = new Vector3(mazePlaneScaleFactor, -1, mazePlaneScaleFactor);
        mazePlaneRoof.transform.localPosition = Grid.CellToLocal(new Vector3Int(maze.Length/2, 1, maze[0].Length/2));
        
        var endAreaObj = Instantiate(endArea, Grid.gameObject.transform, false);
        endAreaObj.transform.localPosition = Grid.CellToLocal(new Vector3Int(maze.Length, 0, maze.Length));
    }

    void OldSpawnMaze() {
        var maze = WilsonsAlgorithm.WilsonsMaze(10, 10);
        for (int i = 0; i < maze.Length; i++) {
            for (int j = 0; j < maze[i].Length; j++) {
                if (maze[i][j] == 1) {
                    var wallobj = Instantiate(Spawner);
                    wallobj.transform.SetParent(Grid.gameObject.transform, false);
                    wallobj.transform.localPosition = Grid.CellToLocal(new Vector3Int(i, 0, j));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected() {
        //Gizmos.color = Color.yellow;
        //for (int i = 0; i < maze.Length; i++) {
        //    for (int j = 0; j < maze[i].Length; j++) {
        //        Gizmos.DrawCube(Grid.CellToWorld(new Vector3Int(i,0,j)),Vector3.one*3);
        //    }
        //}
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawCube(Grid.WorldToCell(testpoint),Vector3.one*3);
        //Gizmos.color = Color.magenta;
        //Gizmos.DrawSphere(testpoint,2f);
    }
}

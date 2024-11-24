using UnityEngine;
using System.Linq;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private Grid Grid;
    [SerializeField] private GameObject Spawner;
    [SerializeField] private MazeCellDic cellDic;
    [SerializeField] private GameObject mazePlane;
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
        const float mazePlaneScaleFactor = 3.3980582524271844660194174757282f; // Magic number, not 100% accurate. Need to calculate this properly somehow.
        
        var mazePlaneFloor = Instantiate(mazePlane, Grid.gameObject.transform, false);
        mazePlaneFloor.transform.localScale = new Vector3(maze.Length / mazePlaneScaleFactor, 1, maze[0].Length / mazePlaneScaleFactor);
        mazePlaneFloor.transform.localPosition = Grid.CellToLocal(new Vector3Int(maze.Length/2, 0, maze[0].Length/2));
        
        var mazePlaneRoof = Instantiate(mazePlane, Grid.gameObject.transform, false);
        mazePlaneRoof.transform.localScale = new Vector3(maze.Length / mazePlaneScaleFactor, -1, maze[0].Length / mazePlaneScaleFactor);
        mazePlaneRoof.transform.localPosition = Grid.CellToLocal(new Vector3Int(maze.Length/2, 1, maze[0].Length/2));
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

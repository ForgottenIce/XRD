using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WanderController : MonoBehaviour
{
    [SerializeField] private MazeSpawner m_Spawner;
    [SerializeField] private Grid m_Grid;
    private Stack<Vector2Int> path = new();

    private Vector3 nextdis;
    [SerializeField] private float gridTargetPrecision;

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (path.Count <= 0) {
            var x = Random.Range(0, m_Spawner.maze.Length - 1);
            var y = Random.Range(0, m_Spawner.maze[0].Length - 1);
            SetPath(x, y);
        }

        nextdis = m_Grid.CellToWorld(Vector3Int.RoundToInt(new Vector3(path.Peek().x, 0, path.Peek().y)));
        nextdis.y = 1;

        transform.position = transform.position + (speed * Time.deltaTime
            * ((nextdis - transform.position).normalized).normalized);
        // TODO make it turn toward it's target
        Quaternion toRotation = Quaternion.LookRotation(nextdis-transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation,toRotation,1*Time.deltaTime*turnSpeed);

        if (transform.forward != (nextdis - transform.position).normalized) {
            transform.position = transform.position + transform.forward * Time.deltaTime;
        }


        if ((nextdis - transform.position).sqrMagnitude <= gridTargetPrecision) {
            path.Pop();
        }
    }

    void OnEnable() {
        //path.Clear();
        //if (path.Count <= 0) {
        //    var x = Random.Range(0, m_Spawner.maze.Length - 1);
        //    var y = Random.Range(0, m_Spawner.maze[0].Length - 1);
        //    SetPath(x,y);
        //}
    }

    private void SetPath(int x, int y) {
        var pos = m_Grid.WorldToCell(Vector3Int.RoundToInt(gameObject.transform.position)+Vector3Int.one);
        var m = m_Spawner.maze;

        var disNode = AStar.ShortestPath(m, new int[] { pos.x, pos.z }, new int[] { x, y });
        Debug.Log(disNode.distance);
        while (disNode.parent != null) {
            path.Push(Vector2Int.RoundToInt(new Vector2(disNode.x, disNode.y)));
            disNode = disNode.parent;
        }

    }

    void OnDrawGizmosSelected() {
        var pos = m_Grid.WorldToCell(Vector3Int.RoundToInt(gameObject.transform.position)+Vector3Int.one);
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(m_Grid.CellToWorld(pos), Vector3.one * 3);
        if (nextdis!=null) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(nextdis, Vector3.one * 3);
        }
    }
    private void OnDrawGizmos() {
        if (path.Count <= 0) return;
        var vec3 = path.Select(x => m_Grid.CellToWorld(Vector3Int.RoundToInt(new Vector3(x.x,0,x.y))));
        foreach (Vector3 cord in vec3) {
            Gizmos.DrawCube(cord, Vector3.one * 3);
        }
        Gizmos.DrawLineStrip(vec3.ToArray(), false);
    }
}

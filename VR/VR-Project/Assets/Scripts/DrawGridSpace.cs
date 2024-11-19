using System;
using UnityEngine;

public class DrawGridSpace : MonoBehaviour
{
    [SerializeField] private MazeSpawner m_Spawner;
    [SerializeField] private Grid grid;
    [SerializeField] private int windowRadius;

    [SerializeField] Vector3Int[] spaces;
    [SerializeField] Vector3Int specificSpace;
    [SerializeField] Vector3 specificSpaceFloat;

    void Start() {
    }

    private void OnEnable() {
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        if (spaces != null && spaces.Length > 0) {
            foreach (var space in spaces) {
                Gizmos.DrawCube(space * 3, Vector3.one * 3);
            }
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(specificSpace * 3, Vector3.one * 3);
        var spaceFloat = Vector3Int.RoundToInt(gameObject.transform.position);
        if (specificSpaceFloat != null && m_Spawner.maze != null) {
            Gizmos.color = Color.cyan;
            var spaceModifierX = ((spaceFloat.x + 1) % 3) - 1;
            var spaceModifierZ = ((spaceFloat.z + 1) % 3) - 1;
            Gizmos.DrawCube(grid.CellToWorld(grid.WorldToCell(spaceFloat+Vector3.one) + new Vector3Int(spaceModifierX,0, spaceModifierZ)), Vector3.one * 3);
        }

        if (grid != null && m_Spawner != null && m_Spawner.maze != null) {
            var gridSpace = grid.WorldToCell(Vector3Int.RoundToInt(transform.position) + Vector3Int.one);
            for (int i = Math.Max(gridSpace.x - windowRadius + 1, 0); i < Math.Min(gridSpace.x + windowRadius,m_Spawner.maze.Length); i++) {
                for (int j = Math.Max(gridSpace.z - windowRadius + 1, 0); j < Math.Min(gridSpace.z + windowRadius, m_Spawner.maze[i].Length); j++) {
                    if (i == Math.Max(gridSpace.x - windowRadius + 1, 0)
                        || j == Math.Max(gridSpace.z - windowRadius + 1, 0)
                        || i == Math.Min(gridSpace.x + windowRadius, m_Spawner.maze.Length) - 1
                        || j == Math.Min(gridSpace.z + windowRadius, m_Spawner.maze[i].Length) - 1) {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawCube(new Vector3(i,0,j)*3, Vector3.one * 3);
                    }
                    if (i == gridSpace.x || j == gridSpace.z) {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(i, 0, j) * 3, Vector3.one * 3);
                    }
                }
            }
        }
    }
}

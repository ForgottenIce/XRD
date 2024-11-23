using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private int mazeSizeMax = 35;
    [SerializeField] private int mazeSizeMin = 15;
    [SerializeField] private int mazeSize = 25;
    [SerializeField] private int seed = 1;

    private bool mazeHasGenerated = false;

    [Header("Events")]
    [SerializeField] private UnityEvent<string,string> UpdateSettingsText;
    [SerializeField] private UnityEvent<int[][]> MazeGenerated;

    private void OnEnable() {
        UpdateSettingsText?.Invoke("mazeSize", "Maze Size:\n" + mazeSize);
        UpdateSettingsText?.Invoke("randomSeed", "Seed:\n" + seed);
    }

    public void HandleButtonPress(string buttonKey) {
        if (mazeHasGenerated)
        {
            Debug.Log("maze already exists");
            return;
        }
        switch (buttonKey) {
            case "sizeUp": 
                if (mazeSize >= mazeSizeMax) { break; }
                mazeSize++;
                mazeSize++;
                UpdateSettingsText?.Invoke("mazeSize", "Maze Size:\n" + mazeSize);
                Debug.Log(mazeSize);
                break;
            case "sizeDown":
                if (mazeSize <= mazeSizeMin) { break; }
                mazeSize--;
                mazeSize--;
                UpdateSettingsText?.Invoke("mazeSize", "Maze Size:\n" + mazeSize);
                Debug.Log(mazeSize);
                break;
            case "randomSeed":
                seed = Random.Range(1, 10000);
                UpdateSettingsText?.Invoke("randomSeed", "Seed:\n" + seed);
                Debug.Log("new seed generated: " +seed);
                break;
        }
    }

    public void HandleConfirmSwitch(float releaseValue) {
        if (!mazeHasGenerated && releaseValue > 0) {
            Debug.Log("Created Maze of size: " + mazeSize);
            var maze = WilsonsAlgorithm.WilsonsMaze(mazeSize,mazeSize, seed);
            var node = AStar.ShortestPath(maze, new int[] { 0, 1 }, new int[] { maze.Length - 1, maze[0].Length - 2 });
            if (node.distance > 1024) {
                List<AStar.Node> list = new();
                while (node.parent != null) {
                    list.Add(node);
                    node = node.parent;
                }
                list.Reverse();
                int wallsBlocking = 0;
                foreach (var lNode in list) {
                    if (lNode.distance - (1024 * wallsBlocking) > 1024) {
                        wallsBlocking++;
                        maze[lNode.x][lNode.y] = 0;
                    }
                }
            }
            MazeGenerated.Invoke(maze);
            mazeHasGenerated = true;
        } else {
            Debug.Log("maze already exists");
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class WilsonsAlgorithm {
    private static Random _random = null!;

    public static TypeDirectionPair[][] MetaStructMaze(int[][] maze) {
        TypeDirectionPair[][] result = new TypeDirectionPair[maze.Length][];
        for (var i = 0; i < maze.Length; i++) {
            result[i] = new TypeDirectionPair[maze[i].Length];
            for (var j = 0; j < maze[i].Length; j++) {
                //if (i == 0 && j == 0 
                //    || i == 0 && j == maze[i].Length - 1
                //    || i == maze[i].Length - 1 && j == 0
                //    || i == maze[i].Length - 1 && j == maze[j].Length - 1) {
                //    result[i][j] = new() { type = MazeCellTypeEnum.SingleWall, direction = Vector3.back};
                //}
                //else if (i == 0 || i == maze[i].Length - 1) {
                //    result[i][j] = new() { type = MazeCellTypeEnum.StraitWall, direction = Vector3.forward };
                //}
                //else if (j == 0 || j == maze[j].Length - 1) {
                //    result[i][j] = new() { type = MazeCellTypeEnum.StraitWall, direction = Vector3.right };
                //}
                //else {
                    int[][] window = new int[3][];
                    window[0] = new int[3];
                    window[1] = new int[3];
                    window[2] = new int[3];
                    for (int wi = -1; wi <= 1; wi++) {
                        for (int wj = -1; wj <= 1; wj++) {
                            try {
                                window[wi + 1][wj + 1] = maze[i + wi][j + wj];
                            } catch (Exception) {
                                window[wi + 1][wj + 1] = 0;
                            }
                        }
                    }
                    result[i][j] = MetaStructType(window);
                //}
            }
        }
        return result;
    }
    private static TypeDirectionPair MetaStructType(int[][] window) {
        if (window[1][1] == 0) {
            return MetaStructTypePath(window);
        }
        else {
            return MetaStructTypeWall(window);
        }
    }
    private static TypeDirectionPair MetaStructTypeWall(int[][] window) {
        /* type vals
         * x 1 x
         * 2 x 8
         * x 4 x
         */
        int typeVal = 0;
        if (window[0][1] == 1) {
            typeVal += 1;
        }
        if (window[1][0] == 1) {
            typeVal += 2;
        }
        if (window[2][1] == 1) {
            typeVal += 4;
        }
        if (window[1][2] == 1) {
            typeVal += 8;
        }

        return typeVal switch
        {
            0 => new() { type = MazeCellTypeEnum.SingleWall, direction = Vector3.zero },
            1 => new() { type = MazeCellTypeEnum.EndWall, direction = Vector3.left },
            2 => new() { type = MazeCellTypeEnum.EndWall, direction = Vector3.back },
            4 => new() { type = MazeCellTypeEnum.EndWall, direction = Vector3.right },
            8 => new() { type = MazeCellTypeEnum.EndWall, direction = Vector3.forward },
            3 => new() { type = MazeCellTypeEnum.CornerWall, direction = Vector3.left },
            5 => new() { type = MazeCellTypeEnum.StraitWall, direction = Vector3.right },
            6 => new() { type = MazeCellTypeEnum.CornerWall, direction = Vector3.back },
            10 => new() { type = MazeCellTypeEnum.StraitWall, direction = Vector3.forward },
            12 => new() { type = MazeCellTypeEnum.CornerWall, direction = Vector3.right },
            9 => new() { type = MazeCellTypeEnum.CornerWall, direction = Vector3.forward },
            7 => new() { type = MazeCellTypeEnum.TWall, direction = Vector3.back },
            11 => new() { type = MazeCellTypeEnum.TWall, direction = Vector3.left },
            13 => new() { type = MazeCellTypeEnum.TWall, direction = Vector3.forward },
            14 => new() { type = MazeCellTypeEnum.TWall, direction = Vector3.right },
            15 => new() { type = MazeCellTypeEnum.XWall, direction = Vector3.zero },
            _ => throw new Exception("should never happen")
        };
    }

    private static TypeDirectionPair MetaStructTypePath(int[][] window) {
        return new() { type = MazeCellTypeEnum.XPath, direction = Vector3.zero };
        /* type vals
         * x 1 x
         * 2 x 8
         * x 4 x
         */

        int typeVal = 0;
        if (window[0][1] == 0) {
            typeVal += 1;
        }
        if (window[1][0] == 0) {
            typeVal += 2;
        }
        if (window[2][1] == 0) {
            typeVal += 4;
        }
        if (window[1][2] == 0) {
            typeVal += 8;
        }

        return typeVal switch
        {
            0 => new() { type = MazeCellTypeEnum.XPath, direction = Vector3.zero },
            1 => new() { type = MazeCellTypeEnum.EndPath, direction = Vector3.right },
            2 => new() { type = MazeCellTypeEnum.EndPath, direction = Vector3.back },
            4 => new() { type = MazeCellTypeEnum.EndPath, direction = Vector3.left },
            8 => new() { type = MazeCellTypeEnum.EndPath, direction = Vector3.forward },
            3 => new() { type = MazeCellTypeEnum.CornerPath, direction = Vector3.right },
            5 => new() { type = MazeCellTypeEnum.StraitPath, direction = Vector3.right },
            6 => new() { type = MazeCellTypeEnum.CornerPath, direction = Vector3.back },
            10 => new() { type = MazeCellTypeEnum.StraitPath, direction = Vector3.forward },
            12 => new() { type = MazeCellTypeEnum.CornerPath, direction = Vector3.left },
            9 => new() { type = MazeCellTypeEnum.CornerPath, direction = Vector3.forward },
            7 => new() { type = MazeCellTypeEnum.TPath, direction = Vector3.back },
            11 => new() { type = MazeCellTypeEnum.TPath, direction = Vector3.right },
            13 => new() { type = MazeCellTypeEnum.TPath, direction = Vector3.forward },
            14 => new() { type = MazeCellTypeEnum.TPath, direction = Vector3.right },
            15 => new() { type = MazeCellTypeEnum.XPath, direction = Vector3.zero },
            _ => throw new Exception("should never happen")
        };


        //return typeVal switch
        //{
        //    0 => "ow",
        //    1 => "nn",
        //    2 => "ww",
        //    4 => "ss",
        //    8 => "ee",
        //    3 => "nw",
        //    5 => "ns",
        //    6 => "ws",
        //    10 => "we",
        //    12 => "se",
        //    9 => "en",
        //    7 => "tw",
        //    11 => "tn",
        //    13 => "te",
        //    14 => "ts",
        //    15 => "xw",
        //    _ => throw new Exception("should never happen")
        //};
    }


    public static string[][] MetaMaze(int[][] maze) {
        string[][] result = new string[maze.Length][];
        for (var i = 0; i < maze.Length; i++) {
            result[i] = new string[maze[i].Length];
            for (var j = 0; j < maze[i].Length; j++) {
                if (i==0||j==0||i==maze[i].Length-1||j==maze[i].Length-1) {
                    result[i][j] = "ed";
                }
                else {
                    int[][] window = new int[3][];
                    window[0] = new int[3];
                    window[1] = new int[3];
                    window[2] = new int[3];
                    for (int wi = -1; wi <= 1; wi++)
                    {
                        for (int wj = -1; wj <= 1; wj++)
                        {
                            window[wi + 1][wj + 1] = maze[i + wi][j + wj];
                        }
                    }
                    result[i][j] = MetaType(window);
                }
            }
        }
        return result;
    }

    private static string MetaType(int[][] window) {
        if (window[1][1] == 0) {
            return "mp";
        }
        /* type vals
         * x 1 x
         * 2 x 8
         * x 4 x
         */
        int typeVal = 0;
        if (window[0][1] == 1) {
            typeVal += 1;
        }
        if (window[1][0] == 1) {
            typeVal += 2;
        }
        if (window[2][1] == 1) {
            typeVal += 4;
        }
        if (window[1][2] == 1) {
            typeVal += 8;
        }

        return typeVal switch {
            0 => "ow",
            1 => "nn",
            2 => "ww",
            4 => "ss",
            8 => "ee",
            3 => "nw",
            5 => "ns",
            6 => "ws",
            10 => "we",
            12 => "se",
            9 => "en",
            7 => "tw",
            11 => "tn",
            13 => "te",
            14 => "ts",
            15 => "xw",
            _ => throw new Exception("should never happen")
        };
    }
    
    public static int[][] WilsonsMaze(int width, int height, int seed = 1) {
        _random = new Random(seed);
        
        // Make dimensions odd
        width -= width % 2; width++;
        height -= height % 2; height++;
        
        int[][] maze = new int[height][];
        for (var i = 0; i < height; i++) {
            maze[i] = new int[width];
            for (var j = 0; j < width; j++) {
                maze[i][j] = 1;
            }
        }
        
        var s = RandCoord(width, height);
        maze[s[0]][s[1]] = 0;
        
        while (!Complete(maze)) {
            int[] c;
            do {
                c = RandCoord(width, height);
            } while (maze[c[0]][c[1]] != 1); 
            
            maze[c[0]][c[1]] = 2;
            
            int[][] path = new int[1][];
            path[0] = c;
            while (maze[path[^1][0]][path[^1][1]] != 0) {
                var last = path[^1];
                var n = NeighborsAb(maze, last[0], last[1]);
                var nb = n[(int)Math.Floor(_random.NextDouble() * n.Length)];
                
                path = path.Append(nb).ToArray();
                
                maze[(nb[0] + last[0]) / 2][(nb[1] + last[1]) / 2] = 2;
                if (maze[nb[0]][nb[1]] == 0) {
                    
                    for (var i = 0; i < height; i++) {
                        for (var j = 0; j < width; j++) {
                            if (maze[i][j] == 2)
                                maze[i][j] = 0;
                        }
                    }
                }
                
                else {
                    
                    maze[nb[0]][nb[1]] = 2;
                    var loc = IndexOfCoord(path, nb);
                    if (loc != path.Length - 1) {
                        
                        var removed = path.Skip(loc + 1).Take(path.Length - loc - 1).ToArray();
                        for (int i = loc + 1; i < path.Length - loc; i++) {
                            var temp = path.ToList();
                            temp.RemoveRange(loc+1, path.Length - loc-1);
                            path = temp.ToArray();
                        }
                        maze[(nb[0] + last[0]) / 2][(nb[1] + last[1]) / 2] = 1;
                        last = path[^1];
                        
                        for (var k = removed.Length - 1; k >= 0; k--) {
                            var on = removed[k];
                            var next = k != 0 ? removed[k - 1] : last;
                            
                            if (k != removed.Length - 1)
                                maze[on[0]][on[1]] = 1;
                            
                            maze[(on[0] + next[0]) / 2][(on[1] + next[1]) / 2] = 1;
                        }
                        
                    }
                    
                }
                
            }
            
        }
        
        maze[0][1] = 0;
        maze[height - 1][width - 2] = 0;
        
        return maze;
        
    }

    private static int[][] NeighborsAb(int[][] maze, int ic, int jc) {
        List<int[]> final = new();
        for (var i = 0; i < 4; i++) {
            int[] n = { ic, jc };
            
            // Iterates through four neighbors
            // [i][j - 2] 
            // [i][j + 2]
            // [i - 2][j]
            // [i + 2][j]
            n[i % 2] += ((Math.Floor(i / 2D) * 2)!=0?(int)(Math.Floor(i / 2D) * 2) : -2);
            if (n[0] < maze.Length && 
                n[1] < maze[0].Length && 
                n[0] > 0 && 
                n[1] > 0) {
                
                final.Add(n);
            }
        }
        return final.ToArray();
    }

    private static int IndexOfCoord(int[][] s, int[] c) {
        for (var i = 0; i < s.Length; i++) {
            if (s[i][0] == c[0] && s[i][1] == c[1])
                return i;
        }
        return -1;
    }

    private static bool Complete(int[][] maze) {
        for (var i = 1; i < maze.Length; i += 2) {
            for (var j = 1; j < maze[0].Length; j += 2) {
                if (maze[i][j] != 0)
                    return false;
            }
        }
        return true;
    }

    private static int[] RandCoord(int width, int height) {
        int[] c = new int[2];
        c[0] = (int)(Math.Floor(_random.NextDouble() * Math.Floor(height / 2D)) * 2) + 1;
        c[1] = (int)(Math.Floor(_random.NextDouble() * Math.Floor(width / 2D)) * 2) + 1;
        return c;
    }
}

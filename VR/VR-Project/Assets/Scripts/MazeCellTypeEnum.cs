using UnityEngine;

public enum MazeCellTypeEnum
{
    XWall,
    TWall,
    StraitWall,
    CornerWall,
    EndWall,
    SingleWall,
    XPath,
    TPath,
    StraitPath,
    CornerPath,
    EndPath,
}

public struct TypeDirectionPair { public MazeCellTypeEnum type; public Vector3 direction; }
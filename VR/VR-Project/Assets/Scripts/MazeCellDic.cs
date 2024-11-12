using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Maze/CellDictionary")]
public class MazeCellDic : ScriptableObject
{
    [SerializeField] List<KeyValPair> m_MazeCells;

    public GameObject GetCell(MazeCellTypeEnum type) {
        var cell = m_MazeCells.FirstOrDefault(cell => cell.Key == type);
        if (cell.Value == null) {
            throw new Exception("PrefabNotSet");
        }
        return cell.Value;
    }
}

[Serializable]
public struct KeyValPair {  public MazeCellTypeEnum Key; public GameObject Value; }

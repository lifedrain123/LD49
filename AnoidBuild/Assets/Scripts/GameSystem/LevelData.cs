using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    // 砖块数组
    public BlockData[] blocks;
    // 初始位置
    public Vector3 initialPosition;
    // 当前一格的长度
    public float length;
    // 当前一格的高度
    public float height;
    // 关卡名称
    public string levelName;
    // 目标高度
    public float targetHeight;
    // 最大列长
    public int maxColumn;
}

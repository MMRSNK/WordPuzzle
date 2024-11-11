using UnityEngine;

[CreateAssetMenu(fileName ="NewLevelData", menuName ="ScriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelNum;
    public Sprite sprite;

    public LevelData(int levelNum, Sprite sprite)
    {
        this.levelNum = levelNum;
        this.sprite = sprite;
    }
}

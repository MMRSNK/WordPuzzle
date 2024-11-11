using System.Collections.Generic;
using System.Numerics;

[System.Serializable]
public class CrosswordData
{
    public List<string> levelLetters;
    public List<List<string>> crosswordGrid;
    public Dictionary<string, List<Vector2>> levelWordsDict; 
}

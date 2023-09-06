using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoaderElement", menuName = "ScriptableObjects/Loaders/LoaderElement", order = 1)]
[System.Serializable]
public class LoaderElement : ScriptableObject
{
    public Sprite loadingSprite;
    public Color textColor;
    public Color barColor;

    [TextArea(3, 10)]
    public List<string> loadingTexts = new();

    public int GetTextCount()
    {
        return loadingTexts.Count;
    }
}
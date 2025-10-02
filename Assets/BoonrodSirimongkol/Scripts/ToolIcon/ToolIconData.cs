using System;
using UnityEngine;

public enum ToolIconType
{
    None,
    Trashcan,
    Mopping,
    Hand,

}
[Serializable]
public class ToolIconData 
{
    [SerializeField] private ToolIconType toolIconType;
    [SerializeField] private Sprite toolIconSprite;
    public ToolIconType ToolIconType => toolIconType;
    public Sprite ToolIconSprite => toolIconSprite;
    
}

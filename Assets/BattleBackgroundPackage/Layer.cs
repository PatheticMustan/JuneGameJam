using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Layer
{
    //Varible Declaration
    //Oscillation Modifiers
    //Str = Strength
    //Frc = Frequency
    //Spd = Speed
    [SerializeField]
    private float _horOscXStr;
    [SerializeField]
    private float _horOscXFrc;
    [SerializeField]
    private float _horOscXSpd = 1;
    [Space(5)]

    [SerializeField]
    private float _vertOscXStr;
    [SerializeField]
    private float _vertOscXFrc;
    [SerializeField]
    private float _vertOscXSpd = 1;
    [Space(5)]

    [SerializeField]
    private float _horOscYStr;
    [SerializeField]
    private float _horOscYFrc;
    [SerializeField]
    private float _horOscYSpd = 1;
    [Space(5)]

    [SerializeField]
    private float _vertOscYStr;
    [SerializeField]
    private float _vertOscYFrc;
    [SerializeField]
    private float _vertOscYSpd = 1;

    //Scroll Offset
    [SerializeField]
    private Vector2 _scrollOffset;

    //Getters and Setters
    //Horizontal X Oscillation Strength, Frecuency, and Speed
    public float HorOscXStr
    {
        get => _horOscXStr;
        set => _horOscXStr = value;
    }
    public float HorOscXFrc
    {
        get => _horOscXFrc;
        set => _horOscXFrc = value;
    }
    public float HorOscXSpd
    {
        get => _horOscXSpd;
        set => _horOscXSpd = value;
    }
    //Vertical X Oscillation Strength, Frecuency, and Speed
    public float VertOscXStr
    {
        get => _vertOscXStr;
        set => _vertOscXStr = value;
    }
    public float VertOscXFrc
    {
        get => _vertOscXFrc;
        set => _vertOscXFrc = value;
    }
    public float VertOscXSpd
    {
        get => _vertOscXSpd;
        set => _vertOscXSpd = value;
    }
    //Horizontal Y Oscillation Strength, Frecuency, and Speed
    public float HorOscYStr
    {
        get => _horOscYStr;
        set => _horOscYStr = value;
    }
    public float HorOscYFrc
    {
        get => _horOscYFrc;
        set => _horOscYFrc = value;
    }
    public float HorOscYSpd
    {
        get => _horOscYSpd;
        set => _horOscYSpd = value;
    }
    //Vertical Y Oscillation Strength, Frecuency, and Speed
    public float VertOscYStr
    {
        get => _vertOscYStr;
        set => _vertOscYStr = value;
    }
    public float VertOscYFrc
    {
        get => _vertOscYFrc;
        set => _vertOscYFrc = value;
    }
    public float VertOscYSpd
    {
        get => _vertOscYSpd;
        set => _vertOscYSpd = value;
    }
    //Scroll Offset
    public Vector2 ScrollOffset
    {
        get => _scrollOffset;
        set => _scrollOffset = value;
    }
}

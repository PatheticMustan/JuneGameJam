using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Background
{
    //Varible Declaration
    //Name
    [SerializeField]
    private string _name;

    //Layer One
    [Header("Layer One")]

    //Layer Texture
    [SerializeField]
    private Sprite _layerOneTex;
    [Space(10)]

    //Check if want Interleaved Oscillation
    [SerializeField]
    private bool _allowInterOscLO;
    [Space(10)]

    //Layer Info
    [SerializeField]
    private Layer _layerOne;
    [SerializeField]
    private Layer _layerOneInter;

    //Pallete Cycling Info
    [SerializeField]
    private Color32[] _colorArrayOne;
    [SerializeField]
    private float _timerTickOne;

    //Layer Two
    [Header("Layer Two")]

    //Layer Texture
    [SerializeField]
    private Texture _layerTwoTex;
    [Space(10)]

    //Check if want Interleaved Oscillation, or even to consider this layer at all
    [SerializeField]
    private bool _allowLayering;
    [SerializeField]
    private bool _allowInterOscLT;
    [Space(10)]

    //Layer Info
    [SerializeField]
    private Layer _layerTwo;
    [SerializeField]
    private Layer _layerTwoInter;

    //Pallete Cycling Info
    [SerializeField]
    private Color32[] _colorArrayTwo;
    [SerializeField]
    private float _timerTickTwo;

    //Getters and Setters
    //Base Info
    public string Name
    {
        get => _name;
        set => _name = value;
    }
    //Layer One Info
    public Sprite LayerOneTex
    {
        get => _layerOneTex;
        private set => _layerOneTex = value;
    }
    public bool AllowInterOscLO
    {
        get => _allowInterOscLO;
        private set => _allowInterOscLO = value;
    }
    //Layers
    public Layer LayerOne
    {
        get => _layerOne;
        private set => _layerOne = value;
    }
    public Layer LayerOneInter
    {
        get => _layerOneInter;
        private set => _layerOneInter = value;
    }
    public Color32[] ColorArrayOne
    {
        get => _colorArrayOne;
        private set => _colorArrayOne = value;
    }
    public float TimerTickOne
    {
        get => _timerTickOne;
        private set => _timerTickOne = value;
    }
    //Layer Two Info
    public Texture LayerTwoTex
    {
        get => _layerTwoTex;
        private set => _layerTwoTex = value;
    }
    public bool AllowLayering
    {
        get => _allowLayering;
        private set => _allowLayering = value;
    }
    public bool AllowInterOscLT
    {
        get => _allowInterOscLT;
        private set => _allowInterOscLT = value;
    }
    //Layers
    public Layer LayerTwo
    {
        get => _layerTwo;
        private set => _layerTwo = value;
    }
    public Layer LayerTwoInter
    {
        get => _layerTwoInter;
        private set => _layerTwoInter = value;
    }
    public Color32[] ColorArrayTwo
    {
        get => _colorArrayTwo;
        private set => _colorArrayTwo = value;
    }
    public float TimerTickTwo
    {
        get => _timerTickTwo;
        private set => _timerTickTwo = value;
    }
}

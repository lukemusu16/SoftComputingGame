using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static int _score = 0;
    private static int _height = 30;
    private static int _width = 30;
    private static float _offset = 15f;
    private static int _health = 100;

    private static int _hs1;
    private static int _hs2;
    private static int _hs3;
    private static int _hs4;
    private static int _hs5;

    public static int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public static int Height
    {
        get { return _height; }
    }

    public static int Width
    {
        get { return _width; }
    }

    public static float Offset
    {
        get { return _offset; }
    }

    public static int Health
    {
        get { return _health; }
        set { _health = value; }
    }


    public static int Highscore1
    {
        get { return _hs1; }
        set { _hs1 = value; }
    }

    public static int Highscore2
    {
        get { return _hs2; }
        set { _hs3 = value; }
    }

    public static int Highscore3
    {
        get { return _hs3; }
        set { _hs3 = value; }
    }
    public static int Highscore4
    {
        get { return _hs4; }
        set { _hs4 = value; }
    }
    public static int Highscore5
    {
        get { return _hs5; }
        set { _hs5 = value; }
    }

}

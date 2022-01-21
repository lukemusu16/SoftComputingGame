using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static int _score = 0;
    private static int _height = 30;
    private static int _width = 30;

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
}

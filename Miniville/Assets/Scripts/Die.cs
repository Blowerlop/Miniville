using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public abstract class Die : MonoBehaviour
{
    private int _nbrFaces = 6;
    private static int _face;
    private static Random _random = new Random();

    public static int Roll(int nbrDeDes)
    {
        _face = 0;
        int rollResult;

        for (int i = 0; i < nbrDeDes; i++)
        {
            rollResult = _random.Next(1, 7);
            Debug.Log($"Roll result : {rollResult}");
            _face += rollResult;
        }

        return _face;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public abstract  class Die
{
    private int _nbrFaces = 6;
    public static int face;
    private static Random _random = new Random();

    public static void Roll(int nbrDeDes)
    {
        face = 0;
        int rollResult;

        for (int i = 0; i < nbrDeDes; i++)
        {
            rollResult = _random.Next(9, 10);
            Debug.Log($"Roll result : {rollResult}");
            face += rollResult;
        }


        Game.Play();
        //return _face;
    }
}

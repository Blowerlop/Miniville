using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public  class Die : MonoBehaviour
{
    private int _nbrFaces = 6;
    public static int face;
    [SerializeField] private static GameObject _canvas;

    private static int rollResult;

    private void Start()
    {
        _canvas = GameObject.Find("Game Canvas");
    }

    public static void Roll(int nbrDeDes)
    {
        /*
        face = 0;
        int rollResult = -1;
        
        /*
        for (int i = 0; i < nbrDeDes; i++)
        {
            
            //rollResult = Random.Range(1, 6);
            Debug.Log($"Roll result : {rollResult}");
            face += rollResult;
        }
        */
        
            
        Debug.Log($"Roll result : {rollResult}");
        Debug.Log(dicesVal[0]);
        _canvas.SetActive(true);
        Game.Play();
        //return _face;
    }
    
    
    
    
    public static List<Rigidbody> dices = new();
    public static List<int> dicesVal = new();
    public Rigidbody redDice;
    // public Rigidbody blueDice;
    // public Rigidbody blackDice;
    // public Rigidbody whiteDice;
    internal Rigidbody dice;
    internal bool res = false;

    void Update() {
        if (dices.Count>0&&!res) {
            DiceRes();
            res = true;
        }
    }


    public void Throw(int nbDice)
    {
        _canvas.SetActive(false);
        dices.Clear();
        dicesVal.Clear();
        res=false;
        int eRot = 0;
        Vector3 pos = new();
        bool isIn = true;

        for (int i = 0; i<nbDice; i++) {
            RanDizer(ref eRot, ref pos);
            dice=redDice;
            // switch (Random.Range(1, 5)) {
            //     case 1:
            //         dice=redDice;
            //         break;
            //     case 2:
            //         dice=blackDice;
            //         break;
            //     case 3:
            //         dice=whiteDice;
            //         break;
            //     case 4:
            //         dice=blueDice;
            //         break;
            // }
            while (isIn&&dices.Count>0) {
                foreach (var d in dices) {
                    if (pos.x<d.transform.position.x-1||pos.x>d.transform.position.x+1||
                       pos.z<d.transform.position.z-1||pos.z>d.transform.position.z+1) {
                        isIn=false;
                    } else {
                        isIn=true;
                        break;
                    }
                }
                RanDizer(ref eRot, ref pos);
            }
            dices.Add(Instantiate( dice, pos,
            Quaternion.Euler(Random.Range(10, 30), eRot+Random.Range(-10, 11), Random.Range(0, 360)),
            this.transform ));
            dices[^1].AddForce(dices[^1].transform.forward*Random.Range(400, 700));
        }
    }

    public void DiceRes() {
        foreach (Rigidbody Dice in dices) {
            StartCoroutine(ResD(Dice));
        }
    }

    private IEnumerator ResD(Rigidbody diecy) {
        yield return new WaitUntil(() => diecy.velocity!=Vector3.zero);
        yield return new WaitWhile(() => diecy.velocity!=Vector3.zero);

        Vector3 rotD = diecy.transform.rotation.eulerAngles;
        if ((rotD.x>355&&rotD.x<365||rotD.x>-5&&rotD.x<5)&&rotD.z<275&&rotD.z>265||
            rotD.x<185&&rotD.x>175&&rotD.z<95&&rotD.z>85) {

            dicesVal.Add(1);

        } else if ((rotD.x>355&&rotD.x<365||rotD.x>-5&&rotD.x<5)&&(rotD.z>355&&rotD.z<365||rotD.z>-5&&rotD.z<5)||
            rotD.x<185&&rotD.x>175&&rotD.z<185&&rotD.z>175) {

            dicesVal.Add(2);

        } else if (rotD.x<275&&rotD.x>265) {

            dicesVal.Add(3);

        } else if (rotD.x<95&&rotD.x>85) {

            dicesVal.Add(4);

        } else if ((rotD.x>355&&rotD.x<365||rotD.x>-5&&rotD.x<5)&&rotD.z<185&&rotD.z>175||
            rotD.x<185&&rotD.x>175&&(rotD.z>355&&rotD.z<365||rotD.z>-5&&rotD.z<5)) {

            dicesVal.Add(5);

        } else if ((rotD.x>355&&rotD.x<365||rotD.x>-5&&rotD.x<5)&&rotD.z<95&&rotD.z>85||
            rotD.x<185&&rotD.x>175&&rotD.z<275&&rotD.z>265) {

            dicesVal.Add(6);
            
        } else {
            diecy.AddForce(new Vector3(1, 1, 1)*300);
            StartCoroutine(ResD(diecy));
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
        rollResult = dicesVal[0];
        Roll(1);
        Destroy(diecy.gameObject);
    }


    private void RanDizer(ref int eRot, ref Vector3 pos) {
        int ePosX1 = 0;
        int ePosX2 = 0;
        int ePosZ1 = 0;
        int ePosZ2 = 0;

        switch (Random.Range(1, 5)) {
            case 1:
                ePosX1=-4;
                ePosX2=4;
                ePosZ1=5;
                ePosZ2=5;
                eRot=180;
                break;
            case 2:
                ePosX1=-5;
                ePosX2=-5;
                ePosZ1=-4;
                ePosZ2=4;
                eRot=90;
                break;
            case 3:
                ePosX1=-4;
                ePosX2=4;
                ePosZ1=-5;
                ePosZ2=-5;
                eRot=0;
                break;
            case 4:
                ePosX1=5;
                ePosX2=5;
                ePosZ1=-4;
                ePosZ2=4;
                eRot=-90;
                break;
        }
        pos=this.transform.position+new Vector3(Random.Range(ePosX1, ePosX2), 0, Random.Range(ePosZ1, ePosZ2));
    }

    public void ClearDices() {
        foreach (Rigidbody D in dices) {
            Destroy(D.gameObject);
        }
        dices.Clear();
        dicesVal.Clear();
    }
}

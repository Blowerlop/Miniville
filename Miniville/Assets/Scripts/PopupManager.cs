using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PopupManager : MonoBehaviour
{
    public LePopHimself popopup;
    public List<LePopHimself> SomePopup = new List<LePopHimself>();
    public string dataa;

    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;

    public static PopupManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SomePopup = new List<LePopHimself>() { };
        InvocationPop(dataa);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvocationPop(dataa);
        }
    }

    public void InvocationPop(string data)
    {
            if (SomePopup.Count > 0)
            {
                print("ah ouai ?" + SomePopup.Count);
                foreach (LePopHimself toi in SomePopup)
                {
                    toi.MoveVertical(-75); //Descendre un coup
                }
            }

            LePopHimself lui = Instantiate<LePopHimself>(popopup, this.transform.position, Quaternion.identity, this.transform);
            lui.changetexte(data);
            SomePopup.Add(lui);
    }
}
 
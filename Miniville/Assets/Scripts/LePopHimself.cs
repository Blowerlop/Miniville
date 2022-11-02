using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LePopHimself : MonoBehaviour
{

    public TMPro.TextMeshProUGUI text;

    public PopupManager Chef;


    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;

    private bool moveVerticaly;
    private float moveXValue = -205f;
    private float moveYValue;
    private float currentTime;
    private Vector3 lastPosition;
    public float tempsdevie;
    private bool mort;


    // Start is called before the first frame update
    void Start()
    {
        mort = false;
        print("tu passe ici ?");
        tempsdevie = 3;
        lastPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        tempsdevie -= Time.deltaTime;
        if (tempsdevie <= 0)
        {
            this.MoveHorizontal(300);
            this.MoveH();

            if (tempsdevie <= -2) {
                PopupManager.instance.SomePopup.Remove(this);
                Destroy(gameObject);
            }
        }

        if(!moveVerticaly) MoveH();
        if (moveVerticaly) MoveV();
    }

    public void changetexte(string data)
    {
        text.text = data;
    }

    void MoveH()
    {
        float moveX = Mathf.Lerp(transform.localPosition.x, lastPosition.x + moveXValue, currentTime);
        transform.localPosition = new Vector3(moveX, transform.localPosition.y, transform.localPosition.z);
        currentTime += Time.deltaTime * horizontalSpeed;
    }

    void MoveV()
    {
        float moveY = Mathf.Lerp(transform.localPosition.y, lastPosition.y + moveYValue, currentTime);
        transform.localPosition = new Vector3(transform.localPosition.x, moveY, transform.localPosition.z);
        currentTime += Time.deltaTime * verticalSpeed;
    }

    public void MoveVertical(float valeurY)
    {
        lastPosition = transform.localPosition;
        currentTime = 0;
        moveYValue = valeurY;
        moveVerticaly = true;
    }

    public void MoveHorizontal(float valeurX)
    {
        lastPosition = transform.localPosition;
        currentTime = 0;
        moveXValue = valeurX;
        moveVerticaly = false;
    }
}

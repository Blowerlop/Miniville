using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public SOCard card;
    public bool canBeBought = false;
    
    private void Start()
    {
        //card._cardEffect = new GetCoinsFromOther(card.coins);
    }

    private void OnValidate()
    {
        var imageUI = GetComponentInChildren<Image>();
        if (imageUI != null && card != null)
        {
            imageUI.sprite = card.sprite;
        }
    }
    public void LoadImage()
    {
        var imageUI = GetComponentInChildren<Image>();
        if (imageUI != null)
        {
            imageUI.sprite = card.sprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.clickedCard = eventData.pointerClick.GetComponent<Card>();
        Player.TryBuyCard();

        //Debug.Log(Game.DeckCards.IndexOf(eventData.pointerClick.GetComponent<Card>().gameObject));
    }

    public GameObject GetGameObject(Card card)
    {
        return gameObject;
    }
}

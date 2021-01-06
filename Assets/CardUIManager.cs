using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CardHolder : MonoBehaviour
{
    public static List<GameObject> allCards = new List<GameObject>();

    [SerializeField] private float CardsAngle = 0;
    [SerializeField] private float CardsSpacing = 0;

    private void Awake()
    {
        
    }

    private void Start()
    {
        SpawnCards();
    }

    private void Update()
    {
        UpdateCards();
    }

    private void SpawnCards()
    {
        allCards.ForEach(card =>
        {
            GameObject.Instantiate(card, Vector3.zero, Quaternion.identity, this.transform);
        });
    }

    private static void UpdateCards()
    {
        
    }
    
    public static void SetCardsAngle(float angle)
    {
        
    }

    public static void SetCardsSpacing(float spacing)
    {
        
    }

    public static void HighlightCard(int id)
    {
        
    }

    public void AddCard(GameObject card)
    {
        allCards.Add(card);
        GameObject.Instantiate(card, Vector3.zero, Quaternion.identity, this.transform);
    }

    public void RemoveCard(int id)
    {
        
    }
    
    
}

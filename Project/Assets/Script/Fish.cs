using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/Fishes", order = 1)]
public class Fish : ScriptableObject
{
    [field: SerializeField]
    private String Name{get;set;}

    [field: SerializeField]
    private float Size{get;set;}

    [field: SerializeField]
    private Rarity Rarity{get;set;}

    [field: SerializeField]
    private Sprite Sprite{get;set;}

    [field: SerializeField]
    private GameObject Prefab{get;set;}


    public String GetName() => Name;
    
    public Rarity GetRarity() => Rarity;

    public Sprite GetSprite() => Sprite;

    public GameObject GetPrefab() => Prefab;

    public float GetSize() => Size;
}


public enum Rarity
{
    Unknow = 0,
    Commun = 1,
    Rare = 2,
    UltraRare = 3 
}

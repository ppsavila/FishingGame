using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UniqueFishController : MonoBehaviour
{
    [field: SerializeField]
    private TextMeshProUGUI Name{get;set;}

    [field: SerializeField]
    private Image Image{get;set;}


    public void SetNameSprite(string name, Sprite sprite)
    {
        Name.text = name;
        Image.sprite = sprite;
    }
}


using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingManager : MonoBehaviour
{
    // Definindo pesos para cada raridade
    double pesoComum = 0.6;    // 60% de chance

    double pesoRaro = 0.3;     // 30% de chance

    double pesoUltraRaro = 0.1; // 10% de chance


    [field: SerializeField]
    List<Fish> CommunFish = new List<Fish>();

    [field: SerializeField]
    List<Fish> RareFish = new List<Fish>();

    [field: SerializeField]
    List<Fish> UltraRareFish = new List<Fish>();

    [field: SerializeField]
    private Image FishImage {get;set;}

    [field: SerializeField]
    private TextMeshProUGUI FishName {get;set;}

    [field: SerializeField]
    private TextMeshProUGUI RarityName {get;set;}

    [field: SerializeField]
    private CanvasGroup PickedFishCanvasGroup {get;set;}

    [field: SerializeField]
    private RectTransform PickedFishRect {get;set;}

    private Tween Tween;

    float RareFactor = 0f;

    void Start()
    {
        PickedFishCanvasGroup.alpha = 0f;
        PickedFishRect.localScale = Vector3.zero;
    }


    public void ShowPickedFish(Fish fish)
    {
        Tween.Kill();

        PickedFish(fish);
        Sequence seq = DOTween.Sequence();
        seq.Insert(0f, PickedFishCanvasGroup.DOFade(1f, .25f));
        seq.Insert(0f, PickedFishRect.DOScale(Vector3.one, .3f)).SetEase(Ease.OutBack);
        
        seq.Insert(3f, PickedFishCanvasGroup.DOFade(0f, .3f));
        seq.Insert(3f, PickedFishRect.DOScale(Vector3.zero, .3f));

        Tween = seq;
        Tween.Play();
    }


    private void PickedFish(Fish fish)
    {
        FishName.text = fish.GetName();

        FishImage.sprite = fish.GetSprite();

        FishImage.rectTransform.sizeDelta = new Vector2(10 * fish.GetSize(), 10 * fish.GetSize());
        
        RarityText(fish.GetRarity());
    }

    public Fish GetAFish()
    {
            
        System.Random random = new System.Random();

        double valor = random.NextDouble();

        if(RareFactor >= 1f)
        {
            RareFactor = 0f;
            return UltraRareFish[Random.Range(0, UltraRareFish.Count)];
        }

        if (valor < pesoComum)
        {
            RareFactor += .05f; //20 peixes comuns 1 ultrararo
            return CommunFish[Random.Range(0, CommunFish.Count)];
        }
        else if (valor < pesoComum + pesoRaro)
        {
            RareFactor += .1f; //10 peixes comuns 1 ultrararo
            return RareFish[Random.Range(0, RareFish.Count)];
        }
        else
        {
            RareFactor = 0f;
            return UltraRareFish[Random.Range(0, UltraRareFish.Count)];
        }
    }

    private void RarityText(Rarity rarity)
    {
        if(rarity == Rarity.Commun)
        {
            RarityName.color = Color.green;
            RarityName.text = "Commun";
        }
        else if(rarity == Rarity.Rare)
        {
            RarityName.color = Color.blue;
            RarityName.text = "Rare";
        }
        else if(rarity == Rarity.UltraRare)
        {
            RarityName.color = Color.magenta;
            RarityName.text = "UltraRare";
        }
            
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [field: SerializeField]
    private Image CaptureBarBG { get; set; }

    [field: SerializeField]
    private RectTransform CapturePercent { get; set; }

    [field: SerializeField]
    private TextMeshProUGUI CanFishText { get; set; }

    [field: SerializeField]
    private GameObject StartMenu { get; set; }

    [field: SerializeField]
    private GameObject PauseMenu { get; set; }

    [field: SerializeField]
    private GameObject Settings { get; set; }

    [field: SerializeField]
    private GameObject Onboarding { get; set; }

    [field: SerializeField]
    private GameObject Collectible { get; set; }

    [field: SerializeField]
    private GameObject YouWon { get; set; }

    [field: SerializeField]
    private Image LastCapturedImage { get; set; }
    
    [field: SerializeField]
    private TextMeshProUGUI LastCapturedText { get; set; }

    [field: SerializeField]
    private RectTransform CapturesContainer { get; set; }

    [field: SerializeField]
    private UniqueFishController CapturesFishPrefab { get; set; }



    [field: SerializeField]
    private Button LeaveButton { get; set; }

    [field: SerializeField]
    private Button PlayButton { get; set; }

    [field: SerializeField]
    private Button SettingsStartButton { get; set; }
    
    [field: SerializeField]
    private Button SettingsPauseButton { get; set; }

    [field: SerializeField]
    private Button SettingsPauseBackButton { get; set; }

    [field: SerializeField]
    private Button MenuBackButton { get; set; }

    [field: SerializeField]
    private Button OnboardingBackButton { get; set; }

    [field: SerializeField]
    private Button OnboardingPauseMenuButton { get; set; }

    [field: SerializeField]
    private Button CollectibleOpenMenuButton { get; set; }
    
    [field: SerializeField]
    private Button CollectibleOpenPauseButton { get; set; }

    [field: SerializeField]
    private Button CollectibleBackButton { get; set; }

    public List<Fish> CapturedFish = new List<Fish>();

    public List<Fish> UniqueCapturedFish = new List<Fish>();

    void Start()
    {
        StartMenu.SetActive(true);
        PlayButton.onClick.AddListener(Play);
        SettingsPauseButton.onClick.AddListener(OpenSettings);
        SettingsStartButton.onClick.AddListener(OpenSettings);
        SettingsPauseBackButton.onClick.AddListener(CloseSettings);
        MenuBackButton.onClick.AddListener(ClosePauseMenu);
        OnboardingBackButton.onClick.AddListener(CloseOnboarding);
        OnboardingPauseMenuButton.onClick.AddListener(OpenOnboarding);
        CollectibleOpenMenuButton.onClick.AddListener(OpenCollectibles);
        CollectibleOpenPauseButton.onClick.AddListener(OpenCollectibles);
        CollectibleBackButton.onClick.AddListener(CloseCollectible);
        LeaveButton.onClick.AddListener(()=>{
            Application.Quit();
        });
    }


    private void Update()
    {
        PauseMenu.SetActive(GameManager.Instance.isPaused);
        if(Input.GetButtonDown("Pause"))
        {
            if(Settings.activeSelf)
            {
                Settings.SetActive(false);
            }

            if(Onboarding.activeSelf)
            {
                Onboarding.SetActive(false);
            }

            if(Collectible.activeSelf)
            {
                Collectible.SetActive(false);
            }
        }

        YouWon.SetActive(UniqueCapturedFish.Count >=10);
    }

    public void CaptureBar(bool isCatching, float catchingPercent)
    {
        CaptureBarBG.gameObject.SetActive(isCatching);

        if(isCatching)
        {
            CapturePercent.anchorMax = new Vector2(catchingPercent, 1f);
        }
    }

    public void SetCanFishText(bool canFish)
    {
        CanFishText.gameObject.SetActive(canFish);
    }

    private void OpenCollectibles()
    {
        Collectible.SetActive(true);

        LastCapturedImage.sprite = CapturedFish.Last().GetSprite();
        LastCapturedText.text = CapturedFish.Last().GetName();  
        
        foreach (Transform child in CapturesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var Unique in UniqueCapturedFish)
        {
            UniqueFishController fish = Instantiate(CapturesFishPrefab, CapturesContainer.transform);
            fish.SetNameSprite(Unique.GetName(), Unique.GetSprite());
        }
    }

    private void Play()
    {
        StartMenu.SetActive(false);
        Onboarding.SetActive(true);
    }

    private void OpenSettings()
    {
        Settings.SetActive(true);
    }

    private void CloseSettings()
    {
        Settings.SetActive(false);
    }

    private void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        GameManager.Instance.isPaused = false;
    }

    private void CloseOnboarding()
    {
        Onboarding.SetActive(false);
        GameManager.Instance.isPaused = false;
    }

    private void CloseCollectible()
    {
        Collectible.SetActive(false);
    
    }

    private void OpenOnboarding()
    {
        Onboarding.SetActive(true);
        OnboardingBackButton.onClick.AddListener(()=>{
            Onboarding.SetActive(false);
        } );
    }

}

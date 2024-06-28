using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    [field: SerializeField]
    private Animator FishingRodAnimator { get; set; }

    [field: SerializeField]
    private Animator FloaterAnimator { get; set; }

    [field: SerializeField]
    private Animator PlayerAnimator { get; set; }

    [field: SerializeField]
    private FishingManager FishingManager { get; set; }

    [field: SerializeField]
    private UIController UIController { get; set; }

    private bool Cachted = false;
    private bool CanFish = false;
    private bool Catching = false;

    private Fish SortedFish;


    [field: SerializeField]
    private float ScrollThreshold { get; set; } = 1.0f; // Percentual de rotação necessário para acionar a ação (100%)

    [field: SerializeField]
    private float TimeToReachThreshold = 3.0f; // Tempo necessário para atingir 100% (3 segundos)

    [field: SerializeField]
    private float TimeLimit = 10.0f; // Tempo limite para tentar alcançar 100% (10 segundos)

    [field: SerializeField]
    private float ScrollFactor = 35.0f;

    [field: SerializeField]
    private float UnscrollFactor = 35.0f;


    [field: SerializeField]
    private LineRenderer LineRenderer { get; set; }

    [field: SerializeField]
    private Transform FishingRodPointer { get; set; }

    public List<Fish> CapturedFish = new List<Fish>();

    private float ScrollProgress = 0.0f;
    private float ElapsedTime = 0.0f;
    private float ScrollTimeAccumulator = 0.0f;
    private float ScrollInput = 0.0f;

    private void Start()
    {
        FloaterAnimator.gameObject.SetActive(false);
        LineRenderer.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (GameManager.Instance.isPaused)
            return;
        ScrollInput = Input.GetAxis("Mouse ScrollWheel");
    }


    private void FixedUpdate()
    {
        if (GameManager.Instance.isPaused)
            return;

        UIController.SetCanFishText(CanFish);

        if (Input.GetButton("Fire2"))
        {
            Throw();
        }


        if (Cachted && Input.GetButton("Fire1"))
        {
            Catch();
        }

        if (Catching)
        {

            ElapsedTime += Time.fixedDeltaTime;
            if (ScrollInput < 0)
            {
                ScrollTimeAccumulator += Mathf.Abs(ScrollInput) * Time.fixedDeltaTime * ScrollFactor;
                ScrollProgress = Mathf.Clamp01(ScrollTimeAccumulator / TimeToReachThreshold);
            }
            else
            {
                ScrollProgress = Mathf.Clamp01(ScrollProgress - Time.fixedDeltaTime / TimeToReachThreshold);
            }



            // Verifica se o jogador atingiu o percentual necessário
            if (ScrollProgress >= ScrollThreshold)
            {
                Debug.Log("Scrollwheel atingiu 100%!");
                Capture();
            }

            // Verifica se o tempo limite foi atingido
            if (ElapsedTime >= TimeLimit)
            {
                Debug.Log("Tempo limite atingido!");

                FishingRodAnimator.SetBool("Fishing", false);
                Cachted = false;
                Catching = false;
                ScrollProgress = 0.0f;
                ScrollTimeAccumulator = 0.0f;
                ElapsedTime = 0.0f;
            }


            // Reset the scroll input for the next FixedUpdate cycle
            ScrollInput = 0.0f;
        }

        UIController.CaptureBar(Catching, ScrollProgress);

        LineRenderer.SetPosition(0, FishingRodPointer.position);
        LineRenderer.SetPosition(1, FloaterAnimator.transform.position);
    }

    private void Throw()
    {
        FishingRodAnimator.SetTrigger("Throw");
        PlayerAnimator.SetTrigger("Throw");
        if (CanFish)
            Fishing();
    }

    private void Fishing()
    {
        LineRenderer.gameObject.SetActive(true);
        FloaterAnimator.gameObject.SetActive(true);
        FishingRodAnimator.SetBool("Fishing", true);
        SortedFish = FishingManager.GetAFish();
        Cachted = true;
    }

    private void Catch()
    {
        Catching = true;
        FloaterAnimator.SetTrigger("Catch");
    }

    private void Capture()
    {
        // Mostra o peixe capturado e registra no log 
        LineRenderer.gameObject.SetActive(false);
        FloaterAnimator.gameObject.SetActive(false);
        UIController.CaptureBar(true, 0);
        Catching = false;
        Cachted = false;
        FishingRodAnimator.SetBool("Fishing", false);
        Cachted = false;
        Catching = false;

        CapturedFish.Add(SortedFish);
        UIController.CapturedFish = CapturedFish;
        
        if(!UIController.UniqueCapturedFish.Contains(SortedFish))
            UIController.UniqueCapturedFish.Add(SortedFish);

        ScrollProgress = 0.0f;
        ScrollTimeAccumulator = 0.0f;
        ElapsedTime = 0.0f;
        FishingManager.ShowPickedFish(SortedFish);
    }

    public void SetFishingPossible(bool isPossible)
    {
        CanFish = isPossible;
        if (!isPossible)
        {
            LineRenderer.gameObject.SetActive(false);
            FloaterAnimator.gameObject.SetActive(false);
            UIController.CaptureBar(true, 0);
            Catching = false;
            Cachted = false;
            FishingRodAnimator.SetBool("Fishing", false);
            Cachted = false;
            Catching = false;

            ScrollProgress = 0.0f;
            ScrollTimeAccumulator = 0.0f;
            ElapsedTime = 0.0f;
        }
    }
}

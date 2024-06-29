using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public Light directionalLight;
    public float dayDuration = 120f; // Duração de um dia em segundos

    [Header("Light Colors")]
    public Color morningColor = new Color(1f, 0.64f, 0.32f); // Laranja
    public Color dayColor = Color.white;
    public Color eveningColor = new Color(1f, 0.64f, 0.32f); // Laranja
    public Color nightColor = new Color(0.2f, 0.2f, 0.35f); // Azul Escuro

    [Header("Initial Time")]
    [Range(0, 1)]
    public float initialTime = 0f; // Valor entre 0 (meia-noite) e 1 (meia-noite do dia seguinte)

    private float time;

    void Start()
    {
        // Define o tempo inicial
        time = initialTime;
    }

    void Update()
    {
        time += Time.deltaTime / dayDuration;
        if (time >= 1) time = 0;

        UpdateLighting(time);
    }

    void UpdateLighting(float time)
    {
        // Rotaciona a luz direcional para simular o movimento do sol
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((time * 360f) - 90f, 170f, 0));

        // Modifica a cor da luz com base na posição
        if (time <= 0.25f)
        {
            // Manhã: do azul escuro ao laranja
            directionalLight.color = Color.Lerp(nightColor, morningColor, time * 4);
        }
        else if (time <= 0.5f)
        {
            // Dia: do laranja ao branco
            directionalLight.color = Color.Lerp(morningColor, dayColor, (time - 0.25f) * 4);
        }
        else if (time <= 0.75f)
        {
            // Tarde: do branco ao laranja
            directionalLight.color = Color.Lerp(dayColor, eveningColor, (time - 0.5f) * 4);
        }
        else
        {
            // Noite: do laranja ao azul escuro
            directionalLight.color = Color.Lerp(eveningColor, nightColor, (time - 0.75f) * 4);
        }

        // Modifica a intensidade da luz com base na posição
        float intensityMultiplier = 1;
        if (time <= 0.23f || time >= 0.75f)
        {
            intensityMultiplier = 0; // Noite
        }
        else if (time <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((time - 0.23f) * (1 / 0.02f)); // Amanhecer
        }
        else if (time >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((time - 0.73f) * (1 / 0.02f))); // Anoitecer
        }

        directionalLight.intensity = intensityMultiplier;
    }
}

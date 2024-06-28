using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> musicClips;
    private int currentClipIndex = 0;

    private bool CanChange = false;

    void Start()
    {

        if (musicClips.Count > 0)
        {
            currentClipIndex = Random.Range(0, musicClips.Count);
            PlayClip(musicClips[currentClipIndex]);
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextClip();
        }

        if (CanChange)
        {
            if (Input.GetKeyDown(KeyCode.E))
                PlayNextClip();

            if (Input.GetKeyDown(KeyCode.Q))
                PlayPreviousClip();
        }


    }

    public void PlayNextClip()
    {
        currentClipIndex++;
        if (currentClipIndex >= musicClips.Count)
        {
            currentClipIndex = 0; // Reinicia a playlist ao final
        }
        PlayClip(musicClips[currentClipIndex]);
    }

    public void PlayPreviousClip()
    {
        currentClipIndex--;
        if (currentClipIndex < 0)
        {
            currentClipIndex = musicClips.Count - 1; // Vai para a última música se já estiver na primeira
        }
        PlayClip(musicClips[currentClipIndex]);
    }

    private void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    // Método para o jogador passar para a próxima música manualmente
    public void SkipToNext()
    {
        PlayNextClip();
    }

    // Método para o jogador voltar para a música anterior manualmente
    public void SkipToPrevious()
    {
        PlayPreviousClip();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            CanChange = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            CanChange = false;

    }
}

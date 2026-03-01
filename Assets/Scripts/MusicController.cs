using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource backgroundMusic;   // 2D music
    public AudioSource speakerMusic;      // 3D speaker

    [Header("Fade Settings")]
    public float fadeDuration = 1f;
    public float mutedVolume = 0f;

    private float originalBgVolume;
    private Coroutine fadeRoutine;

    private void Start()
    {
        if (backgroundMusic != null)
            originalBgVolume = backgroundMusic.volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Fade background music down
        StartFade(backgroundMusic, mutedVolume);

        // Start speaker
        if (speakerMusic != null && !speakerMusic.isPlaying)
            speakerMusic.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Fade background music back up
        StartFade(backgroundMusic, originalBgVolume);

        // Stop speaker
        if (speakerMusic != null && speakerMusic.isPlaying)
            speakerMusic.Stop();
    }

    void StartFade(AudioSource audio, float targetVolume)
    {
        if (audio == null) return;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(Fade(audio, targetVolume));
    }

    IEnumerator Fade(AudioSource audio, float target)
    {
        float start = audio.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audio.volume = Mathf.Lerp(start, target, time / fadeDuration);
            yield return null;
        }

        audio.volume = target;
    }
}

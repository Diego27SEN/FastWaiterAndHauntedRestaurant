using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(AudioClip clip, float volume, float pitch)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        audioSource.Play();

        StartCoroutine(ReturnToPool());
    }

    IEnumerator ReturnToPool()
    {
        // Espera el tiempo real del clip (ajustado por pitch)
        yield return new WaitForSeconds(audioSource.clip.length / audioSource.pitch);

        // NO SE DESTRUYE
        // SOLO SE DESACTIVA PARA QUE EL POOL LO REUSE
        gameObject.SetActive(false);
    }
}
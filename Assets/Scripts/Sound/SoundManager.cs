using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip pedidoRecibidoClip;
    public AudioClip pedidoEntregadoClip;
    public AudioClip ghostClip;


    public Dictionary<string, AudioClip> musicData = new();


    public GameObject AudioReproducerPrefab;

    public int PoolSize = 10;

    public List<GameObject> AudioPool = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;


        for (int i = 0; i < PoolSize; i++)
        {
            GameObject obj = Instantiate(AudioReproducerPrefab, transform);

            AudioPool.Add(obj);
        }
    }
    void Start()
    {
        musicData.Add("PedidoRecibido", pedidoRecibidoClip);
        musicData.Add("PedidoEntregado", pedidoEntregadoClip);
        musicData.Add("GhostSpawn", ghostClip);

    }
    public void PlaySound(string musicName, float volume)
    {
        if (musicData.TryGetValue(musicName, out AudioClip clip))
        {
            print(clip.name);

            AudioSource audioSource = GetAvalibleSoundReproducer().GetComponent<AudioSource>();

            if (audioSource == null)
            {
                print("se acabaron los objetos en la pool");
                return;
            }

            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.gameObject.SetActive(true);
            audioSource.GetComponent<AudioReproducer>().SetAudio();
        }
        else
        {
            print(" no existe");
        }
    }
    public GameObject GetAvalibleSoundReproducer()
    {
        foreach (var item in AudioPool)
        {
            if (item.activeSelf == false)
                return item;
        }
        return null;
    }
}
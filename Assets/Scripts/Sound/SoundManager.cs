using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Header("Library de sonidos")]
    public SoundLibrary library; // Base de datos de sonidos
    [Header("Prefab para reproducir sonidos")]
    public GameObject soundPlayerPrefab;
    [Header("Tamaño de Pool")]
    public int poolSize = 10; // Cantidad de objetos reutilizables
    private GameObject[] pool; // Arreglo donde guardamos los SoundPlayers
    private int poolIndex = 0; // Puntero circular para elegir el siguiente objeto
    public List<SoundPlayer> AudioPool = new List<SoundPlayer>();
    void Awake()
    {
        // Singleton para acceso global
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        library.Initialize(); // Convierte la lista en diccionario
        InitializePool();     // Crea los objetos iniciales
    }
    void InitializePool()
    {
        pool = new GameObject[poolSize];

        // Instancia los SoundPlayers desactivados al inicio
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(soundPlayerPrefab, transform);
            pool[i].SetActive(false); // No están reproduciendo aún
        }
    }
    GameObject GetFromPool()
    {
        // Usamos un "índice circular"
        GameObject obj = pool[poolIndex];
        // poolIndex avanza 1,2,3,4... pero cuando llega al final vuelve a 0
        poolIndex = (poolIndex + 1) % poolSize;
        return obj;
    }

    public void Play(string id)
    {
        var s = library.Get(id);
        if (s == null)
        {
            Debug.LogWarning("Sonido no encontrado: " + id);
            return;
        }
        // Obtiene un objeto del pool
        GameObject p = GetFromPool();
        p.SetActive(true);

        // Inicializa el reproductor con clip/volumen/pitch
        var player = p.GetComponent<SoundPlayer>();
        player.Initialize(s.clip, s.volume, s.pitch);
    }
}
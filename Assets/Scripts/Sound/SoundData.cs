using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Audio/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    // Esta clase representa un sonido individual en la librería
    [System.Serializable]
    public class SoundData
    {
        public string id;            // Nombre único del sonido (ej: "attack", "dash", "hit")
        public AudioClip clip;       // Archivo de audio
        [Range(0f, 1f)] public float volume = 1f;  // Volumen por defecto
        [Range(0.5f, 2f)] public float pitch = 1f; // Tonalidad (velocidad del sonido)
    }

    // Lista editable en el inspector (Unity)
    public List<SoundData> sounds = new List<SoundData>();

    // Diccionario interno para buscar rápidamente usando el ID
    private Dictionary<string, SoundData> soundDict;

    // Llamado por el SoundManager al iniciar
    public void Initialize()
    {
        soundDict = new Dictionary<string, SoundData>();

        // Convierte la lista en diccionario
        foreach (var s in sounds)
        {
            if (!soundDict.ContainsKey(s.id))
                soundDict.Add(s.id, s);
        }
    }

    // Devuelve el sonido usando su ID
    public SoundData Get(string id)
    {
        soundDict ??= new Dictionary<string, SoundData>(); // Seguridad

        return soundDict.ContainsKey(id) ? soundDict[id] : null;
    }
}
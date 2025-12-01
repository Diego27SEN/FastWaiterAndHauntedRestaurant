using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioReproducer : MonoBehaviour
{
    AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {

    }

    public void SetAudio()
    {
        //  source.clip.length

        Invoke("DesactiveObj", source.clip.length);
    }
    public void DesactiveObj()
    {
        gameObject.SetActive(false);
    }
}

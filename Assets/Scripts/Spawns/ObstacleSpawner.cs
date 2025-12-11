using System.Security.Cryptography;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;//Prefab del obstáculo
    public Transform[] spawnPoints; //Puntos donde puede aparecer

    public float spawnInterval = 20f;//Cada cuanto aparece el fantasma
    public float obstacleLifetime = 7f;//Duración del obstáculo
    private GameObject currentObstacle;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), spawnInterval, spawnInterval);
    }

    void SpawnObstacle()
    {
        print("espiritu de fuego!!");
        SoundManager.Instance.Play("Fantasmal"); 
        if (currentObstacle != null) return; // Si ya hay un obstáculo, no crear otro
        int randomIndex = Random.Range(0, spawnPoints.Length); // Elegir un punto aleatorio
        Transform spawnPoint = spawnPoints[randomIndex];

        currentObstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity); // Instanciar el obstáculo
        Destroy(currentObstacle, obstacleLifetime);// Destruirlo luego del tiempo configurado
    }

}

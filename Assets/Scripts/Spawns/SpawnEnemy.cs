using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Mesero;
    public GameObject EnemyPrefab;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float currentTime;
    [SerializeField] private float radius;
    [SerializeField] private List<Transform> spawnPosition;

    public void Update()
    {
        SpawnMobTimer(); // Llama al temporizador en cada frame
    }

    public void SpawnMobTimer()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeToSpawn)
        {
            SpawnMob();
            currentTime = 0;
        }
    }

    public void SpawnMob()
    {
        print("Mounstro Invocado");
        SoundManager.Instance.Play("Fantasmal");// Reproduce el sonido de invocacion
        float randonAngle = Random.Range(0, 2 * Mathf.PI);// Angulo aleatorio en radianes
        Vector2 randomDirInAnlge = new Vector2(Mathf.Cos(randonAngle), Mathf.Sin(randonAngle));// Vector2 en direccion aleatoria
        Vector2 randomDirInAnlgeMagnitude = Mesero.transform.position + (Vector3)(randomDirInAnlge * radius);// Vector2 con magnitud segun el radio

        GameObject enemigo = Instantiate(EnemyPrefab, randomDirInAnlgeMagnitude, Quaternion.identity);// Instancia el enemigo en la posicion calculada

        // Asigna el Mesero al enemigo instanciado
        Enemy scriptEnemigo = enemigo.GetComponent<Enemy>(); // Obtiene el script Enemie del enemigo instanciado
        if (scriptEnemigo != null)// Verifica que el script no sea nulo
        {
            scriptEnemigo.ControllerMesero = Mesero;// Asigna el Mesero al script del enemigo
        }
    }
}

using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float currentTime;
    [SerializeField] private Transform[] puntosB;
    private int indicePunto = 0;

    public void Update()
    {
        SpawnNPCTimer();
    }
    public void SpawnNPCTimer()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToSpawn)
        {
            SpawnNPCInstance();
            currentTime = 0;
        }
    }
    public void SpawnNPCInstance()
    {
        GameObject npc = Instantiate(NPCPrefab, transform.position, Quaternion.identity); 

        ControllerNPC npcScript = npc.GetComponent<ControllerNPC>();
        if (npcScript != null && puntosB.Length > 0)
        {
            npcScript.PUNTOB = puntosB[indicePunto % puntosB.Length]; // Asignar el punto B correspondiente
            indicePunto++;
        }
    }
    public void ShowDialog()
    {
        GameManager.Instance.dialogSystemUI.ShowDialog("Textito");
    }
}

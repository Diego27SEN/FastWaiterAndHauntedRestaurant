using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public GameObject mesero;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xPlayer = mesero.transform.position.x;
        float yPlayer = mesero.transform.position.y;
        transform.position = new Vector3(xPlayer, yPlayer, -10);
    }
}

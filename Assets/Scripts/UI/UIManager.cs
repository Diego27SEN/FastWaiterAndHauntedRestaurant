using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }

    public void VolverAJugar()
    {
        SceneManager.LoadScene("Restaurante"); 
    }
}

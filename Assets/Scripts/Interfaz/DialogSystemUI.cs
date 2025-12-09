using UnityEngine;
using TMPro;

public class DialogSystemUI : MonoBehaviour
{
    public TMP_Text dialogText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowDialog(string message)
    {
        dialogText.text = message;
    }
}

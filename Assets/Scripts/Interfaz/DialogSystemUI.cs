using UnityEngine;
using TMPro;

public class DialogSystemUI : MonoBehaviour
{
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    private bool isDialogVisible = false;
    public bool IsDialogVisible => isDialogVisible;

    public void ShowDialog(string message)
    {
        dialogText.text = message;
        dialogPanel.SetActive(true);
        isDialogVisible = true;
    }

    public void HideDialog()
    {
        dialogPanel.SetActive(false);
        isDialogVisible = false;
    }

    void Update()
    {
    }
}

using UnityEngine;

public class OpenOptions : MonoBehaviour
{
    [SerializeField] private GameObject optionsCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsCanvas != null)
                optionsCanvas.SetActive(!optionsCanvas.activeSelf);
        }
    }
}

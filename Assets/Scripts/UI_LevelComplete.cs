using UnityEngine;

public class UI_LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("Angel"))
            levelCompletePanel.SetActive(true);
    }
}
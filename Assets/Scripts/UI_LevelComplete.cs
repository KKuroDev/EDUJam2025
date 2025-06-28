using UnityEngine;

public class UI_LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Devil"))
            levelCompletePanel.SetActive(true);
    }
}
using UnityEngine;

public class AngelDeath : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Angel"))
        {
            Debug.Log("Hit");
            other.GetComponent<GridMovement>().CanMove = false;
            gameOverPanel.SetActive(true);
        }
    }
}

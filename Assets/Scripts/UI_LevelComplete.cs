using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelComplete : MonoBehaviour
{
    [SerializeField] private GameObject levelCompletePanel;

    public static event Action<int> OnLevelUnlocked;

    private void Start()
    {
        StartCoroutine(EnableLevelManager());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("Angel"))
        {
            levelCompletePanel.SetActive(true);
            string levelNumber = SceneManager.GetActiveScene().name.Replace("Level_", "");
            OnLevelUnlocked?.Invoke(int.Parse(levelNumber));
        }
    }

    private IEnumerator EnableLevelManager()
    {
        yield return new WaitForSecondsRealtime(1f);
        GameObject.Find("GameManager").gameObject.transform.Find("LevelManager").gameObject.SetActive(true);
    }
}
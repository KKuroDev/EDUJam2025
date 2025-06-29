using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    private void OnEnable()
    {
        if (GameObject.FindFirstObjectByType<UI_LevelComplete>() != null)
            UI_LevelComplete.OnLevelUnlocked += UnlockNextLevel;
    }

    private void OnDisable()
    {
        if (GameObject.FindFirstObjectByType<UI_LevelComplete>() != null)
            UI_LevelComplete.OnLevelUnlocked -= UnlockNextLevel;
        
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject.transform.parent);

        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = (i < unlockedLevel);
        }
    }

    public void UnlockNextLevel(int levelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel");

        if (levelIndex > unlockedLevel)
        {
            PlayerPrefs.SetInt("unlockedLevel", levelIndex);
        }
    }
}

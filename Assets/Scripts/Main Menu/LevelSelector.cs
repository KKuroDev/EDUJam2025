using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = (i < unlockedLevel);
        }
    }

    public void UnlockNextLevel(int levelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);
        if (levelIndex + 1 > unlockedLevel)
        {
            PlayerPrefs.SetInt("unlockedLevel", levelIndex + 1);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;
    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = (i == 0);
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

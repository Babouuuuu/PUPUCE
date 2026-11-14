using UnityEngine;

public class ResetAchievementsButton : MonoBehaviour
{
    public GameObject achievementManagerObject; // Reference to the Achievementmanager GameObject

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            achievementManagerObject.SetActive(!achievementManagerObject.activeSelf); // Toggle the active state of the Achievementmanager GameObject
        }
    }

    public void ResetAchievements()
    {
        PlayerPrefs.DeleteAll();
        Achievementmanager.RefreshSlots();
    }
}

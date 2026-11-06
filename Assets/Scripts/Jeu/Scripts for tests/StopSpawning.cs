using UnityEngine;
using UnityEngine.UI;

public class StopSpawning : MonoBehaviour
{
    private SpawnerPopups spawnerPopup;
    public Toggle stopSpawningToggle; // Assurez-vous d'avoir un Toggle dans votre scène et de le lier à cette variable

    // Update is called once per frame
    void Update()
    {
        if (stopSpawningToggle != null)
        {
            if (stopSpawningToggle.isOn)
            {
                if (spawnerPopup == null)
                {
                    spawnerPopup = FindAnyObjectByType<SpawnerPopups>();
                }
                if (spawnerPopup != null)
                {
                    spawnerPopup.enabled = false; // Désactive le script SpawnerPopup pour arrêter l'apparition des popups
                }
            }
            else
            {
                if (spawnerPopup != null)
                {
                    spawnerPopup.enabled = true; // Réactive le script SpawnerPopup pour permettre l'apparition des popups
                }
            }
        }
    }
}

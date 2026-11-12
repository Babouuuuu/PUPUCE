using UnityEngine;
using UnityEngine.UI;

public class StopSpawning : MonoBehaviour
{
    public Toggle stopSpawningToggle; // Assurez-vous d'avoir un Toggle dans votre scène et de le lier à cette variable

    // Update is called once per frame
    void Update()
    {
        if (stopSpawningToggle != null)
        {
            if (stopSpawningToggle.isOn)
            {
                if (SpawnerPopups.instance == null)
                {
                    SpawnerPopups.instance = FindAnyObjectByType<SpawnerPopups>();
                }
                if (SpawnerPopups.instance != null)
                {
                    SpawnerPopups.instance.enabled = false; // Désactive le script SpawnerPopup pour arrêter l'apparition des popups
                }
            }
            else
            {
                if (SpawnerPopups.instance != null)
                {
                    SpawnerPopups.instance.enabled = true; // Réactive le script SpawnerPopup pour permettre l'apparition des popups
                }
            }

            if (stopSpawningToggle.isOn)
            {
                if (SpawnerSpecials.instance == null)
                {
                    SpawnerSpecials.instance = FindAnyObjectByType<SpawnerSpecials>();
                }
                if (SpawnerSpecials.instance != null)
                {
                    SpawnerSpecials.instance.enabled = false; // Désactive le script SpawnerSpecials pour arrêter l'apparition des popups spéciaux
                }
            }
            else
            {
                if (SpawnerSpecials.instance != null)
                {
                    SpawnerSpecials.instance.enabled = true; // Réactive le script SpawnerSpecials pour permettre l'apparition des popups spéciaux
                }
            }
        }
    }
}

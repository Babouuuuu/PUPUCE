using UnityEngine;
using UnityEngine.UI;

public class CheatMenuManager : MonoBehaviour
{
    public GameObject cheatMenuCanvas; 
    public GameObject popups;
    public GameObject boss;
    public GameObject upgrades;
    public GameObject spawnMenu;

    // Update is called once per frame
    public void OuvrirFermerAllCheat()
    {
        if (cheatMenuCanvas != null)
        {
            cheatMenuCanvas.SetActive(!cheatMenuCanvas.activeSelf); // Active ou désactive le Canvas du menu de triche
        }
    }

    public void OuvrirPopups()
    {
        if (popups != null)
        {
            popups.SetActive(true);
            spawnMenu.SetActive(false); // Active le Canvas des popups
        }
    }

    public void FermerPopups()
    {
        if (popups != null)
        {
            popups.SetActive(false); // Désactive le Canvas des popups
            spawnMenu.SetActive(true); // Réactive le Canvas du menu de triche
        }
    }

    public void OuvrirBoss()
    {
        if (boss != null)
        {
            boss.SetActive(true); // Active ou désactive le Canvas du boss
            spawnMenu.SetActive(false); // Active le Canvas des popups
        }
    }

    public void FermerBoss()
    {
        if (boss != null)
        {
            boss.SetActive(false); // Désactive le Canvas du boss
            spawnMenu.SetActive(true); // Réactive le Canvas du menu de triche
        }
    }

    public void OuvrirUpgrades()
    {
        if (upgrades != null)
        {
            upgrades.SetActive(true); // Active ou désactive le Canvas des upgrades
            spawnMenu.SetActive(false); // Désactive le Canvas du menu de triche
        }
    }

    public void FermerUpgrades()
    {
        if (upgrades != null)
        {
            upgrades.SetActive(false); // Désactive le Canvas des upgrades
            spawnMenu.SetActive(true); // Réactive le Canvas du menu de triche
        }
    }

    void Update()
    {
        // Vérifie si la touche " " est pressée pour ouvrir/fermer le menu de triche
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OuvrirFermerAllCheat();
        }
    }
}

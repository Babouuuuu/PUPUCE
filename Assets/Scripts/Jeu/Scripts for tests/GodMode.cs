using UnityEngine;
using UnityEngine.UI;

public class GodMode : MonoBehaviour
{
    private Joueur joueur;
    public Toggle godModeToggle; // Assurez-vous d'avoir un Toggle dans votre scène et de le lier à cette variable

    // Update is called once per frame
    void Update()
    {
        if (godModeToggle != null)
        {
            if (godModeToggle.isOn)
            {
                if (joueur == null)
                {
                    joueur = FindAnyObjectByType<Joueur>();
                }
                if (joueur != null)
                {
                    joueur.GetComponent<Collider2D>().enabled = false; // Supprime le collider du joueur pour qu'il ne puisse plus être touché
                }
            }
            else
            {
                if (joueur != null)
                {
                    joueur.GetComponent<Collider2D>().enabled = true; // Rétablit le collider du joueur
                }
            }
        }
    }
}

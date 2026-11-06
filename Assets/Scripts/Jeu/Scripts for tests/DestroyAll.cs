using UnityEngine;
using UnityEngine.UI;

public class DestroyAll : MonoBehaviour
{
    public Toggle toggleDestroy; // Assurez-vous d'avoir un Toggle dans votre scène et de le lier à cette variable
    private Popup popup;

    // Update is called once per frame
    void Update()
    {
        if (toggleDestroy != null && toggleDestroy.isOn)
        {
            if (popup == null)
            {
                popup = FindAnyObjectByType<Popup>();
            }
            if (popup != null)
            {
                Debug.Log("NoXP activé : Réinitialisation de l'expérience du joueur à 0");
                popup.Detruire(); // Réinitialise l'expérience du joueur à 0
            }
        
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// interaction "garder le bouton enfoncé pendant X secondes"
// IPointerDownHandler et IPointerUpHandler : grâce à eux, Unity appelle OnPointerDown / OnPointerUp quand on appuie / relâche le clic sur cet objet (UNITY BON CHIEN)
public class InteractionMaintenir : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // reference du parent
    public Popup popup;
    public float duree = 1.2f;
    public Image jauge;

    bool appuye;
    float tempsAppuye;

    public void OnPointerDown(PointerEventData eventData) 
    {
        appuye = true;
    } 
    public void OnPointerUp(PointerEventData eventData) 
    {
        appuye = false;
    }

    void Update()
    {
        if (appuye) tempsAppuye += Time.deltaTime;
        else tempsAppuye = 0f; // si on lache on recommence de 0

        jauge.fillAmount = tempsAppuye / duree;

        // quand le joueur a fini
        if (tempsAppuye >= duree)
        {
            tempsAppuye = 0f;
            appuye = false; // il faudra relâcher et rappuyer pour l'interaction suivante
            popup.Interaction();
        }
    }
}

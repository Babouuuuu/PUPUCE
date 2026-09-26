using UnityEngine;
using UnityEngine.UI;

// interaction des case a coché
public class InteractionCases : MonoBehaviour
{
    public Popup popup;
    public Toggle[] cases;

    // branchée sur le OnClick du bouton "Suivant"
    public void CliquerSuivant()
    {
        // on parcours la liste des cases a coché pour verif si elle sont toutes décocher ou non
        foreach (Toggle caseACocher in cases)
        {
            if (caseACocher.isOn)
            {
                popup.Trembler(); // il reste une case cochée : raté !
                return;
            }
        }

        // validé si toutes décocher
        popup.Interaction();

        // On recoche tout (utile pour les popups à plusieurs interactions)
        foreach (Toggle caseACocher in cases) caseACocher.isOn = true;
    }
}

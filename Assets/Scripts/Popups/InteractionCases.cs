using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

// interaction des case a coché
public class InteractionCases : MonoBehaviour
{
    public Popup popup;
    public Toggle[] cases;
    public Toggle[] badCases;
    
    // branchée sur le OnClick du bouton "Suivant"
    public void CliquerSuivant()
    {
        // on parcours la liste des cases a coché pour verif si elle sont toutes cocher ou non
        foreach (Toggle caseACocher in cases)
        {
            if (caseACocher.isOn == false)
            {
                popup.Trembler(); // il reste une case décochée : raté !
                Debug.Log("Manque case");
                return;
            }
        }

        // on parcours la liste des cases a ne pas cocher pour verif si il y en a des cochés ou non
        foreach (Toggle caseAPasCocher in badCases)
        {
            if (caseAPasCocher.isOn)
            {
                popup.Trembler(); // il y a une mauvaise case cochée : raté !
                Debug.Log("badcases");
                return;
            }
        }
        
        // validé si toutes cocher
        popup.Interaction();

        // On redécoche tout (utile pour les popups à plusieurs interactions)
        foreach (Toggle caseACocher in cases) caseACocher.isOn = false;
        foreach (Toggle caseAPasCocher in badCases) caseAPasCocher.isOn = false;
    }
}

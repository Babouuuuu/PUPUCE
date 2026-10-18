using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

// interaction des case a coché
public class InteractionCasesBoss : MonoBehaviour
{
    public Popup popup;

    public Toggle[] casesV;
    public Toggle[] badCasesV;
    public Toggle[] casesC;
    public Toggle[] badCasesC;
    public Toggle[] casesG;
    public Toggle[] badCasesG;

    public BossCaptcha bossCaptcha;
    
    // branchée sur le OnClick du bouton "Suivant"
    public void CliquerSuivant()
    {
       
       if (bossCaptcha.captchaCar.activeInHierarchy == true)
        {
                // on parcours la liste des cases a coché pour verif si elle sont toutes cocher ou non
            foreach (Toggle caseACocher in casesV)
            {
                if (caseACocher.isOn == false)
                {
                    popup.Trembler(); // il reste une case décochée : raté !
                    Debug.Log("Manque case");
                    return;
               }
            }

        // on parcours la liste des cases a ne pas cocher pour verif si il y en a des cochés ou non
            foreach (Toggle caseAPasCocher in badCasesV)
            {
                if (caseAPasCocher.isOn)
                {
                    popup.Trembler(); // il y a une mauvaise case cochée : raté !
                    Debug.Log("badcases");
                    return;
                }
            }
        }
        else if (bossCaptcha.captchaDog.activeInHierarchy == true)
        {
                // on parcours la liste des cases a coché pour verif si elle sont toutes cocher ou non
            foreach (Toggle caseACocher in casesC)
            {
                if (caseACocher.isOn == false)
                {
                    popup.Trembler(); // il reste une case décochée : raté !
                    Debug.Log("Manque case");
                    return;
               }
            }

                    // on parcours la liste des cases a ne pas cocher pour verif si il y en a des cochés ou non
            foreach (Toggle caseAPasCocher in badCasesC)
            {
                if (caseAPasCocher.isOn)
                {
                    popup.Trembler(); // il y a une mauvaise case cochée : raté !
                    Debug.Log("badcases");
                    return;
                }
            }
        }
        else if (bossCaptcha.captchaGame.activeInHierarchy == true)
        {
                // on parcours la liste des cases a coché pour verif si elle sont toutes cocher ou non
            foreach (Toggle caseACocher in casesG)
            {
                if (caseACocher.isOn == false)
                {
                    popup.Trembler(); // il reste une case décochée : raté !
                    Debug.Log("Manque case");
                    return;
               }
            }

                    // on parcours la liste des cases a ne pas cocher pour verif si il y en a des cochés ou non
            foreach (Toggle caseAPasCocher in badCasesG)
            {
                if (caseAPasCocher.isOn)
                {
                    popup.Trembler(); // il y a une mauvaise case cochée : raté !
                    Debug.Log("badcases");
                    return;
                }
            }
        }        

        // validé si toutes cocher
        popup.Interaction();

        // On redécoche tout (utile pour les popups à plusieurs interactions)
        foreach (Toggle caseACocher in casesV) caseACocher.isOn = false;
        foreach (Toggle caseAPasCocher in badCasesV) caseAPasCocher.isOn = false;
        foreach (Toggle caseACocher in casesG) caseACocher.isOn = false;
        foreach (Toggle caseAPasCocher in badCasesG) caseAPasCocher.isOn = false;
        foreach (Toggle caseACocher in casesC) caseACocher.isOn = false;
        foreach (Toggle caseAPasCocher in badCasesC) caseAPasCocher.isOn = false;

        bossCaptcha.ResetCaptcha();
    }

    public void CliquerRobot()
    {
        bossCaptcha.captchaRobot.SetActive(false);
        bossCaptcha.StartCaptcha();
    }
}

using UnityEngine;

public class InterractionPaint : MonoBehaviour
{
    [HideInInspector] 
    public bool isRedColorIn = false;
    [HideInInspector]
    public bool isYellowColorIn = false;
    [HideInInspector]
    public bool isGreenColorIn = false;
    [HideInInspector]
    public bool isCyanColorIn = false;
    [HideInInspector]
    public bool isBlueColorIn = false;
    [HideInInspector]
    public bool isMagentaColorIn = false;


    public Popup popup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CliquerFinish()
    {
        if(isRedColorIn && isYellowColorIn && isGreenColorIn && isCyanColorIn && isBlueColorIn && isMagentaColorIn)
        {
            popup.Interaction();
            Debug.Log("Bravo, vous avez trouvé toutes les couleurs !");
        }
        else
        {
            popup.Trembler();
            Debug.Log("Il vous manque encore des couleurs à trouver.");
        }
    }
}

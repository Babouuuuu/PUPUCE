using UnityEngine;
using UnityEngine.UI;

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

    public LineGenerator lineGenerator;

    public Popup popup;

    public GameObject artPaintPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CliquerFinish()
    {
        if(isRedColorIn && isYellowColorIn && isGreenColorIn && isCyanColorIn && isBlueColorIn && isMagentaColorIn)
        {
            lineGenerator.image.transform.SetParent(null);
            lineGenerator.image.transform.position = new Vector2(100, 100);
            popup.Interaction();
            SpawnerSpecials.instance.popups = new SpawnerSpecials.TypeDePopup[] { new SpawnerSpecials.TypeDePopup { prefab = artPaintPrefab, apparaitApres = 0f } };
            Debug.Log("Bravo, vous avez trouvé toutes les couleurs !");
        }
        else
        {
            popup.Trembler();
            Debug.Log("Il vous manque encore des couleurs à trouver.");
        }
    }
}

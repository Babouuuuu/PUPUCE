using UnityEngine;

public class MalusVitesse : MonoBehaviour
{
    [Header("la vitesse qui va être MULTIPLIÉ à la vitesse des popups")]
    public float vitesseMultiplié = 1.25f;
    [Header("la vitesse qui va être ajouté au multiplier quand on reprends le malus")]
    public float vitesseAjoutéAuMultiplier = 0.25f;

    void Start()
    {
        if (GameManager.instance.vitesseActif == true)
        {
            GameManager.instance.vitesseMultiplier += vitesseAjoutéAuMultiplier;
            Destroy(gameObject);
        }
        else if (GameManager.instance.vitesseActif == false)
        {
            GameManager.instance.vitesseMultiplier = vitesseMultiplié;
            GameManager.instance.vitesseActif = true;
        }
        
    }
}

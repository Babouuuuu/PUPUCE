using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MalusImmobile : MonoBehaviour
{
    public float tempsImmobile = 5f;
    public float tempsImmoAjouté = 5f;

    public float délai = 8f;
    float countdown;

    void Start()
    {
        countdown = délai;
        
        if (GameManager.instance.immobileActif == true)
        {
            GameManager.instance.tempsImmobilitéJoueur += tempsImmoAjouté;
            Destroy(gameObject);
        }
        else if (GameManager.instance.immobileActif == false)
        {
            GameManager.instance.tempsImmobilitéJoueur += tempsImmobile;
            GameManager.instance.immobileActif = true;
        }
    }

    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0 && GameManager.instance.joueur.peutBouger == true)
        {
            GameManager.instance.joueur.peutBouger = false;
            countdown = GameManager.instance.tempsImmobilitéJoueur;
            Debug.Log("immobile");
            GameManager.instance.joueur.imageJoueur.color = Color.red;
            GameManager.instance.joueur.dossier.color = Color.red;
        }
        else if (countdown <= 0 && GameManager.instance.joueur.peutBouger == false)
        {
            GameManager.instance.joueur.peutBouger = true;
            countdown = délai;
            Debug.Log("peutBouger");
            GameManager.instance.joueur.imageJoueur.color = Color.white;
            GameManager.instance.joueur.dossier.color = Color.white;
        }
    }
}

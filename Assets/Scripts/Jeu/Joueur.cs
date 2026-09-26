using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Le joueur
// La caméra est son enfant dans la scène -> elle le suit donc partout et il reste au centre de l'écran
public class Joueur : MonoBehaviour
{
    public bool peutBouger = true;
    public float vitesse = 3.5f;
    [Tooltip("L'image du dossier")]
    public Image dossier;

    Rigidbody2D rb;
    float finClignotement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // clignote en rouge après avoir été touché
        bool clignote = Time.time < finClignotement && (int)(Time.time * 10) % 2 == 0; // (int) sert a sassurer que la valeur du calcul va sortir en entier
        dossier.color = clignote ? Color.red : Color.white; // pour select la couleur entre rouge et blanc celon ci il clignote
    }

    // FixedUpdate est appelé a chaque pas de la physique -> c'est là qu'on déplace un Rigidbody2D
    void FixedUpdate()
    {
        Vector2 direction = Vector2.zero; // vecteur null par defaut = on ne se déplace pas
        Keyboard clavier = Keyboard.current; // on récupère le clavier

        if (peutBouger && clavier != null)
        {
            // wKey = la touche en haut à gauche des lettres : c'est Z sur un clavier AZERTY (ZQSD)
            if (clavier.wKey.isPressed || clavier.upArrowKey.isPressed) direction.y += 1;
            if (clavier.sKey.isPressed || clavier.downArrowKey.isPressed) direction.y -= 1;
            if (clavier.aKey.isPressed || clavier.leftArrowKey.isPressed) direction.x -= 1;
            if (clavier.dKey.isPressed || clavier.rightArrowKey.isPressed) direction.x += 1;
        }
        rb.linearVelocity = direction.normalized * vitesse; //on normalize pour eviter quon se déplace plus vite en diagonale

        // Empêche de sortir du monde
        Vector2 tailleDuMonde = GameManager.instance.tailleDuMonde;
        //on divide par 2 car le monde est centrée en 0,0 et on enleve la moitier de la largeur du joueur
        float limiteX = tailleDuMonde.x / 2f - 0.75f;
        float limiteY = tailleDuMonde.y / 2f - 0.75f;
        Vector2 position = rb.position;

        // on clamp la poisition du joueur dans les bornes de la map)
        position.x = Mathf.Clamp(position.x, -limiteX, limiteX);
        position.y = Mathf.Clamp(position.y, -limiteY, limiteY);
        rb.position = position;
    }

    // appelé automatiquement par Unity quand un autre collider entre dans notre collider SI IL EST BIEN EN IS TRIGGER !!!!
    void OnTriggerEnter2D(Collider2D autre)
    {
        Popup popup = autre.GetComponent<Popup>(); //equivalent dun Cast sur Unreal Engine
        if (popup == null) return; // ce n'est pas une popup (par exemple le logo DVD)

        popup.Detruire();
        GameManager.instance.PerdreUneVie();
        finClignotement = Time.time + 0.5f; //on set le temps de fin du clignotement a temps actuel + 0.5 (en gros sa va clignoter 0.5s)
    }
}

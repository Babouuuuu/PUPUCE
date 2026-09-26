using UnityEngine;

// le code de leffet de trainee derriere les upgrades /  purement héstétique
// a mettre sur un enfant de l'upgrade : elle se tourne vers l'arrière du déplacement et disparaît quand l'upgrade ne bouge plus
public class Trainee : MonoBehaviour
{
    SpriteRenderer sprite;
    Vector3 anciennePosition;
    float dernierMouvement = -1f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anciennePosition = transform.parent.position;
        sprite.enabled = false;
    }

    // LateUpdate est appelé après tous les Update -> la cible aura donc déja bouger comme ça
    void LateUpdate()
    {
        if (Time.deltaTime <= 0f) return; // jeu en pause donc logique elle bouge pas

        // on calcul le déplacement effectuer pendant la frame
        Vector3 deplacement = transform.parent.position - anciennePosition;
        // puis on stock la nouvelle position pour savoir le déplacement a la prochaine
        anciennePosition = transform.parent.position;

        if (deplacement.magnitude > 0.001f) // si le déplacement est infime on considère que c juste une erreur de précision
        {
            dernierMouvement = Time.time;
            // L'image de la traînée part vers la droite de base -> donc on la tourne vers larriere du deplacement
            transform.right = -deplacement;
        }

        // visible tant que la cible a bougé il y a moins d'un dixième de seconde
        sprite.enabled = dernierMouvement >= 0f && Time.time - dernierMouvement < 0.1f;
    }
}

using UnityEngine;

// Malus "Écran qui lag" : des bandes de l'image partent sur le côté par à-coups, on voit moins bien les popups
// c'est seulement visuel (les clics restent au vrai endroit), le dessin est codé dans Assets/Effets/EffetCRT.shader
// ici on augmente juste la force de l'effet, c'est le GameManager qui l'envoie au shader
public class MalusLag : MonoBehaviour
{
    [Tooltip("Ajouté à la force du lag à chaque fois qu'on prend ce malus")]
    public float force = 1f;

    void Start()
    {
        GameManager.instance.lagHorizontal += force;
    }
}

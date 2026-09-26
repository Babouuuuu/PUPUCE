using UnityEngine;

// le fond d'écran de étiré pour couvrir tout le monde, pour bien voir qu'on se déplace
// avec un dégradé sombre vers les bords, pour savoir si on est au centre ou dans un coin
public class Fond : MonoBehaviour
{
    [Tooltip("Sprite Renderer du fond d'écran")]
    public SpriteRenderer fondEcran;
    [Tooltip("Sprite Renderer du dégradé")]
    public SpriteRenderer degrade;

    void Start()
    {
        Vector2 taille = GameManager.instance.tailleDuMonde;

        // on garde le centre de l'image, avec les mêmes proportions que le monde (l'image n'est pas déformée)
        // Partie un peut chiante et pas intéressante a skip de préférence
        Texture2D image = fondEcran.sprite.texture;
        float largeur = image.width;
        float hauteur = image.height;
        if (largeur / hauteur > taille.x / taille.y) largeur = hauteur * taille.x / taille.y; // trop large : on coupe les côtés
        else hauteur = largeur * taille.y / taille.x;                                          // trop haute : on coupe en haut et en bas
        Rect zone = new Rect((image.width - largeur) / 2f, (image.height - hauteur) / 2f, largeur, hauteur);

        // "Pixels par unité" choisi pour que l'image fasse exactement la hauteur du monde
        fondEcran.sprite = Sprite.Create(image, zone, new Vector2(0.5f, 0.5f), hauteur / taille.y);
        degrade.transform.localScale = new Vector3(taille.x, taille.y, 1f);
    }
}

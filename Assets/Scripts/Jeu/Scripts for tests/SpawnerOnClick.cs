using UnityEngine;

public class SpawnerOnClick : MonoBehaviour
{
    public GameObject prefabPopupToSpawn;
    public GameObject prefabUpgradeToSpawn;
    public int ordreAffichage = 10000; // chaque nouvelle popup s'affiche derrière les précédentes ducoup on laisse de la marge

    public void SpawnPopup()
    {
        Vector2 spawnPosition = PositionHorsEcran();
        FaireApparaitrePopup(prefabPopupToSpawn, spawnPosition);
    }

    public void SpawnUpgrade()
    {
        if (prefabUpgradeToSpawn == null) return;

        FaireApparaitreUpgrade(prefabUpgradeToSpawn);
    }

    private void FaireApparaitrePopup(GameObject prefab, Vector2 position)
    {
        if (prefab == null) return;

        GameObject nouvelle = Instantiate(prefab, position, Quaternion.identity);
        nouvelle.GetComponentInChildren<Canvas>().sortingOrder = ordreAffichage;
        ordreAffichage--;
    }

    void FaireApparaitreUpgrade(GameObject prefab)
    {
        Instantiate(prefab, GameManager.instance.joueur.transform.position, Quaternion.identity);
        GameManager.instance.UpgradeChoisie();
    }

    private Vector2 PositionHorsEcran()
    {
        Camera cam = Camera.main;
        Vector2 centre = cam.transform.position;
        float demiHauteur = cam.orthographicSize;
        float demiLargeur = cam.orthographicSize * cam.aspect;

        // On choisit un côté au hasard
        int cote = Random.Range(0, 4);
        if (cote == 0) return centre + new Vector2(Random.Range(-demiLargeur, demiLargeur), demiHauteur); // haut
        if (cote == 1) return centre + new Vector2(Random.Range(-demiLargeur, demiLargeur), -demiHauteur); // bas
        if (cote == 2) return centre + new Vector2(-demiLargeur, Random.Range(-demiHauteur, demiHauteur)); // gauche
        return centre + new Vector2(demiLargeur, Random.Range(-demiHauteur, demiHauteur)); // droite
    }
}

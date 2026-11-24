using UnityEngine;

public class BlueScreenMaster : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = GameManager.instance.joueur.transform.position;
    }
}

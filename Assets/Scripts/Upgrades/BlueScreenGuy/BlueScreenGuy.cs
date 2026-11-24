using Unity.VisualScripting;
using UnityEngine;

public class BlueScreenGuy : MonoBehaviour
{
    public float rotatingSpeed = 100f;
    public float offsetPlayerX = 3f;
    public float offsetPlayerY = 3f;
    public float dureeBlocage = 0.5f;
    public BlueScreenMaster playerFollower;

    void Start()
    {
        Vector2 joueurPos = playerFollower.gameObject.transform.position;
        this.gameObject.transform.position = joueurPos + new Vector2(offsetPlayerX, offsetPlayerY);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(playerFollower.gameObject.transform.position, Vector3.forward, rotatingSpeed * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D autre)
    {
        Popup popup = autre.GetComponent<Popup>();
        if (popup != null)
        {
            popup.Bloquer(dureeBlocage);
        }
    }
}

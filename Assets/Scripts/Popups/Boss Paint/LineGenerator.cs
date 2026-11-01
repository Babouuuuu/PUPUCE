using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using System.Collections;

public class LineGenerator : MonoBehaviour
{
    [Header("Prefabs lignes")]
    public GameObject linePrefabRed;
    public GameObject linePrefabYellow;
    public GameObject linePrefabGreen;
    public GameObject linePrefabCyan;
    public GameObject linePrefabBlue;
    public GameObject linePrefabMagenta;

    [Header("Toggles couleurs")]
    public Toggle toggleRed;
    public Toggle toggleYellow;
    public Toggle toggleGreen;
    public Toggle toggleCyan;
    public Toggle toggleBlue;
    public Toggle toggleMagenta;
    //public Toggle toggleGomme;

    [Header("Others")]
    public Image image;
    public PaintingArea paintingArea;
    private bool mouseIsPressed;

    void Start()
    {
        mouseIsPressed = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            mouseIsPressed = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            mouseIsPressed = false;
        }

        if(mouseIsPressed == true)
        {
            if(toggleRed.isOn == true)
            {
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject newLine = Instantiate(linePrefabRed, objectPosition, Quaternion.identity);
                newLine.transform.SetParent(image.transform);
            }
            else if(toggleYellow.isOn == true)
            {
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject newLine = Instantiate(linePrefabYellow, objectPosition, Quaternion.identity);
                newLine.transform.SetParent(image.transform);
            }
            else if(toggleGreen.isOn == true)
            {
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject newLine = Instantiate(linePrefabGreen, objectPosition, Quaternion.identity);
                newLine.transform.SetParent(image.transform);
            }
            else if(toggleCyan.isOn == true)
            {
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject newLine = Instantiate(linePrefabCyan, objectPosition, Quaternion.identity);
                newLine.transform.SetParent(image.transform);
            }
            else if(toggleBlue.isOn == true)
            {
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject newLine = Instantiate(linePrefabBlue, objectPosition, Quaternion.identity);
                newLine.transform.SetParent(image.transform);
            }
            else if(toggleMagenta.isOn == true)
            {
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject newLine = Instantiate(linePrefabMagenta, objectPosition, Quaternion.identity);
                newLine.transform.SetParent(image.transform);
            }
            /*else if(toggleGomme.isOn == true)
            {
                Vector2 objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Destroy.linePrefabMagenta.objectPosition.Quaternion.identity;
            }*/
            
            Debug.Log("++ line");
        }

        
    }



}

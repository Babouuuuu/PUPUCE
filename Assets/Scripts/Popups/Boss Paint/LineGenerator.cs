using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefabRed;
    public GameObject linePrefabGreen;
    public GameObject linePrefabBlue;
    public GameObject linePrefabYellow;
    public GameObject linePrefabMagenta;
    public GameObject linePrefabCyan;
    public Image image;
    Line activeLine;

    private void Start()
    {
        ChooseColorRed();
    }
    
    public void ChooseColorRed()
    {
        linePrefabRed.SetActive(true);
        linePrefabGreen.SetActive(false);
        linePrefabBlue.SetActive(false);
        linePrefabYellow.SetActive(false);
        linePrefabMagenta.SetActive(false);
        linePrefabCyan.SetActive(false);
    }

    public void ChooseColorGreen()
    {
        linePrefabRed.SetActive(false);
        linePrefabGreen.SetActive(true);
        linePrefabBlue.SetActive(false);
        linePrefabYellow.SetActive(false);
        linePrefabMagenta.SetActive(false);
        linePrefabCyan.SetActive(false);
    }

    public void ChooseColorBlue()
    {
        linePrefabRed.SetActive(false);
        linePrefabGreen.SetActive(false);
        linePrefabBlue.SetActive(true);
        linePrefabYellow.SetActive(false);
        linePrefabMagenta.SetActive(false);
        linePrefabCyan.SetActive(false);
    }

    public void ChooseColorYellow()
    {
        linePrefabRed.SetActive(false);
        linePrefabGreen.SetActive(false);
        linePrefabBlue.SetActive(false);
        linePrefabYellow.SetActive(true);
        linePrefabMagenta.SetActive(false);
        linePrefabCyan.SetActive(false);
    }

    public void ChooseColorMagenta()
    {
        linePrefabRed.SetActive(false);
        linePrefabGreen.SetActive(false);
        linePrefabBlue.SetActive(false);
        linePrefabYellow.SetActive(false);
        linePrefabMagenta.SetActive(true);
        linePrefabCyan.SetActive(false);
    }

    public void ChooseColorCyan()
    {
        linePrefabRed.SetActive(false);
        linePrefabGreen.SetActive(false);
        linePrefabBlue.SetActive(false);
        linePrefabYellow.SetActive(false);
        linePrefabMagenta.SetActive(false);
        linePrefabCyan.SetActive(true);
    }



    void Update()
    {
        if (linePrefabRed == true)
        {
            if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == true)
            {
                GameObject newLine = Instantiate(linePrefabRed);
                activeLine = newLine.GetComponent<Line>();
                newLine.transform.SetParent(image.transform);
            }

            if(Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if(activeLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePosition);
            }
        }

        if (linePrefabGreen == true)
        {
            if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == true)
            {
                GameObject newLine = Instantiate(linePrefabGreen);
                activeLine = newLine.GetComponent<Line>();
                newLine.transform.SetParent(image.transform);
            }

            if(Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if(activeLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePosition);
            }
        }

        if (linePrefabBlue == true)
        {
            if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == true)
            {
                GameObject newLine = Instantiate(linePrefabBlue);
                activeLine = newLine.GetComponent<Line>();
                newLine.transform.SetParent(image.transform);
            }

            if(Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if(activeLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePosition);
            }
        }

        if (linePrefabYellow == true)
        {
            if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == true)
            {
                GameObject newLine = Instantiate(linePrefabYellow);
                activeLine = newLine.GetComponent<Line>();
                newLine.transform.SetParent(image.transform);
            }

            if(Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if(activeLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePosition);
            }
        }

        if (linePrefabMagenta == true)
        {
            if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == true)
            {
                GameObject newLine = Instantiate(linePrefabMagenta);
                activeLine = newLine.GetComponent<Line>();
                newLine.transform.SetParent(image.transform);
            }

            if(Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if(activeLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePosition);
            }
        }

        if (linePrefabCyan == true)
        {
            if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == true)
            {
                GameObject newLine = Instantiate(linePrefabCyan);
                activeLine = newLine.GetComponent<Line>();
                newLine.transform.SetParent(image.transform);
            }

            if(Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if(activeLine != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousePosition);
            }
        }
    }


}

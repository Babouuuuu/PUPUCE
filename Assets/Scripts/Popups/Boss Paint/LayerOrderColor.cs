
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;

public class LayerOrderColor : MonoBehaviour
{
    [Header("Renderers des Prefabs")]
    public Renderer rendererRed;
    public Renderer rendererYellow;
    public Renderer rendererGreen;
    public Renderer rendererCyan;
    public Renderer rendererBlue;
    public Renderer rendererMagenta;
    private int orderInLayer = 1;


    public void Start()
    {
        rendererRed.sortingOrder = rendererRed.sortingOrder = 2;
        rendererYellow.sortingOrder = rendererYellow.sortingOrder = 2;
        rendererGreen.sortingOrder = rendererGreen.sortingOrder = 2;
        rendererCyan.sortingOrder = rendererCyan.sortingOrder = 2;
        rendererBlue.sortingOrder = rendererBlue.sortingOrder = 2;
        rendererMagenta.sortingOrder = rendererMagenta.sortingOrder = 2; 
    }

    public void ChooseRed()
    {
        orderInLayer = orderInLayer + 1;
        rendererRed.sortingOrder = rendererRed.sortingOrder + orderInLayer;
        rendererYellow.sortingOrder = rendererYellow.sortingOrder = 2;
        rendererGreen.sortingOrder = rendererGreen.sortingOrder = 2;
        rendererCyan.sortingOrder = rendererCyan.sortingOrder = 2;
        rendererBlue.sortingOrder = rendererBlue.sortingOrder = 2;
        rendererMagenta.sortingOrder = rendererMagenta.sortingOrder = 2;
    }

    public void ChooseYellow()
    {
        orderInLayer = orderInLayer + 1;
        rendererRed.sortingOrder = rendererRed.sortingOrder = 2;
        rendererYellow.sortingOrder = rendererYellow.sortingOrder + orderInLayer;
        rendererGreen.sortingOrder = rendererGreen.sortingOrder = 2;
        rendererCyan.sortingOrder = rendererCyan.sortingOrder = 2;
        rendererBlue.sortingOrder = rendererBlue.sortingOrder = 2;
        rendererMagenta.sortingOrder = rendererMagenta.sortingOrder = 2;
    }

    public void ChooseGreen()
    {
        orderInLayer = orderInLayer + 1;
        rendererRed.sortingOrder = rendererRed.sortingOrder = 2;
        rendererYellow.sortingOrder = rendererYellow.sortingOrder = 2;
        rendererGreen.sortingOrder = rendererGreen.sortingOrder + orderInLayer;
        rendererCyan.sortingOrder = rendererCyan.sortingOrder = 2;
        rendererBlue.sortingOrder = rendererBlue.sortingOrder = 2;
        rendererMagenta.sortingOrder = rendererMagenta.sortingOrder = 2;
    }

    public void ChooseCyan()
    {
        orderInLayer = orderInLayer + 1;
        rendererRed.sortingOrder = rendererRed.sortingOrder = 2;
        rendererYellow.sortingOrder = rendererYellow.sortingOrder = 2;
        rendererGreen.sortingOrder = rendererGreen.sortingOrder = 2;
        rendererCyan.sortingOrder = rendererCyan.sortingOrder + orderInLayer;
        rendererBlue.sortingOrder = rendererBlue.sortingOrder = 2;
        rendererMagenta.sortingOrder = rendererMagenta.sortingOrder = 2;
    }

    public void ChooseBlue()
    {
        orderInLayer = orderInLayer + 1;
        rendererRed.sortingOrder = rendererRed.sortingOrder = 2;
        rendererYellow.sortingOrder = rendererYellow.sortingOrder = 2;
        rendererGreen.sortingOrder = rendererGreen.sortingOrder = 2;
        rendererCyan.sortingOrder = rendererCyan.sortingOrder = 2;
        rendererBlue.sortingOrder = rendererBlue.sortingOrder + orderInLayer;
        rendererMagenta.sortingOrder = rendererMagenta.sortingOrder = 2;
    }

    public void ChooseMagenta()
    {
        orderInLayer = orderInLayer + 1;
        rendererRed.sortingOrder = rendererRed.sortingOrder = 2;
        rendererYellow.sortingOrder = rendererYellow.sortingOrder = 2;
        rendererGreen.sortingOrder = rendererGreen.sortingOrder = 2;
        rendererCyan.sortingOrder = rendererCyan.sortingOrder = 2;
        rendererBlue.sortingOrder = rendererBlue.sortingOrder = 2;
        rendererMagenta.sortingOrder = rendererMagenta.sortingOrder + orderInLayer;
    }
}

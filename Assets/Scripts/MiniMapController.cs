using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    private GameObject MinimapCanvas;
    private void Start()
    {
       MinimapCanvas = GameObject.Find("CanvasMinimap");
    }
    public void MiniMapHide()
    {
        MinimapCanvas.SetActive(false);
    }

    public void MiniMapShow()
    {
        MinimapCanvas.SetActive(true);
    }
}

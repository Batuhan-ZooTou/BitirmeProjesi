using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using System;

namespace _Game.Scripts.Managers
{
    public class CanvasManager : Singleton<CanvasManager>
    {
        [SerializeField] private GameObject travelMapCanvas;
        public static Action OnCanvasOpened;
        public static Action OnCanvasClosed;
        private void OnEnable()
        {
            OnCanvasOpened += OpenTravelCanvas;
            OnCanvasClosed += CloseTravelCanvas;

        }
        private void OnDisable()
        {
            OnCanvasOpened -= OpenTravelCanvas;
            OnCanvasClosed -= CloseTravelCanvas;

        }
        public void OpenTravelCanvas()
        {
            travelMapCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        public void CloseTravelCanvas()
        {
            travelMapCanvas.SetActive(false);
        }
    }
}
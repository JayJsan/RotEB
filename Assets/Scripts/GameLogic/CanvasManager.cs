using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Code taken and modified from https://youtu.be/vmKxLibGrMo
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }
    private List<CanvasController> m_canvasControllers;
    private CanvasController m_lastActiveCanvas;

    private void Awake() {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        DontDestroyOnLoad(gameObject);

        // Find Canavs Controllers
        m_canvasControllers = GetComponentsInChildren<CanvasController>().ToList();
        m_canvasControllers.ForEach(x => x.gameObject.SetActive(false));
        SwitchCanvas(CanvasType.MainMenu);
    }

    public void SwitchCanvas(CanvasType type) {
        if (m_lastActiveCanvas != null) {
            m_lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = m_canvasControllers.Find(x => x.canvasType == type);

        if (desiredCanvas != null) {
            desiredCanvas.gameObject.SetActive(true);
            m_lastActiveCanvas = desiredCanvas;
        } else { Debug.LogWarning("The " + type +" canvas was not found!");}

    }
}

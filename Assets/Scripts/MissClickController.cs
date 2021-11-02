using Events;
using UnityEngine;

public class MissClickController : MonoBehaviour
{

    private Camera _mainCamera;
    
    #region MonoBehaviour CallBacks

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out var hit))
        {
            if (!hit.collider.CompareTag("Connector"))
                MissClick();
            return;
        }
        
        MissClick();
    }
    
    #endregion
    
    #region Private Methods

    private static void MissClick()
    {
        EventsManager.ConnectorClick.Publish(null);
    }
    
    #endregion
    
}

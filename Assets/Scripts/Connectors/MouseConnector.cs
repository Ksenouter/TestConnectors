using Events;
using UnityEngine;

namespace Connectors
{
    public class MouseConnector : MonoBehaviour
    {
    
        private Camera _mainCamera;
        private Connector _parentConnector;
    
        private Collider _lastHitCollider;
        private Connector _lastHitConnector;
    
        #region MonoBehaviour CallBacks
    
        private void Awake()
        {
            _mainCamera = Camera.main;
        }
    
        private void Update()
        {
            GetInput();
            CheckScreenRaycast();
        }
    
        #endregion
    
        #region Private Methods

        private void GetInput()
        {
            if (!Input.GetMouseButtonUp(0)) return;

            EventsManager.ConnectorClick.Publish(null);
            if (_lastHitConnector != null)
                _parentConnector.CreateConnect(_lastHitConnector);
            
            Destroy(gameObject);
        }

        private void CheckScreenRaycast()
        {
            var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(cameraRay, out var hit))
            {
                if (_lastHitCollider == hit.collider) return;
                _lastHitCollider = hit.collider;

                ClearLastHitConnector();
            
                if (_lastHitCollider.CompareTag("Connector"))
                {
                    _lastHitConnector = _lastHitCollider.GetComponent<Connector>();
                    _lastHitConnector.SetState(Connector.State.Active);
                }
            }
            else
            {
                _lastHitCollider = null;
                ClearLastHitConnector();
            }
        }

        private void ClearLastHitConnector()
        {
            if (_lastHitConnector == null) return;
            
            _lastHitConnector.SetState(Connector.State.Idle);
            _lastHitConnector = null;
        }
    
        #endregion
    
        #region Interface
    
        public void SetParentConnector(Connector connector)
        {
            _parentConnector = connector;
        }
    
        #endregion
    
    }
}

using UnityEngine;

namespace Mouse
{
    public class MouseRelocatable : MonoBehaviour
    {
    
        [Tooltip("If this field is null - movable object set is current transform component")]
        [SerializeField] private Transform movableObject;
    
        private MouseInWorldSpace _mouseInWorld;
    
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            movableObject = movableObject ? movableObject : transform;
            _mouseInWorld = new MouseInWorldSpace(movableObject);
        }
    
        void OnMouseDown()
        {
            _mouseInWorld.GetMouseOnPositionInWorldSpace();
        }
    
        void OnMouseDrag()
        {
            var lastTouchPosition = _mouseInWorld.LastTouchPosition;
            var touchPosition = _mouseInWorld.GetMouseOnPositionInWorldSpace();
            movableObject.Translate(touchPosition - lastTouchPosition);
        }
    
        #endregion
    
    }
}

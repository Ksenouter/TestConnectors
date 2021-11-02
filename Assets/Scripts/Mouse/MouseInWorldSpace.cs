using UnityEngine;

namespace Mouse
{
    public class MouseInWorldSpace
    {
    
        public Vector3 LastTouchPosition;
    
        private readonly Camera _mainCamera;
        private readonly Transform _target;
    
        #region Interface

        public MouseInWorldSpace(Transform target)
        {
            LastTouchPosition = Vector3.zero;
        
            _mainCamera = Camera.main;
            _target = target;
        }
    
        public Vector3 GetMouseOnPositionInWorldSpace()
        {
            var dragPlane = new Plane(_target.up, _target.position);
            var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
            var position = dragPlane.Raycast(cameraRay, out var enter)
                ? cameraRay.GetPoint(enter)
                : LastTouchPosition;
        
            LastTouchPosition = position;
            return LastTouchPosition;
        }
    
        #endregion
    
    }
}

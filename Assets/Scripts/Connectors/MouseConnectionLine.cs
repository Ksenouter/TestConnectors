using Mouse;
using UnityEngine;

namespace Connectors
{
    public class MouseConnectionLine : ConnectionLine
    {
    
        private MouseInWorldSpace _mouseInWorld;
    
        #region Private Methods
    
        private void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
            _mouseInWorld = new MouseInWorldSpace(transform);
        }
    
        #endregion
    
        #region Public Methods
    
        public override void UpdateLineRendererPositions()
        {
            base.UpdateLineRendererPositions();
        
            if (Targets.Length < 1) return;
            var touchPosition = _mouseInWorld.GetMouseOnPositionInWorldSpace();
            LineRenderer.SetPosition(Targets.Length, touchPosition);
        }
    
        #endregion
    
        #region Interface
    
        public override void SetTargets(Transform[] targets)
        {
            base.SetTargets(targets);
            LineRenderer.positionCount++;
        }
    
        #endregion
    
    }
}

using UnityEngine;

namespace Connectors
{
    [RequireComponent(typeof(LineRenderer))]
    public class ConnectionLine : MonoBehaviour
    {
    
        protected LineRenderer LineRenderer;
        protected Transform[] Targets;
    
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            UpdateLineRendererPositions();
        }

        #endregion
    
        #region Public Methods

        public virtual void UpdateLineRendererPositions()
        {
            for (var i = 0; i < Targets.Length; i++)
                LineRenderer.SetPosition(i, Targets[i].position);
        }
    
        #endregion
    
        #region Interface

        public virtual void SetTargets(Transform[] targets)
        {
            Targets = targets;
            LineRenderer.positionCount = Targets.Length;
        }
    
        #endregion
    
    }
}

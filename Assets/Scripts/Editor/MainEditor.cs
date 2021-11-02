using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Main))]
    public class MainEditor : UnityEditor.Editor
    {
        
        #region Editor CallBacks
        
        private void OnSceneGUI()
        {
            var targetMain = target as Main;
            if (targetMain == null) return;
            
            var mainTransform = targetMain.transform;
            
            Handles.color = Color.green;
            Handles.DrawWireDisc(mainTransform.position, mainTransform.up, targetMain.radius);
        }
        
        #endregion
        
    }
}

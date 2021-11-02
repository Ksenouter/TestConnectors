using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    
    [SerializeField] private GameObject prefab;
    [SerializeField] private int prefabsCount = 10;
    
    public float radius = 10;
    
    #region MonoBehaviour CallBacks
    
    private void Start()
    {
        for (var i = 0; i < prefabsCount; i++)
            CreatePrefab();
    }
    
    #endregion
    
    #region Private Methods
    
    private void CreatePrefab()
    {
        var circlePosition = Random.insideUnitCircle * radius;
        var prefabPosition = new Vector3(circlePosition.x, 0, circlePosition.y);
        
        Instantiate(prefab, prefabPosition, Quaternion.identity, transform);
    }
    
    #endregion
    
}

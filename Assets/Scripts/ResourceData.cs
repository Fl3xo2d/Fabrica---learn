using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = "Resource")]
public class ResourceData : ScriptableObject
{
    public int id;
    public string resourceName;
    public GameObject prefab; // <- сюда привязывается префаб ресурса
}

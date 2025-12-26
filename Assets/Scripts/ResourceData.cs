using UnityEngine;

[CreateAssetMenu(fileName = "NewResource", menuName = "Resources/ResourceData")]
public class ResourceData : ScriptableObject
{
    public string resourceName;     // Имя ресурса
    public Sprite icon;             // Иконка для UI
    public int value = 1;           // Сколько даёт игроку
    public GameObject prefab;       // Префаб, который спавнится
}

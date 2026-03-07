using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemtName;
    [SerializeField] private string itemDescription;

}

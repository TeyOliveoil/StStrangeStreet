using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [SerializeField] public Sprite itemSprite;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;

}

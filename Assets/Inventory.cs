using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(InventoryItem newItem)
    {
        GameObject itemObject = new GameObject();

        itemObject.AddComponent<SpriteRenderer>();
        itemObject.GetComponent<SpriteRenderer>().sprite = newItem.itemSprite;

        itemObject.AddComponent<PolygonCollider2D>();
        itemObject.AddComponent<Rigidbody2D>();
        itemObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        Instantiate(itemObject);
    }
}

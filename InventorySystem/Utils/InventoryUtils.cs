using UnityEngine;
namespace Utils
{
    public static class InventoryUtils 
    {
        public static float chestDetectionRadius = 2.0f; 

        public static Chest Find(Transform transform)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, chestDetectionRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                Chest chest = hitCollider.GetComponent<Chest>();
                if (chest != null)
                {
                    return chest; 
                }
            }
            return null; 
        }

        public static bool HasEnoughItems(Inventory inventory, InventoryItem requiredItem)
        {
            if (requiredItem == null) return false;

            int totalCount = 0;

            foreach (var item in inventory.inventory)
            {
                if (item.ItemName == requiredItem.ItemName)
                {
                    totalCount += item.Count;
                    if (totalCount >= requiredItem.Count) return true;
                }
            }
            return false;
        }
    }
}

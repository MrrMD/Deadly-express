using UnityEngine;

public class TrainEngine : MonoBehaviour
{
    //[SerializeField] private Inventory inventory;
    //[SerializeField] private float oneCoalTimeToGo = 45f;
    //[SerializeField] private bool isTurnOn;
    //[SerializeField] private Coal currentCoal;
    //[SerializeField] private int currentCoalCount => currentCoal != null ? currentCoal.Count : 0;

    //private float coalUsageTimer;

    //private void Start()
    //{
    //    inventory = GetComponent<Inventory>();
    //    isTurnOn = false;
    //}

    //private void TurnOn()
    //{
    //    currentCoal = inventory.GetAndRemoveItemByType<Coal>();
    //    if (currentCoal != null) {
    //        isTurnOn = true;
    //    }
    //}

    //private void TurnOnEngine()
    //{
    //    if (currentCoal != null && currentCoalCount > 0)
    //    {
    //        //if (inventory.FinditemByType<Bum>() != null){
    //        //    Bum();
    //        //}

    //        coalUsageTimer += Time.deltaTime;
    //        if (coalUsageTimer >= oneCoalTimeToGo)
    //        {
    //            currentCoal.DecreaseStack(1);
    //            coalUsageTimer = oneCoalTimeToGo;

    //            if (currentCoal.Count <= 0)
    //            {
    //                currentCoal = inventory.GetAndRemoveItemByType<Coal>();

    //                if (currentCoal == null)
    //                {
    //                    isTurnOn = false;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        isTurnOn = false;
    //    }
    //}

    //private void Update()
    //{
    //    if (isTurnOn)
    //    {
    //        TurnOnEngine();
    //    }
    //}

    //private void Bum()
    //{
    //    currentCoal = null;
    //    isTurnOn = false;
    //}
}

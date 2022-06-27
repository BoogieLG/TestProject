using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    public Resources TypeOfCraftedResource => typeOfCraftedResource;
    [SerializeField] protected float timeNeededToCraft;
    [SerializeField] protected float timerToCraft;
    [SerializeField] protected Resources typeOfCraftedResource;
    [SerializeField] protected Warehouse materialsWarehouse;
    [SerializeField] protected Warehouse readyProductionWarehouse;
    [SerializeField] protected Text factoryInfo;
    [SerializeField] protected Image tube1;  
    [SerializeField] protected Image tube2;
    protected void Update()
    {
        if (!checkForCraft())
        {
            tube1.fillAmount = 0f;
            tube2.fillAmount = 0f;
            timerToCraft = timeNeededToCraft;
            return;
        }
        if (timerToCraft <= 0)
        {
            craft();
            return;
        }
        timerToCraft -= Time.deltaTime;
        tube1.fillAmount = (timeNeededToCraft - timerToCraft) / timeNeededToCraft;
        tube2.fillAmount = (timeNeededToCraft - timerToCraft) / timeNeededToCraft;
    }
    protected virtual bool checkForCraft()
    {
        if (readyProductionWarehouse.CheckIfFull())
        {
            factoryInfo.text = "Factory A stopped! \nNot enough space in warehouse!";
            return false;
        }
        return true;
    }
    protected virtual void craft()
    {

        readyProductionWarehouse.AddResource(typeOfCraftedResource);
        timerToCraft = timeNeededToCraft;
        tube1.fillAmount = 0f;
        tube2.fillAmount = 0f;
        factoryInfo.text = "Factory A is working!";
    }
}

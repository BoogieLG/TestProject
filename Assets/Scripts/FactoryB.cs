using UnityEngine;

public class FactoryB : Factory
{
    [SerializeField] Resources typeOfMaterialResource;

    protected override bool checkForCraft()
    {
        if (!materialsWarehouse.CheckIfEnough(typeOfMaterialResource))
        {
            factoryInfo.text = "Factory B stopped! \nNot enough material TypeA in warehouse!";
            return false;
        }
        if (readyProductionWarehouse.CheckIfFull())
        {
            factoryInfo.text = "Factory B stopped! \nNot enough space in warehouse!";
            return false;
        }
        return true;
    }
    protected override void craft()
    {
        if (checkForCraft())
        {
            materialsWarehouse.RemoveResource(typeOfMaterialResource);
            readyProductionWarehouse.AddResource(typeOfCraftedResource);
            timerToCraft = timeNeededToCraft;
            factoryInfo.text = "Factory B is working!";

        }
    }
}

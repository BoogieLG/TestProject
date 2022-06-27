using UnityEngine;

public class FactoryC : Factory
{
    [SerializeField] Resources typeOfMaterialResource;
    [SerializeField] Resources typeOfSecondMaterialResource;

    protected override bool checkForCraft()
    {
        if (!materialsWarehouse.CheckIfEnough(typeOfMaterialResource))
        {
            factoryInfo.text = "Factory C stopped! \nNot enough material TypeA in warehouse!";
            return false;
        }
        if (!materialsWarehouse.CheckIfEnough(typeOfSecondMaterialResource))
        {
            factoryInfo.text = "Factory C stopped! \nNot enough material TypeB in warehouse!";
            return false;
        }
        if (readyProductionWarehouse.CheckIfFull())
        {
            factoryInfo.text = "Factory C stopped! \nNot enough space in warehouse!";
            return false;
        }
        return true;
    }
    protected override void craft()
    {
        if (checkForCraft())
        {
            materialsWarehouse.RemoveResource(typeOfMaterialResource);
            materialsWarehouse.RemoveResource(typeOfSecondMaterialResource);
            readyProductionWarehouse.AddResource(typeOfCraftedResource);
            timerToCraft = timeNeededToCraft;
            factoryInfo.text = "Factory C is working!";
        }
    }
}

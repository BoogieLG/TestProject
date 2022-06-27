using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warehouse : MonoBehaviour
{
    public Image arrow;

    [SerializeField] private int maxAmountOfResources;
    [SerializeField] private int currentAmountOfResources;
    [SerializeField] private int typeA;
    [SerializeField] private int typeB;
    [SerializeField] private int typeC;
    [SerializeField] private Factory factory;
    [SerializeField] private bool isMaterialWarehouse;
    [SerializeField] private Resources[] neededResources;
    private Resources craftedResource;

    public bool CheckIfFull()
    {
        if (currentAmountOfResources >= maxAmountOfResources)
        {
            return true;
        }
        return false;
    }
    public virtual bool CheckIfEnough(Resources type)
    {
        if (Resources.TypeA == type && typeA > 0) return true;
        else if (Resources.TypeB == type && typeB > 0) return true;
        else if (Resources.TypeC == type && typeC > 0) return true;
        else return false;
    }
    public void AddResource(Resources type)
    {
        switch (type)
        {
            case Resources.TypeA:
                typeA ++;
                currentAmountOfResources++;
                break;
            case Resources.TypeB:
                typeB ++;
                currentAmountOfResources++;
                break;
            case Resources.TypeC:
                typeC ++;
                currentAmountOfResources++;
                break;
            default:
                Debug.LogError("Wrong type sended");
                break;
        }
    }
    public void RemoveResource(Resources type)
    {
        switch (type)
        {
            case Resources.TypeA:
                typeA--;
                currentAmountOfResources--;
                break;
            case Resources.TypeB:
                typeB--;
                currentAmountOfResources--;
                break;
            case Resources.TypeC:
                typeC--;
                currentAmountOfResources--;
                break;
            default:
                Debug.LogError("Wrong type sended");
                break;
        }
    }
    private void Start()
    {
        craftedResource = factory.GetComponent<Factory>().TypeOfCraftedResource;
    }
    private void OnTriggerStay(Collider other)
    {
        if (isMaterialWarehouse)
        {
            if (other.TryGetComponent<Backpack>(out Backpack backpack))
            {
                backpack.UnloadBackpack(neededResources, this);
            }
            return;
        }
        else if (other.TryGetComponent<Backpack>(out Backpack backpack))
        {
            if (!CheckIfEnough(craftedResource)) return;
            backpack.LoadToBackpack(craftedResource,this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Backpack>(out Backpack backpack))
        {
            backpack.BreakToLoad();
            arrow.fillAmount = 0f;
        }
    }
}

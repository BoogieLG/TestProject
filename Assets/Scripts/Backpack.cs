using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] float timeToLoad;
    [SerializeField] bool loading;
    [SerializeField] Warehouse warehouse;
    [SerializeField] private List<Container> backpackSlots;
    [SerializeField] private List<Material> materials;

    public void BreakToLoad()
    {
        StopAllCoroutines();
        loading = false;
        warehouse = null;
    }
    public void LoadToBackpack(Resources type, Warehouse warehouse)
    {
        if (!CheckIfHaveSpace())
        {
            return;
        }
        if (loading) return;
        loading = true;
        StartCoroutine(startLoading(type, warehouse));

        this.warehouse = warehouse;
    }
    public void UnloadBackpack(Resources[] neededResources, Warehouse warehouse)
    {
        if (loading) return;
        Resources tempRes = CheckWhatWeCanGive(neededResources);
        if ( tempRes == Resources.Null) return;
        loading = true;
        StartCoroutine(startUnloading(tempRes, warehouse));
        this.warehouse = warehouse;
    }
    private Material chooseMaterial(Resources type)
    {
        Material material;
        switch (type)
        {
            case Resources.Null:
                material = materials[0];
                break;
            case Resources.TypeA:
                material = materials[1];
                break;
            case Resources.TypeB:
                material = materials[2];
                break;
            case Resources.TypeC:
                material = materials[3];
                break;
            default:
                Debug.LogError("Choosed wrong type for materials in Backpack method chooseMaterial");
                material = null;
                break;
                
        }
        return material;
    }
    private bool CheckIfHaveSpace()
    {
        if (backpackSlots.Exists(x => x.isEmpty == true)) return true;
        return false;
    }
    private Resources CheckWhatWeCanGive(Resources[] resources)
    {
        if (resources == null) return Resources.Null;
        foreach (Resources res in resources)
        {
            Container temp = backpackSlots.FindLast(x => x.resource == res);
            if (temp != null) return temp.resource;

        }
        return Resources.Null;
    }
    IEnumerator startLoading(Resources type, Warehouse warehouse)
    {
        float countdown = timeToLoad;
        while (countdown > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            countdown -= Time.deltaTime;
            warehouse.arrow.fillAmount = (timeToLoad - countdown) / countdown;
        }
        loading = false;
        Container temp = backpackSlots.Find(x => x.isEmpty == true);
        temp.isEmpty = false;
        temp.resource = type;
        temp.GetComponent<MeshRenderer>().material = chooseMaterial(type);
        warehouse.RemoveResource(type);
        warehouse.arrow.fillAmount = 0f;

    }
    IEnumerator startUnloading(Resources type, Warehouse warehouse)
    {
        float countdown = timeToLoad;
        while (countdown > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            countdown -= Time.deltaTime;
            warehouse.arrow.fillAmount = (timeToLoad - countdown) / countdown;
        }
        loading = false;
        Container temp = backpackSlots.FindLast(x => x.resource == type);
        temp.isEmpty = true;
        temp.resource = Resources.Null;
        temp.GetComponent<MeshRenderer>().material = chooseMaterial(Resources.Null);
        warehouse.AddResource(type);
        warehouse.arrow.fillAmount = 0f;
    }

}


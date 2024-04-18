using UnityEngine;

public class BuildingManagerUI : MonoBehaviour
{
    public GameObject buildingItem;
    public Transform[] categories;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup();
    }

    private void Setup()
    {

        foreach (var category in categories)
        {
            foreach (Transform child in category)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (var category in categories)
        {
            category.gameObject.SetActive(false);
        }
        categories[0].gameObject.SetActive(true);

        int i = 0;
        foreach (GameObject bo in BuildingManager.instance.buildingPrefabs)
        {
            GameObject go = Instantiate(buildingItem, categories[(int)bo.GetComponent<BuildingObject>().buildingType]);
            go.GetComponent<BuildingObjectUIElement>().Setup(bo.GetComponent<BuildingObject>(), i);
            i++;
        }
    }

    public void SetCategory(int i)
    {
        foreach (var category in categories)
        {
            category.gameObject.SetActive(false);
        }
        categories[i].gameObject.SetActive(true);
    }
}

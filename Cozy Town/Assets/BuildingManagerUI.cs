using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

public class BuildingManagerUI : SerializedMonoBehaviour
{
    public GameObject buildingItem;
    public GameObject buildingDrawer;
    public TextMeshProUGUI buildingDrawerTitle;
    [DictionaryDrawerSettings(KeyLabel = "Index", ValueLabel = "Categories")]
    public Dictionary<int, CategoryInformation> categories;
    public RectTransform scrollRectContent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Setup();
    }

    private void Setup()
    {
        foreach (var category in categories)
        {
            foreach (Transform child in category.Value.transform)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (var category in categories.Values)
        {
            category.transform.gameObject.SetActive(false);
        }
        categories[0].transform.gameObject.SetActive(true);

        int i = 0;
        foreach (GameObject bo in BuildingManager.instance.buildingPrefabs)
        {
            GameObject go = Instantiate(buildingItem, categories[(int)bo.GetComponent<BuildingObject>().buildingType].transform);
            go.GetComponent<BuildingObjectUIElement>().Setup(bo.GetComponent<BuildingObject>(), i);
            i++;
        }
    }

    public void SetCategory(int i)
    {
        foreach (var category in categories.Values)
        {
            category.transform.gameObject.SetActive(false);
        }
        categories[i].transform.gameObject.SetActive(true);
        buildingDrawerTitle.text = categories[i].name;
        // Set scrollrect height to category height
        scrollRectContent.sizeDelta = new Vector2(scrollRectContent.sizeDelta.x, categories[i].transform.GetComponent<RectTransform>().sizeDelta.y);
        buildingDrawer.SetActive(true);
    }

    public void CloseDrawer()
    {
        buildingDrawer.SetActive(false);
        BuildingManager.instance.PlaceBuildingMode = false;
    }
}

[InlineProperty(LabelWidth = 90)]
public struct CategoryInformation
{
    public string name;
    public Transform transform;
}

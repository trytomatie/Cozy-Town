using TMPro;
using UnityEngine;

public class DescriptorUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    RectTransform rectTransform;

    public static DescriptorUI instance;

    private void Awake()
    {
        if(instance == null) instance = this;
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetDescriptor(string message)
    {
        gameObject.SetActive(true);
        text.text = message;
        rectTransform.sizeDelta = new Vector2(text.preferredWidth+50, text.preferredHeight+20);
    }

    public void HideDescriptor()
    {
        text.text = "";
        gameObject.SetActive(false);
    }
}

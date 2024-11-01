using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] //Aparece no inspector
    private Transform content;
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private TMP_InputField inputField;
    private ItemData[] itemsData;


    // Start is called before the first frame update
    void Start()
    {
        itemsData = Resources.LoadAll<ItemData>("Items"); //Vai na pasta Resources, pega tudo que tem e carrega todos os itens
        BubbleSort();

        if(itemsData.Length > 0)
        {
            for ( int i = 0; i < itemsData.Length; i++)
            {
                Debug.Log(itemsData[i]);
                CreateUIItem(itemsData[i]);
            }
        }
    }

    public void Search()
    {
        string value = inputField.text;
        int index = BinarySearchByName(value);
        if(index <= 0)
        {
            ClearAll();
            CreateUIItem(itemsData[index]);
        } else
        {
            
        }
    } 

    //Usa o prefab para exibir as informções na tela 
    private void CreateUIItem(ItemData data)
    {
        GameObject uiItem = Instantiate(itemPrefab, content); //Instancia os itens

        TextMeshProUGUI nameText = uiItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText.text = data.itemName;
        TextMeshProUGUI priceText = uiItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        priceText.text = data.price.ToString("0.00");
        TextMeshProUGUI descriptionText = uiItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        descriptionText.text = data.description;
    }

    private void ClearAll()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    private void BubbleSort()
    {
        for (int i = itemsData.Length - 1; i > 0; i--)
        {
            bool stop = true;
            for (int j = 0; j < i; j++)
            {
                if (itemsData[j].itemName.CompareTo(itemsData[j + 1].itemName) > 0)
                {
                    stop = false;
                    ItemData temp = itemsData[j];
                    itemsData[j] = itemsData[j + 1];
                    itemsData[j + 1] = temp;
                }
            }
            if (stop)
                break;
        }
    }

    private int BinarySearchByName(string value)
    {

        int start = 0;
        int end = itemsData.Length - 1;

        while (start <= end)
        {
            int middle = (start + end) / 2;
            if (value == itemsData[middle].itemName)
            {
                return middle;
            }

            if (value.CompareTo(itemsData[middle].itemName) > 0)
            {
                start = middle + 1;
            }
            else if (value.CompareTo(itemsData[middle].itemName) < 0)
            {
                end = middle - 1;
            }
        }

        return -1;
    }
}

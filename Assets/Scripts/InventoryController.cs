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
        ClearAll();

        if(itemsData.Length > 0)
        {
            for ( int i = 0; i < itemsData.Length; i++)
            {
                Debug.Log(itemsData[i]);
                CreateUIItem(itemsData[i]);
            }
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

    //Método de organização dos itens
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

    //Método de busca binária
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

    //Método de busca sequencial
    private List<ItemData> SequencialSearchByName(string value)
    {

        List<ItemData> result = new List<ItemData>();
        string upperValue = value.ToUpper();

        for (int i = 0; i < itemsData.Length; i++)
        {
            string upperSource = itemsData[i].itemName.ToUpper();
            if (upperValue[0].CompareTo(upperSource[0]) > 0)
                continue;

            if (upperValue[0].CompareTo(upperSource[0]) < 0)
                break;


            if (upperSource.Substring(0, upperValue.Length) == upperValue)
                result.Add(itemsData[i]);
        }

        return result;
    }

    //Método de busca sensitiva
    public void SensitiveSearch(string value)
    {
        ClearAll();
        if(string.IsNullOrEmpty(value))
        {
            for (int i = 0; i < itemsData.Length; i++)
            {
                CreateUIItem(itemsData[i]);
            }

            return;
        }

        List<ItemData> result = SequencialSearchByName(value);

        for (int i = 0; i < result.Count; i++)
            CreateUIItem(result[i]);
    }
}

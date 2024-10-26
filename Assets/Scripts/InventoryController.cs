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
    private ItemData[] items;


    // Start is called before the first frame update
    void Start()
    {
        items = Resources.LoadAll<ItemData>("Itens"); //Vai na pasta Resources, pega tudo que tem e carrega todos os itens
        
        if(items.Length > 0)
        {
            for ( int i = 0; i < items.Length; i++)
            {
                Debug.Log(items[i]);
                CreateUIItem(items[i]);
            }
        }
    }


    //Usa o prefab para exibir as informções na tela 
    private void CreateUIItem(ItemData data)
    {
        GameObject uiItem = Instantiate(itemPrefab, content); //Instancia os itens

        TextMeshProUGUI nameText = uiItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText.text = data.itemName;
    }
}

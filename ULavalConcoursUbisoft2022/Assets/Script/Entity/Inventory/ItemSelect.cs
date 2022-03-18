using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    [System.Serializable]
    public class PopupSelectionBox
    {
        public Image Image = null;
        public TextMeshProUGUI Name = null;
        public TextMeshProUGUI Description = null;
        public ItemData ItemData = null;
    }

    [SerializeField] private List<ItemData> _items = new List<ItemData>();
    [SerializeField] private List<PopupSelectionBox> _itemBoxes = new List<PopupSelectionBox>();
    [SerializeField] private float _delayBeforeChoosing = 0.0f;
    [SerializeField] private UnityEvent _onItemSelected = null;

    private float _timeSinceOpen = 0.0f;

    public void GenerateItems()
    {
        _timeSinceOpen = Time.time;

        Player player = GameObject.FindObjectOfType<Player>();
        player.LockControl();

        List<int> itemsIndex = GenerateRandomsWithoutRepeat(_itemBoxes.Count, _items.Count);

        for (int i = 0; i < _itemBoxes.Count; ++i)
        {
            _itemBoxes[i].ItemData = _items[itemsIndex[i]];
            _itemBoxes[i].Image.sprite = _items[itemsIndex[i]].Image;
            _itemBoxes[i].Name.text = _items[itemsIndex[i]].Name;
            _itemBoxes[i].Description.text = _items[itemsIndex[i]].Description;
        }
    }

    private List<int> GenerateRandomsWithoutRepeat(int numberOfChoosen, int size)
    {
        List<int> choosenIndex = new List<int>();
        if (size < numberOfChoosen)
        {
            Debug.LogError("There is less choice than desired choosen");
            return choosenIndex;
        }

        List<int> index = new List<int>();
        for (int i = 0; i < size; ++i)
        {
            index.Add(i);
        }

        for (int i = 0; i < numberOfChoosen; ++i)
        {
            int random = Random.Range(0, index.Count);
            choosenIndex.Add(index[random]);
            index.RemoveAt(random);
        }

        return choosenIndex;
    }

    public void OnClick(int index)
    {
        if(Time.time - _timeSinceOpen < _delayBeforeChoosing)
        {
            return;
        }

        Player player = GameObject.FindObjectOfType<Player>();
        Inventory inventory = player.Entity.Inventory;

        GameObject itemInstance = Instantiate(_itemBoxes[index].ItemData.Prefab, inventory.transform);
        inventory.Items.Add(itemInstance);

        _onItemSelected?.Invoke();
        player.UnlockControl();
    }
}

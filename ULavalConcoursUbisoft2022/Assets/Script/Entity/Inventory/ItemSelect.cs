using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private ItemData _reliefItem = null;
    [SerializeField] private List<PopupSelectionBox> _itemBoxes = new List<PopupSelectionBox>();
    [SerializeField] private UnityEvent _onItemSelected = null;
    [SerializeField] private float _healthAmount = 0.0f;
    [SerializeField] private ToggleGroup _toggleGroup = null;
    [SerializeField] private Button _acceptButton = null;

    private float _timeSinceOpen = 0.0f;


    public void GenerateItems()
    {
        _timeSinceOpen = Time.time;
        Player player = GameObject.FindObjectOfType<Player>();
        player.LockControl();

        List<int> itemsIndex = GenerateRandomsWithoutRepeat(2, _items.Count);

        for (int i = 0; i < 2; ++i)
        {
            _itemBoxes[i].ItemData = _items[itemsIndex[i]];
            _itemBoxes[i].Image.sprite = _items[itemsIndex[i]].Image;
            _itemBoxes[i].Name.text = _items[itemsIndex[i]].Name;
            _itemBoxes[i].Description.text = _items[itemsIndex[i]].Description;
        }

        _itemBoxes[2].ItemData = _reliefItem;
        _itemBoxes[2].Image.sprite = _reliefItem.Image;
        _itemBoxes[2].Name.text = _reliefItem.Name;
        _itemBoxes[2].Description.text = _reliefItem.Description;
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

    public void Accept()
    {
        Toggle toggle = _toggleGroup.ActiveToggles().FirstOrDefault();
        if(toggle.name == "Item 1")
        {
            GetItem(0);
        }
        else if (toggle.name == "Item 2")
        {
            GetItem(1);
        }
        else if(toggle.name == "Item 3")
        {
            GetItem(2);
        }

        foreach(Toggle activeToggle in _toggleGroup.ActiveToggles())
        {
            activeToggle.isOn = false;
        }
    }

    public void OnValueChange()
    {
        Toggle toggle = _toggleGroup.ActiveToggles().FirstOrDefault();
        _acceptButton.interactable = toggle != null;
    }

    public void GetItem(int index)
    {
        Player player = GameObject.FindObjectOfType<Player>();
        Inventory inventory = player.Entity.Inventory;

        GameObject itemInstance = Instantiate(_itemBoxes[index].ItemData.Prefab, inventory.transform);
        inventory.AddItems(itemInstance);

        Close();
    }

    private void Close()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        _onItemSelected?.Invoke();
        player.UnlockControl();
    }

    public void OnAddHp()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        player.Entity.Health.HealthPoint = Mathf.Clamp(player.Entity.Health.HealthPoint + _healthAmount, 0, player.Entity.Health.MaxHealth);
        Close();
    }
}

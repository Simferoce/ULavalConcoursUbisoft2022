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
    [SerializeField] private UnityEvent _onItemSelected = null;
    [SerializeField] private TextMeshProUGUI _addHp = null;
    [SerializeField] private float _healthAmount = 0.0f;

    private string _stressText = "<color=\"purple\">-{0}<color=\"white\"> Stress";
    private float _timeSinceOpen = 0.0f;


    public void GenerateItems()
    {
        _timeSinceOpen = Time.time;
        _addHp.text = string.Format(_stressText, _healthAmount);
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
        Player player = GameObject.FindObjectOfType<Player>();
        Inventory inventory = player.Entity.Inventory;

        GameObject itemInstance = Instantiate(_itemBoxes[index].ItemData.Prefab, inventory.transform);
        inventory.Items.Add(itemInstance);

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

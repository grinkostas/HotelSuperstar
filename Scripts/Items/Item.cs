using NaughtyAttributes;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField, Dropdown(nameof(_items))] private string _itemId;
    [SerializeField, Dropdown(nameof(_places))] private string _itemMaker;
    
    [SerializeField] private StackSize _stackStackSize;

    private string[] _items = ItemsId.All;
    private string[] _places = PlaceId.All;
    public StackSize StackSize => _stackStackSize;
    public string ItemId => _itemId;
    public string ItemMakerId => _itemMaker;
    public Sprite Icon => _icon;

}

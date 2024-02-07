
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

[CreateAssetMenu]
public class UpgradeShopItem : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _icon;
    [SerializeField] private UpgradeProperty _price;
    
    [Header("Enable Condition")]
    [SerializeField] private bool _hasRequiredPlace;
    [SerializeField, ShowIf(nameof(_hasRequiredPlace)), Dropdown(nameof(PlaceIds))] 
    private string _requiredPlace;
    
    [SerializeField] private List<Upgrade> _upgrades;

    private List<UpgradeModel> _models = new List<UpgradeModel>();
    private string[] PlaceIds => PlaceId.All;


    public string Id => _id;
    public List<Upgrade> Upgrades => _upgrades;
    public Sprite Icon => _icon;

    public bool IsAvailable => !_hasRequiredPlace || Shop.IsBought(_requiredPlace);

    public List<UpgradeModel> GetModels(UpgradesController upgradesController)
    {
        if (_models.Count > 0) return _models;
        _models.AddRange(_upgrades.Select(x=> upgradesController.GetModel(x)));
        return _models;
    }
    
    public int GetCurrentLevel(UpgradesController upgradesController) => GetModels(upgradesController).Max(x => x.CurrentLevel);
    
    public float GetPrice(UpgradesController upgradesController) => _price.Calculate(GetCurrentLevel(upgradesController));
    
    public bool CanLevelUp(UpgradesController upgradesController) => GetModels(upgradesController).Any(x => x.CanLevelUp());
    
}

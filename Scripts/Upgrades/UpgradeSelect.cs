
using NaughtyAttributes;

[System.Serializable]
public class UpgradeSelect
{
    [Dropdown(nameof(Dropdown))]
    public string Text;
    public string[] Dropdown => UpgradeIds.UpgradesDropdown;
}

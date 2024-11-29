/// <summary>
/// Consumable items will inherits from this class
/// It is used in inventory to check if item is consumbale
/// </summary>
public abstract class Consumable : Item
{
    public abstract int effect { get; set; }

}



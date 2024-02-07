public static class UpgradeIds
{
    public static readonly string GuestPatience = "GuestPatience";
    public static readonly string GuestCheckInPatience = "GuestCheckInPatience";
    public static readonly string GuestArrivalDelay = "GuestArrivalDelay";
    public static readonly string DefaultPickUpTime = "DefaultPickUpTime";

    public static readonly string CleaningTime = "CleaningTime";
    
    public static readonly string CoffeeProductTime = "CoffeeProductTime";
    public static readonly string CoffeeGeneratorCapacity = "CoffeeGeneratorCapacity";
    
    public static readonly string BurgerProductTime = "BurgerProductTime";
    public static readonly string BurgerGeneratorCapacity = "BurgerGeneratorCapacity";
    
    public static readonly string CheckInPrice = "CheckInPrice";
    public static readonly string HandCapacity = "HandCapacity";
    
    public static readonly string MovementSpeed = "MovementSpeedUpgradeId";

    public static readonly string RequestHelperHandCapacity = "RequestHelperHandCapacity";
    public static readonly string RequestHelperSpeed = "RequestHelperSpeed";
    
    public static readonly string TestUpgradeId = "TestUpgradeId";
    
    public static readonly string RequestReward = "RequestReward";
    public static readonly string WaterRequestReward = "WaterRequestReward";
    public static readonly string BurgerRequestReward = "BurgerRequestReward";
    public static readonly string CoffeeRequestReward = "CoffeeRequestReward";
    public static readonly string TowelRequestReward = "TowelRequestReward";
    

    

    public static readonly string[] UpgradesDropdown = new string[]
    {
        //Guests
        GuestPatience,
        GuestCheckInPatience,
        GuestArrivalDelay,
        
        DefaultPickUpTime,
        CleaningTime,
        RequestReward,
        
        CheckInPrice,
        WaterRequestReward,
        BurgerRequestReward,
        CoffeeRequestReward,
        TowelRequestReward,
        
        //Player
        HandCapacity,
        MovementSpeed,
        //Request Helper
        RequestHelperHandCapacity,
        RequestHelperSpeed,
        //Kitchen
        CoffeeProductTime,
        CoffeeGeneratorCapacity,
        BurgerProductTime,
        BurgerGeneratorCapacity,
        
        
        TestUpgradeId,
        
        
    };
}


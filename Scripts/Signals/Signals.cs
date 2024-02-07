
public static class Signals
{
    public class NewLimitedInTimeRequest : ASignal<LimitedInTimeRequest> {}
    public class RequestCompetedSignal : ASignal<Request> {}
    public class UpgradeLevelUp : ASignal<Upgrade> {}
    public class NewCleaningRequest : ASignal<Request> {}
    public class EndRequest : ASignal<Request> {}
    public class EarnMoney : ASignal<float> {}
}

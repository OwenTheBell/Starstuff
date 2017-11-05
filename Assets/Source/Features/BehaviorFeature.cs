using Entitas;

public class BehaviorFeature : Feature {
    public BehaviorFeature(Contexts contexts) : base("Behavior") {
        Add(new BehaviorSelectSystem(contexts));
        Add(new TwirlSystem(contexts));
        Add(new PullTowardsSystem(contexts));
    }
}

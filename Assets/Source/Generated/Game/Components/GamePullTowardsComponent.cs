//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PullTowardsComponent pullTowards { get { return (PullTowardsComponent)GetComponent(GameComponentsLookup.PullTowards); } }
    public bool hasPullTowards { get { return HasComponent(GameComponentsLookup.PullTowards); } }

    public void AddPullTowards(int newId, float newForce) {
        var index = GameComponentsLookup.PullTowards;
        var component = CreateComponent<PullTowardsComponent>(index);
        component.id = newId;
        component.force = newForce;
        AddComponent(index, component);
    }

    public void ReplacePullTowards(int newId, float newForce) {
        var index = GameComponentsLookup.PullTowards;
        var component = CreateComponent<PullTowardsComponent>(index);
        component.id = newId;
        component.force = newForce;
        ReplaceComponent(index, component);
    }

    public void RemovePullTowards() {
        RemoveComponent(GameComponentsLookup.PullTowards);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPullTowards;

    public static Entitas.IMatcher<GameEntity> PullTowards {
        get {
            if (_matcherPullTowards == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PullTowards);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPullTowards = matcher;
            }

            return _matcherPullTowards;
        }
    }
}

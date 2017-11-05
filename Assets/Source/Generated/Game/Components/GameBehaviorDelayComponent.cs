//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BehaviorDelayComponent behaviorDelay { get { return (BehaviorDelayComponent)GetComponent(GameComponentsLookup.BehaviorDelay); } }
    public bool hasBehaviorDelay { get { return HasComponent(GameComponentsLookup.BehaviorDelay); } }

    public void AddBehaviorDelay(float newValue) {
        var index = GameComponentsLookup.BehaviorDelay;
        var component = CreateComponent<BehaviorDelayComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBehaviorDelay(float newValue) {
        var index = GameComponentsLookup.BehaviorDelay;
        var component = CreateComponent<BehaviorDelayComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBehaviorDelay() {
        RemoveComponent(GameComponentsLookup.BehaviorDelay);
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

    static Entitas.IMatcher<GameEntity> _matcherBehaviorDelay;

    public static Entitas.IMatcher<GameEntity> BehaviorDelay {
        get {
            if (_matcherBehaviorDelay == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BehaviorDelay);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBehaviorDelay = matcher;
            }

            return _matcherBehaviorDelay;
        }
    }
}

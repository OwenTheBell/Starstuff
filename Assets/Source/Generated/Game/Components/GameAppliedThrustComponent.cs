//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AppliedThrustComponent appliedThrust { get { return (AppliedThrustComponent)GetComponent(GameComponentsLookup.AppliedThrust); } }
    public bool hasAppliedThrust { get { return HasComponent(GameComponentsLookup.AppliedThrust); } }

    public void AddAppliedThrust(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.AppliedThrust;
        var component = CreateComponent<AppliedThrustComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAppliedThrust(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.AppliedThrust;
        var component = CreateComponent<AppliedThrustComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAppliedThrust() {
        RemoveComponent(GameComponentsLookup.AppliedThrust);
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

    static Entitas.IMatcher<GameEntity> _matcherAppliedThrust;

    public static Entitas.IMatcher<GameEntity> AppliedThrust {
        get {
            if (_matcherAppliedThrust == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AppliedThrust);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAppliedThrust = matcher;
            }

            return _matcherAppliedThrust;
        }
    }
}

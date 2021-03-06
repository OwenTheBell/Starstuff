//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ReactiveTorqueComponent reactiveTorque { get { return (ReactiveTorqueComponent)GetComponent(GameComponentsLookup.ReactiveTorque); } }
    public bool hasReactiveTorque { get { return HasComponent(GameComponentsLookup.ReactiveTorque); } }

    public void AddReactiveTorque(float newPercent) {
        var index = GameComponentsLookup.ReactiveTorque;
        var component = CreateComponent<ReactiveTorqueComponent>(index);
        component.percent = newPercent;
        AddComponent(index, component);
    }

    public void ReplaceReactiveTorque(float newPercent) {
        var index = GameComponentsLookup.ReactiveTorque;
        var component = CreateComponent<ReactiveTorqueComponent>(index);
        component.percent = newPercent;
        ReplaceComponent(index, component);
    }

    public void RemoveReactiveTorque() {
        RemoveComponent(GameComponentsLookup.ReactiveTorque);
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

    static Entitas.IMatcher<GameEntity> _matcherReactiveTorque;

    public static Entitas.IMatcher<GameEntity> ReactiveTorque {
        get {
            if (_matcherReactiveTorque == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ReactiveTorque);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherReactiveTorque = matcher;
            }

            return _matcherReactiveTorque;
        }
    }
}

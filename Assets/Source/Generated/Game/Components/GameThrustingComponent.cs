//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Thrusting thrusting { get { return (Thrusting)GetComponent(GameComponentsLookup.Thrusting); } }
    public bool hasThrusting { get { return HasComponent(GameComponentsLookup.Thrusting); } }

    public void AddThrusting(UnityEngine.Vector3 newDirection) {
        var index = GameComponentsLookup.Thrusting;
        var component = CreateComponent<Thrusting>(index);
        component.direction = newDirection;
        AddComponent(index, component);
    }

    public void ReplaceThrusting(UnityEngine.Vector3 newDirection) {
        var index = GameComponentsLookup.Thrusting;
        var component = CreateComponent<Thrusting>(index);
        component.direction = newDirection;
        ReplaceComponent(index, component);
    }

    public void RemoveThrusting() {
        RemoveComponent(GameComponentsLookup.Thrusting);
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

    static Entitas.IMatcher<GameEntity> _matcherThrusting;

    public static Entitas.IMatcher<GameEntity> Thrusting {
        get {
            if (_matcherThrusting == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Thrusting);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherThrusting = matcher;
            }

            return _matcherThrusting;
        }
    }
}
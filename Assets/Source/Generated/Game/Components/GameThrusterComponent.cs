//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ThrusterComponent thruster { get { return (ThrusterComponent)GetComponent(GameComponentsLookup.Thruster); } }
    public bool hasThruster { get { return HasComponent(GameComponentsLookup.Thruster); } }

    public void AddThruster(float newForce) {
        var index = GameComponentsLookup.Thruster;
        var component = CreateComponent<ThrusterComponent>(index);
        component.Force = newForce;
        AddComponent(index, component);
    }

    public void ReplaceThruster(float newForce) {
        var index = GameComponentsLookup.Thruster;
        var component = CreateComponent<ThrusterComponent>(index);
        component.Force = newForce;
        ReplaceComponent(index, component);
    }

    public void RemoveThruster() {
        RemoveComponent(GameComponentsLookup.Thruster);
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

    static Entitas.IMatcher<GameEntity> _matcherThruster;

    public static Entitas.IMatcher<GameEntity> Thruster {
        get {
            if (_matcherThruster == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Thruster);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherThruster = matcher;
            }

            return _matcherThruster;
        }
    }
}

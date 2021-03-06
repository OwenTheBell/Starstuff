//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ReactiveAccelerationComponent reactiveAcceleration { get { return (ReactiveAccelerationComponent)GetComponent(GameComponentsLookup.ReactiveAcceleration); } }
    public bool hasReactiveAcceleration { get { return HasComponent(GameComponentsLookup.ReactiveAcceleration); } }

    public void AddReactiveAcceleration(float newPercent) {
        var index = GameComponentsLookup.ReactiveAcceleration;
        var component = CreateComponent<ReactiveAccelerationComponent>(index);
        component.percent = newPercent;
        AddComponent(index, component);
    }

    public void ReplaceReactiveAcceleration(float newPercent) {
        var index = GameComponentsLookup.ReactiveAcceleration;
        var component = CreateComponent<ReactiveAccelerationComponent>(index);
        component.percent = newPercent;
        ReplaceComponent(index, component);
    }

    public void RemoveReactiveAcceleration() {
        RemoveComponent(GameComponentsLookup.ReactiveAcceleration);
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

    static Entitas.IMatcher<GameEntity> _matcherReactiveAcceleration;

    public static Entitas.IMatcher<GameEntity> ReactiveAcceleration {
        get {
            if (_matcherReactiveAcceleration == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ReactiveAcceleration);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherReactiveAcceleration = matcher;
            }

            return _matcherReactiveAcceleration;
        }
    }
}

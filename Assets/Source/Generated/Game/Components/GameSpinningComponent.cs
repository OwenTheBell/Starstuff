//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SpinningComponent spinning { get { return (SpinningComponent)GetComponent(GameComponentsLookup.Spinning); } }
    public bool hasSpinning { get { return HasComponent(GameComponentsLookup.Spinning); } }

    public void AddSpinning(int newDirection) {
        var index = GameComponentsLookup.Spinning;
        var component = CreateComponent<SpinningComponent>(index);
        component.direction = newDirection;
        AddComponent(index, component);
    }

    public void ReplaceSpinning(int newDirection) {
        var index = GameComponentsLookup.Spinning;
        var component = CreateComponent<SpinningComponent>(index);
        component.direction = newDirection;
        ReplaceComponent(index, component);
    }

    public void RemoveSpinning() {
        RemoveComponent(GameComponentsLookup.Spinning);
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

    static Entitas.IMatcher<GameEntity> _matcherSpinning;

    public static Entitas.IMatcher<GameEntity> Spinning {
        get {
            if (_matcherSpinning == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Spinning);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSpinning = matcher;
            }

            return _matcherSpinning;
        }
    }
}

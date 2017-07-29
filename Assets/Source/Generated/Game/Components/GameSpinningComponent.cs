//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly SpinningComponent spinningComponent = new SpinningComponent();

    public bool isSpinning {
        get { return HasComponent(GameComponentsLookup.Spinning); }
        set {
            if (value != isSpinning) {
                if (value) {
                    AddComponent(GameComponentsLookup.Spinning, spinningComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Spinning);
                }
            }
        }
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
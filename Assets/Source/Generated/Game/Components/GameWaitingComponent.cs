//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly WaitingComponent waitingComponent = new WaitingComponent();

    public bool isWaiting {
        get { return HasComponent(GameComponentsLookup.Waiting); }
        set {
            if (value != isWaiting) {
                if (value) {
                    AddComponent(GameComponentsLookup.Waiting, waitingComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Waiting);
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

    static Entitas.IMatcher<GameEntity> _matcherWaiting;

    public static Entitas.IMatcher<GameEntity> Waiting {
        get {
            if (_matcherWaiting == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Waiting);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWaiting = matcher;
            }

            return _matcherWaiting;
        }
    }
}

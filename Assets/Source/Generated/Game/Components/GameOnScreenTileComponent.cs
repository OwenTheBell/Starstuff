//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly OnScreenTile onScreenTileComponent = new OnScreenTile();

    public bool isOnScreenTile {
        get { return HasComponent(GameComponentsLookup.OnScreenTile); }
        set {
            if (value != isOnScreenTile) {
                if (value) {
                    AddComponent(GameComponentsLookup.OnScreenTile, onScreenTileComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.OnScreenTile);
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

    static Entitas.IMatcher<GameEntity> _matcherOnScreenTile;

    public static Entitas.IMatcher<GameEntity> OnScreenTile {
        get {
            if (_matcherOnScreenTile == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.OnScreenTile);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherOnScreenTile = matcher;
            }

            return _matcherOnScreenTile;
        }
    }
}
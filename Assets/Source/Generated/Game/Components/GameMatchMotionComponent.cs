//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MatchMotionComponent matchMotion { get { return (MatchMotionComponent)GetComponent(GameComponentsLookup.MatchMotion); } }
    public bool hasMatchMotion { get { return HasComponent(GameComponentsLookup.MatchMotion); } }

    public void AddMatchMotion(UnityEngine.GameObject newGameObject, float newScale) {
        var index = GameComponentsLookup.MatchMotion;
        var component = CreateComponent<MatchMotionComponent>(index);
        component.gameObject = newGameObject;
        component.Scale = newScale;
        AddComponent(index, component);
    }

    public void ReplaceMatchMotion(UnityEngine.GameObject newGameObject, float newScale) {
        var index = GameComponentsLookup.MatchMotion;
        var component = CreateComponent<MatchMotionComponent>(index);
        component.gameObject = newGameObject;
        component.Scale = newScale;
        ReplaceComponent(index, component);
    }

    public void RemoveMatchMotion() {
        RemoveComponent(GameComponentsLookup.MatchMotion);
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

    static Entitas.IMatcher<GameEntity> _matcherMatchMotion;

    public static Entitas.IMatcher<GameEntity> MatchMotion {
        get {
            if (_matcherMatchMotion == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MatchMotion);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMatchMotion = matcher;
            }

            return _matcherMatchMotion;
        }
    }
}

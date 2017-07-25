//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BodyComponent body { get { return (BodyComponent)GetComponent(GameComponentsLookup.Body); } }
    public bool hasBody { get { return HasComponent(GameComponentsLookup.Body); } }

    public void AddBody(UnityEngine.Rigidbody newValue) {
        var index = GameComponentsLookup.Body;
        var component = CreateComponent<BodyComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBody(UnityEngine.Rigidbody newValue) {
        var index = GameComponentsLookup.Body;
        var component = CreateComponent<BodyComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBody() {
        RemoveComponent(GameComponentsLookup.Body);
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

    static Entitas.IMatcher<GameEntity> _matcherBody;

    public static Entitas.IMatcher<GameEntity> Body {
        get {
            if (_matcherBody == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Body);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBody = matcher;
            }

            return _matcherBody;
        }
    }
}
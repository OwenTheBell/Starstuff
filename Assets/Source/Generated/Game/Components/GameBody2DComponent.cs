//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Body2DComponent body2D { get { return (Body2DComponent)GetComponent(GameComponentsLookup.Body2D); } }
    public bool hasBody2D { get { return HasComponent(GameComponentsLookup.Body2D); } }

    public void AddBody2D(UnityEngine.Rigidbody2D newValue) {
        var index = GameComponentsLookup.Body2D;
        var component = CreateComponent<Body2DComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBody2D(UnityEngine.Rigidbody2D newValue) {
        var index = GameComponentsLookup.Body2D;
        var component = CreateComponent<Body2DComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBody2D() {
        RemoveComponent(GameComponentsLookup.Body2D);
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

    static Entitas.IMatcher<GameEntity> _matcherBody2D;

    public static Entitas.IMatcher<GameEntity> Body2D {
        get {
            if (_matcherBody2D == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Body2D);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBody2D = matcher;
            }

            return _matcherBody2D;
        }
    }
}

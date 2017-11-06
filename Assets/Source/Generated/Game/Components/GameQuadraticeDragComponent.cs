//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public QuadraticeDragComponent quadraticeDrag { get { return (QuadraticeDragComponent)GetComponent(GameComponentsLookup.QuadraticeDrag); } }
    public bool hasQuadraticeDrag { get { return HasComponent(GameComponentsLookup.QuadraticeDrag); } }

    public void AddQuadraticeDrag(float newValue) {
        var index = GameComponentsLookup.QuadraticeDrag;
        var component = CreateComponent<QuadraticeDragComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceQuadraticeDrag(float newValue) {
        var index = GameComponentsLookup.QuadraticeDrag;
        var component = CreateComponent<QuadraticeDragComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveQuadraticeDrag() {
        RemoveComponent(GameComponentsLookup.QuadraticeDrag);
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

    static Entitas.IMatcher<GameEntity> _matcherQuadraticeDrag;

    public static Entitas.IMatcher<GameEntity> QuadraticeDrag {
        get {
            if (_matcherQuadraticeDrag == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.QuadraticeDrag);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherQuadraticeDrag = matcher;
            }

            return _matcherQuadraticeDrag;
        }
    }
}
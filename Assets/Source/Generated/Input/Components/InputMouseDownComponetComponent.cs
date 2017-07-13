//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public MouseDownComponent mouseDown { get { return (MouseDownComponent)GetComponent(InputComponentsLookup.MouseDown); } }
    public bool hasMouseDownComponent { get { return HasComponent(InputComponentsLookup.MouseDown); } }

    public void AddMouseDown(UnityEngine.Vector2 newPosition) {
        var index = InputComponentsLookup.MouseDown;
        var component = CreateComponent<MouseDownComponent>(index);
        component.position = newPosition;
        AddComponent(index, component);
    }

    public void ReplaceMouseDown(UnityEngine.Vector2 newPosition) {
        var index = InputComponentsLookup.MouseDown;
        var component = CreateComponent<MouseDownComponent>(index);
        component.position = newPosition;
        ReplaceComponent(index, component);
    }

    public void RemoveMouseDown() {
        RemoveComponent(InputComponentsLookup.MouseDown);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherMouseDown;

    public static Entitas.IMatcher<InputEntity> MouseDown {
        get {
            if (_matcherMouseDown == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.MouseDown);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherMouseDown = matcher;
            }

            return _matcherMouseDown;
        }
    }
}

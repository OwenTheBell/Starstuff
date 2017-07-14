//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public KeyUpComponent keyUp { get { return (KeyUpComponent)GetComponent(InputComponentsLookup.KeyUp); } }
    public bool hasKeyUp { get { return HasComponent(InputComponentsLookup.KeyUp); } }

    public void AddKeyUp() {
        var index = InputComponentsLookup.KeyUp;
        var component = CreateComponent<KeyUpComponent>(index);
        AddComponent(index, component);
    }

    public void ReplaceKeyUp() {
        var index = InputComponentsLookup.KeyUp;
        var component = CreateComponent<KeyUpComponent>(index);
        ReplaceComponent(index, component);
    }

    public void RemoveKeyUp() {
        RemoveComponent(InputComponentsLookup.KeyUp);
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

    static Entitas.IMatcher<InputEntity> _matcherKeyUp;

    public static Entitas.IMatcher<InputEntity> KeyUp {
        get {
            if (_matcherKeyUp == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.KeyUp);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherKeyUp = matcher;
            }

            return _matcherKeyUp;
        }
    }
}

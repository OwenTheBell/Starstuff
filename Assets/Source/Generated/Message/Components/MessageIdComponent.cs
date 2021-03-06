//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MessageEntity {

    public IdComponent id { get { return (IdComponent)GetComponent(MessageComponentsLookup.Id); } }
    public bool hasId { get { return HasComponent(MessageComponentsLookup.Id); } }

    public void AddId(int newValue) {
        var index = MessageComponentsLookup.Id;
        var component = CreateComponent<IdComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceId(int newValue) {
        var index = MessageComponentsLookup.Id;
        var component = CreateComponent<IdComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveId() {
        RemoveComponent(MessageComponentsLookup.Id);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MessageEntity : IId { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class MessageMatcher {

    static Entitas.IMatcher<MessageEntity> _matcherId;

    public static Entitas.IMatcher<MessageEntity> Id {
        get {
            if (_matcherId == null) {
                var matcher = (Entitas.Matcher<MessageEntity>)Entitas.Matcher<MessageEntity>.AllOf(MessageComponentsLookup.Id);
                matcher.componentNames = MessageComponentsLookup.componentNames;
                _matcherId = matcher;
            }

            return _matcherId;
        }
    }
}

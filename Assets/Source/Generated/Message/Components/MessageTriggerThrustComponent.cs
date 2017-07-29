//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MessageEntity {

    public TriggerThrust triggerThrust { get { return (TriggerThrust)GetComponent(MessageComponentsLookup.TriggerThrust); } }
    public bool hasTriggerThrust { get { return HasComponent(MessageComponentsLookup.TriggerThrust); } }

    public void AddTriggerThrust(UnityEngine.Vector3 newDirection) {
        var index = MessageComponentsLookup.TriggerThrust;
        var component = CreateComponent<TriggerThrust>(index);
        component.direction = newDirection;
        AddComponent(index, component);
    }

    public void ReplaceTriggerThrust(UnityEngine.Vector3 newDirection) {
        var index = MessageComponentsLookup.TriggerThrust;
        var component = CreateComponent<TriggerThrust>(index);
        component.direction = newDirection;
        ReplaceComponent(index, component);
    }

    public void RemoveTriggerThrust() {
        RemoveComponent(MessageComponentsLookup.TriggerThrust);
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
public sealed partial class MessageMatcher {

    static Entitas.IMatcher<MessageEntity> _matcherTriggerThrust;

    public static Entitas.IMatcher<MessageEntity> TriggerThrust {
        get {
            if (_matcherTriggerThrust == null) {
                var matcher = (Entitas.Matcher<MessageEntity>)Entitas.Matcher<MessageEntity>.AllOf(MessageComponentsLookup.TriggerThrust);
                matcher.componentNames = MessageComponentsLookup.componentNames;
                _matcherTriggerThrust = matcher;
            }

            return _matcherTriggerThrust;
        }
    }
}
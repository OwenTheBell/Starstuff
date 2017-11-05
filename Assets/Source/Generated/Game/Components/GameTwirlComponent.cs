//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TwirlComponent twirl { get { return (TwirlComponent)GetComponent(GameComponentsLookup.Twirl); } }
    public bool hasTwirl { get { return HasComponent(GameComponentsLookup.Twirl); } }

    public void AddTwirl(float newDuration, int newPartnerId, float newDistance, float newForce, bool newClockwise) {
        var index = GameComponentsLookup.Twirl;
        var component = CreateComponent<TwirlComponent>(index);
        component.duration = newDuration;
        component.partnerId = newPartnerId;
        component.distance = newDistance;
        component.force = newForce;
        component.clockwise = newClockwise;
        AddComponent(index, component);
    }

    public void ReplaceTwirl(float newDuration, int newPartnerId, float newDistance, float newForce, bool newClockwise) {
        var index = GameComponentsLookup.Twirl;
        var component = CreateComponent<TwirlComponent>(index);
        component.duration = newDuration;
        component.partnerId = newPartnerId;
        component.distance = newDistance;
        component.force = newForce;
        component.clockwise = newClockwise;
        ReplaceComponent(index, component);
    }

    public void RemoveTwirl() {
        RemoveComponent(GameComponentsLookup.Twirl);
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

    static Entitas.IMatcher<GameEntity> _matcherTwirl;

    public static Entitas.IMatcher<GameEntity> Twirl {
        get {
            if (_matcherTwirl == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Twirl);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTwirl = matcher;
            }

            return _matcherTwirl;
        }
    }
}

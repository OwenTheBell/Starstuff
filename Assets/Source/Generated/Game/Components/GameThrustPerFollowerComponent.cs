//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ThrustPerFollowerComponent thrustPerFollower { get { return (ThrustPerFollowerComponent)GetComponent(GameComponentsLookup.ThrustPerFollower); } }
    public bool hasThrustPerFollower { get { return HasComponent(GameComponentsLookup.ThrustPerFollower); } }

    public void AddThrustPerFollower(float newBaseSpeed, float newSpeedPerFollower, float newBaseThrust, float newThrustPerFollower) {
        var index = GameComponentsLookup.ThrustPerFollower;
        var component = CreateComponent<ThrustPerFollowerComponent>(index);
        component.BaseSpeed = newBaseSpeed;
        component.SpeedPerFollower = newSpeedPerFollower;
        component.BaseThrust = newBaseThrust;
        component.ThrustPerFollower = newThrustPerFollower;
        AddComponent(index, component);
    }

    public void ReplaceThrustPerFollower(float newBaseSpeed, float newSpeedPerFollower, float newBaseThrust, float newThrustPerFollower) {
        var index = GameComponentsLookup.ThrustPerFollower;
        var component = CreateComponent<ThrustPerFollowerComponent>(index);
        component.BaseSpeed = newBaseSpeed;
        component.SpeedPerFollower = newSpeedPerFollower;
        component.BaseThrust = newBaseThrust;
        component.ThrustPerFollower = newThrustPerFollower;
        ReplaceComponent(index, component);
    }

    public void RemoveThrustPerFollower() {
        RemoveComponent(GameComponentsLookup.ThrustPerFollower);
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

    static Entitas.IMatcher<GameEntity> _matcherThrustPerFollower;

    public static Entitas.IMatcher<GameEntity> ThrustPerFollower {
        get {
            if (_matcherThrustPerFollower == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ThrustPerFollower);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherThrustPerFollower = matcher;
            }

            return _matcherThrustPerFollower;
        }
    }
}

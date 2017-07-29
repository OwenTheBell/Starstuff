//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ThrustParticleComponent thrustParticle { get { return (ThrustParticleComponent)GetComponent(GameComponentsLookup.ThrustParticle); } }
    public bool hasThrustParticle { get { return HasComponent(GameComponentsLookup.ThrustParticle); } }

    public void AddThrustParticle(UnityEngine.ParticleSystem newSystem) {
        var index = GameComponentsLookup.ThrustParticle;
        var component = CreateComponent<ThrustParticleComponent>(index);
        component.system = newSystem;
        AddComponent(index, component);
    }

    public void ReplaceThrustParticle(UnityEngine.ParticleSystem newSystem) {
        var index = GameComponentsLookup.ThrustParticle;
        var component = CreateComponent<ThrustParticleComponent>(index);
        component.system = newSystem;
        ReplaceComponent(index, component);
    }

    public void RemoveThrustParticle() {
        RemoveComponent(GameComponentsLookup.ThrustParticle);
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

    static Entitas.IMatcher<GameEntity> _matcherThrustParticle;

    public static Entitas.IMatcher<GameEntity> ThrustParticle {
        get {
            if (_matcherThrustParticle == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ThrustParticle);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherThrustParticle = matcher;
            }

            return _matcherThrustParticle;
        }
    }
}
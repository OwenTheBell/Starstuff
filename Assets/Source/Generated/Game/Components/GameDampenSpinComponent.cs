//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public DampenSpinComponent dampenSpin { get { return (DampenSpinComponent)GetComponent(GameComponentsLookup.DampenSpin); } }
    public bool hasDampenSpin { get { return HasComponent(GameComponentsLookup.DampenSpin); } }

    public void AddDampenSpin(float newValue) {
        var index = GameComponentsLookup.DampenSpin;
        var component = CreateComponent<DampenSpinComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDampenSpin(float newValue) {
        var index = GameComponentsLookup.DampenSpin;
        var component = CreateComponent<DampenSpinComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDampenSpin() {
        RemoveComponent(GameComponentsLookup.DampenSpin);
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

    static Entitas.IMatcher<GameEntity> _matcherDampenSpin;

    public static Entitas.IMatcher<GameEntity> DampenSpin {
        get {
            if (_matcherDampenSpin == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.DampenSpin);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherDampenSpin = matcher;
            }

            return _matcherDampenSpin;
        }
    }
}

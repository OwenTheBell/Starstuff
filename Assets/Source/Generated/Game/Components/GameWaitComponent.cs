//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public WaitComponent wait { get { return (WaitComponent)GetComponent(GameComponentsLookup.Wait); } }
    public bool hasWait { get { return HasComponent(GameComponentsLookup.Wait); } }

    public void AddWait(bool newBeVisible, float newRange, float newDelay, float new_RemainingDelay) {
        var index = GameComponentsLookup.Wait;
        var component = CreateComponent<WaitComponent>(index);
        component.BeVisible = newBeVisible;
        component.Range = newRange;
        component.Delay = newDelay;
        component._RemainingDelay = new_RemainingDelay;
        AddComponent(index, component);
    }

    public void ReplaceWait(bool newBeVisible, float newRange, float newDelay, float new_RemainingDelay) {
        var index = GameComponentsLookup.Wait;
        var component = CreateComponent<WaitComponent>(index);
        component.BeVisible = newBeVisible;
        component.Range = newRange;
        component.Delay = newDelay;
        component._RemainingDelay = new_RemainingDelay;
        ReplaceComponent(index, component);
    }

    public void RemoveWait() {
        RemoveComponent(GameComponentsLookup.Wait);
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

    static Entitas.IMatcher<GameEntity> _matcherWait;

    public static Entitas.IMatcher<GameEntity> Wait {
        get {
            if (_matcherWait == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Wait);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWait = matcher;
            }

            return _matcherWait;
        }
    }
}

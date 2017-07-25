using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    public GenerateFeature[] Features;

    Systems _systems;

	void Start () {
        var contexts = Contexts.sharedInstance;

        foreach (var c in contexts.allContexts) {
            c.OnEntityCreated += AddId;
        }


        _systems = new Feature("Systems");

        // add must include features & systems
        _systems.Add(new InputFeature(contexts));
        _systems.Add(new MessagingFeature(contexts));
        _systems.Add(new PauseSystem(contexts));
        _systems.Add(new TickSystem(contexts));
        _systems.Add(new TogglePhysicsPauseSystem(contexts));
        _systems.Add(new StarSystems(contexts));

        // add features & systems chosen in editor
        foreach (var feature in Features) {
            _systems.Add(feature.Generate(contexts));
        }

        _systems.Add(new InertiaDampeningSystem(contexts));
        _systems.Add(new SpinDampeningSystem(contexts));
        _systems.Add(new ClearThrustAndSpin(contexts));

        _systems.Add(new ProcessFixedUpdateSystem(contexts));
        _systems.Add(new DestroySystem(contexts));

        _systems.Initialize();
	}
	
	void Update () {
        _systems.Execute();
        _systems.Cleanup();
	}

    void AddId(IContext c, IEntity e) {
        (e as IId).AddId(e.creationIndex);
    }
}

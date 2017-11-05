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
        //contexts.game.OnEntityCreated += AddId;


        _systems = new sm_Feature("Systems");

        // add must include features & systems
        _systems.Add(new EmitInputSystem(contexts));
        //_systems.Add(new InputFeature(contexts));
        //_systems.Add(new MessagingFeature(contexts));
        _systems.Add(new PauseSystem(contexts));
        _systems.Add(new TickSystem(contexts));
        _systems.Add(new TogglePhysicsPauseSystem(contexts));
        _systems.Add(new StarSystems(contexts));
        _systems.Add(new PullTowardsTest(contexts));

        // add features & systems chosen in editor
        foreach (var feature in Features) {
            _systems.Add(feature.Generate(contexts));
        }

        _systems.Add(new ThrustParticleSystem(contexts));
        _systems.Add(new BehaviorFeature(contexts));

        _systems.Add(new InertiaDampeningSystem(contexts));
        _systems.Add(new SpinDampeningSystem(contexts));
        _systems.Add(new ProcessFixedUpdateSystem(contexts));
        _systems.Add(new CleanupMessages(contexts));
        _systems.Add(new DestroySystem(contexts));

        // Fixed Update Systems
        _systems.Add(new FixedTickSystem(contexts));
        _systems.Add(new ApplyThrustSystem(contexts));
        _systems.Add(new ConvertAppliedThrustSystem(contexts));

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
using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    public GenerateFeature[] Features;

    sm_Feature _systems;

	void Start () {
        var contexts = Contexts.sharedInstance;

        foreach (var c in contexts.allContexts) {
            c.OnEntityCreated += AddId;
        }

        _systems = new sm_Feature("Systems");

        // add must include features & systems
        _systems.Add(new CleanThrustingSystem(contexts));
        _systems.Add(new EmitInputSystem(contexts));
        _systems.Add(new PauseSystem(contexts));
        _systems.Add(new TickSystem(contexts));
        _systems.Add(new TogglePhysicsPauseSystem(contexts));
        _systems.Add(new PullTowardsTest(contexts));

        // add features & systems chosen in editor
        foreach (var feature in Features) {
            _systems.Add(feature.Generate(contexts));
        }

        // Execute Systems
        _systems.Add(new ApplyThrustSystem(contexts));
        _systems.Add(new ThrustParticleSystem(contexts));
        _systems.Add(new BehaviorFeature(contexts));
        _systems.Add(new Wait(contexts));
        _systems.Add(new Catchup(contexts));
        _systems.Add(new Follow(contexts));

        // Fixed Update Systems
        _systems.Add(new FixedTickSystem(contexts));
        _systems.Add(new InertiaDampeningSystem(contexts));
        _systems.Add(new ReactiveAccelerationSystem(contexts));
        _systems.Add(new SpinSystem(contexts));
        _systems.Add(new SpinDampeningSystem(contexts));
        _systems.Add(new ReactiveTorqueSystem(contexts));
        _systems.Add(new RepulserSystem(contexts));
        _systems.Add(new ConvertAppliedThrustSystem(contexts));
        _systems.Add(new MaxVelocitySystem(contexts));
        _systems.Add(new MaxSpinSystem(contexts));

        // Cleanup Systems
        _systems.Add(new CleanupMessages(contexts));
        _systems.Add(new DestroySystem(contexts));

        _systems.Initialize();
	}
	
	void Update () {
        _systems.Execute();
        _systems.Cleanup();
	}

    void FixedUpdate() {
        _systems.FixedUpdate();
    }

    void AddId(IContext c, IEntity e) {
        (e as IId).AddId(e.creationIndex);
    }
}
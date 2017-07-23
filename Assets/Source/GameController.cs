using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    public GenerateFeature[] Features;

    Systems _systems;
    Systems _fixedUpdatedSystems;

	void Start () {
        var contexts = Contexts.sharedInstance;
        _systems = new Feature("Systems");
        _fixedUpdatedSystems = new Feature("Fixed Update Systems");

        // add must include features & systems
        _systems.Add(new InputFeature(contexts));
        _systems.Add(new MessagingFeature(contexts));

        // add features & systems chosen in editor
        foreach (var feature in Features) {
            if (feature.UseFixedUpdate) {
                _fixedUpdatedSystems.Add(feature.Generate(contexts));
            }
            else {
                _systems.Add(feature.Generate(contexts));
            }
        }

        _systems.Add(new StarSystems(contexts));

        _systems.Initialize();
        _fixedUpdatedSystems.Initialize();
	}
	
	void Update () {
        _systems.Execute();
        _systems.Cleanup();
	}

    private void FixedUpdate() {
        _fixedUpdatedSystems.Execute();
        _fixedUpdatedSystems.Cleanup();
    }
}

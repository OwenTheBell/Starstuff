using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    Systems _systems;

	// Use this for initialization
	void Start () {
        var contexts = Contexts.sharedInstance;

        _systems = new Feature("Systems")
            .Add(new TestFeature(contexts));

        _systems.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        _systems.Execute();
        _systems.Cleanup();
	}
}

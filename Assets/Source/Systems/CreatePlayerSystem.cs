using Entitas;
using Entitas.Unity;
using UnityEngine;

public class CreatePlayerSystem : IInitializeSystem {

    private GameContext _context;
    private GameObject _playerPrefab;
    private IComponent[] _components;

    public CreatePlayerSystem(Contexts contexts, GameObject playerPrefab, IComponent[] components) {
        _context = contexts.game;
        _playerPrefab = playerPrefab;
        _components = components;

        var player = GameObject.Instantiate(_playerPrefab);
        player.name = "Player";
        var buffer = player.AddComponent<FixedUpdateBuffer>();
        var e = _context.CreateEntity();
        e.AddView(player);
        e.isPlayer = true;
        var types = GameComponentsLookup.componentTypes;
        foreach (var component in _components) {
            var index = -1;
            for (var i = 0; i < types.Length; i++) {
                if (types[i] == component.GetType()) {
                    index = i;
                    break;
                }
            }
            e.AddComponent(index, component);
        }
        player.Link(e, _context);
        e.AddRigidbody2D(player.GetComponent<Rigidbody2D>());
        e.AddThrustParticle(player.GetComponentInChildren<ParticleSystem>());
    }

    public void Initialize() {
    }
}
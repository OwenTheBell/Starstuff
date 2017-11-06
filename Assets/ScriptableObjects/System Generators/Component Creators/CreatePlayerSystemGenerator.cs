using UnityEngine;
using Entitas;

[CreateAssetMenu(fileName = "Create Player", menuName = "SuperMash/Systems/Create Player")]
public class CreatePlayerSystemGenerator : SystemGenerator {

    public GameObject PlayerPrefab;
    public ThrusterComponent Thruster;
    public DampenInertiaComponent DampenIntertia;
    public ReactiveAccelerationComponent ReactiveAcceleration;
    public SpinComponent Spin;
    public DampenSpinComponent DampenSpin;
    public ReactiveTorqueComponent ReactiveTorque;
    public ThrustPerFollowerComponent ThrustPerFollower;
    public MaxVelocityComponent MaxVelocity;
    public MaxSpinComponent MaxSpin;

    public override ISystem Generate(Contexts contexts) {
        var components = new IComponent[]{
            Thruster,
            Spin,
            ThrustPerFollower,
            DampenIntertia,
            DampenSpin,
            ReactiveAcceleration,
            ReactiveTorque,
            MaxVelocity,
            MaxSpin
        };
        var player = new CreatePlayerSystem(contexts, PlayerPrefab, components);
        return player;
    }
}
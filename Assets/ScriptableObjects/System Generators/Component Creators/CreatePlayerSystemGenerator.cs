using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Entitas;

[CreateAssetMenu(fileName = "Create Player", menuName = "SuperMash/Systems/Create Player")]
public class CreatePlayerSystemGenerator : SystemGenerator {

    public GameObject PlayerPrefab;
    public ThrusterComponent Thruster;
    public SpinComponent Spin;
    public ThrustPerFollowerComponent ThrustPerFollower;

    public override ISystem Generate(Contexts contexts) {
        var components = new IComponent[]{ Thruster, Spin, ThrustPerFollower };
        var player = new CreatePlayerSystem(contexts, PlayerPrefab, components);
        return player;
    }
}

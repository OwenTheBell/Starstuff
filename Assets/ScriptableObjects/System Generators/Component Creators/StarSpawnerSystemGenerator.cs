using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Star Spawner", menuName = "SuperMash/Systems/Star Spawner")]
public class StarSpawnerSystemGenerator : SystemGenerator {
    public StarSpawnInfo SpawnInfo;
    public WaitComponent Wait;
    public CatchupComponent Catchup;
    public FollowComponent Follow;
    public ChangingMovementStateComponent StateChange;
    public override ISystem Generate(Contexts contexts) {
        var components = new IComponent[] { Wait, Catchup, Follow, StateChange};
        return new StarSpawnerSystem(contexts, SpawnInfo, components);
    }
}

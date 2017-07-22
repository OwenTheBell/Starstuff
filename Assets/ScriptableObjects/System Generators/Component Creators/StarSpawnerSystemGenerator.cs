using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Star Spawner", menuName = "SuperMash/Systems/Star Spawner")]
public class StarSpawnerSystemGenerator : SystemGenerator {
    public StarSpawnInfo SpawnInfo;
    public WaitComponent Wait;
    public CatchupComponent Catchup;
    public FollowComponent Follow;
    public override ISystem Generate(Contexts contexts) {
        var components = new IComponent[] { Wait, Catchup, Follow };
        return new StarSpawnerSystem(contexts, SpawnInfo, components);
    }
}

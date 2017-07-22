using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Star Spawner", menuName = "SuperMash/Systems/Star Spawner")]
public class StarSpawnerSystemGenerator : SystemGenerator {
    public StarSpawnInfo SpawnInfo;
    public override ISystem Generate(Contexts contexts) {
        return new StarSpawnerSystem(contexts, SpawnInfo);
    }
}

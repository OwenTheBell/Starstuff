using Entitas;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Control", menuName = "SuperMash/Systems/Player Control")]
public class PlayerControlSystemGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new PlayerControlSystem(contexts);
    }
}

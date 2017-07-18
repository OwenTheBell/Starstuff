using UnityEngine;
using Entitas;

[CreateAssetMenu(fileName = "Restrict Velocity", menuName = "SuperMash/Systems/Restrict Velocity")]
public class RestrictVelocitySystemGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new RestrictVelocitySystem(contexts);
    }
}
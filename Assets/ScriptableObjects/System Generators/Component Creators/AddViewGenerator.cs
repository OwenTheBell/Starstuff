using UnityEngine;
using Entitas;

[CreateAssetMenu(fileName = "Add View", menuName = "SuperMash/Systems/Add View")]
public class AddViewGenerator : SystemGenerator {
    public override ISystem Generate(Contexts contexts) {
        return new AddViewSystem(contexts);
    }
}

using Entitas;

public class InputFeature : Feature {
    public InputFeature(Contexts contexts) : base("Input Feature") {
        Add(new EmitInputSystem(contexts));
        Add(new CreateMoverSystem(contexts));
        Add(new CommandMoveSystem(contexts));
    }
}

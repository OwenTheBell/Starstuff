using Entitas;

public class InputFeature : Feature {
    public InputFeature(Contexts contexts) : base("Input") {
        Add(new EmitInputSystem(contexts));
    }
}

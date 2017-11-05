using Entitas;

public class InputFeature : sm_Feature {
    public InputFeature(Contexts contexts) : base("Input") {
        Add(new EmitInputSystem(contexts));
    }
}

using System;

/* an extension of the System.Random class that adds a few useful functions
* that I felt the original was missing */
public class SeededRandom : Random {

	public SeededRandom(int seed) : base(seed) {}

	public float Range(float lower, float upper) {
		var diff = upper-lower;
		return (float)this.NextDouble() * diff + lower;
	}

	public float Sample() {
		return (float)base.Sample();
	}
}
using UnityEngine;
using System;
using System.Collections.Generic;

public static class PointDistributer {

	public static Vector3[] Mitchells(Rect rect, int count, int seed) {
		var noise = new float[,]{{0.5f}};
		return Mitchells(rect, count, seed, new Vector3[0], noise);
	}

	public static Vector3[] Mitchells(Rect rect,
										int count,
										int seed,
										float[,] noise) {
		
		return Mitchells(rect, count, seed, new Vector3[0], noise);
	}

	public static Vector3[] Mitchells(
										Rect rect,
										int count,
										int seed,
										Vector3[] samples) {

		var noise = new float[,]{{0.5f}};
		return Mitchells(rect, count, seed, samples, noise);
	}

	public static Vector3[] Mitchells(
										Rect rect,
										int count,
										int seed,
										Vector3[] samples,
										float[,] noiseMap) {

		var noiseHeight = noiseMap.GetLength(0);
		var noiseWidth = noiseMap.Length / noiseHeight;

		var random = new SeededRandom(seed);
		var outputSamples = new Vector3[count + samples.Length];
		Array.Copy(samples, outputSamples, samples.Length);

		var width = rect.width;
		var height = rect.height;

	    for (var index = 0; index < count; index++) {
	    	var newSample = Vector3.zero;
	    	var furthestNeighbor = 0f;
	    	for (int i = 0; i < 10; i++) {
	    		var test = new Vector3(
	    								random.Range(0f, width),
		    							0f,
	    								random.Range(0f, height)
	    							);
	    		if (index == 0) {
	    			newSample = test;
	    			break;
	    		}

	    		/* noiseValue is used to adjust the distance value to change
	    		* the density of the points. A noise value of 0.5 will have no
	    		* effect, a noise value of 0 will nullify distnace, and a 1
	    		* will double with distance value */
	    		var noiseValue = noiseMap[
	    									(int)(test.x * (noiseWidth/width)),
	    									(int)(test.y * (noiseHeight/height))
	    								];
	    		var closestNeighbor = 0f;
	    		for (var j = 0; j < index; j++) {
	    			var distance = Vector3.Distance(test, outputSamples[j]);
	    			distance *= noiseValue * 3;
	    			if (distance < closestNeighbor || j == 0) {
	    				closestNeighbor = distance;
	    			}
	    		}
	    		if (furthestNeighbor < closestNeighbor) {
	    			furthestNeighbor = closestNeighbor;
	    			newSample = test;
	    		}
	    	}
	    	outputSamples[index] = newSample;
		}

		return outputSamples;
	}
}

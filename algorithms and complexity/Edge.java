package com.ot.grephq;

/**
 * Representation of the edge between two vertices in a graph
 */
public class Edge {

	private Vertex vertex;
	private int weight;
	
	public Edge(Vertex vertex, int weight) {
		this.vertex = vertex;
		this.weight = weight;
	}
	
	public int getWeight() {
		return weight;
	}
	
	public Vertex getVertex() {
		return vertex;
	}
}

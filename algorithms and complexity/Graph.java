package com.ot.grephq;

import java.util.List;

/**
 * Representation of a graph
 */
public class Graph {

	private List<Vertex> vertices;
	
	public Graph(List<Vertex> vertices) {
		this.vertices = vertices;
	}
	
	public List<Vertex> getVertices(){
		return vertices;
	}
}

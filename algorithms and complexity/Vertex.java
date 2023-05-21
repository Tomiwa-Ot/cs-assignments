package com.ot.grephq;

import java.util.ArrayList;
import java.util.List;

/**
 * Representation of a node in a graph
 */
public class Vertex {

	private char id;
	private int weight;
	private List<Edge> edges;
	
	public Vertex(char id, int weight) {
		this.id = id;
		this.weight = weight;
		this.edges = new ArrayList<Edge>();
	}
	
	public Vertex(char id, int weight, List<Edge> edges) {
		this.id = id;
		this.weight = weight;
		this.edges = edges;
	}
	
	public char getId() {
		return id;
	}
	
	public int getWeight() {
		return weight;
	}
	
	public void setWeight(int weight) {
		this.weight = weight;
	}
	
	public List<Edge> getEdges(){
		return edges;
	}
	
	public void addEdge(Edge edge) {
		edges.add(edge);
	}
}

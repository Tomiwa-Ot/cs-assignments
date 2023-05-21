package com.ot.grephq;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class CSC320 {

	public static void main(String[] args) {
		
		Graph graph = createGraphForBellmanFord();
		bellmanFordAlgorithm(graph);
		System.out.println("Bellman Ford");
		System.out.println("------------");
		for(Vertex vertex : graph.getVertices()) {
			System.out.println(vertex.getId() + ": " + vertex.getWeight());
		}
		
		graph = createGraphForDijkstra();
		dijkstraAlgorithm(graph);
		System.out.println("\nDijkstra");
		System.out.println("----------");
		for(Vertex vertex : graph.getVertices()) {
			System.out.println(vertex.getId() + ": " + vertex.getWeight());
		}
	}
	
	/**
	 * Create a graph for Bellman Ford's algorithm
	 * 
	 * @return Graph
	 */
	public static Graph createGraphForBellmanFord() {
		Vertex a = new Vertex('A', 0);
		Vertex b = new Vertex('B', Integer.MAX_VALUE);
		Vertex c = new Vertex('C', Integer.MAX_VALUE);
		Vertex d = new Vertex('D', Integer.MAX_VALUE);
		Vertex e = new Vertex('E', Integer.MAX_VALUE);
		Vertex f = new Vertex('F', Integer.MAX_VALUE);
		
		a.addEdge(new Edge(b, 6));
		a.addEdge(new Edge(c, 4));
		a.addEdge(new Edge(d, 5));
		
		b.addEdge(new Edge(e, -1));
		
		c.addEdge(new Edge(b, -2));
		c.addEdge(new Edge(e, 3));
		
		d.addEdge(new Edge(c, -2));
		d.addEdge(new Edge(f, -1));
		
		e.addEdge(new Edge(f, 3));
		
		List<Vertex> vertices = new ArrayList<Vertex>(Arrays.asList(a, b, c, d, e, f));
		
		return new Graph(vertices);
	}
	
	/**
	 * Create a graph for Dijkstra's algorithm
	 * 
	 * @return
	 */
	public static Graph createGraphForDijkstra() {
		Vertex a = new Vertex('0', 0);
		Vertex b = new Vertex('1', Integer.MAX_VALUE);
		Vertex c = new Vertex('2', Integer.MAX_VALUE);
		Vertex d = new Vertex('3', Integer.MAX_VALUE);
		Vertex e = new Vertex('4', Integer.MAX_VALUE);
		Vertex f = new Vertex('5', Integer.MAX_VALUE);
		Vertex g = new Vertex('6', Integer.MAX_VALUE);
		Vertex h = new Vertex('7', Integer.MAX_VALUE);
		Vertex i = new Vertex('8', Integer.MAX_VALUE);
		
		a.addEdge(new Edge(b, 4));
		a.addEdge(new Edge(h, 8));
		
		b.addEdge(new Edge(a, 4));
		b.addEdge(new Edge(c, 8));
		b.addEdge(new Edge(h, 11));
		
		c.addEdge(new Edge(b, 8));
		c.addEdge(new Edge(d, 7));
		c.addEdge(new Edge(f, 4));
		c.addEdge(new Edge(i, 2));
		
		d.addEdge(new Edge(c, 7));
		d.addEdge(new Edge(e, 9));
		d.addEdge(new Edge(f, 14));
		
		e.addEdge(new Edge(d, 9));
		e.addEdge(new Edge(f, 10));
		
		f.addEdge(new Edge(c, 4));
		f.addEdge(new Edge(d, 14));
		f.addEdge(new Edge(e, 10));
		f.addEdge(new Edge(g, 2));
		
		g.addEdge(new Edge(f, 2));
		g.addEdge(new Edge(h, 1));
		g.addEdge(new Edge(i, 6));
		
		h.addEdge(new Edge(a, 8));
		h.addEdge(new Edge(b, 11));
		h.addEdge(new Edge(g, 1));
		h.addEdge(new Edge(i, 7));
		
		i.addEdge(new Edge(c, 2));
		i.addEdge(new Edge(g, 6));
		i.addEdge(new Edge(h, 7));
		
		List<Vertex> vertices = new ArrayList<Vertex>(Arrays.asList(a, b, c, d, e, f, g, h, i));
		
		return new Graph(vertices);
	}
	
	/**
	 * Implementation of Bellman Ford's algorithm
	 * 
	 * @param graph
	 */
	public static void bellmanFordAlgorithm(Graph graph) {
		for(int i = 1; (i <= graph.getVertices().size() - 1); i++) {
			for(Vertex vertex : graph.getVertices()) {
				for(Edge edge : vertex.getEdges()) {
					if((vertex.getWeight() + edge.getWeight()) < edge.getVertex().getWeight())
						edge.getVertex().setWeight(vertex.getWeight() + edge.getWeight());
				}
			}
		}
	}
	
	/**
	 * Implementation of Dijkstra's algorithm
	 * 
	 * @param graph
	 */
	public static void dijkstraAlgorithm(Graph graph) {
		Map<Vertex, Boolean> selected = new HashMap<>() {{
			for(Vertex vertex : graph.getVertices())
				put(vertex, false);
		}};
		
		Vertex selectedVertex = graph.getVertices().get(0);
		
		for (int i = 1; i <= (graph.getVertices().size() - 1); i++) {
			selected.put(selectedVertex, true);
			
			for(Edge edge : selectedVertex.getEdges()) {
				if((selectedVertex.getWeight() + edge.getWeight()) < edge.getVertex().getWeight() && !selected.get(edge.getVertex()))
					edge.getVertex().setWeight(selectedVertex.getWeight() + edge.getWeight());
			}
			
			int score = Integer.MAX_VALUE;
			for(Vertex vertex : graph.getVertices()) {
				if(!selected.get(vertex) && vertex.getWeight() < score) {
					selectedVertex = vertex;
					score = vertex.getWeight();
				}
			}
		}
	}

}

from OpenGL.GL import *

class Chair:
    body = ((1.5, -1, -0.5),
            (1.5, 0.5, -0.5),
            (-1.5, 0.5, -0.5),
            (-1.5, -1, -0.5),
            (1.5, -1, -0.3),
            (1.5, 0.5, -0.3),
            (-1.5, -1, -0.3),
            (-1.5, 0.5, -0.3))
    
    right_arm = ((-1.5, -1, -0.5),
            (-1.5, 0, -0.5),
            (-1.7, 0, -0.5),
            (-1.7, -1, -0.5),
            (-1.5, -1, 0.5),
            (-1.5, 0, 0.5),
            (-1.7, -1, 0.5),
            (-1.7, 0, 0.5))
    
    left_arm = ((1.7, -1, -0.5),
            (1.7, 0, -0.5),
            (1.5, 0, -0.5),
            (1.5, -1, -0.5),
            (1.7, -1, 0.5),
            (1.7, 0, 0.5),
            (1.5, -1, 0.5),
            (1.5, 0, 0.5))
    
    seat = ((1.5, -1, -0.3),
            (1.5, -0.5, -0.3),
            (-1.5, -0.5, -0.3),
            (-1.5, -1, -0.3),
            (1.5, -1, 0.5),
            (1.5, -0.5, 0.5),
            (-1.5, -1, 0.5),
            (-1.5, -0.5, 0.5))
    
    edges = (
        (0,1),
        (0,3),
        (0,4),
        (2,1),
        (2,3),
        (2,7),
        (6,3),
        (6,4),
        (6,7),
        (5,1),
        (5,4),
        (5,7))
    
    surfaces = (
        (0,1,2,3),
        (3,2,7,6),
        (6,7,5,4),
        (4,5,1,0),
        (1,5,7,2),
        (4,0,3,6)
    )

    parts = {
        body : (0.3, 0.3, 0.3),
        right_arm : (0.6, 0.6, 0.6),
        left_arm : (0.6, 0.6, 0.6),
    }
    
    def draw(self):
        for part, color in self.parts.items():
            glBegin(GL_QUADS)
            for surface in self.surfaces:
                for vertex in surface:
                    glColor3f(color[0], color[1], color[2])
                    glVertex3fv(part[vertex])
            glEnd()

            glBegin(GL_LINES)
            for edge in self.edges:
                for index in edge:
                    glColor3f(color[0], color[1], color[2])
                    glVertex3fv(part[index])
            glEnd()

        glBegin(GL_QUADS)
        for surface in self.surfaces:
            for vertex in surface:
                glColor3f(0.3, 0.3, 0.3)
                glVertex3fv(self.seat[vertex])
        glEnd()

        glBegin(GL_LINES)
        for edge in self.edges:
            for index in edge:
                glColor3f(1, 1, 1)
                glVertex3fv(self.seat[index])
        glEnd()
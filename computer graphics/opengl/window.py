import pygame
from pygame.locals import *
from OpenGL.GL import *
from OpenGL.GLU import *
from object.car import Car
from object.chair import Chair
from object.chicken import Chicken
from object.duck import Duck
from object.gun import Gun

class Window:

    index = 0
    running = True
    objects = [Chicken(), Duck(), Gun(), Chair(), Car()]

    def __init__(self):
        pygame.init()
        display = (800, 600)
        pygame.display.set_mode(display, DOUBLEBUF | OPENGL)
        pygame.display.set_caption("OpenGL")
        gluPerspective(60, (display[0] / display[1]), 0.1, 50.0)
        glTranslatef(0.0, 0.0, -10)
        glEnable(GL_DEPTH_TEST)
        glDepthFunc(GL_LESS)

        while self.running:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    pygame.quit()
                    quit()
                if event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_LEFT:
                        if self.index == 0:
                            self.index = 4
                        else:
                            self.index = self.index - 1
                    elif event.key == pygame.K_RIGHT:
                        if self.index == 4:
                            self.index = 0
                        else:
                            self.index = self.index + 1
                            
            glRotatef(0.5, 0, 1, 0)
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT)
            self.objects[self.index].draw()
            pygame.display.flip()

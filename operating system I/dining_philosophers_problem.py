#!/usr/bin/env python3

from __future__ import print_function
from threading import Thread, Lock
from time import sleep
import random

__description__ = "Implementaion of dining philosophers problem"


def eat(index, left_fork, right_fork):
    '''Pick up left & right fork and eat'''
    left_fork.acquire()
    right_fork.acquire()
    eating_philosophers[index % 5], eating_philosophers[(index + 1) % 5] = index + 1, index + 1
    print(f"Philosopher {index + 1} started eating")
    print(eating_philosophers)
    sleep(random.randint(1, 10))

def release_fork(index, left_fork, right_fork):
    '''Drop left & right fork'''
    left_fork.release()
    right_fork.release()
    eating_philosophers[index % 5], eating_philosophers[(index + 1) % 5] = 0, 0
    print(f"Philosopher {index + 1} stopped eating")

def think(index):
    '''Start thinking'''
    print(f"Philosopher {index + 1} started thinking")
    sleep(random.randint(1, 10))
    print(f"Philosopher {index + 1} is hungry")

def main(index, left_fork, right_fork):
    while True:
        # Eat if left & right fork are not claimed
        if not left_fork.locked() and not right_fork.locked():
            eat(index, left_fork, right_fork)
            release_fork(index, left_fork, right_fork)
        think(index)            


if __name__ == "__main__":
    fork = [Lock() for x in range(5)]
    eating_philosophers = [0] * 5
    for i in range(5):
        Thread(target=main, args=(i, fork[i % 5], fork[(i + 1) % 5])).start()

#!/usr/bin/env python3

from __future__ import print_function
import copy

__description__ = "Implementation of multiple level queue scheduling algorithm"

jobs = [
    {"id" : 1, "cpu cycle" : 8, "queue" : 1},
    {"id" : 2, "cpu cycle" : 4, "queue" : 2},
    {"id" : 3, "cpu cycle" : 1, "queue" : 2},
    {"id" : 4, "cpu cycle" : 5, "queue" : 1},
    {"id" : 5, "cpu cycle" : 2, "queue" : 2},
    {"id" : 6, "cpu cycle" : 3, "queue" : 1}
]

# NB: Queue 1 has higher priority than Queue 2
queue1, queue2 = [], []


def no_movement_btw_queues(queue1, queue2):
    '''Multiple level scheduling with no movement between queues'''
    priority, execution_sequence = [queue1, queue2], []
    execution_time = [0]
    for queue in priority:
        for job in queue:
            execution_sequence.append(job["id"])
            execution_time.append(execution_time[-1] + job["cpu cycle"])
    print("No movement between queues: ", end="")
    print(str([f"P{x}" for x in execution_sequence])[1:-1].replace(",", " ->"))
    print(f"Execution time: {str(execution_time)[1:-1]}\n")
    

def movement_btw_queues(queue1, queue2):
    '''Multiple level scheduling with movement between queues'''
    time_slice = 3
    priority, execution_sequence = [queue1, queue2], []
    execution_time = [0]
    for i, queue in enumerate(priority):
        for job in queue:
            execution_sequence.append(job["id"])
            # Keep track of execution timeline
            if job["cpu cycle"] - time_slice > 0:
                execution_time.append(execution_time[-1] + time_slice)
            else:
                execution_time.append(execution_time[-1] + job["cpu cycle"])
            # Check if job has finished executing
            job["cpu cycle"] -= time_slice
            if job["cpu cycle"] > 0:
                # Add job to next queue
                if i == len(priority) - 1:
                    priority[-1].append(job)
                else:
                    priority[i + 1].append(job)
            else:   
                del job
    print("Movement between queues: ", end="")
    print(str([f"P{x}" for x in execution_sequence])[1:-1].replace(",", " ->"))
    print(f"Execution time: {str(execution_time)[1:-1]}\n")


def variable_time_quantum_per_queue(queue1, queue2):
    '''Multiple level scheduling with variable time quantum per queue'''
    time_slice = 3
    priority, execution_sequence = [queue1, queue2], []
    execution_time = [0]
    for i, queue in enumerate(priority):
        for job in queue:
            execution_sequence.append(job["id"])
            # Keep track of execution timeline
            if job["cpu cycle"] - time_slice > 0:
                execution_time.append(execution_time[-1] + time_slice)
            else:
                execution_time.append(execution_time[-1] + job["cpu cycle"])
            # Check if job has finished executing
            job["cpu cycle"] -= time_slice
            if job["cpu cycle"] > 0:
                # Add job to next queue
                if i == len(priority) - 1:
                    priority[-1].append(job)
                else:
                    priority[i + 1].append(job)
            else:
                del job
        # Double time slice
        time_slice += time_slice
    print("Variable time quantum per queue: ", end="")
    print(str([f"P{x}" for x in execution_sequence])[1:-1].replace(",", " ->"))
    print(f"Execution time: {str(execution_time)[1:-1]}")



if __name__ == "__main__":
    # Place jobs in their respective queues
    for job in jobs:
        if job["queue"] == 1:
            queue1.append(job)
        else:
            queue2.append(job)
    
    # Display process, cpu cycle and queue
    print("-"*24)
    print("| {:<5} {:<10} {:<5}".format("PID", "CPU Cycle", "Queue"))
    print("-"*24)
    for job in jobs:
        print(
            "| {:<5} {:<10} {:<5}"
            .format(str(job["id"]), job["cpu cycle"], str(job["queue"]))
        )
    print("-"*24)
    
    no_movement_btw_queues(copy.deepcopy(queue1), copy.deepcopy(queue2))
    movement_btw_queues(copy.deepcopy(queue1), copy.deepcopy(queue2))
    variable_time_quantum_per_queue(copy.deepcopy(queue1), copy.deepcopy(queue2))

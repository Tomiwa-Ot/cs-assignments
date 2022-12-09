#!/usr/bin/env python3

from __future__ import print_function

__description__ = "Implementaion of bankers algorithm"
__author__ = "Group 5"


def bankers_algorithm():
    '''
    attr:
        deadlocked: (bool)
            Checks if deadlock has been ecountered
        safe_sequence: (list)
            Order to execute processes without encountering deadlock
        unexecuted_process: (list)
            List of unexecuted processes
    '''
    deadlocked, safe_sequence = False, []
    number_of_processes, number_of_resources  = 4, 3
    unexecuted_process = [x for x in range(number_of_processes)]
    available_resources = [4, 4, 2]
    max_resources = [
        [7, 4, 5],  # P0
        [5, 6, 4],  # P1
        [2, 2, 3],  # P2
        [6, 2, 0]   # P3
    ]
    allocated_resources = [
        [3, 1, 2],  # P0
        [0, 1, 3],  # P1
        [1, 0, 3],  # P2
        [1, 1, 0]   # P3
    ]
    resources_needed = []

    # Populate resources_needed list
    for i in range(number_of_processes):
        resources_needed_for_process_i = []
        for j in range(number_of_resources):
            resources_needed_for_process_i.append(max_resources[i][j] - allocated_resources[i][j])
        resources_needed.append(resources_needed_for_process_i)

    print(f"\nAvailable Resources: {available_resources}")
    # Search for executing sequence void of deadlock
    while not deadlocked:
        initial_number_of_unexecuted_processes = len(unexecuted_process)
        for i in range(number_of_processes):
            # Check if process has been executed
            if i in safe_sequence:
                continue
            # Check if available resources are enough for process
            is_available_resource_enough = [False] * number_of_resources
            for j in range(number_of_resources):
                is_available_resource_enough[j] = available_resources[j] >= resources_needed[i][j]
            if all(is_available_resource_enough):
                safe_sequence.append(i)
                unexecuted_process.remove(i)
                for j in range(number_of_resources):
                    available_resources[j] += allocated_resources[i][j]
        # Stop algorithm if all processes have been executed
        if len(unexecuted_process) == 0:
            break
        deadlocked = initial_number_of_unexecuted_processes == len(unexecuted_process)

    print("-"*39)
    print("| {:<4} {:<10} {:<10} {:<10}".format("ID", "Max", "Allocated", "Needed"))
    print("-"*39)
    for i in range(4):
        print(
            "| {:<4} {:<10} {:<10} {:<10}"
            .format(f"P{i}", str(max_resources[i]), str(allocated_resources[i]), str(resources_needed[i]))
        )
    print("-"*39)
    
    if not deadlocked:
        print("\nSafe Sequence: ", end="")
        print(str([f"P{x}" for x in safe_sequence])[1:-1].replace(",", " ->"))
    elif deadlocked and len(safe_sequence) != 0:
        print("{} -> Deadlock".format(str([f"P{x}" for x in safe_sequence])[1:-1].replace(",", " ->")))
    else:
        print("Deadlock!!!")


if __name__ == "__main__":
    bankers_algorithm()

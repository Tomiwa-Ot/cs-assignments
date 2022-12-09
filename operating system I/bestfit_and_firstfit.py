#!/usr/bin/env python3

from __future__ import print_function
import sys

'''
Course: CSC 314
Description: Implementaion of best fit & first fit memory allocation algorithm
Group: 5
'''

def set_parameter_values():
    '''
    Sets parameters needed by the algorithm
        Parameters:
            MEMORY_BLOCK_SIZE     (int): The size of the memory block
            NUMBER_OF_PARTITIONS  (int): The number of partitions in memory
            PARTITION_SIZES      (list): The sizes of the partitions in memory
            PROCESS_SIZES        (list): The sizes of the jobs to be allocated memory
    '''
    global MEMORY_BLOCK_SIZE, NUMBER_OF_PARTITIONS, PARTITION_SIZES, PROCESS_SIZES
    try:
        MEMORY_BLOCK_SIZE = int(input("[+] Enter total size of memory block (KB): "))
        NUMBER_OF_PARTITIONS = int(input("[+] Enter the number of partitions: "))
        PARTITION_SIZES = [int(x) for x in input("[+] Enter the partition sizes seperated by commas (KB): ").split(",")]
        if len(PARTITION_SIZES) !=  NUMBER_OF_PARTITIONS:
            print(f"[+] Error: expected {NUMBER_OF_PARTITIONS} partition sizes")
            sys.exit(1)
        elif sum(PARTITION_SIZES) != MEMORY_BLOCK_SIZE:
            print(f"[+] Error: sum of partition sizes ({sum(PARTITION_SIZES)}KB) is not equal to memory size({MEMORY_BLOCK_SIZE}KB)")
            sys.exit(1)
        PROCESS_SIZES = [int(x) for x in input("[+] Enter the process sizes seperated by commas (KB): ").split(",")]
        if len(PROCESS_SIZES) !=  NUMBER_OF_PARTITIONS:
            print(f"[+] Error: expected {NUMBER_OF_PARTITIONS} process sizes")
            sys.exit(1)
    except KeyboardInterrupt:
        print("\n[+] Program was abruptly terminated")
        sys.exit(1)
    except ValueError:
        print("[+] Error: expected an integer")
        sys.exit(1)


def display_parameter_values():
    '''Displays the inputed parameters'''
    print(f"\n[+] Memory Size: {MEMORY_BLOCK_SIZE}KB")
    print(f"[+] Partition Sizes (KB): {str(PARTITION_SIZES)[1:-1]}\n")
    print(f"Process List:\n{'-'*30}")
    print("| {:<11} {:<16}".format("Process No.", "Process Size(KB)"))
    print(f"|{'-'*28}")
    for i, process_size in enumerate(PROCESS_SIZES):
        print("| {:<11} {:<16}".format(f"P{i + 1}", str(process_size)))
    print(f"{'-'*30}\n")


def display_output(title, output, total_used_memory, total_fragment_size, unallocated_processes):
    '''
    Tabulates the output
        args:
            title                  (str): The name of the algorithm
            output                (dict): Key    (int) = Partition Number
                                          Value (list) = [Block Size, Process Number, Process Size, Process Status, Fragment Size]
            total_used_memory      (int): Sum of utilized memory
            total_fragment_size    (int): Sum of fragmented memory
            unallocated_processes (list): List of jobs without memory allocation
    '''
    print(f"{title}:\n{'-'*90}")
    print(
        "| {:<14} {:<15} {:<12} {:<17} {:<7} {:<18}"
        .format("Partition No.", "Block Size(KB)", "Process No.", "Process Size(KB)", "Status", "Fragment Size(KB)")
    )
    print(f"|{'-'*89}")
    for key in sorted(output):
        print(
            "| {:<14} {:<15} {:<12} {:<17} {:<7} {:<18}"
            .format(key, str(output[key][0]), output[key][1], str(output[key][2]), output[key][3], str(output[key][4]))
        )
    print(f"{'-'*90}")
    print(f"[+] Total memory used: {total_used_memory}KB")
    print(f"[+] Total fragment size: {total_fragment_size}KB")
    print(f"[+] Processes without allocated memory: {str(unallocated_processes)[1:-1]}\n")


def best_fit():
    '''
    Best fit algorithm implementation
        parameters:
            output                     (dict): Key    (int) = Partition Number
                                               Value (list) = [Block Size, Process Number, Process Size, Process Status, Fragment Size]
            is_partition_taken         (list): Tracks if a partition has been assigned a job
            unallocated_processes      (list): List of jobs without memory allocation
            total_used_memory           (int): Sum of utilized memory
            total_fragment_size         (int): Sum of fragmented memory
            potential_occupiable_space (dict): Memory locations greater than or equal to the job size
                                               Key    (int) = Partition Number
                                               Value  (int) = Partition Size
    '''
    output, is_partition_taken, unallocated_processes = {}, [False] * NUMBER_OF_PARTITIONS, []
    total_used_memory, total_fragment_size = 0, 0
    for i, process_size in enumerate(PROCESS_SIZES):
        potential_occupiable_space = {}
        for j, partition_size in enumerate(PARTITION_SIZES):
            if is_partition_taken[j] == True:
                continue
            elif process_size <= partition_size:
                potential_occupiable_space[j + 1] = partition_size
            else:
                output[j + 1] = [partition_size, "-", "-", "Free", "-"]
        if len(potential_occupiable_space) != 0:
            smallest_partition = min(potential_occupiable_space.values())
            smallest_partition_index = min(potential_occupiable_space, key=potential_occupiable_space.get)
            output[smallest_partition_index] = [smallest_partition, f"P{i + 1}", process_size, "Busy", (smallest_partition - process_size)]
            is_partition_taken[smallest_partition_index - 1] = True
            total_used_memory = total_used_memory + process_size
            total_fragment_size = total_fragment_size + (smallest_partition - process_size)
        else:
            unallocated_processes.append(f"P{i + 1}")
    display_output("Best Fit Method", output, total_used_memory, total_fragment_size, unallocated_processes)


def first_fit():
    '''
    First fit algorithm implementation
        parameters:
            output                (dict): Key    (int) = Partition Number
                                          Value (list) = [Block Size, Process Number, Process Size, Process Status, Fragment Size]
            is_partition_taken    (list): Tracks if a partition has been assigned a job
            is_process_x_taken    (dict): Tracks if a job has been assigned a memory location
            unallocated_processes (list): List of jobs without memory allocation
            total_used_memory      (int): Sum of utilized memory
            total_fragment_size    (int): Sum of fragmented memory
    '''
    output, is_partition_taken, unallocated_processes = {}, [False] * NUMBER_OF_PARTITIONS, []
    is_process_x_taken = {}
    total_used_memory, total_fragment_size = 0, 0
    for i, process_size in enumerate(PROCESS_SIZES):
        for j, partition_size in enumerate(PARTITION_SIZES):
            if is_partition_taken[j] == True:
                continue
            elif process_size <= partition_size:
                output[j + 1] = [partition_size, f"P{i + 1}", process_size, "Busy", (partition_size - process_size)]
                total_used_memory = total_used_memory + process_size
                total_fragment_size = total_fragment_size + (partition_size - process_size)
                is_partition_taken[j], is_process_x_taken[i + 1] = True, True
                break
            else:
                output[j + 1] = [partition_size, "-", "-", "Free", "-"]
                is_process_x_taken[i + 1] = False
    for key in is_process_x_taken:
        if is_process_x_taken[key] == False:
            unallocated_processes.append(f"P{key}")
    display_output("First Fit Method", output, total_used_memory, total_fragment_size, unallocated_processes)


def main():
    set_parameter_values()
    display_parameter_values()
    first_fit()
    best_fit()


if __name__ == "__main__":
    main()
    

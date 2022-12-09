#!/usr/bin/env python3

from __future__ import print_function


memory = [
    {"addr" : 5, "size" : 105, "free" : True},
    {"addr" : 10, "size" : 20, "free" : True},
    {"addr" : 15, "size" : 600, "free" : False},
    {"addr" : 20, "size" : 50, "free" : True},
    {"addr" : 25, "size" : 225, "free" : True}
]

def deallocation():
    '''
    Implementaion of deallocation scheme when the
    block to be deallocated is b/w two free blocks.
    '''
    display_output("Before: ", memory)

    for i, block in enumerate(memory):
        # Check if the block is busy and not the last block
        if not block["free"] and (i != len(memory) - 1):
            # Check if adjacent blocks are free
            if memory[i - 1]["free"] and memory[i + 1]["free"]:
                # Add block and next block size to previous block
                memory[i - 1]["size"] += memory[i]["size"] + memory[i + 1]["size"]
                # Set next block to null
                memory[i + 1]["addr"] = "null"
                memory[i + 1]["size"], memory[i + 1]["free"] = "null", "null"
                # Delete block
                del memory[i]

    display_output("After: ", memory)

def display_output(period, memory):
    '''Displays memory info'''
    print(f"{period}\n{'-'*25}")
    print("| {:<9} {:<6} {:<6}".format("Address", "Size", "Status"))
    print("-"*25)
    for block in memory:
        print("| {:<9} {:<6} {:<6}".format(block["addr"], block["size"], block["free"]))
    print("-"*25)

if __name__ == "__main__":
    deallocation()

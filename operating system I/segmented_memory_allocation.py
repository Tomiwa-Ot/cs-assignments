#!/usr/bin/env python3

from __future__ import print_function
import random


__description__ = "Implementation of segmented memory allocation scheme"

memory_size = 5000
memory = [
    {"address" : 0, "size" : 900},
    {"address" : 900, "size" : 600},
    {"address" : 1500, "size" : 1500},
    {"address" : 3000, "size" : 1100},
    {"address" : 4100, "size" : 200},
    {"address" : 4300, "size" : 700},
]
is_memory_claimed = [False] * 6
segments = [
    {"id" : 1, "size" : 500, "address" : None},
    {"id" : 2, "size" : 1000, "address" : None},
    {"id" : 3, "size" : 750, "address" : None}
]


def segmented_memory_allocation():
    for segment in segments:
        # Get a list of free memory locations
        available_locations = []
        for i, claimed in enumerate(is_memory_claimed):
            if not claimed: available_locations.append(i)
        # Get a list of memory locations large enough to contain segment
        big_enough_locations = []
        for location in available_locations:
            if segment["size"] <= memory[location]["size"]:
                big_enough_locations.append(location)
        # Claim a location
        selected_partition = random.choice(big_enough_locations)
        is_memory_claimed[selected_partition] = True
        segment["address"] = memory[selected_partition]["address"]
    print("\nSegment Map Table: ")
    print("-"*25)
    print("| {:<8} {:<5} {:<7}".format("Segment", "Size", "Address"))
    print("-"*25)
    for segment in segments:
        print(
            "| {:<8} {:<5} {:<7}"
            .format(segment["id"], segment["size"], segment["address"])
        )
    print("-"*25)


if __name__ == "__main__":
    # Print memory addresses and their sizes
    print("Memory: ")
    print("-"*17)
    print("| {:<9} {:<5}".format("Address", "Size"))
    print("-"*17)
    for block in memory:
        print("| {:<9} {:<5}".format(block["address"], block["size"]))
    print("-"*17)

    segmented_memory_allocation()

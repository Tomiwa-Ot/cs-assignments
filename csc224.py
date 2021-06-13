'''
OLORUNFEMI-OJO DANIEL TOMIWA
190805503
COMPUTER SCIENCE
'''
import sys

def insert(array, new_value, new_value_index):
    modified_array = array[:new_value_index]
    modified_array.append(new_value)
    for i in range(new_value_index, len(array)):
        modified_array.append(array[i])
    array = modified_array
    print(array)

def delete(array, index_to_delete):
    modified_array = array[:index_to_delete]
    for i in range(index_to_delete + 1, len(array)):
        modified_array.append(array[i])
    array = modified_array
    print(array)

def main():
    insert([1, 2, 4, 5, 3, 6], 9, 3)
    delete([1, 2, 4, 5, 3, 6], 2)

if __name__ == "__main__":
    sys.exit(main())
import time
import sys

def func(x):
    return (x**3 - x  - 1)

def derivative_of_func(x):
    return ((3 * (x ** 2)) - 1)

def bisection_method(a, b):
    if a > b:
        print("a must be less than b")
        sys.exit()
    if func(a) * func(b) > 0:
        print("f(a).f(b) must be less than 0")
        sys.exit()
    err, iterations = 0.000001, 0
    temp_a, temp_b, c = a, b, 0
    f_a, f_b, f_c = 0, 0, 0
    print("a\t\tb\t\tc\t\tf(a)\t\tf(b)\t\tf(c)")
    start_time = time.time()
    while temp_b - temp_a > err:
        c = (temp_a + temp_b) / 2
        f_a, f_b, f_c = func(temp_a), func(temp_b), func(c)
        print("{}\t\t{}\t\t{}\t\t{}\t\t{}\t\t{}".format(temp_a, temp_b, c, f_a, f_b, f_c))
        if f_c < 0:
            temp_a = c
        else:
            temp_b = c
        iterations = iterations + 1
    end_time = time.time()
    print("="*70)
    print("Took {} seconds".format(end_time - start_time))
    print("Iterations: {}".format(iterations))
    print(c)

def secant(a, b):
    if func(a) * func(b) > 0:
        print("f(a).f(b) must be less than 0")
        sys.exit()
    if a > b:
        print("a must be less than b")
        sys.exit()
    err, iterations = 0.0001, 0
    t1, t2, tn = a, b, 0
    print("t1\t\tt2\t\ttn\t\tf(t1)\t\tf(t2)\t\tf(tn)")
    start_time = time.time()
    while abs(t2 - t1) > err:
        tn = ((t1 * func(t2)) - (t2 * func(t1))) / (func(t2) - func(t1))
        print("{}\t\t{}\t\t{}\t\t{}\t\t{}\t\t{}".format(t1, t2, tn, func(t1), func(t2), func(tn)))
        #print(tn)
        t1, t2 = t2, tn
        iterations = iterations + 1
    end_time = time.time()
    print("="*70)
    print("Took {} seconds".format(end_time - start_time))
    print("Iterations: {}".format(iterations - 1))
    print(t1)


def newton_raphson(a):
    if func(a) * derivative_of_func(a) >= 0:
        print("f(a).f'(a) must be less than 0")
        sys.exit()
    err, iterations = 0.0001, 0
    x1, x2 = a, 0
    print("x\t\t\tr")
    start_time = time.time()
    while True:
        x2 = x1 - (func(x1) / derivative_of_func(x1))
        if abs(x2 - x1) < err:
            break
        print("{}\t\t\t{}".format(x1, x2))
        x1 = x2
        iterations = iterations + 1
    end_time = time.time()
    print("="*70)
    print("Took {} seconds".format(end_time - start_time))
    print("Iterations: {}".format(iterations))
    print(x1)
    



def main():
    #bisection_method(2, 3)
    newton_raphson(1)
    secant(1, 2)

if __name__ == "__main__":
    main()
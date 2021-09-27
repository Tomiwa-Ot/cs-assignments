'''
NAME: OLORUNFEMI-OJO TOMIWA
DEPARTMENT: COMPUTER SCIENCE
'''


from tkinter import *
from tkinter import messagebox
from tkinter.font import Font
from tkinter.simpledialog import askinteger
import time

# Window UI
window = Tk()
window.geometry('200x400')
window.resizable(False, False)
window.title("CSC 225")
window.config(background="#20232A")
label_font = Font(size=13)
btn_font = Font(size=10, weight="bold")
equation_label = Label(window, text="f(x) = x^3 - x - 1", font=label_font)
derivative_label = Label(window, text="f'(x) = 3x^2 - 1", font=label_font)
equation_label.place(x=40, y=50)
derivative_label.place(x=40, y=70)


# Function
def func(x):
    return (x**3 - x  - 1)

# Derivative of Function
def derivative_of_func(x):
    return ((3 * (x ** 2)) - 1)

# Bisection Method
def bisection():
    a = askinteger(title="Bisection Method", prompt="Enter a value for a")
    b = askinteger(title="Bisection Method", prompt="Enter a value for b")
    if a == None or b == None:
        messagebox.showerror(title="Bisection Method", message="a and b should not be empty")
        return [
            "Bisection",
            None,
            None,
            None
        ]
    if a > b:
        messagebox.showerror(title="Bisection Method", message="b must be greater than a")
        return [
            "Bisection",
            None,
            None,
            None
        ]
    elif func(a) * func(b) > 0:
        messagebox.showerror(title="Bisection Method", message="f(a).f(b) must be less than 0")
        return [
            "Bisection",
            None,
            None,
            None
        ]
    else:
        err, iterations = 0.0000001, 0
        temp_a, temp_b, c = a, b, 0
        f_a, f_b, f_c = 0, 0, 0
        start_time = time.time()
        while temp_b - temp_a > err:
            c = (temp_a + temp_b) / 2
            f_a, f_b, f_c = func(temp_a), func(temp_b), func(c)
            if f_c < 0:
                temp_a = c
            else:
                temp_b = c
            iterations = iterations + 1
            print(c)
        end_time = time.time()
        return [
            "Bisection",
            iterations,
            end_time - start_time,
            c
        ]

# Newton Raphson Method
def newton_raphson():
    a = askinteger(title="Newton Raphson Method", prompt="Enter a value for a")
    if a == None:
        messagebox.showerror(title="Newton Raphson Method", message="a and b should not be empty")
        return [
            "Bisection",
            None,
            None,
            None
        ]
    if func(a) * derivative_of_func(a) >= 0:
        messagebox.showerror(title="Newton Raphson Method", message="f'(a).f(a) must be less than 0")
        return [
            "Newton Raphson",
            None,
            None,
            None
        ]
    else:
        err, iterations = 0.0000001, 0
        x1, x2 = a, 0
        start_time = time.time()
        while True:
            x2 = x1 - (func(x1) / derivative_of_func(x1))
            if abs(x2 - x1) < err:
                break
            x1 = x2
            iterations = iterations + 1
            print(x1)
        end_time = time.time()
        return [
            "Newton Raphson",
            iterations,
            end_time - start_time,
            x1
        ]

# Secant Method
def secant():
    a = askinteger(title="Secant Method", prompt="Enter a value for a")
    b = askinteger(title="Secant Method", prompt="Enter a value for b")
    if a == None or b == None:
        messagebox.showerror(title="Secant Method", message="a and b should not be empty")
        return [
            "Secant",
            None,
            None,
            None
        ]
    if func(a) * func(b) > 0:
        messagebox.showerror(title="Secant Method", message="f(a).f(b) must be less than 0")
        return [
            "Secant",
            None,
            None,
            None
        ]
    elif a > b:
        messagebox.showerror(title="Secant Method", message="b must be greater than a")
        return [
            "Secant",
            None,
            None,
            None
        ]
    else:
        err, iterations = 0.0000001, 0
        t1, t2, tn = a, b, 0
        start_time = time.time()
        while abs(t2 - t1) > err:
            tn = ((t1 * func(t2)) - (t2 * func(t1))) / (func(t2) - func(t1))        
            t1, t2 = t2, tn
            iterations = iterations + 1
            print(t1)
        end_time = time.time()
        return [
            "Secant",
            iterations - 1,
            end_time - start_time,
            t1
        ]

# Use all methods
def all(args1, args2, args3):
    messagebox.showinfo(
        title="All Methods",
        message="""
            {}\nIteration: {}\nTime Elapsed: {}\nResult: {}\n---------------------------------\n
            {}\nIteration: {}\nTime Elapsed: {}\nResult: {}\n---------------------------------\n
            {}\nIteration: {}\nTime Elapsed: {}\nResult: {}\n---------------------------------\n
        """.format(
            args1[0], args1[1], args1[2], args1[3],
            args2[0], args2[1], args2[2], args2[3],
            args3[0], args3[1], args3[2], args3[3],
        )
    )

# Display results
def result(args):
    if args[1] is None:
        return
    else:
        messagebox.showinfo(
            title=args[0],
            message="Iteration: {}\nTime Elapsed: {}\nResult: {}".format(
                args[1], args[2], args[3]
            )
        )

# Bisection Method Button
bisection_btn = Button(
    window, relief=FLAT, font=btn_font, text="Bisection", 
    foreground="#FFFFFF", activeforeground="#FFFFFF", 
    activebackground="#3CB371", background="#3CB371",
    command=lambda: result(
        bisection()
    )
)
bisection_btn.place(width=110, x=40, y=200)

# Newton Raphson Method Button
newton_raphson_btn = Button(
    window, relief=FLAT, font=btn_font, text="Newton\nRaphson",
    foreground="#FFFFFF", activeforeground="#FFFFFF", 
    activebackground="#3CB371", background="#3CB371",
    command=lambda: result(
        newton_raphson()
    )
)
newton_raphson_btn.place(width=110, x=40, y=235)

# Secant Method Button
secant_btn = Button(
    window, relief=FLAT, font=btn_font, text="Secant",
    activeforeground="#FFFFFF", foreground="#FFFFFF", 
    activebackground="#3CB371", background="#3CB371",
    command=lambda: result(
        secant()
    )
)
secant_btn.place(width=110, x=40, y=285)

# Use All Methods Button
calculate_all_btn = Button(
    window, relief=FLAT, font=btn_font, text="All",
    activeforeground="#FFFFFF", foreground="#FFFFFF", 
    activebackground="#3CB371", background="#3CB371",
    command=lambda : all(
        bisection(),
        newton_raphson(),
        secant()
    )
)
calculate_all_btn.place(width=110, x=40, y=370)



if __name__ == "__main__":
    window.mainloop()
